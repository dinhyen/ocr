using System;
using System.Collections.Generic;
using System.Text;

namespace ocr
{
	/// <remarks>
	/// Encapsulates all default values used in the application
	/// </remarks>
	struct DefaultValues
	{
	
		// Neural network parameters
		public const double LEARNING_RATE = 0.5d;
		public const double MOMENTUM = 0d;
		public const double SIGMOID_ALPHA = 0.5d;
		public const double ERROR_LIMIT = 0.01d;	// Learning stops when error is below limit
		public const int MAX_EPOCH_COUNT = 10000;
		
		public enum SigmoidTypes : int { Unipolar = 0, Bipolar = 1 };
		public const SigmoidTypes SIGMOID_TYPE = SigmoidTypes.Unipolar;

		// Data parameters
		public const int INPUT_COUNT = 10;	// Number of input rows in data file

		// UI parameters
		public const int CHART_UPDATE_INTERVAL = 50;	// Number of training epochs between chart updates
	
	}	// struct
}	// namespace
