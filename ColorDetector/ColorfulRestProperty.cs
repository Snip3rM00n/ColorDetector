/*
	ColorDetector
	By: Anthony McKeever (GitHub: Snip3rM00n - http://github.com/snip3rm00n)

	App: Designed to work with RGB Sensor from Adafruit and provide color codes and values (RGB, CMYK, HSL, HSV, XYZ) about a color.
	Based on: Microsoft IoT Lesson 205 - "What Color Is It"
	API Used: The Color API by Josh Beckman (http://thecolorapi.com).

	File: ColorfulRestProperty.cs
	Coded in: C# (Microsoft .Net 5.0 Framework)
	Namespace: ColorDetector
	Purpose: Creates an object that represents all data from The Color API.  This object can be created as empty (ColorfulRestProperty())
			 or from a ColorfulJsonParser object (ColorfulRestProperty(json)).

	Use:
		Creating Empty:
			ColorfulRestProperty color = new ColorfulRestProperty();
			//	DO STUFF

		Creating from Object:
			DataContractJsonSerializer jSonSerializer = new DataContractJsonSerializer(typeof(ColorfulJsonParser));
			object json = jSonSerializer.ReadObject(stream);

			ColorfulRestProperty color = new ColorfulRestProperty(json);
			//	DO STUFF
*/

using System;

namespace ColorDetector
{
	public class ColorfulRestProperty
	{
		#region Color Identity Constructors

		public struct hexvalue
		{
			string Value;
			string CleanValue;

			public hexvalue(string value, string cvalue)
			{
				Value = value;
				CleanValue = cvalue;
			}

			public hexvalue(object jsonData)
			{
				var hexData = (hexData)jsonData;
				Value = hexData.value;
				CleanValue = hexData.clean;
			}
		}

		public struct rgb
		{
			public ColorfulGeneric.RGB<ushort> Values;
			public ColorfulGeneric.RGB<decimal> Fraction;

			public override string ToString()
			{
				return string.Format("rgb({0},{1},{2})", Values.Red, Values.Green, Values.Blue);
			}

			public string ToApiUrl()
			{
				string apiUrl = string.Format(ApiUrl, "rgb={0}");
				return string.Format(apiUrl, ToString());
			}

			public rgb(object jsonData)
			{
				var _rgbData = (rgbData)jsonData;

				Values = new ColorfulGeneric.RGB<ushort>();
				Values.Red = _rgbData.r;
				Values.Green = _rgbData.g;
				Values.Blue = _rgbData.b;

				Fraction = new ColorfulGeneric.RGB<decimal>();
				Fraction.Red = _rgbData.Fraction.r;
				Fraction.Green = _rgbData.Fraction.g;
				Fraction.Blue = _rgbData.Fraction.b;
			}

			public rgb(ushort[] values, decimal[] fraction)
			{
				Values = new ColorfulGeneric.RGB<ushort>();
				Fraction = new ColorfulGeneric.RGB<decimal>();

				Values.Red = values[0];
				Values.Green = values[1];
				Values.Blue = values[2];

				Fraction.Red = fraction[0];
				Fraction.Green = fraction[1];
				Fraction.Blue = fraction[2];
			}
		}

		public struct hsl
		{
			public ColorfulGeneric.HSL<ushort> Values;
			public ColorfulGeneric.HSL<decimal> Fraction;

			public override string ToString()
			{
				return string.Format("hsl({0},{1}%,{2}%)", Values.H, Values.S, Values.L);
			}

			public string ToApiUrl()
			{
				string apiUrl = string.Format(ApiUrl, "hsl={0}");
				return string.Format(apiUrl, ToString());
			}

			public hsl(object jsonData)
			{
				var _hslData = (hslData)jsonData;

				Values = new ColorfulGeneric.HSL<ushort>();
				Values.H = _hslData.h;
				Values.S = _hslData.s;
				Values.L = _hslData.l;

				Fraction = new ColorfulGeneric.HSL<decimal>();
				Fraction.H = _hslData.fraction.h;
				Fraction.S = _hslData.fraction.s;
				Fraction.L = _hslData.fraction.l;
			}

			public hsl(ushort[] values, decimal[] fraction)
			{
				Values = new ColorfulGeneric.HSL<ushort>();
				Fraction = new ColorfulGeneric.HSL<decimal>();

				Values.H = values[0];
				Values.S = values[1];
				Values.L = values[2];

				Fraction.H = fraction[0];
				Fraction.S = fraction[1];
				Fraction.L = fraction[2];
			}
		}

		public struct hsv
		{
			public ColorfulGeneric.HSV<ushort> Values;
			public ColorfulGeneric.HSV<decimal> Fraction;

			public override string ToString()
			{
				return string.Format("hsv({0},{1}%,{2}%)", Values.H, Values.S, Values.V);
			}

			public string ToApiUrl()
			{
				string apiUrl = string.Format(ApiUrl, "hsv={0}");
				return string.Format(apiUrl, ToString());
			}

			public hsv(object jsonData)
			{
				var _hsvData = (hsvData)jsonData;

				Values = new ColorfulGeneric.HSV<ushort>();
				Values.H = _hsvData.h;
				Values.S = _hsvData.s;
				Values.V = _hsvData.v;

				Fraction = new ColorfulGeneric.HSV<decimal>();
				Fraction.H = _hsvData.fraction.h;
				Fraction.S = _hsvData.fraction.s;
				Fraction.V = _hsvData.fraction.v;
			}

			public hsv(ushort[] values, decimal[] fraction)
			{
				Values = new ColorfulGeneric.HSV<ushort>();
				Values.H = values[0];
				Values.S = values[1];
				Values.V = values[2];

				Fraction = new ColorfulGeneric.HSV<decimal>();
				Fraction.H = fraction[0];
				Fraction.S = fraction[1];
				Fraction.V = fraction[2];
			}

		}

		public struct cmyk
		{
			public ColorfulGeneric.CMYK<ushort> Values;
			public ColorfulGeneric.CMYK<decimal> Fraction;

			public override string ToString()
			{
				return string.Format("cmyk({0},{1},{2},{3})", Values.C, Values.M, Values.Y, Values.K);
			}

			public string ToApiUrl()
			{
				string apiUrl = string.Format(ApiUrl, "cmyk={0}");
				return string.Format(apiUrl, ToString());
			}

			public cmyk(object jsonData)
			{
				var _cmykData = (cmykData)jsonData;

				Values = new ColorfulGeneric.CMYK<ushort>();
				Values.C = _cmykData.c;
				Values.M = _cmykData.m;
				Values.Y = _cmykData.y;
				Values.K = _cmykData.k;

				Fraction = new ColorfulGeneric.CMYK<decimal>();
				Fraction.C = _cmykData.fraction.c;
				Fraction.M = _cmykData.fraction.m;
				Fraction.Y = _cmykData.fraction.y;
				Fraction.K = _cmykData.fraction.k;
			}

			public cmyk(ushort[] values, decimal[] fraction)
			{
				Values = new ColorfulGeneric.CMYK<ushort>();
				Values.C = values[0];
				Values.M = values[1];
				Values.Y = values[2];
				Values.K = values[3];

				Fraction = new ColorfulGeneric.CMYK<decimal>();
				Fraction.C = fraction[0];
				Fraction.M = fraction[1];
				Fraction.Y = fraction[2];
				Fraction.K = fraction[3];
			}
		}

		public struct xyz
		{
			public ColorfulGeneric.XYZ<ushort> Values;
			public ColorfulGeneric.XYZ<decimal> Fraction;

			public override string ToString()
			{
				return string.Format("XYZ({0},{1},{2})", Values.X, Values.Y, Values.Z);
			}

			[Obsolete("The Color API currently does not support retrieving color with XYZ via direct URL, use a Json payload to query API instead.", true)]
			public string ToApiUrl()
			{
				string apiUrl = string.Format(ApiUrl, "xyz={0}");
				return string.Format(apiUrl, ToString());
			}

			public xyz(object jsonData)
			{
				var _xyzData = (xyzData)jsonData;

				Values = new ColorfulGeneric.XYZ<ushort>();
				Values.X = _xyzData.x;
				Values.Y = _xyzData.y;
				Values.Z = _xyzData.z;

				Fraction = new ColorfulGeneric.XYZ<decimal>();
				Fraction.X = _xyzData.fraction.x;
				Fraction.Y = _xyzData.fraction.y;
				Fraction.Z = _xyzData.fraction.z;
			}

			public xyz(ushort[] values, decimal[] decimals)
			{
				Values = new ColorfulGeneric.XYZ<ushort>();
				Values.X = values[0];
				Values.Y = values[1];
				Values.Z = values[2];

				Fraction = new ColorfulGeneric.XYZ<decimal>();
				Fraction.X = decimals[0];
				Fraction.Y = decimals[1];
				Fraction.Z = decimals[2];
			}
		}

		public struct name
		{
			public string Value;
			public string ClosestNamedHex;
			public bool ExactMatchName;
			public int Distance;

			public name(object jsonData)
			{
				var _nameData = (nameData)jsonData;

				Value = _nameData.value;
				ClosestNamedHex = _nameData.closestNamedHex;
				ExactMatchName = _nameData.exactNameMatch;
				Distance = _nameData.distance;
			}

			public name(string value, string closestHex, bool exactMatch, int dist)
			{
				Value = value;
				ClosestNamedHex = closestHex;
				ExactMatchName = exactMatch;
				Distance = dist;
			}
		}

		public struct image
		{
			public string Bare;
			public string Named;

			public image(object jsonData)
			{
				var _imageData = (imageData)jsonData;
				Bare = _imageData.Bare;
				Named = _imageData.Named;
			}

			public image(string bare, string named)
			{
				Bare = bare;
				Named = named;
			}
		}

		#endregion

		#region Instance Variables

		private const string ApiUrl = "http://www.thecolorapi.com/id?{0}";

		public hexvalue HexValue;
		public rgb RGB;
		public hsl HSL;
		public hsv HSV;
		public cmyk CMYK;
		public xyz XYZ;
		public name Name;
		public image Image;
		public string Contrast;

		#endregion

		#region Instantiate ColorfulRestProperty

		public ColorfulRestProperty()
		{
			initEmpty();
		}

		public ColorfulRestProperty(object json)
		{
			var colorData = (ColorfulJsonParser)json;

			HexValue = new hexvalue(colorData.Hex);
			RGB = new rgb(colorData.RGB);
			HSL = new hsl(colorData.HSL);
			HSV = new hsv(colorData.HSV);
			CMYK = new cmyk(colorData.CMYK);
			XYZ = new xyz(colorData.XYZ);
			Name = new name(colorData.Name);
			Image = new image(colorData.Image);
			Contrast = colorData.Contrast.value;
		}

		private void initEmpty()
		{
			HexValue = new hexvalue();
			RGB = new rgb();
			HSL = new hsl();
			HSV = new hsv();
			CMYK = new cmyk();
			XYZ = new xyz();
			Name = new name();
			Image = new image();
			Contrast = string.Empty;
		}

		#endregion

		#region String Helpers

		public override string ToString()
		{
			return Name.Value;
		}

		public string ToString(bool allData)
		{
			if (allData)
			{
				string cData = "Name: {0}\r\nRGB: {1}\r\nCMYK: {2}\r\nHSV: {3}\r\nHSL: {4}\r\nXYZ: {5}";

				string _nameData = "\r\n\tValue: {0}\r\n\tClosest Named Match: {1}\r\n\tExact Match: {2}\r\n\tDistance: {3}";
				_nameData = string.Format(_nameData, Name.Value, Name.ClosestNamedHex, Name.ExactMatchName, Name.Distance);

				string _rgbData = "\r\n\tR: {0}\t(Fraction: {1})\r\n\tG: {2}\t(Fraction: {3})\r\n\tB: {4}\t(Fraction: {5})";
				_rgbData = string.Format(_rgbData, RGB.Values.Red, RGB.Fraction.Red, RGB.Values.Green, RGB.Fraction.Green, RGB.Values.Blue, RGB.Fraction.Blue);

				string _hsvData = "\r\n\tH: {0}\t(Fraction: {1})\r\n\tS: {2}\t(Fraction: {3})\r\n\tV: {4}\t(Fraction: {5})";
				_hsvData = string.Format(_hsvData, HSV.Values.H, HSV.Fraction.H, HSV.Values.S, HSV.Fraction.S, HSV.Values.V, HSV.Fraction.V);

				string _hslData = "\r\n\tH: {0}\t(Fraction: {1})\r\n\tS: {2}\t(Fraction: {3})\r\n\tL: {4}\t(Fraction: {5})";
				_hslData = string.Format(_hslData, HSL.Values.H, HSL.Fraction.H, HSL.Values.S, HSL.Fraction.S, HSL.Values.L, HSL.Fraction.L);

				string _xyzData = "\r\n\tX: {0}\t(Fraction: {1})\r\n\tY: {2}\t(Fraction: {3})\r\n\tZ: {4}\t(Fraction: {5})";
				_xyzData = string.Format(_xyzData, XYZ.Values.X, XYZ.Fraction.X, XYZ.Values.Y, XYZ.Fraction.Y, XYZ.Values.Z, XYZ.Fraction.Z);

				string _cmykData = "\r\n\tC: {0}\t(Fraction: {1})\r\n\tM: {2}\t(Fraction: {3})\r\n\tY: {4}\t(Fraction: {5})\r\n\tK: {6}\t(Fraction: {7})";
				_cmykData = string.Format(_cmykData, CMYK.Values.C, CMYK.Fraction.C, CMYK.Values.M, CMYK.Fraction.M, CMYK.Values.Y, CMYK.Fraction.Y, CMYK.Values.K, CMYK.Fraction.K);

				return string.Format(cData, _nameData, _rgbData, _cmykData, _hsvData, _hslData, _xyzData);
			}
			else
			{
				return ToString();
			}
		}

		#endregion
	}
}
