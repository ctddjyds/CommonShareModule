namespace ZedGraph
{
	partial class ZedGraphControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.pointToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.zedContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SuspendLayout();
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Location = new System.Drawing.Point(128, 0);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 118);
            this.vScrollBar1.TabIndex = 0;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Location = new System.Drawing.Point(0, 118);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(128, 17);
            this.hScrollBar1.TabIndex = 1;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // pointToolTip
            // 
            this.pointToolTip.AutoPopDelay = 5000;
            this.pointToolTip.InitialDelay = 100;
            this.pointToolTip.ReshowDelay = 0;
            // 
            // zedContextMenuStrip
            // 
            this.zedContextMenuStrip.Name = "contextMenuStrip1";
            this.zedContextMenuStrip.Size = new System.Drawing.Size(153, 26);
            this.zedContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // ZedGraphControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.zedContextMenuStrip;
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.vScrollBar1);
            this.Name = "ZedGraphControl";
            this.Size = new System.Drawing.Size(150, 138);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ZedGraphControl_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ZedGraphControl_KeyUp);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.ZedGraphControl_MouseWheel);
            this.Resize += new System.EventHandler(this.ZedGraphControl_ReSize);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.VScrollBar vScrollBar1;
		private System.Windows.Forms.HScrollBar hScrollBar1;
		private System.Windows.Forms.ToolTip pointToolTip;
		private System.Windows.Forms.ContextMenuStrip zedContextMenuStrip;
	}
}
