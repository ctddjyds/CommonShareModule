﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTKMapMaker.Utility;

namespace OpenTKMapMaker.GraphicsSystem
{
    public class VBO
    {
        uint _VertexVBO;
        uint _IndexVBO;
        uint _NormalVBO;
        uint _TexCoordVBO;
        uint _ColorVBO;
        uint _BoneIDVBO;
        uint _BoneWeightVBO;
        public uint _VAO;

        public Texture Tex;

        public List<Vector3> Vertices;
        public List<uint> Indices;
        public List<Vector3> Normals;
        public List<Vector3> TexCoords;
        public List<Vector4> Colors;
        public List<Vector4> BoneIDs;
        public List<Vector4> BoneWeights;

        public void AddSide(Location normal, TextureCoordinates tc)
        {
            // TODO: IMPROVE!
            for (int i = 0; i < 6; i++)
            {
                Normals.Add(normal.ToOVector());
                Colors.Add(new Vector4(1f, 1f, 1f, 1f));
                Indices.Add((uint)Indices.Count);
                BoneIDs.Add(new Vector4(0, 0, 0, 0));
                BoneWeights.Add(new Vector4(0f, 0f, 0f, 0f));
            }
            float aX = (tc.xflip ? 1 : 0) + tc.xshift;
            float aY = (tc.yflip ? 1 : 0) + tc.yshift;
            float bX = (tc.xflip ? 0 : 1) * tc.xscale + tc.xshift;
            float bY = (tc.yflip ? 1 : 0) + tc.yshift;
            float cX = (tc.xflip ? 0 : 1) * tc.xscale + tc.xshift;
            float cY = (tc.yflip ? 0 : 1) * tc.yscale + tc.yshift;
            float dX = (tc.xflip ? 1 : 0) + tc.xshift;
            float dY = (tc.yflip ? 0 : 1) * tc.yscale + tc.yshift;
            float zero = -1; // Sssh
            if (normal.Z == 1)
            {
                // T1
                TexCoords.Add(new Vector3(bX, bY, 0));
                Vertices.Add(new Vector3(zero, zero, 1));
                TexCoords.Add(new Vector3(cX, cY, 0));
                Vertices.Add(new Vector3(zero, 1, 1));
                TexCoords.Add(new Vector3(dX, dY, 0));
                Vertices.Add(new Vector3(1, 1, 1));
                // T2
                TexCoords.Add(new Vector3(bX, bY, 0));
                Vertices.Add(new Vector3(zero, zero, 1));
                TexCoords.Add(new Vector3(dX, dY, 0));
                Vertices.Add(new Vector3(1, 1, 1));
                TexCoords.Add(new Vector3(aX, aY, 0));
                Vertices.Add(new Vector3(1, zero, 1));
            }
            else if (normal.Z == -1)
            {
                // T1
                TexCoords.Add(new Vector3(cX, cY, 0));
                Vertices.Add(new Vector3(1, 1, zero));
                TexCoords.Add(new Vector3(dX, dY, 0));
                Vertices.Add(new Vector3(zero, 1, zero));
                TexCoords.Add(new Vector3(aX, aY, 0));
                Vertices.Add(new Vector3(zero, zero, zero));
                // T2
                TexCoords.Add(new Vector3(bX, bY, 0));
                Vertices.Add(new Vector3(1, zero, zero));
                TexCoords.Add(new Vector3(cX, cY, 0));
                Vertices.Add(new Vector3(1, 1, zero));
                TexCoords.Add(new Vector3(aX, aY, 0));
                Vertices.Add(new Vector3(zero, zero, zero));
            }
            else if (normal.X == 1)
            {
                // T1
                TexCoords.Add(new Vector3(bX, bY, 0));
                Vertices.Add(new Vector3(1, 1, 1));
                TexCoords.Add(new Vector3(cX, cY, 0));
                Vertices.Add(new Vector3(1, 1, zero));
                TexCoords.Add(new Vector3(dX, dY, 0));
                Vertices.Add(new Vector3(1, zero, zero));
                // T2
                TexCoords.Add(new Vector3(aX, aY, 0));
                Vertices.Add(new Vector3(1, zero, 1));
                TexCoords.Add(new Vector3(bX, bY, 0));
                Vertices.Add(new Vector3(1, 1, 1));
                TexCoords.Add(new Vector3(dX, dY, 0));
                Vertices.Add(new Vector3(1, zero, zero));
            }
            else if (normal.X == -1)
            {
                // T1
                TexCoords.Add(new Vector3(cX, cY, 0));
                Vertices.Add(new Vector3(zero, zero, zero));
                TexCoords.Add(new Vector3(dX, dY, 0));
                Vertices.Add(new Vector3(zero, 1, zero));
                TexCoords.Add(new Vector3(aX, aY, 0));
                Vertices.Add(new Vector3(zero, 1, 1));
                // T2
                TexCoords.Add(new Vector3(cX, cY, 0));
                Vertices.Add(new Vector3(zero, zero, zero));
                TexCoords.Add(new Vector3(aX, aY, 0));
                Vertices.Add(new Vector3(zero, 1, 1));
                TexCoords.Add(new Vector3(bX, bY, 0));
                Vertices.Add(new Vector3(zero, zero, 1));
            }
            else if (normal.Y == 1)
            {
                // T1
                TexCoords.Add(new Vector3(aX, aY, 0));
                Vertices.Add(new Vector3(1, 1, 1));
                TexCoords.Add(new Vector3(bX, bY, 0));
                Vertices.Add(new Vector3(zero, 1, 1));
                TexCoords.Add(new Vector3(cX, cY, 0));
                Vertices.Add(new Vector3(zero, 1, zero));
                // T2
                TexCoords.Add(new Vector3(dX, dY, 0));
                Vertices.Add(new Vector3(1, 1, zero));
                TexCoords.Add(new Vector3(aX, aY, 0));
                Vertices.Add(new Vector3(1, 1, 1));
                TexCoords.Add(new Vector3(cX, cY, 0));
                Vertices.Add(new Vector3(zero, 1, zero));
            }
            else if (normal.Y == -1)
            {
                // T1
                TexCoords.Add(new Vector3(dX, dY, 0));
                Vertices.Add(new Vector3(zero, zero, zero));
                TexCoords.Add(new Vector3(aX, aY, 0));
                Vertices.Add(new Vector3(zero, zero, 1));
                TexCoords.Add(new Vector3(bX, bY, 0));
                Vertices.Add(new Vector3(1, zero, 1));
                // T2
                TexCoords.Add(new Vector3(dX, dY, 0));
                Vertices.Add(new Vector3(zero, zero, zero));
                TexCoords.Add(new Vector3(bX, bY, 0));
                Vertices.Add(new Vector3(1, zero, 1));
                TexCoords.Add(new Vector3(cX, cY, 0));
                Vertices.Add(new Vector3(1, zero, zero));
            }
            else
            {
                throw new Exception("Lazy code can't handle unique normals! Only axis-aligned, normalized normals!");
            }
        }

        public void Prepare()
        {
            Vertices = new List<Vector3>();
            Indices = new List<uint>();
            Normals = new List<Vector3>();
            TexCoords = new List<Vector3>();
            Colors = new List<Vector4>();
            BoneIDs = new List<Vector4>();
            BoneWeights = new List<Vector4>();
        }

        bool generated = false;

        public void Destroy()
        {
            if (generated)
            {
                GL.DeleteVertexArray(_VAO);
                GL.DeleteBuffer(_VertexVBO);
                GL.DeleteBuffer(_IndexVBO);
                GL.DeleteBuffer(_NormalVBO);
                GL.DeleteBuffer(_TexCoordVBO);
                GL.DeleteBuffer(_ColorVBO);
                GL.DeleteBuffer(_BoneIDVBO);
                GL.DeleteBuffer(_BoneWeightVBO);
            }
        }

        public void GenerateVBO()
        {
            if (generated)
            {
                Destroy();
            }
            if (Vertices.Count == 0)
            {
                return;
            }
            GL.BindVertexArray(0);
            Vector3[] vecs = Vertices.ToArray();
            uint[] inds = Indices.ToArray();
            Vector3[] norms = Normals.ToArray();
            Vector3[] texs = TexCoords.ToArray();
            Vector4[] cols = Colors.ToArray();
            Vector4[] ids = BoneIDs.ToArray();
            Vector4[] weights = BoneWeights.ToArray();
            for (int i = 0; i < weights.Length; i++)
            {
                if (weights[i].LengthSquared > 0)
                {
                    //weights[i] = weights[i].Normalized();
                }
                else
                {
                    //weights[i] = new Vector4(1, 0, 0, 0);
                }
            }
            // Vertex buffer
            GL.GenBuffers(1, out _VertexVBO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _VertexVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vecs.Length * Vector3.SizeInBytes),
                    vecs, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            // Normal buffer
            GL.GenBuffers(1, out _NormalVBO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _NormalVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(norms.Length * Vector3.SizeInBytes),
                    norms, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            // TexCoord buffer
            GL.GenBuffers(1, out _TexCoordVBO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _TexCoordVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(texs.Length * Vector3.SizeInBytes),
                    texs, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            // Color buffer
            GL.GenBuffers(1, out _ColorVBO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _ColorVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(cols.Length * Vector4.SizeInBytes),
                    cols, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            // Weight buffer
            GL.GenBuffers(1, out _BoneWeightVBO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _BoneWeightVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(weights.Length * Vector4.SizeInBytes),
                    weights, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            // ID buffer
            GL.GenBuffers(1, out _BoneIDVBO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _BoneIDVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(ids.Length * Vector4.SizeInBytes),
                    ids, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            // Index buffer
            GL.GenBuffers(1, out _IndexVBO);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _IndexVBO);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(inds.Length * sizeof(uint)),
                    inds, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            // VAO
            GL.GenVertexArrays(1, out _VAO);
            GL.BindVertexArray(_VAO);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _VertexVBO);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _NormalVBO);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _TexCoordVBO);
            GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _ColorVBO);
            GL.VertexAttribPointer(3, 4, VertexAttribPointerType.Float, false, 0, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _BoneWeightVBO);
            GL.VertexAttribPointer(4, 4, VertexAttribPointerType.Float, false, 0, 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _BoneIDVBO);
            GL.VertexAttribPointer(5, 4, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            GL.EnableVertexAttribArray(2);
            GL.EnableVertexAttribArray(3);
            GL.EnableVertexAttribArray(4);
            GL.EnableVertexAttribArray(5);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _IndexVBO);
            // Clean up
            GL.BindVertexArray(0);
            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);
            GL.DisableVertexAttribArray(2);
            GL.DisableVertexAttribArray(3);
            GL.DisableVertexAttribArray(4);
            GL.DisableVertexAttribArray(5);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            generated = true;

        }

        public static void BonesIdentity()
        {
            int bones = 50;
            float[] floats = new float[bones * 4 * 4];
            for (int i = 0; i < bones; i++)
            {
                for (int x = 0; x < 4; x++)
                {
                    for (int y = 0; y < 4; y++)
                    {
                        floats[i * 16 + x * 4 + y] = Matrix4.Identity[x, y];
                    }
                }
            }
            GL.UniformMatrix4(6, bones, false, floats);
        }

        public void Render(bool texture)
        {
            if (!generated)
            {
                return;
            }
            if (texture && Tex != null)
            {
                Tex.Bind();
            }
            GL.BindVertexArray(_VAO);
            GL.DrawElements(PrimitiveType.Triangles, Vertices.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);
            GL.BindVertexArray(0);
        }
    }

    public class TextureCoordinates
    {
        public TextureCoordinates()
        {
            xscale = 1;
            yscale = 1;
            xshift = 0;
            yshift = 0;
            xflip = false;
            yflip = false;
        }

        public float xscale;
        public float yscale;
        public float xshift;
        public float yshift;
        public bool xflip;
        public bool yflip;

        public override string ToString()
        {
            return xscale + "/" + yscale + "/" + xshift + "/" + yshift + "/" + (xflip ? "t" : "f") + "/" + (yflip ? "t" : "f");
        }

        public static TextureCoordinates FromString(string str)
        {
            TextureCoordinates tc = new TextureCoordinates();
            string[] data = str.Split('/');
            tc.xscale = Utilities.StringToFloat(data[0]);
            tc.yscale = Utilities.StringToFloat(data[1]);
            tc.xshift = Utilities.StringToFloat(data[2]);
            tc.yshift = Utilities.StringToFloat(data[3]);
            tc.xflip = data[4] == "t";
            tc.yflip = data[5] == "t";
            return tc;
        }
    }
}
