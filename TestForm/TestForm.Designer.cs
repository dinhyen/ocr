namespace TestForm
{
	partial class TestForm
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
			this.btnTestLed = new System.Windows.Forms.Button();
			this.characterPanel = new CharacterPanel.CharacterPanel();
			this.led = new Led.Led();
			this.chart1 = new CustomChart.CustomChart();
			this.SuspendLayout();
			// 
			// btnTestLed
			// 
			this.btnTestLed.Location = new System.Drawing.Point(13, 162);
			this.btnTestLed.Name = "btnTestLed";
			this.btnTestLed.Size = new System.Drawing.Size(75, 23);
			this.btnTestLed.TabIndex = 2;
			this.btnTestLed.Text = "Toggle LED";
			this.btnTestLed.UseVisualStyleBackColor = true;
			this.btnTestLed.Click += new System.EventHandler(this.btnTestLed_Click);
			// 
			// characterPanel
			// 
			this.characterPanel.BackColor = System.Drawing.SystemColors.ControlDark;
			this.characterPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.characterPanel.CharArray = new double[] {
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
			this.characterPanel.Location = new System.Drawing.Point(13, 13);
			this.characterPanel.Name = "characterPanel";
			this.characterPanel.Size = new System.Drawing.Size(66, 96);
			this.characterPanel.TabIndex = 3;
			this.characterPanel.TestButtonEnabled = true;
			this.characterPanel.TestEvent += new CharacterPanel.CharacterPanel.TestEventHandler(this.characterPanel_TestEvent);
			// 
			// led
			// 
			this.led.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.led.Location = new System.Drawing.Point(13, 118);
			this.led.Name = "led";
			this.led.Size = new System.Drawing.Size(70, 25);
			this.led.TabIndex = 1;
			// 
			// chart1
			// 
			this.chart1.Location = new System.Drawing.Point(124, 13);
			this.chart1.Name = "chart1";
			this.chart1.Size = new System.Drawing.Size(216, 199);
			this.chart1.TabIndex = 4;
			this.chart1.Text = "chart";
			// 
			// TestForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(386, 266);
			this.Controls.Add(this.chart1);
			this.Controls.Add(this.characterPanel);
			this.Controls.Add(this.btnTestLed);
			this.Controls.Add(this.led);
			this.Name = "TestForm";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}

		#endregion

		private Led.Led led;
		private System.Windows.Forms.Button btnTestLed;
		private CharacterPanel.CharacterPanel characterPanel;
		private CustomChart.CustomChart chart1;
	}
}

