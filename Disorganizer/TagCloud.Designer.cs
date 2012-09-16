namespace zasz.me.Disorganizer
{
    partial class TagCloud
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
            this.Cloud = new System.Windows.Forms.PictureBox();
            this.Words = new System.Windows.Forms.TextBox();
            this.Generate = new System.Windows.Forms.Button();
            this.MaxFontSize = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.MinFontSize = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Width = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Height = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Angle = new System.Windows.Forms.TextBox();
            this.FontsCombo = new System.Windows.Forms.ComboBox();
            this.StrategyCombo = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.BgfgStrategyCombo = new System.Windows.Forms.ComboBox();
            this.FgStrategyCombo = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.ColorPick = new System.Windows.Forms.ColorDialog();
            this.SetBg = new System.Windows.Forms.Button();
            this.SetFg = new System.Windows.Forms.Button();
            this.BackG = new System.Windows.Forms.Label();
            this.ForeG = new System.Windows.Forms.Label();
            this.VerticalTextRight = new System.Windows.Forms.CheckBox();
            this.ShowBoundaries = new System.Windows.Forms.CheckBox();
            this.Cropper = new System.Windows.Forms.CheckBox();
            this.Margin = new System.Windows.Forms.TextBox();
            this.Skipped = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Cloud)).BeginInit();
            this.SuspendLayout();
            // 
            // Cloud
            // 
            this.Cloud.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Cloud.Location = new System.Drawing.Point(428, 43);
            this.Cloud.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Cloud.Name = "Cloud";
            this.Cloud.Size = new System.Drawing.Size(100, 50);
            this.Cloud.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.Cloud.TabIndex = 0;
            this.Cloud.TabStop = false;
            // 
            // Words
            // 
            this.Words.Location = new System.Drawing.Point(59, 43);
            this.Words.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Words.Multiline = true;
            this.Words.Name = "Words";
            this.Words.Size = new System.Drawing.Size(327, 170);
            this.Words.TabIndex = 1;
            this.Words.Text = "asp.net, 15\r\ngames, 10\r\nfun, 18\r\nbooks, 5\r\nmusic, 9\r\ncrapo, 8\r\ndota, 6\r\n";
            // 
            // Generate
            // 
            this.Generate.Location = new System.Drawing.Point(296, 261);
            this.Generate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Generate.Name = "Generate";
            this.Generate.Size = new System.Drawing.Size(91, 155);
            this.Generate.TabIndex = 2;
            this.Generate.Text = "Generate";
            this.Generate.UseVisualStyleBackColor = true;
            this.Generate.Click += new System.EventHandler(this.GenerateClick);
            // 
            // MaxFontSize
            // 
            this.MaxFontSize.Location = new System.Drawing.Point(155, 265);
            this.MaxFontSize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaxFontSize.Name = "MaxFontSize";
            this.MaxFontSize.Size = new System.Drawing.Size(132, 22);
            this.MaxFontSize.TabIndex = 3;
            this.MaxFontSize.Text = "72";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 268);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "MaxFontSize";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 300);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "MinFontSize";
            // 
            // MinFontSize
            // 
            this.MinFontSize.Location = new System.Drawing.Point(155, 297);
            this.MinFontSize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinFontSize.Name = "MinFontSize";
            this.MinFontSize.Size = new System.Drawing.Size(132, 22);
            this.MinFontSize.TabIndex = 5;
            this.MinFontSize.Text = "12";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(72, 428);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "FontName";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(100, 336);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Width";
            // 
            // Width
            // 
            this.Width.Location = new System.Drawing.Point(155, 327);
            this.Width.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Width.Name = "Width";
            this.Width.Size = new System.Drawing.Size(132, 22);
            this.Width.TabIndex = 9;
            this.Width.Text = "500";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(96, 363);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "Height";
            // 
            // Height
            // 
            this.Height.Location = new System.Drawing.Point(155, 359);
            this.Height.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Height.Name = "Height";
            this.Height.Size = new System.Drawing.Size(132, 22);
            this.Height.TabIndex = 11;
            this.Height.Text = "500";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(101, 395);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 17);
            this.label6.TabIndex = 14;
            this.label6.Text = "Angle";
            // 
            // Angle
            // 
            this.Angle.Location = new System.Drawing.Point(155, 391);
            this.Angle.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Angle.Name = "Angle";
            this.Angle.Size = new System.Drawing.Size(132, 22);
            this.Angle.TabIndex = 13;
            // 
            // FontsCombo
            // 
            this.FontsCombo.FormattingEnabled = true;
            this.FontsCombo.Location = new System.Drawing.Point(155, 423);
            this.FontsCombo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FontsCombo.Name = "FontsCombo";
            this.FontsCombo.Size = new System.Drawing.Size(231, 24);
            this.FontsCombo.TabIndex = 15;
            // 
            // StrategyCombo
            // 
            this.StrategyCombo.FormattingEnabled = true;
            this.StrategyCombo.Location = new System.Drawing.Point(155, 457);
            this.StrategyCombo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.StrategyCombo.Name = "StrategyCombo";
            this.StrategyCombo.Size = new System.Drawing.Size(231, 24);
            this.StrategyCombo.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(36, 460);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(111, 17);
            this.label7.TabIndex = 16;
            this.label7.Text = "Display Strategy";
            // 
            // BgfgStrategyCombo
            // 
            this.BgfgStrategyCombo.FormattingEnabled = true;
            this.BgfgStrategyCombo.Location = new System.Drawing.Point(155, 491);
            this.BgfgStrategyCombo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BgfgStrategyCombo.Name = "BgfgStrategyCombo";
            this.BgfgStrategyCombo.Size = new System.Drawing.Size(231, 24);
            this.BgfgStrategyCombo.TabIndex = 18;
            // 
            // FgStrategyCombo
            // 
            this.FgStrategyCombo.FormattingEnabled = true;
            this.FgStrategyCombo.Location = new System.Drawing.Point(155, 526);
            this.FgStrategyCombo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FgStrategyCombo.Name = "FgStrategyCombo";
            this.FgStrategyCombo.Size = new System.Drawing.Size(231, 24);
            this.FgStrategyCombo.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(39, 491);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(108, 17);
            this.label8.TabIndex = 20;
            this.label8.Text = "BG FG Strategy";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(63, 526);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 17);
            this.label9.TabIndex = 21;
            this.label9.Text = "FG Strategy";
            // 
            // SetBg
            // 
            this.SetBg.Location = new System.Drawing.Point(155, 559);
            this.SetBg.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SetBg.Name = "SetBg";
            this.SetBg.Size = new System.Drawing.Size(151, 28);
            this.SetBg.TabIndex = 22;
            this.SetBg.Text = "Set Background";
            this.SetBg.UseVisualStyleBackColor = true;
            this.SetBg.Click += new System.EventHandler(this.SetBgClick);
            // 
            // SetFg
            // 
            this.SetFg.Location = new System.Drawing.Point(155, 594);
            this.SetFg.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SetFg.Name = "SetFg";
            this.SetFg.Size = new System.Drawing.Size(151, 28);
            this.SetFg.TabIndex = 23;
            this.SetFg.Text = "Set Foreground";
            this.SetFg.UseVisualStyleBackColor = true;
            this.SetFg.Click += new System.EventHandler(this.SetFgClick);
            // 
            // BackG
            // 
            this.BackG.AutoSize = true;
            this.BackG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BackG.Location = new System.Drawing.Point(92, 565);
            this.BackG.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.BackG.Name = "BackG";
            this.BackG.Size = new System.Drawing.Size(46, 19);
            this.BackG.TabIndex = 24;
            this.BackG.Text = "BACK";
            // 
            // ForeG
            // 
            this.ForeG.AutoSize = true;
            this.ForeG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ForeG.Location = new System.Drawing.Point(92, 594);
            this.ForeG.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ForeG.Name = "ForeG";
            this.ForeG.Size = new System.Drawing.Size(48, 19);
            this.ForeG.TabIndex = 25;
            this.ForeG.Text = "FORE";
            // 
            // VerticalTextRight
            // 
            this.VerticalTextRight.AutoSize = true;
            this.VerticalTextRight.Location = new System.Drawing.Point(96, 662);
            this.VerticalTextRight.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.VerticalTextRight.Name = "VerticalTextRight";
            this.VerticalTextRight.Size = new System.Drawing.Size(145, 21);
            this.VerticalTextRight.TabIndex = 26;
            this.VerticalTextRight.Text = "Vertical Text Right";
            this.VerticalTextRight.UseVisualStyleBackColor = true;
            // 
            // ShowBoundaries
            // 
            this.ShowBoundaries.AutoSize = true;
            this.ShowBoundaries.Location = new System.Drawing.Point(255, 662);
            this.ShowBoundaries.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ShowBoundaries.Name = "ShowBoundaries";
            this.ShowBoundaries.Size = new System.Drawing.Size(140, 21);
            this.ShowBoundaries.TabIndex = 27;
            this.ShowBoundaries.Text = "Show Boundaries";
            this.ShowBoundaries.UseVisualStyleBackColor = true;
            // 
            // Cropper
            // 
            this.Cropper.AutoSize = true;
            this.Cropper.Location = new System.Drawing.Point(97, 634);
            this.Cropper.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Cropper.Name = "Cropper";
            this.Cropper.Size = new System.Drawing.Size(143, 21);
            this.Cropper.TabIndex = 28;
            this.Cropper.Text = "Crop with Margin :";
            this.Cropper.UseVisualStyleBackColor = true;
            // 
            // Margin
            // 
            this.Margin.Location = new System.Drawing.Point(253, 630);
            this.Margin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Margin.Name = "Margin";
            this.Margin.Size = new System.Drawing.Size(132, 22);
            this.Margin.TabIndex = 29;
            // 
            // Skipped
            // 
            this.Skipped.Location = new System.Drawing.Point(155, 222);
            this.Skipped.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Skipped.Name = "Skipped";
            this.Skipped.Size = new System.Drawing.Size(231, 22);
            this.Skipped.TabIndex = 30;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(81, 225);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 17);
            this.label10.TabIndex = 31;
            this.label10.Text = "Skipped";
            // 
            // TagCloud
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1323, 734);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.Skipped);
            this.Controls.Add(this.Margin);
            this.Controls.Add(this.Cropper);
            this.Controls.Add(this.ShowBoundaries);
            this.Controls.Add(this.VerticalTextRight);
            this.Controls.Add(this.ForeG);
            this.Controls.Add(this.BackG);
            this.Controls.Add(this.SetFg);
            this.Controls.Add(this.SetBg);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.FgStrategyCombo);
            this.Controls.Add(this.BgfgStrategyCombo);
            this.Controls.Add(this.StrategyCombo);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.FontsCombo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Angle);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Height);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Width);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.MinFontSize);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MaxFontSize);
            this.Controls.Add(this.Generate);
            this.Controls.Add(this.Words);
            this.Controls.Add(this.Cloud);
            this.Name = "TagCloud";
            this.Text = "TagCloud";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TagCloudFormClosing);
            this.Load += new System.EventHandler(this.TagCloudLoad);
            ((System.ComponentModel.ISupportInitialize)(this.Cloud)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Cloud;
        private System.Windows.Forms.TextBox Words;
        private System.Windows.Forms.Button Generate;
        private System.Windows.Forms.TextBox MaxFontSize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox MinFontSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private new System.Windows.Forms.TextBox Width;
        private System.Windows.Forms.Label label5;
        private new System.Windows.Forms.TextBox Height;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox Angle;
        private System.Windows.Forms.ComboBox FontsCombo;
        private System.Windows.Forms.ComboBox StrategyCombo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox BgfgStrategyCombo;
        private System.Windows.Forms.ComboBox FgStrategyCombo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ColorDialog ColorPick;
        private System.Windows.Forms.Button SetBg;
        private System.Windows.Forms.Button SetFg;
        private System.Windows.Forms.Label BackG;
        private System.Windows.Forms.Label ForeG;
        private System.Windows.Forms.CheckBox VerticalTextRight;
        private System.Windows.Forms.CheckBox ShowBoundaries;
        private System.Windows.Forms.CheckBox Cropper;
        private new System.Windows.Forms.TextBox Margin;
        private System.Windows.Forms.TextBox Skipped;
        private System.Windows.Forms.Label label10;
    }
}