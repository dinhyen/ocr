namespace TestForm2
{
	partial class Form1
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
			this.led1 = new Led.Led();
			this.characterPanel1 = new CharacterPanel.CharacterPanel();
			this.SuspendLayout();
			// 
			// led1
			// 
			this.led1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.led1.Location = new System.Drawing.Point(13, 13);
			this.led1.Name = "led1";
			this.led1.Size = new System.Drawing.Size(70, 25);
			this.led1.TabIndex = 0;
			// 
			// characterPanel1
			// 
			this.characterPanel1.BackColor = System.Drawing.SystemColors.ControlDark;
			this.characterPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.characterPanel1.CharArray = new double[] {
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0,
        0};
			this.characterPanel1.Location = new System.Drawing.Point(13, 45);
			this.characterPanel1.Name = "characterPanel1";
			this.characterPanel1.Size = new System.Drawing.Size(66, 96);
			this.characterPanel1.TabIndex = 1;
			this.characterPanel1.TestButtonEnabled = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 266);
			this.Controls.Add(this.characterPanel1);
			this.Controls.Add(this.led1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}

		#endregion

		private Led.Led led1;
		private CharacterPanel.CharacterPanel characterPanel1;
	}
}

