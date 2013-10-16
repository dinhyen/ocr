using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Led
{
	/// <remarks>
	/// Store default colors and other display settings.
	/// </remarks>
	public struct DisplayStyle
	{
		public static Color BackColor = SystemColors.ControlDark;
		public static Color ForeColorOn = Color.FromArgb(0, 255, 0);
		public static Color ForeColorOff = Color.FromArgb(60, 80, 60);	// Dark green
		public static Color ForeColorInactive = SystemColors.Control;
		public static Color HighlightColor = SystemColors.ControlLightLight;
		public static Color ShadowColor = SystemColors.ControlDarkDark;
	}	// struct

	/// <remarks>
	/// Track the state of the Led (determines the Led's color).
	/// </remarks>
	public enum LedState : int { Inactive = -1, Off = 0, On = 1 };

	/// <remarks>
	/// This user control implements an LED-like display.
	/// Turn this on to indicate the recognized character.
	/// </remarks>
	public partial class Led : UserControl
	{
		private Size _offset;	// Offset from upper left corner
		private Size _displaySize;	// Size of LED face
		private double _dimmerValue;	// Brightness of LED
		private LedState _state;	// On, off, or inactive

		public Led()
		{
			InitializeComponent();
			Initialize();
		}

		public void Initialize()
		{
			_offset = new Size(2, 2);
			_state = LedState.Inactive;
			_dimmerValue = 0.5d;
			ComputeDimensions();
		}

		/// <summary>
		/// Render the user control.  Handle the Paint event.
		/// </summary>
		protected override void OnPaint(PaintEventArgs pea)
		{
			Graphics grfx = (Graphics)pea.Graphics;

			// Determine paint color based on state
			Color color;
			if (_state == LedState.On)
				// Scale color by dimmer value
				color = Color.FromArgb(
					(byte)Math.Floor(DisplayStyle.ForeColorOn.R * _dimmerValue),
					(byte)Math.Floor(DisplayStyle.ForeColorOn.G * _dimmerValue),
					(byte)Math.Floor(DisplayStyle.ForeColorOn.B * _dimmerValue)
				);
			else if (_state == LedState.Off)
				color = DisplayStyle.ForeColorOff;
			else
				color = DisplayStyle.ForeColorInactive;
			Brush brush = new SolidBrush(color);

			// Draw Led face
			Point location = Point.Empty + _offset;	// Upper left corner of Led
			Rectangle rectangle = new Rectangle(location, _displaySize);
			grfx.FillRectangle(brush, rectangle);

			int thickness = 2;	// Thickness in pixels of the rectangle
			// Draw shadow
			Brush shadower = new SolidBrush(DisplayStyle.ShadowColor);
			// Bottom
			grfx.FillRectangle(
				shadower,
				new Rectangle(
					location + new Size(0, _displaySize.Height - thickness),
					new Size(_displaySize.Width, thickness))
				);
			// Right
			grfx.FillRectangle(
				shadower,
				new Rectangle(
					location + new Size(_displaySize.Width - thickness, 0),
					new Size(thickness, _displaySize.Height))
				);
			// Draw highlight
			Brush highlighter = new SolidBrush(DisplayStyle.HighlightColor);
			// Top
			grfx.FillRectangle(
				highlighter,
				new Rectangle(
					location,
					new Size(_displaySize.Width, thickness))
				);
			// Left
			grfx.FillRectangle(
				highlighter,
				new Rectangle(
					location,
					new Size(thickness, _displaySize.Height))
				);
		}

		/// <summary>
		/// Activate the LED with a dimmer value.
		/// </summary>
		/// <param name="dimmerValue">Fraction (max 1) indicating how bright the LED should be</param>
		public void TurnOn(double dimmerValue)
		{
			_state = LedState.On;
			if (dimmerValue > 1.0d)	// Max=1
				_dimmerValue = 1.0d;
			else if (dimmerValue < 0.5)	// Prevent LED from being too dark
				_dimmerValue = 0.5d;
			else
				_dimmerValue = dimmerValue;
			Invalidate();	// Raise Paint event
		}

		/// <summary>
		/// Overloaded version. Activate the LED at maximum brightness.
		/// </summary>
		public void TurnOn()
		{
			TurnOn(1.0d);
		}

		/// <summary>
		/// De-activate the LED
		/// </summary>
		public void TurnOff()
		{
			_state = LedState.Off;
			Invalidate();	// Raise Paint event
		}

		/// <summary>
		/// Put the LED in the inactive state
		/// </summary>
		public void MakeInactive()
		{
			_state = LedState.Inactive;
			Invalidate();
		}

		/// <summary>
		/// ClientSize is automatically reduced to account for the border,
		/// menu, etc. Recompute all dimensions when this happens.
		/// </summary>
		private void Led_ClientSizeChanged(object sender, EventArgs e)
		{
			ComputeDimensions();
		}

		/// <summary>
		/// Compute dimensions for the display.
		/// </summary>
		public void ComputeDimensions()
		{
			_displaySize = ClientSize - (_offset + _offset);
		}

	}	// class

}	// namespace