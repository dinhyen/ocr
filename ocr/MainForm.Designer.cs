namespace ocr
{
	partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.pnlTop = new System.Windows.Forms.Panel();
			this.grpData = new System.Windows.Forms.GroupBox();
			this.led9 = new Led.Led();
			this.led8 = new Led.Led();
			this.led7 = new Led.Led();
			this.led6 = new Led.Led();
			this.led5 = new Led.Led();
			this.led4 = new Led.Led();
			this.led3 = new Led.Led();
			this.led2 = new Led.Led();
			this.led1 = new Led.Led();
			this.led0 = new Led.Led();
			this.cPanel9 = new CharacterPanel.CharacterPanel();
			this.cPanel8 = new CharacterPanel.CharacterPanel();
			this.cPanel7 = new CharacterPanel.CharacterPanel();
			this.cPanel6 = new CharacterPanel.CharacterPanel();
			this.cPanel5 = new CharacterPanel.CharacterPanel();
			this.cPanel4 = new CharacterPanel.CharacterPanel();
			this.cPanel3 = new CharacterPanel.CharacterPanel();
			this.cPanel2 = new CharacterPanel.CharacterPanel();
			this.cPanel1 = new CharacterPanel.CharacterPanel();
			this.cPanel0 = new CharacterPanel.CharacterPanel();
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
			this.menuLoad = new System.Windows.Forms.ToolStripMenuItem();
			this.menuSave = new System.Windows.Forms.ToolStripMenuItem();
			this.menuSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
			this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.menuAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.pnlBottom = new System.Windows.Forms.Panel();
			this.grpError = new System.Windows.Forms.GroupBox();
			this.chartError = new CustomChart.CustomChart();
			this.grpInput = new System.Windows.Forms.GroupBox();
			this.cPanelUser = new CharacterPanel.CharacterPanel();
			this.grpNeuralNet = new System.Windows.Forms.GroupBox();
			this.btnStop = new System.Windows.Forms.Button();
			this.btnStart = new System.Windows.Forms.Button();
			this.lblLineBottom = new System.Windows.Forms.Label();
			this.txtCurrentError = new System.Windows.Forms.TextBox();
			this.txtCurrentIteration = new System.Windows.Forms.TextBox();
			this.lblError = new System.Windows.Forms.Label();
			this.lblIterations = new System.Windows.Forms.Label();
			this.lblLineTop = new System.Windows.Forms.Label();
			this.txtErrorLimit = new System.Windows.Forms.TextBox();
			this.comboSigmoidType = new System.Windows.Forms.ComboBox();
			this.lblSigmoidType = new System.Windows.Forms.Label();
			this.lblErrorLimit = new System.Windows.Forms.Label();
			this.txtSigmoidAlpha = new System.Windows.Forms.TextBox();
			this.lblSigmoidAlpha = new System.Windows.Forms.Label();
			this.txtMomentum = new System.Windows.Forms.TextBox();
			this.lblMomentum = new System.Windows.Forms.Label();
			this.txtLearningRate = new System.Windows.Forms.TextBox();
			this.lblLearningRate = new System.Windows.Forms.Label();
			this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
			this.pnlTop.SuspendLayout();
			this.grpData.SuspendLayout();
			this.menuStrip.SuspendLayout();
			this.pnlBottom.SuspendLayout();
			this.grpError.SuspendLayout();
			this.grpInput.SuspendLayout();
			this.grpNeuralNet.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlTop
			// 
			this.pnlTop.Controls.Add(this.grpData);
			this.pnlTop.Controls.Add(this.menuStrip);
			this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlTop.Location = new System.Drawing.Point(0, 0);
			this.pnlTop.Name = "pnlTop";
			this.pnlTop.Size = new System.Drawing.Size(792, 208);
			this.pnlTop.TabIndex = 0;
			// 
			// grpData
			// 
			this.grpData.Controls.Add(this.led9);
			this.grpData.Controls.Add(this.led8);
			this.grpData.Controls.Add(this.led7);
			this.grpData.Controls.Add(this.led6);
			this.grpData.Controls.Add(this.led5);
			this.grpData.Controls.Add(this.led4);
			this.grpData.Controls.Add(this.led3);
			this.grpData.Controls.Add(this.led2);
			this.grpData.Controls.Add(this.led1);
			this.grpData.Controls.Add(this.led0);
			this.grpData.Controls.Add(this.cPanel9);
			this.grpData.Controls.Add(this.cPanel8);
			this.grpData.Controls.Add(this.cPanel7);
			this.grpData.Controls.Add(this.cPanel6);
			this.grpData.Controls.Add(this.cPanel5);
			this.grpData.Controls.Add(this.cPanel4);
			this.grpData.Controls.Add(this.cPanel3);
			this.grpData.Controls.Add(this.cPanel2);
			this.grpData.Controls.Add(this.cPanel1);
			this.grpData.Controls.Add(this.cPanel0);
			this.grpData.Location = new System.Drawing.Point(13, 29);
			this.grpData.Name = "grpData";
			this.grpData.Size = new System.Drawing.Size(767, 179);
			this.grpData.TabIndex = 0;
			this.grpData.TabStop = false;
			this.grpData.Text = "Training Data";
			// 
			// led9
			// 
			this.led9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.led9.Location = new System.Drawing.Point(690, 31);
			this.led9.Name = "led9";
			this.led9.Size = new System.Drawing.Size(70, 25);
			this.led9.TabIndex = 0;
			// 
			// led8
			// 
			this.led8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.led8.Location = new System.Drawing.Point(614, 31);
			this.led8.Name = "led8";
			this.led8.Size = new System.Drawing.Size(70, 25);
			this.led8.TabIndex = 0;
			// 
			// led7
			// 
			this.led7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.led7.Location = new System.Drawing.Point(538, 31);
			this.led7.Name = "led7";
			this.led7.Size = new System.Drawing.Size(70, 25);
			this.led7.TabIndex = 0;
			// 
			// led6
			// 
			this.led6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.led6.Location = new System.Drawing.Point(462, 31);
			this.led6.Name = "led6";
			this.led6.Size = new System.Drawing.Size(70, 25);
			this.led6.TabIndex = 0;
			// 
			// led5
			// 
			this.led5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.led5.Location = new System.Drawing.Point(386, 31);
			this.led5.Name = "led5";
			this.led5.Size = new System.Drawing.Size(70, 25);
			this.led5.TabIndex = 0;
			// 
			// led4
			// 
			this.led4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.led4.Location = new System.Drawing.Point(310, 31);
			this.led4.Name = "led4";
			this.led4.Size = new System.Drawing.Size(70, 25);
			this.led4.TabIndex = 0;
			// 
			// led3
			// 
			this.led3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.led3.Location = new System.Drawing.Point(234, 31);
			this.led3.Name = "led3";
			this.led3.Size = new System.Drawing.Size(70, 25);
			this.led3.TabIndex = 0;
			// 
			// led2
			// 
			this.led2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.led2.Location = new System.Drawing.Point(158, 31);
			this.led2.Name = "led2";
			this.led2.Size = new System.Drawing.Size(70, 25);
			this.led2.TabIndex = 0;
			// 
			// led1
			// 
			this.led1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.led1.Location = new System.Drawing.Point(82, 31);
			this.led1.Name = "led1";
			this.led1.Size = new System.Drawing.Size(70, 25);
			this.led1.TabIndex = 0;
			// 
			// led0
			// 
			this.led0.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.led0.Location = new System.Drawing.Point(6, 31);
			this.led0.Name = "led0";
			this.led0.Size = new System.Drawing.Size(70, 25);
			this.led0.TabIndex = 0;
			// 
			// cPanel9
			// 
			this.cPanel9.BackColor = System.Drawing.SystemColors.ControlDark;
			this.cPanel9.Location = new System.Drawing.Point(690, 64);
			this.cPanel9.Name = "cPanel9";
			this.cPanel9.Size = new System.Drawing.Size(70, 100);
			this.cPanel9.TabIndex = 16;
			this.cPanel9.TestButtonEnabled = true;
			this.cPanel9.TestEvent += new CharacterPanel.CharacterPanel.TestEventHandler(this.cPanel_TestEvent);
			// 
			// cPanel8
			// 
			this.cPanel8.BackColor = System.Drawing.SystemColors.ControlDark;
			this.cPanel8.Location = new System.Drawing.Point(614, 64);
			this.cPanel8.Name = "cPanel8";
			this.cPanel8.Size = new System.Drawing.Size(70, 100);
			this.cPanel8.TabIndex = 15;
			this.cPanel8.TestButtonEnabled = true;
			this.cPanel8.TestEvent += new CharacterPanel.CharacterPanel.TestEventHandler(this.cPanel_TestEvent);
			// 
			// cPanel7
			// 
			this.cPanel7.BackColor = System.Drawing.SystemColors.ControlDark;
			this.cPanel7.Location = new System.Drawing.Point(538, 64);
			this.cPanel7.Name = "cPanel7";
			this.cPanel7.Size = new System.Drawing.Size(70, 100);
			this.cPanel7.TabIndex = 14;
			this.cPanel7.TestButtonEnabled = true;
			this.cPanel7.TestEvent += new CharacterPanel.CharacterPanel.TestEventHandler(this.cPanel_TestEvent);
			// 
			// cPanel6
			// 
			this.cPanel6.BackColor = System.Drawing.SystemColors.ControlDark;
			this.cPanel6.Location = new System.Drawing.Point(462, 64);
			this.cPanel6.Name = "cPanel6";
			this.cPanel6.Size = new System.Drawing.Size(70, 100);
			this.cPanel6.TabIndex = 13;
			this.cPanel6.TestButtonEnabled = true;
			this.cPanel6.TestEvent += new CharacterPanel.CharacterPanel.TestEventHandler(this.cPanel_TestEvent);
			// 
			// cPanel5
			// 
			this.cPanel5.BackColor = System.Drawing.SystemColors.ControlDark;
			this.cPanel5.Location = new System.Drawing.Point(386, 64);
			this.cPanel5.Name = "cPanel5";
			this.cPanel5.Size = new System.Drawing.Size(70, 100);
			this.cPanel5.TabIndex = 12;
			this.cPanel5.TestButtonEnabled = true;
			this.cPanel5.TestEvent += new CharacterPanel.CharacterPanel.TestEventHandler(this.cPanel_TestEvent);
			// 
			// cPanel4
			// 
			this.cPanel4.BackColor = System.Drawing.SystemColors.ControlDark;
			this.cPanel4.Location = new System.Drawing.Point(310, 64);
			this.cPanel4.Name = "cPanel4";
			this.cPanel4.Size = new System.Drawing.Size(70, 100);
			this.cPanel4.TabIndex = 11;
			this.cPanel4.TestButtonEnabled = true;
			this.cPanel4.TestEvent += new CharacterPanel.CharacterPanel.TestEventHandler(this.cPanel_TestEvent);
			// 
			// cPanel3
			// 
			this.cPanel3.BackColor = System.Drawing.SystemColors.ControlDark;
			this.cPanel3.Location = new System.Drawing.Point(234, 64);
			this.cPanel3.Name = "cPanel3";
			this.cPanel3.Size = new System.Drawing.Size(70, 100);
			this.cPanel3.TabIndex = 10;
			this.cPanel3.TestButtonEnabled = true;
			this.cPanel3.TestEvent += new CharacterPanel.CharacterPanel.TestEventHandler(this.cPanel_TestEvent);
			// 
			// cPanel2
			// 
			this.cPanel2.BackColor = System.Drawing.SystemColors.ControlDark;
			this.cPanel2.Location = new System.Drawing.Point(158, 64);
			this.cPanel2.Name = "cPanel2";
			this.cPanel2.Size = new System.Drawing.Size(70, 100);
			this.cPanel2.TabIndex = 9;
			this.cPanel2.TestButtonEnabled = true;
			this.cPanel2.TestEvent += new CharacterPanel.CharacterPanel.TestEventHandler(this.cPanel_TestEvent);
			// 
			// cPanel1
			// 
			this.cPanel1.BackColor = System.Drawing.SystemColors.ControlDark;
			this.cPanel1.Location = new System.Drawing.Point(82, 64);
			this.cPanel1.Name = "cPanel1";
			this.cPanel1.Size = new System.Drawing.Size(70, 100);
			this.cPanel1.TabIndex = 8;
			this.cPanel1.TestButtonEnabled = true;
			this.cPanel1.TestEvent += new CharacterPanel.CharacterPanel.TestEventHandler(this.cPanel_TestEvent);
			// 
			// cPanel0
			// 
			this.cPanel0.BackColor = System.Drawing.SystemColors.ControlDark;
			this.cPanel0.Location = new System.Drawing.Point(6, 64);
			this.cPanel0.Name = "cPanel0";
			this.cPanel0.Size = new System.Drawing.Size(70, 100);
			this.cPanel0.TabIndex = 7;
			this.cPanel0.TestButtonEnabled = true;
			this.cPanel0.TestEvent += new CharacterPanel.CharacterPanel.TestEventHandler(this.cPanel_TestEvent);
			// 
			// menuStrip
			// 
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuHelp});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(792, 26);
			this.menuStrip.TabIndex = 0;
			this.menuStrip.Text = "Menu";
			// 
			// menuFile
			// 
			this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuLoad,
            this.menuSave,
            this.menuSeparator,
            this.menuExit});
			this.menuFile.Name = "menuFile";
			this.menuFile.Size = new System.Drawing.Size(40, 22);
			this.menuFile.Text = "&File";
			// 
			// menuLoad
			// 
			this.menuLoad.Name = "menuLoad";
			this.menuLoad.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
			this.menuLoad.Size = new System.Drawing.Size(220, 22);
			this.menuLoad.Text = "&Load data...";
			this.menuLoad.Click += new System.EventHandler(this.menuLoad_Click);
			// 
			// menuSave
			// 
			this.menuSave.Name = "menuSave";
			this.menuSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.menuSave.Size = new System.Drawing.Size(220, 22);
			this.menuSave.Text = "&Save data...";
			this.menuSave.Click += new System.EventHandler(this.menuSave_Click);
			// 
			// menuSeparator
			// 
			this.menuSeparator.Name = "menuSeparator";
			this.menuSeparator.Size = new System.Drawing.Size(217, 6);
			// 
			// menuExit
			// 
			this.menuExit.Name = "menuExit";
			this.menuExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.menuExit.Size = new System.Drawing.Size(220, 22);
			this.menuExit.Text = "E&xit";
			this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
			// 
			// menuHelp
			// 
			this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAbout});
			this.menuHelp.Name = "menuHelp";
			this.menuHelp.Size = new System.Drawing.Size(48, 22);
			this.menuHelp.Text = "&Help";
			// 
			// menuAbout
			// 
			this.menuAbout.Name = "menuAbout";
			this.menuAbout.Size = new System.Drawing.Size(163, 22);
			this.menuAbout.Text = "&About OCR";
			this.menuAbout.Click += new System.EventHandler(this.menuAbout_Click);
			// 
			// pnlBottom
			// 
			this.pnlBottom.Controls.Add(this.grpError);
			this.pnlBottom.Controls.Add(this.grpInput);
			this.pnlBottom.Controls.Add(this.grpNeuralNet);
			this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlBottom.Location = new System.Drawing.Point(0, 208);
			this.pnlBottom.Name = "pnlBottom";
			this.pnlBottom.Size = new System.Drawing.Size(792, 299);
			this.pnlBottom.TabIndex = 0;
			// 
			// grpError
			// 
			this.grpError.Controls.Add(this.chartError);
			this.grpError.Location = new System.Drawing.Point(493, 6);
			this.grpError.Name = "grpError";
			this.grpError.Size = new System.Drawing.Size(287, 283);
			this.grpError.TabIndex = 0;
			this.grpError.TabStop = false;
			this.grpError.Text = "Error";
			// 
			// chartError
			// 
			this.chartError.Location = new System.Drawing.Point(44, 44);
			this.chartError.Name = "chartError";
			this.chartError.Size = new System.Drawing.Size(200, 200);
			this.chartError.TabIndex = 0;
			this.chartError.Text = "Chart";
			// 
			// grpInput
			// 
			this.grpInput.Controls.Add(this.cPanelUser);
			this.grpInput.Location = new System.Drawing.Point(285, 6);
			this.grpInput.Name = "grpInput";
			this.grpInput.Size = new System.Drawing.Size(202, 283);
			this.grpInput.TabIndex = 0;
			this.grpInput.TabStop = false;
			this.grpInput.Text = "User Input";
			// 
			// cPanelUser
			// 
			this.cPanelUser.BackColor = System.Drawing.SystemColors.ControlDark;
			this.cPanelUser.Location = new System.Drawing.Point(30, 45);
			this.cPanelUser.Name = "cPanelUser";
			this.cPanelUser.Size = new System.Drawing.Size(70, 100);
			this.cPanelUser.TabIndex = 0;
			this.cPanelUser.TestButtonEnabled = true;
			this.cPanelUser.TestEvent += new CharacterPanel.CharacterPanel.TestEventHandler(this.cPanel_TestEvent);
			// 
			// grpNeuralNet
			// 
			this.grpNeuralNet.Controls.Add(this.btnStop);
			this.grpNeuralNet.Controls.Add(this.btnStart);
			this.grpNeuralNet.Controls.Add(this.lblLineBottom);
			this.grpNeuralNet.Controls.Add(this.txtCurrentError);
			this.grpNeuralNet.Controls.Add(this.txtCurrentIteration);
			this.grpNeuralNet.Controls.Add(this.lblError);
			this.grpNeuralNet.Controls.Add(this.lblIterations);
			this.grpNeuralNet.Controls.Add(this.lblLineTop);
			this.grpNeuralNet.Controls.Add(this.txtErrorLimit);
			this.grpNeuralNet.Controls.Add(this.comboSigmoidType);
			this.grpNeuralNet.Controls.Add(this.lblSigmoidType);
			this.grpNeuralNet.Controls.Add(this.lblErrorLimit);
			this.grpNeuralNet.Controls.Add(this.txtSigmoidAlpha);
			this.grpNeuralNet.Controls.Add(this.lblSigmoidAlpha);
			this.grpNeuralNet.Controls.Add(this.txtMomentum);
			this.grpNeuralNet.Controls.Add(this.lblMomentum);
			this.grpNeuralNet.Controls.Add(this.txtLearningRate);
			this.grpNeuralNet.Controls.Add(this.lblLearningRate);
			this.grpNeuralNet.Location = new System.Drawing.Point(13, 6);
			this.grpNeuralNet.Name = "grpNeuralNet";
			this.grpNeuralNet.Size = new System.Drawing.Size(266, 283);
			this.grpNeuralNet.TabIndex = 0;
			this.grpNeuralNet.TabStop = false;
			this.grpNeuralNet.Text = "Neural Net";
			// 
			// btnStop
			// 
			this.btnStop.Location = new System.Drawing.Point(149, 240);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(92, 28);
			this.btnStop.TabIndex = 6;
			this.btnStop.Text = "St&op";
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(28, 240);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(92, 28);
			this.btnStart.TabIndex = 6;
			this.btnStart.Text = "&Start";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// lblLineBottom
			// 
			this.lblLineBottom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblLineBottom.Location = new System.Drawing.Point(11, 226);
			this.lblLineBottom.Name = "lblLineBottom";
			this.lblLineBottom.Size = new System.Drawing.Size(249, 2);
			this.lblLineBottom.TabIndex = 0;
			// 
			// txtCurrentError
			// 
			this.txtCurrentError.Location = new System.Drawing.Point(160, 196);
			this.txtCurrentError.MaxLength = 4;
			this.txtCurrentError.Name = "txtCurrentError";
			this.txtCurrentError.ReadOnly = true;
			this.txtCurrentError.Size = new System.Drawing.Size(100, 22);
			this.txtCurrentError.TabIndex = 20;
			this.txtCurrentError.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txtCurrentIteration
			// 
			this.txtCurrentIteration.Location = new System.Drawing.Point(160, 168);
			this.txtCurrentIteration.Name = "txtCurrentIteration";
			this.txtCurrentIteration.ReadOnly = true;
			this.txtCurrentIteration.Size = new System.Drawing.Size(100, 22);
			this.txtCurrentIteration.TabIndex = 20;
			this.txtCurrentIteration.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// lblError
			// 
			this.lblError.AutoSize = true;
			this.lblError.Location = new System.Drawing.Point(8, 199);
			this.lblError.Name = "lblError";
			this.lblError.Size = new System.Drawing.Size(109, 16);
			this.lblError.TabIndex = 0;
			this.lblError.Text = "Current total error";
			// 
			// lblIterations
			// 
			this.lblIterations.AutoSize = true;
			this.lblIterations.Location = new System.Drawing.Point(8, 171);
			this.lblIterations.Name = "lblIterations";
			this.lblIterations.Size = new System.Drawing.Size(91, 16);
			this.lblIterations.TabIndex = 0;
			this.lblIterations.Text = "Current epoch";
			// 
			// lblLineTop
			// 
			this.lblLineTop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblLineTop.Location = new System.Drawing.Point(10, 159);
			this.lblLineTop.Name = "lblLineTop";
			this.lblLineTop.Size = new System.Drawing.Size(249, 2);
			this.lblLineTop.TabIndex = 0;
			// 
			// txtErrorLimit
			// 
			this.txtErrorLimit.Location = new System.Drawing.Point(160, 130);
			this.txtErrorLimit.Name = "txtErrorLimit";
			this.txtErrorLimit.Size = new System.Drawing.Size(100, 22);
			this.txtErrorLimit.TabIndex = 5;
			this.txtErrorLimit.Text = "0.1";
			this.txtErrorLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// comboSigmoidType
			// 
			this.comboSigmoidType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboSigmoidType.FormattingEnabled = true;
			this.comboSigmoidType.Items.AddRange(new object[] {
            "Unipolar",
            "Bipolar"});
			this.comboSigmoidType.Location = new System.Drawing.Point(160, 72);
			this.comboSigmoidType.Name = "comboSigmoidType";
			this.comboSigmoidType.Size = new System.Drawing.Size(100, 24);
			this.comboSigmoidType.TabIndex = 3;
			this.comboSigmoidType.SelectedIndexChanged += new System.EventHandler(this.comboSigmoidType_SelectedIndexChanged);
			// 
			// lblSigmoidType
			// 
			this.lblSigmoidType.AutoSize = true;
			this.lblSigmoidType.Location = new System.Drawing.Point(8, 75);
			this.lblSigmoidType.Name = "lblSigmoidType";
			this.lblSigmoidType.Size = new System.Drawing.Size(87, 16);
			this.lblSigmoidType.TabIndex = 0;
			this.lblSigmoidType.Text = "Sigmoid type";
			// 
			// lblErrorLimit
			// 
			this.lblErrorLimit.AutoSize = true;
			this.lblErrorLimit.Location = new System.Drawing.Point(7, 133);
			this.lblErrorLimit.Name = "lblErrorLimit";
			this.lblErrorLimit.Size = new System.Drawing.Size(63, 16);
			this.lblErrorLimit.TabIndex = 8;
			this.lblErrorLimit.Text = "Error limit";
			// 
			// txtSigmoidAlpha
			// 
			this.txtSigmoidAlpha.Location = new System.Drawing.Point(160, 102);
			this.txtSigmoidAlpha.Name = "txtSigmoidAlpha";
			this.txtSigmoidAlpha.Size = new System.Drawing.Size(100, 22);
			this.txtSigmoidAlpha.TabIndex = 4;
			this.txtSigmoidAlpha.Text = "2";
			this.txtSigmoidAlpha.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// lblSigmoidAlpha
			// 
			this.lblSigmoidAlpha.AutoSize = true;
			this.lblSigmoidAlpha.Location = new System.Drawing.Point(7, 105);
			this.lblSigmoidAlpha.Name = "lblSigmoidAlpha";
			this.lblSigmoidAlpha.Size = new System.Drawing.Size(95, 16);
			this.lblSigmoidAlpha.TabIndex = 4;
			this.lblSigmoidAlpha.Text = "Sigmoid alpha";
			// 
			// txtMomentum
			// 
			this.txtMomentum.Location = new System.Drawing.Point(160, 44);
			this.txtMomentum.Name = "txtMomentum";
			this.txtMomentum.Size = new System.Drawing.Size(100, 22);
			this.txtMomentum.TabIndex = 2;
			this.txtMomentum.Text = "0";
			this.txtMomentum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// lblMomentum
			// 
			this.lblMomentum.AutoSize = true;
			this.lblMomentum.Location = new System.Drawing.Point(8, 47);
			this.lblMomentum.Name = "lblMomentum";
			this.lblMomentum.Size = new System.Drawing.Size(74, 16);
			this.lblMomentum.TabIndex = 0;
			this.lblMomentum.Text = "Momentum";
			// 
			// txtLearningRate
			// 
			this.txtLearningRate.Location = new System.Drawing.Point(160, 16);
			this.txtLearningRate.Name = "txtLearningRate";
			this.txtLearningRate.Size = new System.Drawing.Size(100, 22);
			this.txtLearningRate.TabIndex = 1;
			this.txtLearningRate.Text = "0.1";
			this.txtLearningRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// lblLearningRate
			// 
			this.lblLearningRate.AutoSize = true;
			this.lblLearningRate.Location = new System.Drawing.Point(8, 19);
			this.lblLearningRate.Name = "lblLearningRate";
			this.lblLearningRate.Size = new System.Drawing.Size(86, 16);
			this.lblLearningRate.TabIndex = 0;
			this.lblLearningRate.Text = "Learning rate";
			// 
			// backgroundWorker
			// 
			this.backgroundWorker.WorkerReportsProgress = true;
			this.backgroundWorker.WorkerSupportsCancellation = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(792, 507);
			this.Controls.Add(this.pnlBottom);
			this.Controls.Add(this.pnlTop);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "Character Recognition";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.pnlTop.ResumeLayout(false);
			this.pnlTop.PerformLayout();
			this.grpData.ResumeLayout(false);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.pnlBottom.ResumeLayout(false);
			this.grpError.ResumeLayout(false);
			this.grpInput.ResumeLayout(false);
			this.grpNeuralNet.ResumeLayout(false);
			this.grpNeuralNet.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlTop;
		private System.Windows.Forms.Panel pnlBottom;
		private System.Windows.Forms.GroupBox grpData;
		private System.Windows.Forms.GroupBox grpInput;
		private System.Windows.Forms.GroupBox grpNeuralNet;
		private System.Windows.Forms.GroupBox grpError;
		private System.Windows.Forms.TextBox txtLearningRate;
		private System.Windows.Forms.Label lblLearningRate;
		private System.Windows.Forms.Label lblSigmoidType;
		private System.Windows.Forms.TextBox txtSigmoidAlpha;
		private System.Windows.Forms.Label lblSigmoidAlpha;
		private System.Windows.Forms.TextBox txtMomentum;
		private System.Windows.Forms.Label lblMomentum;
		private System.Windows.Forms.ComboBox comboSigmoidType;
		private System.Windows.Forms.Label lblErrorLimit;
		private System.ComponentModel.BackgroundWorker backgroundWorker;
		private System.Windows.Forms.TextBox txtErrorLimit;
		private System.Windows.Forms.TextBox txtCurrentError;
		private System.Windows.Forms.TextBox txtCurrentIteration;
		private System.Windows.Forms.Label lblError;
		private System.Windows.Forms.Label lblIterations;
		private System.Windows.Forms.Label lblLineTop;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Label lblLineBottom;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem menuFile;
		private System.Windows.Forms.ToolStripMenuItem menuLoad;
		private System.Windows.Forms.ToolStripMenuItem menuSave;
		private System.Windows.Forms.ToolStripMenuItem menuExit;
		private System.Windows.Forms.ToolStripMenuItem menuHelp;
		private System.Windows.Forms.ToolStripMenuItem menuAbout;
		private System.Windows.Forms.ToolStripSeparator menuSeparator;
		private CharacterPanel.CharacterPanel cPanel9;
		private CharacterPanel.CharacterPanel cPanel8;
		private CharacterPanel.CharacterPanel cPanel7;
		private CharacterPanel.CharacterPanel cPanel6;
		private CharacterPanel.CharacterPanel cPanel5;
		private CharacterPanel.CharacterPanel cPanel4;
		private CharacterPanel.CharacterPanel cPanel3;
		private CharacterPanel.CharacterPanel cPanel2;
		private CharacterPanel.CharacterPanel cPanel1;
		private CharacterPanel.CharacterPanel cPanel0;
		private CharacterPanel.CharacterPanel cPanelUser;
		private Led.Led led9;
		private Led.Led led8;
		private Led.Led led7;
		private Led.Led led6;
		private Led.Led led5;
		private Led.Led led4;
		private Led.Led led3;
		private Led.Led led2;
		private Led.Led led1;
		private Led.Led led0;
		private CustomChart.CustomChart chartError;

	}
}

