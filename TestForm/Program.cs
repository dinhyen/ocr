using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TestForm
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			try
			{
				Application.Run(new TestForm());
			}
			catch (Exception e)
			{
				Exception inner = e;
				while (inner != null)
				{
					MessageBox.Show("Message=" + inner.Message
						+ "\nTargetSite=" + inner.TargetSite
						+ "\nSource=" + inner.Source
						+ "\nStack=" + inner.StackTrace
						+ "\nData=" + inner.Data
						);
					inner = inner.InnerException;
				}
			}
		}
	}	// class
}	// namespace