﻿namespace OpenTKMapMaker
{
    partial class PrimaryEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renderLightingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.consoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wireframeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tODOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textureAutoStretchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.websiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.docsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.glControlTop = new OpenTK.GLControl();
            this.glControlSide = new OpenTK.GLControl();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.glControlView = new OpenTK.GLControl();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.glControlOSide = new OpenTK.GLControl();
            this.glControlTex = new OpenTK.GLControl();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Black;
            this.menuStrip1.ForeColor = System.Drawing.Color.White;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1279, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.newToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+N";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.openToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+O";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.saveToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+S";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.saveAsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.exitToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeyDisplayString = "Alt+F4";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.exitToolStripMenuItem.Tag = "";
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem});
            this.editToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.undoToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.redoToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.cutToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.copyToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.pasteToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renderLightingToolStripMenuItem,
            this.consoleToolStripMenuItem,
            this.wireframeToolStripMenuItem});
            this.viewToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // renderLightingToolStripMenuItem
            // 
            this.renderLightingToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.renderLightingToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.renderLightingToolStripMenuItem.Name = "renderLightingToolStripMenuItem";
            this.renderLightingToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.renderLightingToolStripMenuItem.Text = "Render Lighting";
            this.renderLightingToolStripMenuItem.Click += new System.EventHandler(this.renderLightingToolStripMenuItem_Click);
            // 
            // consoleToolStripMenuItem
            // 
            this.consoleToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.consoleToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.consoleToolStripMenuItem.Name = "consoleToolStripMenuItem";
            this.consoleToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.consoleToolStripMenuItem.Text = "Console";
            // 
            // wireframeToolStripMenuItem
            // 
            this.wireframeToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.wireframeToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.wireframeToolStripMenuItem.Name = "wireframeToolStripMenuItem";
            this.wireframeToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.wireframeToolStripMenuItem.Text = "Wireframe";
            this.wireframeToolStripMenuItem.Click += new System.EventHandler(this.wireframeToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tODOToolStripMenuItem,
            this.textureAutoStretchToolStripMenuItem});
            this.toolsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // tODOToolStripMenuItem
            // 
            this.tODOToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.tODOToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.tODOToolStripMenuItem.Name = "tODOToolStripMenuItem";
            this.tODOToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.tODOToolStripMenuItem.Text = "Add Textures";
            this.tODOToolStripMenuItem.Click += new System.EventHandler(this.tODOToolStripMenuItem_Click);
            // 
            // textureAutoStretchToolStripMenuItem
            // 
            this.textureAutoStretchToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.textureAutoStretchToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.textureAutoStretchToolStripMenuItem.Name = "textureAutoStretchToolStripMenuItem";
            this.textureAutoStretchToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.textureAutoStretchToolStripMenuItem.Text = "Texture Auto-Stretch";
            this.textureAutoStretchToolStripMenuItem.Click += new System.EventHandler(this.textureAutoStretchToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.websiteToolStripMenuItem,
            this.docsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // websiteToolStripMenuItem
            // 
            this.websiteToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.websiteToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.websiteToolStripMenuItem.Name = "websiteToolStripMenuItem";
            this.websiteToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.websiteToolStripMenuItem.Text = "Website";
            // 
            // docsToolStripMenuItem
            // 
            this.docsToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.docsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.docsToolStripMenuItem.Name = "docsToolStripMenuItem";
            this.docsToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.docsToolStripMenuItem.Text = "Tutorials";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.aboutToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(1279, 496);
            this.splitContainer1.SplitterDistance = 426;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.glControlTop);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.glControlSide);
            this.splitContainer2.Size = new System.Drawing.Size(426, 496);
            this.splitContainer2.SplitterDistance = 222;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer2_SplitterMoved);
            // 
            // glControlTop
            // 
            this.glControlTop.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.glControlTop.BackColor = System.Drawing.Color.Black;
            this.glControlTop.Location = new System.Drawing.Point(0, 0);
            this.glControlTop.Name = "glControlTop";
            this.glControlTop.Size = new System.Drawing.Size(423, 231);
            this.glControlTop.TabIndex = 0;
            this.glControlTop.VSync = false;
            this.glControlTop.Load += new System.EventHandler(this.glControlTop_Load);
            this.glControlTop.Paint += new System.Windows.Forms.PaintEventHandler(this.glControlTop_Paint);
            this.glControlTop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glControlTop_KeyDown);
            this.glControlTop.KeyUp += new System.Windows.Forms.KeyEventHandler(this.glControlTop_KeyUp);
            this.glControlTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glControlTop_MouseDown);
            this.glControlTop.MouseEnter += new System.EventHandler(this.glControlTop_MouseEnter);
            this.glControlTop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControlTop_MouseMove);
            this.glControlTop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glControlTop_MouseUp);
            this.glControlTop.Resize += new System.EventHandler(this.glControlTop_Resize);
            // 
            // glControlSide
            // 
            this.glControlSide.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.glControlSide.BackColor = System.Drawing.Color.Black;
            this.glControlSide.Location = new System.Drawing.Point(0, 0);
            this.glControlSide.Name = "glControlSide";
            this.glControlSide.Size = new System.Drawing.Size(423, 266);
            this.glControlSide.TabIndex = 0;
            this.glControlSide.VSync = false;
            this.glControlSide.Load += new System.EventHandler(this.glControlSide_Load);
            this.glControlSide.Paint += new System.Windows.Forms.PaintEventHandler(this.glControlSide_Paint);
            this.glControlSide.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glControlSide_KeyDown);
            this.glControlSide.KeyUp += new System.Windows.Forms.KeyEventHandler(this.glControlSide_KeyUp);
            this.glControlSide.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glControlSide_MouseDown);
            this.glControlSide.MouseEnter += new System.EventHandler(this.glControlSide_MouseEnter);
            this.glControlSide.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControlSide_MouseMove);
            this.glControlSide.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glControlSide_MouseUp);
            this.glControlSide.Resize += new System.EventHandler(this.glControlSide_Resize);
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.glControlView);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer3.Size = new System.Drawing.Size(849, 496);
            this.splitContainer3.SplitterDistance = 222;
            this.splitContainer3.TabIndex = 0;
            this.splitContainer3.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer3_SplitterMoved);
            // 
            // glControlView
            // 
            this.glControlView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.glControlView.BackColor = System.Drawing.Color.Black;
            this.glControlView.Location = new System.Drawing.Point(-2, 0);
            this.glControlView.Name = "glControlView";
            this.glControlView.Size = new System.Drawing.Size(848, 231);
            this.glControlView.TabIndex = 0;
            this.glControlView.VSync = false;
            this.glControlView.Load += new System.EventHandler(this.glControlView_Load);
            this.glControlView.Paint += new System.Windows.Forms.PaintEventHandler(this.glControlView_Paint);
            this.glControlView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glControlView_KeyDown);
            this.glControlView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.glControlView_KeyUp);
            this.glControlView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glControlView_MouseDown);
            this.glControlView.MouseEnter += new System.EventHandler(this.glControlView_MouseEnter);
            this.glControlView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControlView_MouseMove);
            this.glControlView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glControlView_MouseUp);
            this.glControlView.Resize += new System.EventHandler(this.glControlView_Resize);
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.glControlOSide);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.glControlTex);
            this.splitContainer4.Size = new System.Drawing.Size(849, 270);
            this.splitContainer4.SplitterDistance = 412;
            this.splitContainer4.TabIndex = 3;
            this.splitContainer4.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer4_SplitterMoved);
            // 
            // glControlOSide
            // 
            this.glControlOSide.BackColor = System.Drawing.Color.Black;
            this.glControlOSide.Location = new System.Drawing.Point(0, 0);
            this.glControlOSide.Name = "glControlOSide";
            this.glControlOSide.Size = new System.Drawing.Size(409, 266);
            this.glControlOSide.TabIndex = 0;
            this.glControlOSide.VSync = false;
            this.glControlOSide.Load += new System.EventHandler(this.glControlOSide_Load);
            this.glControlOSide.Paint += new System.Windows.Forms.PaintEventHandler(this.glControlOSide_Paint);
            this.glControlOSide.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glControlOSide_KeyDown);
            this.glControlOSide.KeyUp += new System.Windows.Forms.KeyEventHandler(this.glControlOSide_KeyUp);
            this.glControlOSide.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glControlOSide_MouseDown);
            this.glControlOSide.MouseEnter += new System.EventHandler(this.glControlOSide_MouseEnter);
            this.glControlOSide.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControlOSide_MouseMove);
            this.glControlOSide.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glControlOSide_MouseUp);
            this.glControlOSide.Resize += new System.EventHandler(this.glControlOSide_Resize);
            // 
            // glControlTex
            // 
            this.glControlTex.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.glControlTex.BackColor = System.Drawing.Color.Black;
            this.glControlTex.Location = new System.Drawing.Point(3, -1);
            this.glControlTex.Name = "glControlTex";
            this.glControlTex.Size = new System.Drawing.Size(430, 267);
            this.glControlTex.TabIndex = 0;
            this.glControlTex.VSync = false;
            this.glControlTex.Load += new System.EventHandler(this.glControlTex_Load);
            this.glControlTex.Paint += new System.Windows.Forms.PaintEventHandler(this.glControlTex_Paint);
            this.glControlTex.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glControlTex_KeyDown);
            this.glControlTex.KeyUp += new System.Windows.Forms.KeyEventHandler(this.glControlTex_KeyUp);
            this.glControlTex.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.glControlTex_MouseDoubleClick);
            this.glControlTex.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glControlTex_MouseDown);
            this.glControlTex.MouseEnter += new System.EventHandler(this.glControlTex_MouseEnter);
            this.glControlTex.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glControlTex_MouseUp);
            this.glControlTex.Resize += new System.EventHandler(this.glControlTex_Resize);
            // 
            // PrimaryEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1279, 520);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "PrimaryEditor";
            this.Text = "mcmonkey\'s Map Maker";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PrimaryEditor_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.PrimaryEditor_KeyUp);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PrimaryEditor_MouseUp);
            this.Resize += new System.EventHandler(this.PrimaryEditor_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renderLightingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tODOToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem websiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem docsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private OpenTK.GLControl glControlView;
        private OpenTK.GLControl glControlTop;
        private OpenTK.GLControl glControlSide;
        private OpenTK.GLControl glControlOSide;
        private OpenTK.GLControl glControlTex;
        private System.Windows.Forms.ToolStripMenuItem consoleToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.ToolStripMenuItem wireframeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem textureAutoStretchToolStripMenuItem;
    }
}