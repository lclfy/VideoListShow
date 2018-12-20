namespace VideoListShow
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.Images_pnl = new System.Windows.Forms.Panel();
            this.mainMenu_pnl = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(722, 175);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(675, 83);
            this.label1.TabIndex = 0;
            this.label1.Text = "行车安全技术保障体系";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Images_pnl
            // 
            this.Images_pnl.Location = new System.Drawing.Point(168, 315);
            this.Images_pnl.Name = "Images_pnl";
            this.Images_pnl.Size = new System.Drawing.Size(2492, 1506);
            this.Images_pnl.TabIndex = 3;
            this.Images_pnl.Scroll += new System.Windows.Forms.ScrollEventHandler(this.Images_pnl_Scroll);
            this.Images_pnl.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.Images_pnl_ControlAdded);
            // 
            // mainMenu_pnl
            // 
            this.mainMenu_pnl.Location = new System.Drawing.Point(165, 271);
            this.mainMenu_pnl.Name = "mainMenu_pnl";
            this.mainMenu_pnl.Size = new System.Drawing.Size(70, 70);
            this.mainMenu_pnl.TabIndex = 4;
            this.mainMenu_pnl.Scroll += new System.Windows.Forms.ScrollEventHandler(this.Images_pnl_Scroll);
            this.mainMenu_pnl.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.Images_pnl_ControlAdded);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CanResize = false;
            this.CaptionBackColorBottom = System.Drawing.Color.White;
            this.CaptionBackColorTop = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(2732, 1536);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Images_pnl);
            this.Controls.Add(this.mainMenu_pnl);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Main";
            this.Text = "";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel Images_pnl;
        private System.Windows.Forms.Panel mainMenu_pnl;
    }
}

