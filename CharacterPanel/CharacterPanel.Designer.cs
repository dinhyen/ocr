namespace CharacterPanel
{
	partial class CharacterPanel
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

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnTest = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnTest
			// 
			this.btnTest.Location = new System.Drawing.Point(9, 75);
			this.btnTest.Name = "btnTest";
			this.btnTest.Size = new System.Drawing.Size(50, 22);
			this.btnTest.TabIndex = 0;
			this.btnTest.Text = "Test";
			this.btnTest.UseVisualStyleBackColor = true;
			this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
			// 
			// CharacterPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Controls.Add(this.btnTest);
			this.Name = "CharacterPanel";
			this.Size = new System.Drawing.Size(66, 96);
			this.ClientSizeChanged += new System.EventHandler(this.CharacterPanel_ClientSizeChanged);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnTest;

	}
}
