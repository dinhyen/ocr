using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ocr
{
	/// <remarks>
	/// This class encapsulates the data used for training.  The data
	/// includes the set of input data and the corresponding set of
	/// desired output data.  Both unipolar and bipolar versions are 
	/// stored separately.  The caller is responsible for determining 
	/// which version to use depending on the application's settings.
	/// 
	/// IMPORTANT: If the data is linked to another control, such as
	/// a character panel, by directly passing a reference to a field
	/// (such as _unipolarInputData), changes to the control's reference
	/// will affect the content of the field.  This effect enables the
	/// user to make changes in the data by changing the control. See
	/// also the CharacterPanel class.  
	/// 
	/// A side-effect is that we must store the unipolar and bipolar 
	/// versions separately, instead of returning a converted copy on 
	/// the fly.  This is so that the updates made by the linked control 
	/// can be retained instead of overwritten by the conversion.  This
	/// also means less efficiency since we have to ensure consistency
	/// between the two versions (important, for example, when the user
	/// switches the sigmoid type and re-train on the same data).
	/// TODO: Implement consistency checking between unipolar & bipolar 
	/// versions (update button?).
	/// </remarks>
	public partial class Data : Form
	{

		#region Fields
		// Fields
		private int _inputCount = 0;	// Number of different inputs (characters)
		private int _outputCount = 0;
		private int _inputCellCount = 0;
		private int _outputCellCount = 0;

		private double[][] _unipolarInputData = null;
		private double[][] _unipolarOutputData = null;
		private double[][] _bipolarInputData = null;
		private double[][] _bipolarOutputData = null;

		#endregion

		#region Properties

		// The number of inputs.  Each input represents a character.
		public int InputCount
		{
			get { return _inputCount; }
			set
			{
				_inputCount = value;
				InitializeData();	// Update data when this property changes
			}
		}
		// The number of outputs.  Each output is the true output for a given input.
		public int OutputCount
		{
			get { return _outputCount; }
			set
			{
				_outputCount = value;
				InitializeData(); // Update data when this property changes
			}
		}
		// The number of elements per input (i.e., the number of cells per character)
		public int InputCellCount
		{
			get { return _inputCellCount; }
		}
		// The number of elements per output
		public int OutputCellCount
		{
			get { return _outputCellCount; }
		}
		
		// Whether the data set is empty
		public bool IsEmpty
		{
			get { return (_inputCellCount == 0); }
		}

		// Unipolar Input matrix; read-only property
		public double[][] UnipolarInputData
		{
			get { return _unipolarInputData; }
		}
		// Unipolar Output matrix; read-only property
		public double[][] UnipolarOutputData
		{
			get { return _unipolarOutputData; }
		}

		// Bipolar Input matrix; read-only property
		// Same as unipolar matrix, with 0 replaced by -1
		public double[][] BipolarInputData
		{
			get { return _bipolarInputData; }
		}
		// Unipolar Output matrix; read-only property
		// Same as unipolar matrix, with 0 replaced by -1
		public double[][] BipolarOutputData
		{
			get { return _bipolarOutputData; }
		}

		#endregion

		// Constructor
		public Data()
		{
			InitializeComponent();
			InitializeData();
		}

		/// <summary>
		/// Initialize the data matrices (aka jagged arrays)
		/// </summary>
		public void InitializeData()
		{
			_outputCount = _inputCount = DefaultValues.INPUT_COUNT;
			// Data file stores the unipolar version only, so initialize 
			// unipolar data to prepare.  Bipolar data can be created later.
			_unipolarInputData = new double[_inputCount][];
			_unipolarOutputData = new double[_outputCount][];
		}

		/// <summary>
		/// Allow the user to select a file and load its content into the data arrays.
		/// The data file is assumed to contain (InputCount) lines of input data, 
		/// followed by (OutputCount) lines of output data.  Each line represent one 
		/// character.  Comments begin with % and should be ignored.
		/// </summary>
		public void LoadFromFile()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();

			// Set file dialog properties
			openFileDialog.InitialDirectory = System.Environment.CurrentDirectory;
			openFileDialog.Multiselect = false;	// disable multiple file selection
			openFileDialog.Filter = "Data files (*.csv)|*.csv|All files (*.*)|*.*";
			openFileDialog.FilterIndex = 0;	// default file filter
			openFileDialog.RestoreDirectory = true;	// keep last directory 

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					// The using statement ensures that the StreamReader is closed
					using (StreamReader sr = new StreamReader(openFileDialog.FileName))
					{
						string line;
						int i = 0;
						// Read the specified number of input lines while skipping comments
						while (i < UnipolarInputData.Length)
						{
							if ((line = sr.ReadLine()) != null)
							{
								if (line.StartsWith("%"))	// Skip comments
									continue;
								UnipolarInputData[i++] = ParseCsvString(line);
							}
						}
						int j = 0;
						// Read the specified number of output lines
						while (j < UnipolarOutputData.Length)
						{
							if ((line = sr.ReadLine()) != null)
							{
								if (line.StartsWith("%"))	// Skip comments
									continue;
								UnipolarOutputData[j++] = ParseCsvString(line);
							}
						}
					}

					// Update the input and output cell counts to the number of elements read 
					_inputCellCount = _unipolarInputData[0].Length;
					_outputCellCount = _unipolarOutputData[0].Length;

					// Create and update the bipolar version.
					UpdateBipolar();
				}
				catch (Exception ex)
				{
					MessageBox.Show("Error reading or processing file :\n" + ex.Message, 
										 "Load File", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
		}

		/// <summary>
		/// Converts a string containing comma-separated values into a double array.
		/// </summary>
		/// <param name="line">The string of comma-separated values.</param>
		/// <returns>The array of double values.</returns>
		private double[] ParseCsvString(string line)
		{
			double[] array;
			string[] values = line.Split(',');	// Commas separate values
			array = new double[values.Length];	// Allocate a new row
			for (int i = 0; i < values.Length; ++i)
				array[i] = Double.Parse(values[i]);
			return array;
		}

		/// <summary>
		/// Convert the unipolar values of a matrix/jagged array to bipolar 
		/// values.  A bipolar matrix contains -1 in place of 0.
		/// </summary>
		/// <param name="input">The matrix, of either type, to be converted.</param>
		/// <returns>The bipolar matrix.</returns>
		public static double[][] UnipolarToBipolar(double[][] input)
		{
			// Copy to a new array, converting at the same time
			double[][] output = new double[input.Length][];
			for (int row = 0; row < input.Length; ++row)
			{
				output[row] = new double[input[row].Length];	// Add new row
				for (int col = 0; col < input[row].Length; ++col)
					if (input[row][col] == 0d)
						output[row][col] = -1d;
					else
						output[row][col] = input[row][col];
			}
			return output;
		}
		/// <summary>
		/// Overloaded version takes a 1-dimensional array.
		/// </summary>
		/// <param name="input">The array, of either type, to be converted.</param>
		/// <returns>The bipolar array.</returns>
		public static double[] UnipolarToBipolar(double[] input)
		{
			double[][] input2 = new double[1][] { input };
			double[][] output2 = UnipolarToBipolar(input2);
			return output2[0];
		}

		/// <summary>
		/// Convert the bipolar values of a matrix/jagged array to unipolar
		/// values.  A unipolar matrix contains 0 in place of 1.
		/// </summary>
		/// <param name="input">The matrix, of either type, to be converted.</param>
		/// <returns>The unipolar matrix.</returns>
		public static double[][] BipolarToUnipolar(double[][] input)
		{
			// Copy to a new array, converting at the same time
			double[][] output = new double[input.Length][];
			for (int row = 0; row < input.Length; ++row)
			{
				output[row] = new double[input[row].Length];	// Add new row
				for (int col = 0; col < input[row].Length; ++col)
					if (input[row][col] == -1d)
						output[row][col] = 0d;
					else
						output[row][col] = input[row][col];
			}
			return output;
		}
		/// <summary>
		/// Overloaded version takes a 1-dimensional array.
		/// </summary>
		/// <param name="input">The array, of either type, to be converted.</param>
		/// <returns>The unipolar array.</returns>
		public static double[] BipolarToUnipolar(double[] input)
		{
			double[][] input2 = new double[1][] { input };
			double[][] output2 = BipolarToUnipolar(input2);
			return output2[0];
		}

		/// <summary>
		/// Update the unipolar data to be consistent with the bipolar data.
		/// </summary>
		public void UpdateUnipolar()
		{
			_unipolarInputData = BipolarToUnipolar(_bipolarInputData);
			_unipolarOutputData = BipolarToUnipolar(_bipolarOutputData);
		}

		/// <summary>
		/// Update the bipolar data to be consistent with the unipolar version.
		/// </summary>
		public void UpdateBipolar()
		{
			_bipolarInputData = UnipolarToBipolar(_unipolarInputData);
			_bipolarOutputData = UnipolarToBipolar(_unipolarOutputData); 
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			string[] labels =
				new string[] { "Unipolar input:", "Unipolar output:",
									"Bipolar input:", "Bipolar output:"
				};
			double[][][] properties =
				new double[][][] { UnipolarInputData, UnipolarOutputData,
										 BipolarInputData, BipolarOutputData
				};

			for (int i = 0; i < properties.Length; ++i)
			{
				sb.Append(labels[i] + "\n");
				if (properties[i] != null)
				{
					for (int row = 0; row < properties[i].Length; ++row)
					{
						for (int col = 0; col < properties[i][row].Length; ++col)
							sb.Append(String.Format("{0,2} ", properties[i][row][col]));
						sb.Append("\n");
					}
				}
			}
			return sb.ToString();
		}

	}	// class
}	// namespace