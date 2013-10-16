using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TestForm
{
	public partial class TestForm : Form
	{
		public TestForm()
		{
			InitializeComponent();
			// Resize character panel
			//characterPanel1.ClientSize = new Size(100, 100);
			characterPanel.Update(
				new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
				4);
			chart1.BackColor = Color.Black;
			chart1.AddDataSeries("output", Color.Maroon, CustomChart.CustomChart.SeriesType.ConnectedDots, 20);
			double[,] output = new double[10, 2];
			for (int i = 0; i < 10; ++i)
			{
				output[i, 0] = i;
				output[i, 1] = i;
			}
			chart1.RangeX = new AForge.DoubleRange(0, 9);
			chart1.UpdateDataSeries("output", output, Color.Green);
		}

		private Led.LedState _ledState = Led.LedState.Inactive;
		private void btnTestLed_Click(object sender, EventArgs e)
		{
			if (_ledState != Led.LedState.On)
			{
				led.TurnOn();
				_ledState = Led.LedState.On;
			}
			else
			{
				led.TurnOff();
				_ledState = Led.LedState.Off;
			}
		}

		/// <summary>
		/// Handles the TestEvent of the user control
		/// </summary>
		private void characterPanel_TestEvent(object sender, CharacterPanel.TestEventArgs e)
		{
			StringBuilder sb = new StringBuilder();
			double[] array = e.CharArray;
			foreach (double value in array)
				sb.Append(value.ToString() + " ");
			MessageBox.Show(sb.ToString());
		}

	}	// class
}	// namespace