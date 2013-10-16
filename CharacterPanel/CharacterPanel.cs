using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace CharacterPanel
{
	/// <remarks>
	/// Store default colors and other display settings.
	/// </remarks>
	public struct DisplayStyle
	{
		public static Color BackColor = SystemColors.ControlDark;
		public static Color ForeColorOn = Color.Maroon;
		public static Color ForeColorOff = SystemColors.Control;
		public static Color HighlightColor = SystemColors.ControlLightLight;
		public static Color ShadowColor = SystemColors.ControlDarkDark;
	}

	/// <remarks>
	/// User control to display a single character given array for the character.
	/// Control also provides a Test button to allow user to test the neural network
	/// using the corresponding character.  The Test button will generate an event when 
	/// when click; the caller should handle this event to perform the computation.
	/// 
	/// IMPORTANT: The character array (_charArray) refers to the either the
	/// UnipolarInputData or BipolarInputData fields of the MainForm's DataSet.
	/// This link is created when the character panels are updated by the MainForm's 
	/// DisplayCharacters method.  Making changes to either reference will affect the 
	/// underlying values of both.  This "effect" is intentional.  The goal is to allow 
	/// the user to change the training data by drawing on the character display grid.
	/// </remarks>
	public partial class CharacterPanel : UserControl
	{
		#region Fields

		private int _columnCount;	// The number of horizontal tiles
		private int _rowCount;	// The number of vertical tiles
		private Size _pixelsPerTile;	// The number of pixels per side of each tile
		private Size _displaySize;	// Size of character display in pixel
		private Size _tileSize;	// Computed size of each tile
		private Size _offset;	// Offset of display relative to upper left corner
		private double[] _charArray;	// Default is Unipolar

		#endregion

		#region Properties

		/// <summary>
		/// Array of doubles representing the character to display and test.
		/// </summary>
		public double[] CharArray
		{
			get { return _charArray; }
			set { _charArray = value; }
		}
		public bool TestButtonEnabled
		{
			get { return btnTest.Enabled; }
			set { btnTest.Enabled = value; }
		}

		#endregion

		#region Constructor(s)

		// Default constructor required by Windows designer.
		// Create a blank panel.
		public CharacterPanel()
		{
			InitializeComponent();

			Initialize();
		}

		/// <summary>
		/// Create the default blank 4x5 grid.
		/// </summary>
		public void Initialize()
		{
			// Initial grid = 4x5 (all initialized to 0)
			Update(new double[20], 4);
			// Initial grid = 8x8 (all 0's)
			//Update(new double[64], 8);
			// Initial grid = 10x13 (all 0's)
			//Update(new double[130], 10);
		}

		/// <summary>
		/// Perform initialization of data and settings.  Use this method to 
		/// update the data, i.e., the character array and column count, 
		/// or to modify the control, i.e., changing the size and width,
		/// after initial construction with the required default constructor.
		/// </summary>
		/// <param name="charArray">The double array representing the character.</param>
		/// <param name="columnCount">The number of horizontal tiles in the display.</param>
		public void Update(double[] charArray, int columnCount)
		{
			// Set appearance
			this.ResizeRedraw = true;
			this.BackColor = DisplayStyle.BackColor;

			// Initialize data
			this._charArray = charArray;
			this._columnCount = columnCount;

			// Desired offset
			this._offset = new Size(4, 2);

			// Update character display parameters
			UpdateDimensions();

			// Tell control to redraw
			this.Invalidate(false);
		}

		/// <summary>
		/// Update computed dimensions for the display.
		/// </summary>
		private void UpdateDimensions()
		{
			// Adjust the size of the button
			//int newHeight = 22;
			//float fontScale = ((float)newHeight) / btnTest.Height;
			//btnTest.Height = newHeight;
			//Font buttonFont = new Font(btnTest.Font.FontFamily, fontScale * btnTest.Font.SizeInPoints);
			//btnTest.Font = buttonFont;

			// Update button location
			int buttonVerticalPadding = 2;	// Space to leave around top/bottom of button
			btnTest.Location = new Point(
				(ClientSize.Width - btnTest.Width) / 2,
				(ClientSize.Height - btnTest.Height - buttonVerticalPadding)
				);

			int buttonSpace = btnTest.Height + 2 * buttonVerticalPadding;	// Space reserved for Test button
			this._displaySize = new Size(
				ClientSize.Width - 2 * _offset.Width,
				ClientSize.Height - 2 * _offset.Height - buttonSpace	// Account for button
				);
			this._rowCount = _charArray.Length / _columnCount;
			this._pixelsPerTile.Width = _displaySize.Width / _columnCount;
			this._pixelsPerTile.Height = _displaySize.Height / _rowCount;
			this._tileSize = new Size(_pixelsPerTile.Width, _pixelsPerTile.Height);

			// Recalculate offset due to account for truncation errors
			_offset.Width = (ClientSize.Width - _pixelsPerTile.Width * _columnCount) / 2;
			_offset.Height = (ClientSize.Height - _pixelsPerTile.Height * _rowCount - buttonSpace) / 2;
		}

		/// <summary>
		/// ClientSize is automatically reduced to account for the border,
		/// menu, etc. Recompute all dimensions when this happens.
		/// </summary>
		private void CharacterPanel_ClientSizeChanged(object sender, EventArgs e)
		{
			// HACK: Ignore ClientSizeChange event if _charArray is not set.
			// This event is raised during layout before the array is initialized.
			if (_charArray != null)
				UpdateDimensions();
		}

		#endregion

		#region Display

		/// <summary>
		/// Render the user control.  Handle the Paint event.
		/// </summary>
		protected override void OnPaint(PaintEventArgs pea)
		{
			Graphics grfx = (Graphics)pea.Graphics;
			Color tileColor;
			for (int i = 0; i < _charArray.Length; ++i)
			{
				// 1 is on, everything else (0 or -1) is off 
				if (_charArray[i] == 1)
					tileColor = DisplayStyle.ForeColorOn;
				else
					tileColor = DisplayStyle.ForeColorOff;
				DrawTile(grfx, i, tileColor);
			}
		}

		/// <summary>
		/// Draw a tile with the specified size, color and location.
		/// </summary>
		/// <param name="location">The upper left corner of the tile.</param>
		/// <param name="size">The size of the tile.</param>
		private void DrawTile(Graphics grfx, Point location, Size size, Color color)
		{
			Rectangle tile = new Rectangle(location, size);
			Brush brush = new SolidBrush(color);
			grfx.FillRectangle(brush, tile);
			grfx.DrawRectangle(new Pen(DisplayStyle.ShadowColor), tile);

			Brush highlighter = new SolidBrush(DisplayStyle.HighlightColor);
			// Draw highlight
			grfx.FillRectangle(
				highlighter, location.X + 1, location.Y + 1, size.Width - 2, 1
				);
			grfx.FillRectangle(
				highlighter, location.X + 1, location.Y + 1, 1, size.Height - 2
				);
		}

		/// <summary>
		/// Overloaded version.  Takes index into character array and 
		/// compute appropriate location.
		/// </summary>
		/// <param name="index">The tile's index in the character array.</param>
		/// <param name="color">The color of the tile.</param>
		private void DrawTile(Graphics grfx, int index, Color color)
		{
			DrawTile(grfx, IndexToLocation(index), this._tileSize, color);
		}

		#endregion

		#region Custom event

		// Events are handled with delegates
		public delegate void TestEventHandler(object sender, TestEventArgs e);

		// Declare a public event whose type is the TestEventHandler delegate
		public event TestEventHandler TestEvent;

		/// <summary>
		/// Generate the event.  First create TestEventArgs, then 
		/// raise the event, passing TestEventArgs.
		/// </summary>
		/// <param name="charArray">The array of doubles for the character to test.</param>
		public void RaiseEvent(double[] charArray)
		{
			// Create a new event argument
			TestEventArgs tea = new TestEventArgs(charArray);

			// Raise event by invoking the delegate.  Pass in the object that
			// initiates the event (this) as well as the custom argument.
			// The name of the call, FireEvent, is the name of the event.
			// The signature of the call must match that of TestEventHandler.
			TestEvent(this, tea);
		}

		/// <summary>
		/// Handle the Click event of the Test button.  When the Test button is 
		/// click, generate an event, TestEvent, for the whole user control.
		/// </summary>
		private void btnTest_Click(object sender, EventArgs e)
		{
			RaiseEvent(this.CharArray);
		}

		#endregion

		#region Mouse event handlers

		// Track whether the mouse is being dragged (moved with button held down)
		private bool isDragging = false;

		// Store list of indices of tiles clicked on or dragged across
		private List<int> clickedIndexList = null;

		/// <summary>
		/// Handle the MouseDown event. Initiate tracking of mouse drag.
		/// </summary>
		protected override void OnMouseDown(MouseEventArgs mea)
		{
			clickedIndexList = new List<int>();
			isDragging = true;
			ProcessMouseEvent(mea);	// Make sure we handle a single click.
		}

		/// <summary>
		/// Handle the MouseMove event.
		/// </summary>
		protected override void OnMouseMove(MouseEventArgs mea)
		{
			if (!isDragging)
				return;

			// Continue processing as long as mouse is dragged
			ProcessMouseEvent(mea);
		}

		/// <summary>
		/// Handle the MouseUp event.
		/// </summary>
		protected override void OnMouseUp(MouseEventArgs mea)
		{
			if (!isDragging)
				return;

			// Reset when mouse button is released
			isDragging = false;
			clickedIndexList.Clear();
		}

		/// <summary>
		/// Process mouse click and turn on (off) tile if left (right) button is clicked.
		/// </summary>
		/// <param name="mea">The MouseEvent argument.</param>
		private void ProcessMouseEvent(MouseEventArgs mea)
		{
			Point clickedPoint = new Point(mea.X, mea.Y);
			if (IsInsideDisplay(clickedPoint))
			{
				// Convert clicked point into tile index
				int clickedIndex = LocationToIndex(clickedPoint);
				
				// Ensure index is unique (not already clicked)
				// HACK: Inserted check to make sure index is valid
				if (!clickedIndexList.Contains(clickedIndex)
					 && clickedIndex >= 0
					 && clickedIndex < _charArray.Length
					)
				{
					clickedIndexList.Add(clickedIndex);	// Save index so we won't process it again
					if (mea.Button == MouseButtons.Left)	// Left-click turns on
						TurnOnTile(clickedIndex);
					else if (mea.Button == MouseButtons.Right)	// Right-click turns off
						TurnOffTile(clickedIndex);
				}
			}
		}

		#endregion

		#region Utilities

		/// <summary>
		/// Get the location (coordinate of the upper left corner) of the
		/// tile corresponding to the i-th element of the character array.
		/// </summary>
		/// <param name="index">The zero-based index of the character array.</param>
		/// <returns>The computed location.</returns>
		private Point IndexToLocation(int index)
		{
			int x = _offset.Width + (index % _columnCount) * _pixelsPerTile.Width;
			int y = _offset.Height + (index / _columnCount) * _pixelsPerTile.Height;
			return new Point(x, y);
		}

		/// <summary>
		/// Get the zero-based index in the character array 
		/// corresponding to a point on the display.
		/// </summary>
		/// <param name="location">The point on the display</param>
		/// <returns>The computed index.</returns>
		private int LocationToIndex(Point location)
		{
			int numberOfColumns =
				(int)Math.Ceiling(
					(((float)location.X) - _offset.Width) / _pixelsPerTile.Width
				);
			int numberOfRows =
				(int)Math.Floor(
					(((float)location.Y) - _offset.Height) / _pixelsPerTile.Height
				);
			return _columnCount * numberOfRows + numberOfColumns - 1;
		}

		/// <summary>
		/// Update character array and display, if necessary, based on user click.
		/// </summary>
		/// <param name="index">The zero-based index of the tile clicked on.</param>
		private void TurnOnTile(int index)
		{
			if (_charArray[index] != 1d) // If off, turn on
			{
				_charArray[index] = 1d;
				//this.Invalidate();
				DrawTile(CreateGraphics(), index, DisplayStyle.ForeColorOn);
			}
		}
		private void TurnOffTile(int index)
		{
			// HACK: Need better way to determine bipolar data (access parent form?)
			if (Array.IndexOf(_charArray, -1d) != -1)	// Bipolar data
			{
				if (_charArray[index] != -1d)	// If on, turn off
				{
					_charArray[index] = -1d;
					//this.Invalidate();
					DrawTile(CreateGraphics(), index, DisplayStyle.ForeColorOff);
				}
			}
			else   // Unipolar data
			{
				if (_charArray[index] != 0d)	// If on, turn off
				{
					_charArray[index] = 0d;
					//this.Invalidate();
					DrawTile(CreateGraphics(), index, DisplayStyle.ForeColorOff);
				}
			}
		}

		/// <summary>
		/// Determine whether a point is inside the character display area.
		/// </summary>
		/// <param name="Point">A point.</param>
		/// <returns>True inside character display area, false otherwise.</returns>
		private bool IsInsideDisplay(Point point)
		{
			// Subtract Size object from Point object
			// TODO: make sure point.Y doesn't exceed _displaySize.Height
			Point shiftedPoint = point - _offset;
			return (shiftedPoint.X < _displaySize.Width &&
					  shiftedPoint.Y < (_displaySize.Height)
					 );
		}

		#endregion

	}	// class

	/// <remarks>
	/// Custom event argument for TestEvent
	/// </remarks>
	public class TestEventArgs : EventArgs
	{
		private double[] _charArray;

		public TestEventArgs(double[] charArray)
		{
			this._charArray = charArray;
		}

		/// <summary>
		/// The array of doubles representing the character to test. Read-only.
		/// </summary>
		public double[] CharArray
		{
			get { return _charArray; }
		}
	}	// class

}	// namespace