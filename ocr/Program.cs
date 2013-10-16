using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ocr
{
	static class Program
	{
		[STAThread]
		public static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Application.Run(new MainForm());
		}

	}	// class
}	// namespace