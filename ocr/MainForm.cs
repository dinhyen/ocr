#region includes

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using AForge;
using AForge.Neuro;
using AForge.Neuro.Learning;
using AForge.Controls;

#endregion

namespace ocr
{
	public partial class MainForm : Form
	{

		#region Fields
		private double _learningRate = DefaultValues.LEARNING_RATE;
		private double _momentum = DefaultValues.MOMENTUM;
		private double _sigmoidAlpha = DefaultValues.SIGMOID_ALPHA;
		private double _learningErrorLimit = DefaultValues.ERROR_LIMIT;
		private DefaultValues.SigmoidTypes _sigmoidType = DefaultValues.SIGMOID_TYPE;
		private Data _dataSet = null;	// Encapsulate input and desired outputs
		private ActivationNetwork _network = null;
		private CharacterPanel.CharacterPanel[] _cPanels = null;	// character panels
		private struct LedAssembly
		{
			public static Led.Led[] Leds = null;	// Array of LEDs
			private static int _litIndex = -1;	// Index of activated LED
			/// <summary>
			/// Expose composite property enable/disable entire assembly.
			///    - Disabled: All LEDs in inactive state.
			///    - Enabled:  All LEDs in off state.
			/// </summary>
			public static bool Enabled
			{
				set
				{
					if (value == false)	// Inactive state
						foreach (Led.Led led in Leds)
							led.MakeInactive();
					else   // Off state
						foreach (Led.Led led in Leds)
							led.TurnOff();
				}
			}
			/// <summary>
			/// Activate LED with the specified index to indicated recognized character.
			/// </summary>
			/// <param name="ledIndex">The zero-based index of the LED.</param>
			public static void ActivateLed(int indexToActivate, double dimmerValue)
			{
				if (_litIndex >= 0)	// If a LED previously lit
				{
					if (_litIndex != indexToActivate)	// If new LED 
					{
						Leds[_litIndex].TurnOff();	// Turn-off old LED
						_litIndex = indexToActivate;
					}
					Leds[_litIndex].TurnOn(dimmerValue);	// Turn-on desired LED
				}
				else  // All LEDs previously in inactive state
				{
					// TODO: Setting Enabled not needed if already set in UpdateControls
					//Enabled = true;	// Enter off state first
					_litIndex = indexToActivate;
					Leds[_litIndex].TurnOn(dimmerValue);	// Turn-on desired LED
				}
			}
		}	// struct
		#endregion

		#region Properties
		/// <summary>
		/// Class containing character matrix data and associated operations; read-only.
		/// </summary>
		public Data DataSet
		{
			get { return _dataSet; }
		}
		/// <summary>
		/// The neural network; read-only property
		/// </summary>
		public ActivationNetwork Network
		{
			get { return _network; }
		}
		#endregion

		#region Constructor

		public MainForm()
		{
			// Required for Window Form Designer component
			InitializeComponent();

			// Initialize array of character panels
			_cPanels =
				new CharacterPanel.CharacterPanel[] {
									cPanel0, cPanel1, cPanel2, cPanel3, cPanel4, 
									cPanel5, cPanel6, cPanel7, cPanel8, cPanel9
									};

			// Initialize assembly of LEDs
			LedAssembly.Leds =
				new Led.Led[] {
							led0, led1, led2, led3, led4, led5, led6, led7, led8, led9
			            };

			// Double the size of the user panel
			cPanelUser.ClientSize += cPanelUser.ClientSize;
			cPanelUser.Initialize();

			// Create a new data set
			_dataSet = new Data();

			// Specify handlers for background worker operations
			InitializeWorker();

			// Update values of user-input controls
			UpdateSettings();

			// Initialize control state
			EnableControls(true);

			// Initialize error chart (desired output-recognized output transfer function)
			chartError.AddDataSeries("error", Led.DisplayStyle.ForeColorOff,
											 CustomChart.CustomChart.SeriesType.Dots, 20
											 );
			chartError.RangeX = new AForge.DoubleRange(-0.5, 9.5);	// ~ square grid
			chartError.RangeY = new AForge.DoubleRange(-0.5, 9.5);
		}

		#endregion

		#region Training

		// Perform neural net computation
		private double Train(BackgroundWorker worker, DoWorkEventArgs e)
		{

			// User must load a data file first
			if (_dataSet.IsEmpty)
				return -1d;

			// initialize input and output values
			double[][] input = null;
			double[][] output = null;

			// Unipolar data
			if (_sigmoidType == DefaultValues.SigmoidTypes.Unipolar)
			{
				input = _dataSet.UnipolarInputData;
				output = _dataSet.UnipolarOutputData;
			}
			// Bipolar Data
			else
			{
				input = _dataSet.BipolarInputData;
				output = _dataSet.BipolarOutputData;
			}

			// Create 3-layered perceptron network
			int inputLayerCount = _dataSet.InputCellCount;
			int outputLayerCount = _dataSet.OutputCellCount;
			int hiddenLayerCount = (inputLayerCount + outputLayerCount) / 2;

			_network = new ActivationNetwork(
				(_sigmoidType == DefaultValues.SigmoidTypes.Unipolar) ?
					(IActivationFunction)new SigmoidFunction(_sigmoidAlpha) :
					(IActivationFunction)new BipolarSigmoidFunction(_sigmoidAlpha),
				_dataSet.InputCellCount, new int[] { inputLayerCount, hiddenLayerCount, outputLayerCount });

			// Create teacher
			BackPropagationLearning teacher = new BackPropagationLearning(_network);
			teacher.LearningRate = _learningRate;
			teacher.Momentum = _momentum;

			int epoch = 1;
			double error = 1000d;	// Arbitrarily high alue
			while (!worker.CancellationPending)
			{
				System.Threading.Thread.Sleep(1);	// IMPORTANT: Mandatory for responsive UI
				error = teacher.RunEpoch(input, output);

				// Update current epoch and current error text boxes
				worker.ReportProgress(0, new UiUpdates(epoch, error));

				// Show recognition accuracy at fixed intervals
				if (epoch % DefaultValues.CHART_UPDATE_INTERVAL == 0)
					UpdateChart();

				// Increment count
				epoch++;

				// If error less than limit or process is running too long
				if (error <= _learningErrorLimit || epoch > DefaultValues.MAX_EPOCH_COUNT)
					break;
			}
			if (worker.CancellationPending)
				e.Cancel = true;	// Set flag for RunWorkerCompleted handler to check
			return error;
		}

		/// <summary>
		/// Simulate long-running background operation to test GUI (replaces Train)
		/// </summary>
		private double Dummy(BackgroundWorker worker, DoWorkEventArgs e)
		{
			long sum = 0;
			for (int i = 0; i < 10000000; ++i)
			{
				if (backgroundWorker.CancellationPending)
					break;
				backgroundWorker.ReportProgress(0, new UiUpdates(i, i));
				Thread.Sleep(1);
				sum += i;
			}
			return (double)sum;
		}

		#endregion

		#region Testing

		private double[] Test(double[] testArray)
		{
			double[] outputArray = null;
			if (testArray.Length > 0 && _network != null)
				outputArray = _network.Compute(testArray);
			return outputArray;
		}

		#endregion

		#region Background worker event handlers

		/// <summary>
		/// In worker thread, handle background worker's DoWork event to perform asynchronous operation
		/// </summary>
		private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			e.Result = Train(this.backgroundWorker, e);
		}

		// Struct containing data to update UI from worker thread
		private struct UiUpdates
		{
			public readonly string iterationCount, error;

			public UiUpdates(int iter, double err)
			{
				iterationCount = iter.ToString();
				error = String.Format("{0:F8}", err);	// Display 8 decimal digits
			}
		}

		/// <summary>
		/// In UI thread, handle this event to update UI whenever backgroundWorker reports progress change
		/// </summary>
		private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			System.Diagnostics.Debug.Assert(this.InvokeRequired == false);
			UiUpdates data = (UiUpdates)e.UserState;
			// Show current iteration & error
			SetText(txtCurrentIteration, data.iterationCount);
			SetText(txtCurrentError, data.error);
		}

		/// <summary>
		/// In UI thread, handles the completion of the asynchronous operation
		/// </summary>
		private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			BackgroundWorker worker = (BackgroundWorker)sender;
			if (e.Cancelled)
				Console.WriteLine("Operation canceled.");
			else if (e.Error != null)
				Console.WriteLine("Error encountered: " + e.Error);
			else
				Console.WriteLine("Operation successful with error = " + e.Result);
			EnableControls(true);
		}

		#endregion

		#region Neural net buttons event handlers

		/// <summary>
		/// Handles Click event of the Start button.  Based on Kirillov.
		/// </summary>
		private void btnStart_Click(object sender, EventArgs e)
		{
			// Ensure learning rate is between 0.00001 and 1
			try
			{
				_learningRate = Math.Max(0.00001, Math.Min(1, double.Parse(txtLearningRate.Text)));
			}
			catch
			{
				_learningRate = DefaultValues.LEARNING_RATE;
			}

			// Ensure momentum is between 0 and 0.5
			try
			{
				_momentum = Math.Max(0, Math.Min(0.5, double.Parse(txtMomentum.Text)));
			}
			catch
			{
				_momentum = DefaultValues.MOMENTUM;
			}
			// Ensure sigmoid's alpha value is between 0.01 and 100
			try
			{
				_sigmoidAlpha = Math.Max(0.01, Math.Min(100, double.Parse(txtSigmoidAlpha.Text)));
			}
			catch
			{
				_sigmoidAlpha = DefaultValues.SIGMOID_ALPHA;
			}
			// Ensure error limit is at least 0
			try
			{
				_learningErrorLimit = Math.Max(0, double.Parse(txtErrorLimit.Text));
			}
			catch
			{
				_learningErrorLimit = DefaultValues.ERROR_LIMIT;
			}
			// GetValues return Array of SigmoidTypes values; index into Array with SelectedIndex of combo box to get value
			_sigmoidType =
				(DefaultValues.SigmoidTypes)(Enum.GetValues(typeof(DefaultValues.SigmoidTypes))).GetValue(comboSigmoidType.SelectedIndex);

			//saveStatisticsToFiles = saveFilesCheck.Checked;

			// Update values of controls so that they have only valid values
			UpdateSettings();

			// Disable control prior to run
			EnableControls(false);

			// run asynchronous operation
			backgroundWorker.RunWorkerAsync();
		}

		/// <summary>
		/// Handle click event for the Stop button
		/// </summary>
		private void btnStop_Click(object sender, EventArgs e)
		{
			// Cancel the asynchronous operation
			backgroundWorker.CancelAsync();
		}

		#endregion

		#region Test buttons event handler + associated methods

		/// <summary>
		/// Compute neural net output when user clicks Test button on a character panel.
		/// </summary>
		private void cPanel_TestEvent(object sender, CharacterPanel.TestEventArgs tea)
		{
			double[] testInput = tea.CharArray;	// Get character array of panel

			// Convert the input character array to the appropriate type
			if (_sigmoidType == DefaultValues.SigmoidTypes.Unipolar)
				testInput = Data.BipolarToUnipolar(testInput);
			else   // Current setting is bipolar
				testInput = Data.UnipolarToBipolar(testInput);

			double[] testOutput = Test(testInput);	// Compute neural net output
			if (testOutput != null)
			{
				RecognitionOutput recognitionOutput = Recognize(testOutput);

				// Activate corresponding LED, set dimmer value to value of confidence
				LedAssembly.ActivateLed(recognitionOutput.CharacterIndex,
												recognitionOutput.Confidence);
			}
			else
			{
				MessageBox.Show("Empty output", "Recognition");
			}
		}

		/// <remarks>
		/// Store the index of the recognized character and the recognition confidence.
		/// </remarks>
		private struct RecognitionOutput
		{
			public int CharacterIndex;
			public double Confidence;
			public RecognitionOutput(int index, double confidence)
			{
				CharacterIndex = index;
				Confidence = confidence;
			}
		}

		/// <summary>
		/// Return the index of the recognized character and the recognition confidence.
		/// </summary>
		/// <param name="array">The output array of the neural net.</param>
		/// <returns>A struct containing the recognized index and confidence.</returns>
		private RecognitionOutput Recognize(double[] array)
		{
			double[][] trueOutput;
			if (_sigmoidType == DefaultValues.SigmoidTypes.Unipolar)
				trueOutput = _dataSet.UnipolarOutputData;
			else
				trueOutput = _dataSet.BipolarOutputData;

			int testIndex = IndexOfMaxValue(array);
			// Confidence is simply neural net output; it should be less than or equal to 1.
			double confidence = array[testIndex];

			for (int cIndex = 0; cIndex < trueOutput.Length; ++cIndex)
				if (testIndex == IndexOfMaxValue(trueOutput[cIndex]))	// if match
					return new RecognitionOutput(cIndex, confidence);
			return new RecognitionOutput(-1, -1d);
		}

		/// <summary>
		/// Find the index of the character whose output most closely matches
		/// an array. This index indicates the recognized character.
		/// </summary>
		/// <param name="array">The output array of the neural net.</param>
		/// <returns>The zero-based index of the recognized character.</returns>
		private int IndexOfRecognizedCharacter(double[] array)
		{
			double[][] trueOutput;
			if (_sigmoidType == DefaultValues.SigmoidTypes.Unipolar)
				trueOutput = _dataSet.UnipolarOutputData;
			else
				trueOutput = _dataSet.BipolarOutputData;

			int testIndex = IndexOfMaxValue(array);
			for (int cIndex = 0; cIndex < trueOutput.Length; ++cIndex)
				if (testIndex == IndexOfMaxValue(trueOutput[cIndex]))	// if match
					return cIndex;
			return -1;
		}

		/// <summary>
		/// Find the index of the maximum (most positive) value in an array
		/// </summary>
		/// <param name="array">An array.</param>
		/// <returns>The zero-based index of the maximum value.</returns>
		private int IndexOfMaxValue(double[] array)
		{
			if (array.Length < 1)
				return -1;
			int maxIndex = 0;
			for (int index = 1; index < array.Length; ++index)
				if (array[index] > array[maxIndex])
					maxIndex = index;
			return maxIndex;
		}

		#endregion

		#region Menu event handlers

		/// <summary>
		/// Handle click event when user selects Load data under the File menu
		/// </summary>
		private void menuLoad_Click(object sender, EventArgs e)
		{
			_dataSet.LoadFromFile();
			// Bind data to character panels
			DisplayCharacters();
			// Data is loaded, so enable appropriate controls
			EnableControls(true);
		}

		/// <summary>
		/// Handle click event when user selects Save data under the File menu
		/// </summary>
		private void menuSave_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Save data is not yet implemented", "Save data");
		}

		/// <summary>
		/// Handle click event when user selects Exit under the File menu
		/// </summary>
		private void menuExit_Click(object sender, EventArgs e)
		{
			this.Close();	// Generate the FormClosing event (handled by MainForm_FormClosing)
		}

		/// <summary>
		/// Handle click event when user selects About under the Help menu
		/// </summary>
		private void menuAbout_Click(object sender, EventArgs e)
		{
      MessageBox.Show("Automatic Character Recognition using Neural Net\n\nBy Dinh-Yen Tran, Summer 2007", "About OCR");
		}

		#endregion

		#region Form event handlers

		/// <summary>
		/// Shuts down gracefully when user closes form (possibly in the middle of a run)
		/// </summary>
		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			// If worker thread is running, set cancellation flag
			if (backgroundWorker.IsBusy)
				backgroundWorker.CancelAsync();
			while (backgroundWorker.IsBusy)
				Application.DoEvents();	// Keep UI messages moving, so the form remains responsive
		}

		#endregion

		#region Combobox event handler
		/// <summary>
		/// HACK: Ensure that unipolar and bipolar version of dataset is consistent.
		/// Ideally this should be handled by the Data class, but there's currently
		/// no way to detect when each version changes (when user draws on the 
		/// character panels).
		/// </summary>
		private void comboSigmoidType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!_dataSet.IsEmpty)	// Only do this when there is data!
			{
				ComboBox comboBox = (ComboBox)sender;
				// If Bipolar selected, update Bipolar data to be consistent with Unipolar
				if (comboBox.SelectedIndex == (int)DefaultValues.SigmoidTypes.Bipolar)
				{
					_dataSet.UpdateBipolar();
				}
				// If Unipolar selected, update Unipolar data to be consiste with Bipolar
				else
				{
					_dataSet.UpdateUnipolar();
				}
				// Re-bind data to character panels
				// TODO: It'd be nice to have databinding capability to do this automatically
				DisplayCharacters();
			}
		}
		#endregion

		#region Update chart

		/// <summary>
		/// Compute and display desired output-recognized output transfer function;
		/// i.e., mapping of recognized characters to the actual characters.  
		/// This is an indication of the recognition accuracy of the neural net.
		/// </summary>
		public void UpdateChart()
		{
			double[][] inputData;
			if (_sigmoidType == DefaultValues.SigmoidTypes.Unipolar)
				inputData = _dataSet.UnipolarInputData;
			else
				inputData = _dataSet.BipolarInputData;

			double[] testOutput;
			double[,] chartData = new double[10, 2];
			RecognitionOutput recognitionOutput;
			double minConfidence = 0d;	// Minimum confidence of all recognition output

			for (int i = 0; i < inputData.Length; ++i)	// For each character, 0 to 9
			{
				testOutput = Test(inputData[i]);	// Recognize input
				if (testOutput != null)
				{
					recognitionOutput = Recognize(testOutput);
					chartData[i, 0] = i;	// x-coordinate
					chartData[i, 1] = recognitionOutput.CharacterIndex; // y-coordinate 
					if (minConfidence < recognitionOutput.Confidence)
						minConfidence = recognitionOutput.Confidence;
				}
			}

			Console.Write(minConfidence + " ");
			Color color = Color.FromArgb(
					(byte)Math.Floor(Led.DisplayStyle.ForeColorOn.R * minConfidence),
					(byte)Math.Floor(Led.DisplayStyle.ForeColorOn.G * minConfidence),
					(byte)Math.Floor(Led.DisplayStyle.ForeColorOn.B * minConfidence)
					);

			chartError.UpdateDataSeries("error", chartData, color);
		}

		#endregion

		#region Utilities

		/// <summary>
		/// Attach handlers to background worker 
		/// </summary>
		private void InitializeWorker()
		{
			this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
			this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
			this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
		}

		/// <summary>
		/// Update a control's Text field.  Based on Kirillov.
		/// </summary>
		/// <param name="control">The control to update.</param>
		/// <param name="text">The new value of the Text field.</param>
		private void SetText(System.Windows.Forms.Control control, string text)
		{
			System.Diagnostics.Debug.Assert(this.InvokeRequired == false);
			control.Text = text;
			//control.Refresh();	// force refresh to update display (no longer necessary with Thread.Sleep)
		}

		/// <summary>
		/// Update controls with valid values (the user may have entered invalid values)
		/// Based on Kirillov.
		/// </summary>
		private void UpdateSettings()
		{
			txtLearningRate.Text = _learningRate.ToString();
			txtMomentum.Text = _momentum.ToString();
			txtSigmoidAlpha.Text = _sigmoidAlpha.ToString();
			txtErrorLimit.Text = _learningErrorLimit.ToString();
			comboSigmoidType.SelectedIndex = (int)_sigmoidType;	// must cast enum value to int
			//saveFilesCheck.Checked = saveStatisticsToFiles;
		}

		/// <summary>
		/// Enable controls depending on whether application is idle or training.
		/// </summary>
		/// <param name="IsIdle">Whether the application is idle.</param>
		private void EnableControls(bool isIdle)
		{
			// Neural network parameters and menu
			if (_dataSet.IsEmpty)
			{
				// Disable buttons, menu, panels and LEDs if no data loaded
				btnStart.Enabled = false;
				btnStop.Enabled = false;
				menuStrip.Enabled = true;
				foreach (CharacterPanel.CharacterPanel cPanel in _cPanels)
					cPanel.Enabled = false;
				cPanelUser.Enabled = false;
				LedAssembly.Enabled = false;
			}
			else
			{
				// Toggle buttons, menu and panels between training and idling
				btnStart.Enabled = isIdle;
				menuStrip.Enabled = btnStart.Enabled;
				btnStop.Enabled = !btnStart.Enabled;
				// When idling, allow user to modify character panels (hence training data) 
				foreach (CharacterPanel.CharacterPanel cPanel in _cPanels)
					cPanel.Enabled = btnStart.Enabled;

			}
			txtLearningRate.Enabled = btnStart.Enabled;
			txtMomentum.Enabled = btnStart.Enabled;
			txtSigmoidAlpha.Enabled = btnStart.Enabled;
			txtErrorLimit.Enabled = btnStart.Enabled;
			comboSigmoidType.Enabled = btnStart.Enabled;

			// Test buttons
			if (_network == null)
			{
				// Disable ALL test buttons & LEDs if neural network is NOT trained
				foreach (CharacterPanel.CharacterPanel cPanel in _cPanels)
					cPanel.TestButtonEnabled = false;
				cPanelUser.Enabled = false;
				LedAssembly.Enabled = false;
			}
			else
			{
				// Toggle ALL test buttons between training and idling
				foreach (CharacterPanel.CharacterPanel cPanel in _cPanels)
					cPanel.TestButtonEnabled = btnStart.Enabled;
				cPanelUser.Enabled = btnStart.Enabled;
				LedAssembly.Enabled = btnStart.Enabled;
			}
		}

		/// <summary>
		/// Bind data to character panels.
		/// </summary>
		private void DisplayCharacters()
		{
			if (!_dataSet.IsEmpty)
			{
				// HACK: Store column count somewhere instead of computing it
				// Set column count (number of horizontal tile) in character panel
				int columnCount = 4;	// for 20-cell character
				if (_dataSet.InputCellCount == 64)	// 64-cell character
					columnCount = 8;
				else if (_dataSet.InputCellCount == 130)
					columnCount = 10;

				double[][] inputData;
				if (_sigmoidType == DefaultValues.SigmoidTypes.Unipolar)
					inputData = _dataSet.UnipolarInputData;
				else
					inputData = _dataSet.BipolarInputData;
				for (int i = 0; i < _cPanels.Length; ++i)
					_cPanels[i].Update(inputData[i], columnCount);
				
				// Initialize the user character panel with all 0's
				cPanelUser.Update(new double[_dataSet.InputCellCount], columnCount);
			}
		}

		/// <summary>
		/// User should draw on the character panels, then run this method to
		/// generate character arrays to populate the data file, so that 
		/// the data file doesn't hhave to be manually generated.
		/// </summary>
		private void GenerateDataFile()
		{
			Data data = new Data();
			double[][] inputData =
				new double[10][] {
					cPanel0.CharArray, cPanel1.CharArray, cPanel2.CharArray,
					cPanel3.CharArray, cPanel4.CharArray, cPanel5.CharArray,
					cPanel6.CharArray, cPanel7.CharArray, cPanel8.CharArray,
					cPanel9.CharArray
				};

			// Write to text file
			try
			{
				using (StreamWriter dataFile =
					File.CreateText(System.Environment.CurrentDirectory + @"\inputdata.csv"))
				{
					for (int i = 0; i < inputData.Length; ++i)
					{
						for (int j = 0; j < inputData[i].Length; ++j)
							dataFile.Write("{0,1},", inputData[i][j]);
						dataFile.WriteLine();
					}
				}
			}
			catch (IOException)
			{
				MessageBox.Show("Failed writing file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		#endregion

	}	// class
}	// namespace