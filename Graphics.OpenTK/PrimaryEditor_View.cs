﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTKMapMaker.GraphicsSystem;
using OpenTKMapMaker.GraphicsSystem.LightingSystem;
using OpenTKMapMaker.Utility;
using OpenTKMapMaker.EntitySystem;
using System.IO;

namespace OpenTKMapMaker
{
    public partial class PrimaryEditor : Form
    {
        public static List<LightObject> Lights = new List<LightObject>();

        void glControlView_MouseWheel(object sender, MouseEventArgs e)
        {
            CameraPos += Utilities.ForwardVector_Deg(CameraYaw, CameraPitch) * e.Delta / 120f; // 120 = WHEEL_DELTA - By default, at least.
            glControlView.Invalidate();
        }

        public static GLContext ContextView;

        bool view_loaded = false;

        private void glControlView_Load(object sender, EventArgs e)
        {
            glControlView.MakeCurrent();
            ResizeView();
            ContextView = new GLContext();
            ContextView.Control = glControlView;
            InitGL(ContextView);
            ContextView.Textures.OnTextureLoaded += new EventHandler<TextureLoadedEventArgs>(ViewTextures_OnTextureLoaded);
            LoadEntities();
            s_shadow = ContextView.Shaders.GetShader("shadow");
            s_main = ContextView.Shaders.GetShader("test");
            s_fbo = ContextView.Shaders.GetShader("fbo");
            s_shadowadder = ContextView.Shaders.GetShader("shadowadder");
            s_transponly = ContextView.Shaders.GetShader("transponly");
            view_generateLightHelpers();
            view_loaded = true;
        }

        int fbo_texture;
        int fbo_main;
        int fbo2_texture;
        int fbo2_main;

        public void view_destroyLightHelpers()
        {
            RS4P.Destroy();
            GL.DeleteFramebuffer(fbo_main);
            GL.DeleteFramebuffer(fbo2_main);
            GL.DeleteTexture(fbo_texture);
            GL.DeleteTexture(fbo2_texture);
            RS4P = null;
            fbo_main = 0;
            fbo2_main = 0;
            fbo_texture = 0;
            fbo2_texture = 0;
        }

        public void view_generateLightHelpers()
        {
            RS4P = new RenderSurface4Part(glControlView.Width, glControlView.Height, ContextView.Rendering);
            // FBO
            fbo_texture = GL.GenTexture();
            fbo_main = GL.GenFramebuffer();
            GL.BindTexture(TextureTarget.Texture2D, fbo_texture);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, glControlView.Width, glControlView.Height, 0,
                OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, fbo_main);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, fbo_texture, 0);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            fbo2_texture = GL.GenTexture();
            fbo2_main = GL.GenFramebuffer();
            GL.BindTexture(TextureTarget.Texture2D, fbo2_texture);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, glControlView.Width, glControlView.Height, 0,
                OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, fbo2_main);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, fbo2_texture, 0);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        Shader s_shadow;
        Shader s_main;
        Shader s_fbo;
        Shader s_shadowadder;
        Shader s_transponly;
        RenderSurface4Part RS4P;

        Matrix4 proj;
        Matrix4 view;
        Matrix4 combined;

        void SetViewport()
        {
            GL.Viewport(0, 0, CurrentContext.Control.Width, CurrentContext.Control.Height);
            vpw = CurrentContext.Control.Width;
            vph = CurrentContext.Control.Height;
        }

        public void sortEntities()
        {
            Entities = Entities.OrderBy(o => -(o.Position - CameraPos).LengthSquared()).ToList();
        }

        private void glControlView_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                sortEntities();
                CurrentContext = ContextView;
                glControlView.MakeCurrent();
                GL.ClearBuffer(ClearBuffer.Color, 0, new float[] { 0.1f, 0.1f, 0.1f, 1f });
                GL.ClearBuffer(ClearBuffer.Depth, 0, new float[] { 1.0f });
                GL.Enable(EnableCap.DepthTest);
                if (renderLightingToolStripMenuItem.Checked)
                {
                    s_shadow.Bind();
                    for (int i = 0; i < Lights.Count; i++)
                    {
                        for (int x = 0; x < Lights[i].InternalLights.Count; x++)
                        {
                            Lights[i].InternalLights[x].Attach();
                            Render3D(CurrentContext, false, false, true, false);
                            Lights[i].InternalLights[x].Complete();
                        }
                    }
                    SetViewport();
                    s_fbo.Bind();
                    Location CameraTarget = CameraPos + Utilities.ForwardVector_Deg(CameraYaw, CameraPitch);
                    proj = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(CameraFOV), (float)CurrentContext.Control.Width / (float)CurrentContext.Control.Height, CameraZNear, CameraZFar);
                    view = Matrix4.LookAt(CameraPos.ToOVector(), CameraTarget.ToOVector(), CameraUp.ToOVector());
                    combined = view * proj;
                    GL.UniformMatrix4(1, false, ref combined);
                    GL.ActiveTexture(TextureUnit.Texture0);
                    RS4P.Bind();
                    Render3D(CurrentContext, true, false, true, true);
                    RS4P.Unbind();
                    GL.BindFramebuffer(FramebufferTarget.Framebuffer, fbo_main);
                    GL.ClearBuffer(ClearBuffer.Color, 0, new float[] { 0.0f, 0.0f, 0.0f, 1.0f });
                    GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
                    GL.BindFramebuffer(FramebufferTarget.Framebuffer, fbo2_main);
                    GL.ClearBuffer(ClearBuffer.Color, 0, new float[] { 0.0f, 0.0f, 0.0f, 1.0f });
                    GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
                    s_shadowadder.Bind();
                    GL.ActiveTexture(TextureUnit.Texture1);
                    GL.BindTexture(TextureTarget.Texture2D, RS4P.PositionTexture);
                    GL.ActiveTexture(TextureUnit.Texture2);
                    GL.BindTexture(TextureTarget.Texture2D, RS4P.NormalsTexture);
                    GL.ActiveTexture(TextureUnit.Texture3);
                    GL.BindTexture(TextureTarget.Texture2D, RS4P.DepthTexture);
                    GL.ActiveTexture(TextureUnit.Texture5);
                    GL.BindTexture(TextureTarget.Texture2D, RS4P.RenderhintTexture);
                    GL.ActiveTexture(TextureUnit.Texture6);
                    GL.BindTexture(TextureTarget.Texture2D, RS4P.DiffuseTexture);
                    Matrix4 mat = Matrix4.CreateOrthographicOffCenter(-1, 1, -1, 1, -1, 1);
                    GL.UniformMatrix4(1, false, ref mat);
                    mat = Matrix4.Identity;
                    GL.UniformMatrix4(2, false, ref mat);
                    GL.Uniform3(10, CameraPos.ToOVector());
                    bool first = true;
                    GL.BindFramebuffer(FramebufferTarget.Framebuffer, first ? fbo_main : fbo2_main);
                    GL.DrawBuffer(DrawBufferMode.ColorAttachment0);
                    GL.Disable(EnableCap.CullFace);
                    for (int i = 0; i < Lights.Count; i++)
                    {
                        for (int x = 0; x < Lights[i].InternalLights.Count; x++)
                        {
                            GL.BindFramebuffer(FramebufferTarget.Framebuffer, first ? fbo_main : fbo2_main);
                            GL.ActiveTexture(TextureUnit.Texture0);
                            GL.BindTexture(TextureTarget.Texture2D, first ? fbo2_texture : fbo_texture);
                            GL.ActiveTexture(TextureUnit.Texture4);
                            GL.BindTexture(TextureTarget.Texture2D, Lights[i].InternalLights[x].fbo_depthtex);
                            Matrix4 smat = Lights[i].InternalLights[x].GetMatrix();
                            GL.UniformMatrix4(3, false, ref smat);
                            GL.Uniform3(4, ref Lights[i].InternalLights[x].eye);
                            GL.Uniform3(8, ref Lights[i].InternalLights[x].color);
                            GL.Uniform1(9, Lights[i].InternalLights[x].maxrange);
                            CurrentContext.Rendering.RenderRectangle(-1, -1, 1, 1);
                            first = !first;
                            GL.ActiveTexture(TextureUnit.Texture0);
                            GL.BindTexture(TextureTarget.Texture2D, 0);
                        }
                    }
                    GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
                    GL.DrawBuffer(DrawBufferMode.Back);
                    s_main.Bind();
                    GL.Uniform3(5, ambient.ToOVector());
                    GL.ActiveTexture(TextureUnit.Texture4);
                    GL.BindTexture(TextureTarget.Texture2D, first ? fbo2_texture : fbo_texture);
                    GL.ActiveTexture(TextureUnit.Texture0);
                    GL.BindTexture(TextureTarget.Texture2D, RS4P.DiffuseTexture);
                    mat = Matrix4.CreateOrthographicOffCenter(-1, 1, -1, 1, -1, 1);
                    GL.UniformMatrix4(1, false, ref mat);
                    mat = Matrix4.Identity;
                    GL.UniformMatrix4(2, false, ref mat);
                    CurrentContext.Rendering.RenderRectangle(-1, -1, 1, 1);
                    GL.ActiveTexture(TextureUnit.Texture6);
                    GL.BindTexture(TextureTarget.Texture2D, 0);
                    GL.ActiveTexture(TextureUnit.Texture5);
                    GL.BindTexture(TextureTarget.Texture2D, 0);
                    GL.ActiveTexture(TextureUnit.Texture4);
                    GL.BindTexture(TextureTarget.Texture2D, 0);
                    GL.ActiveTexture(TextureUnit.Texture3);
                    GL.BindTexture(TextureTarget.Texture2D, 0);
                    GL.ActiveTexture(TextureUnit.Texture2);
                    GL.BindTexture(TextureTarget.Texture2D, 0);
                    GL.ActiveTexture(TextureUnit.Texture1);
                    GL.BindTexture(TextureTarget.Texture2D, 0);
                    GL.ActiveTexture(TextureUnit.Texture0);
                    GL.BindTexture(TextureTarget.Texture2D, 0);
                    s_transponly.Bind();
                    GL.UniformMatrix4(1, false, ref combined);
                    GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, RS4P.fbo);
                    GL.BlitFramebuffer(0, 0, glControlView.Width, glControlView.Height, 0, 0, glControlView.Width, glControlView.Height, ClearBufferMask.DepthBufferBit, BlitFramebufferFilter.Nearest);
                    GL.BindFramebuffer(FramebufferTarget.ReadFramebuffer, 0);
                    Render3D(CurrentContext, false, false, true, false);
                    CurrentContext.Shaders.ColorMultShader.Bind();
                    GL.UniformMatrix4(1, false, ref combined);
                    GL.Enable(EnableCap.CullFace);
                    if (wireframe)
                    {
                        renderWires(CurrentContext);
                    }
                    renderSelections(CurrentContext, true);
                }
                else
                {
                    CurrentContext.Shaders.ColorMultShader.Bind();
                    Location CameraTarget = CameraPos + Utilities.ForwardVector_Deg(CameraYaw, CameraPitch);
                    proj = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(CameraFOV), (float)CurrentContext.Control.Width / (float)CurrentContext.Control.Height, CameraZNear, CameraZFar);
                    view = Matrix4.LookAt(CameraPos.ToOVector(), CameraTarget.ToOVector(), CameraUp.ToOVector());
                    combined = view * proj;
                    GL.UniformMatrix4(1, false, ref combined);
                    Render3D(CurrentContext, true, false, true, false);
                    if (wireframe)
                    {
                        renderWires(CurrentContext);
                    }
                    renderSelections(CurrentContext, true);
                }
                GL.Disable(EnableCap.DepthTest);
                CurrentContext.Shaders.ColorMultShader.Bind();
                ortho = Matrix4.CreateOrthographicOffCenter(0, CurrentContext.Control.Width, CurrentContext.Control.Height, 0, -1, 1);
                CurrentContext.FontSets.SlightlyBigger.DrawColoredText("^S^" + (glControlView.Focused ? "@" : "!") + "^e^7" + CameraYaw + "/" + CameraPitch + " at " + CameraPos.ToString(), new Location(0, 0, 0));
                glControlView.SwapBuffers();
            }
            catch (Exception ex)
            {
                SysConsole.Output(OutputType.ERROR, "PaintView: " + ex.ToString());
            }
        }

        void ResizeView()
        {
            glControlView.Size = splitContainer3.Panel1.ClientSize;
        }

        private void glControlView_Resize(object sender, EventArgs e)
        {
            if (!view_loaded)
            {
                return;
            }
            glControlView.MakeCurrent();
            GL.Viewport(0, 0, glControlView.Width, glControlView.Height);
            view_destroyLightHelpers();
            view_generateLightHelpers();
        }

        bool view_selected = false;

        Location view_mousepos = new Location(0, 0, 0);

        private void glControlView_MouseMove(object sender, MouseEventArgs e)
        {
            if (NoClickyClicky())
            {
                return;
            }
            Location mpos = new Location((float)e.X / ((float)glControlView.Width / 2f) - 1f, -((float)e.Y / ((float)glControlView.Height / 2f) - 1f), 0f);
            Vector4 vec = Vector4.Transform(new Vector4(mpos.ToOVector(), 1.0f), combined.Inverted());
            view_mousepos = new Location(vec.X / vec.W, vec.Y / vec.W, vec.Z / vec.W);
            if (view_selected)
            {
                invalidateAll();
                float mx = (float)(e.X - glControlView.Width / 2) / 25f * mouse_sens / 5.0f;
                float my = (float)(e.Y - glControlView.Height / 2) / 25f * mouse_sens / 5.0f;
                CameraYaw -= mx;
                CameraPitch -= my;
                while (CameraYaw < 0)
                {
                    CameraYaw += 360;
                }
                while (CameraYaw > 360)
                {
                    CameraYaw -= 360;
                }
                if (CameraPitch < -89.9999f)
                {
                    CameraPitch = -89.9999f;
                }
                if (CameraPitch > 89.9999f)
                {
                    CameraPitch = 89.9999f;
                }
                if (Math.Abs(mx) > 0.1 || Math.Abs(my) > 0.1)
                {
                    Point p = glControlView.PointToScreen(new Point(glControlView.Width / 2, glControlView.Height / 2));
                    OpenTK.Input.Mouse.SetPosition(p.X, p.Y);
                }
            }
        }

        private void glControlView_MouseDown(object sender, MouseEventArgs e)
        {
            if (NoClickyClicky())
            {
                return;
            }
            if (e.Button == MouseButtons.Middle)
            {
                view_selected = !view_selected;
            }
            else if (e.Button == MouseButtons.Right)
            {
                Location normal;
                Entity hit = RayTraceEntity(ContextView, CameraPos, CameraPos + (view_mousepos - CameraPos) * 1000000, out normal);
                if (hit != null)
                {
                    if (hit.Selected)
                    {
                        Deselect(hit);
                    }
                    else
                    {
                        Select(hit);
                    }
                }
            }
        }

        private void glControlView_MouseEnter(object sender, EventArgs e)
        {
            if (!NoClickyClicky())
            {
                glControlView.Focus();
                invalidateAll();
            }
        }

        Timer tW = new Timer() { Interval = 50 };
        Timer tA = new Timer() { Interval = 50 };
        Timer tS = new Timer() { Interval = 50 };
        Timer tD = new Timer() { Interval = 50 };

        private void glControlView_KeyDown(object sender, KeyEventArgs e)
        {
            if (NoClickyClicky())
            {
                return;
            }
            if (e.KeyCode == Keys.W)
            {
                tW_Tick(null, null);
                tW.Start();
            }
            else if (e.KeyCode == Keys.A)
            {
                tA_Tick(null, null);
                tA.Start();
            }
            else if (e.KeyCode == Keys.S)
            {
                tS_Tick(null, null);
                tS.Start();
            }
            else if (e.KeyCode == Keys.D)
            {
                tD_Tick(null, null);
                tD.Start();
            }
            else if (e.KeyCode == Keys.Z)
            {
                view_selected = !view_selected;
            }
            else if (e.KeyCode == Keys.Up)
            {
                CameraPitch += 10f;
                if (CameraPitch > 89.9f)
                {
                    CameraPitch = 89.9f;
                }
                invalidateAll();
            }
            else if (e.KeyCode == Keys.Down)
            {
                CameraPitch -= 10f;
                if (CameraPitch < -89.9f)
                {
                    CameraPitch = -89.9f;
                }
                invalidateAll();
            }
            else if (e.KeyCode == Keys.Right)
            {
                CameraYaw -= 10f;
                if (CameraYaw < 0)
                {
                    CameraYaw += 360;
                }
                invalidateAll();
            }
            else if (e.KeyCode == Keys.Left)
            {
                CameraYaw += 10f;
                if (CameraYaw > 360)
                {
                    CameraYaw -= 360;
                }
                invalidateAll();
            }
            else if (e.KeyCode == Keys.P)
            {
                Location normal;
                Entity hit = RayTraceEntity(ContextView, CameraPos, CameraPos + (view_mousepos - CameraPos) * 1000000, out normal);
                if (hit != null && hit is CubeEntity)
                {
                    CubeEntity ce = (CubeEntity)hit;
                    string tex = ContextView.Textures.LoadedTextures[tex_sel].Name;
                    if (IsCloseTo(normal.Z, 1, 0.1f)) { ce.Textures[0] = tex; }
                    else if (IsCloseTo(normal.Z, -1, 0.1f)) { ce.Textures[1] = tex; }
                    else if (IsCloseTo(normal.X, 1, 0.1f)) { ce.Textures[2] = tex; }
                    else if (IsCloseTo(normal.X, -1, 0.1f)) { ce.Textures[3] = tex; }
                    else if (IsCloseTo(normal.Y, 1, 0.1f)) { ce.Textures[4] = tex; }
                    else if (IsCloseTo(normal.Y, -1, 0.1f)) { ce.Textures[5] = tex; }
                    ce.Recalculate();
                    glControlView.Invalidate();
                }
            }
            else if (e.KeyCode == Keys.F)
            {
                Location normal;
                Entity hit = RayTraceEntity(ContextView, CameraPos, CameraPos + (view_mousepos - CameraPos) * 1000000, out normal);
                if (hit != null && hit is CubeEntity)
                {
                    CubeEntity ce = (CubeEntity)hit;
                    fe = new FaceEditor(ce, normal);
                    fe.Show();
                    view_selected = false;
                }
            }
            PrimaryEditor_KeyDown(sender, e);
        }

        public bool IsCloseTo(double one, double two, double delta)
        {
            return one + delta > two && one - delta < two;
        }

        private void glControlView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                tW.Stop();
            }
            else if (e.KeyCode == Keys.A)
            {
                tA.Stop();
            }
            else if (e.KeyCode == Keys.S)
            {
                tS.Stop();
            }
            else if (e.KeyCode == Keys.D)
            {
                tD.Stop();
            }
            PrimaryEditor_KeyUp(sender, e);
        }

        void tD_Tick(object sender, EventArgs e)
        {
            if (NoClickyClicky())
            {
                return;
            }
            CameraPos += Utilities.ForwardVector_Deg(CameraYaw - 90, 0);
            invalidateAll();
        }

        void tS_Tick(object sender, EventArgs e)
        {
            if (NoClickyClicky())
            {
                return;
            }
            CameraPos -= Utilities.ForwardVector_Deg(CameraYaw, CameraPitch);
            invalidateAll();
        }

        void tA_Tick(object sender, EventArgs e)
        {
            if (NoClickyClicky())
            {
                return;
            }
            CameraPos += Utilities.ForwardVector_Deg(CameraYaw + 90, 0);
            invalidateAll();
        }

        void tW_Tick(object sender, EventArgs e)
        {
            if (NoClickyClicky())
            {
                return;
            }
            CameraPos += Utilities.ForwardVector_Deg(CameraYaw, CameraPitch);
            invalidateAll();
        }

        private void glControlView_MouseUp(object sender, MouseEventArgs e)
        {
            PrimaryEditor_MouseUp(sender, e);
        }
    }
}
