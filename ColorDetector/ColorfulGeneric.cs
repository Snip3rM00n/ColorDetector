/*
	ColorDetector
	By: Anthony McKeever (GitHub: Snip3rM00n - http://github.com/snip3rm00n)

	App: Designed to work with RGB Sensor from Adafruit and provide color codes and values (RGB, CMYK, HSL, HSV, XYZ) about a color.
	Based on: Microsoft IoT Lesson 205 - "What Color Is It"
	API Used: The Color API by Josh Beckman (http://thecolorapi.com).

	File: ColorfulGeneric.cs
	Coded in: C# (Microsoft .Net 5.0 Framework)
	Namespace: ColorDetector
	Purpose: Provides generic structures for ColorfulRestProperties.  These structs allow the colorful rest properties to quickly 
			 create various data objects for Values (ushort) and Fractions (decimal).  Can also be used with int, double, float, etc.

	Use:
		ColorfulGeneric.RGB<ushort> rgb = new ColorfulGeneric.RGB<ushort>();
*/

namespace ColorDetector
{
	public class ColorfulGeneric
	{
		public struct RGB<T>
		{
			public T Red;
			public T Green;
			public T Blue;
		}

		public struct HSL<T>
		{
			public T H;
			public T S;
			public T L;
		}

		public struct HSV<T>
		{
			public T H;
			public T S;
			public T V;
		}

		public struct CMYK<T>
		{
			public T C;
			public T M;
			public T Y;
			public T K;
		}

		public struct XYZ<T>
		{
			public T X;
			public T Y;
			public T Z;
		}
	}
}