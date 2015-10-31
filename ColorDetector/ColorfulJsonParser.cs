/*
	ColorDetector
	By: Anthony McKeever (GitHub: Snip3rM00n - http://github.com/snip3rm00n)

	App: Designed to work with RGB Sensor from Adafruit and provide color codes and values (RGB, CMYK, HSL, HSV, XYZ) about a color.
	Based on: Microsoft IoT Lesson 205 - "What Color Is It"
	API Used: The Color API by Josh Beckman (http://thecolorapi.com).

	File: ColorfulJsonParser.cs
	Coded in: C# (Microsoft .Net 5.0 Framework)
	Namespace: ColorDetector
	Purpose: Used to parse the results from The Color API into a usable collection of classes.
*/

using System.Runtime.Serialization;

namespace ColorDetector
{
	[DataContract]
	public class ColorfulJsonParser
	{
		[DataMember(Name = "hex")]
		public hexData Hex { get; set; }

		[DataMember(Name = "rgb")]
		public rgbData RGB { get; set; }

		[DataMember(Name = "hsl")]
		public hslData HSL { get; set; }

		[DataMember(Name = "hsv")]
		public hsvData HSV { get; set; }

		[DataMember(Name = "name")]
		public nameData Name { get; set; }

		[DataMember(Name = "cmyk")]
		public cmykData CMYK { get; set; }

		[DataMember(Name = "XYZ")]
		public xyzData XYZ { get; set; }

		[DataMember(Name = "image")]
		public imageData Image { get; set; }

		[DataMember(Name = "contrast")]
		public contrastData Contrast { get; set; }

		[DataMember(Name = "_links")]
		public linksData Links { get; set; }
	}

	#region RGB

	[DataContract]
	public class rgbData
	{
		[DataMember(Name = "fraction")]
		public rgbFraction Fraction { get; set; }

		[DataMember(Name = "r")]
		public ushort r { get; set; }

		[DataMember(Name = "g")]
		public ushort g { get; set; }

		[DataMember(Name = "b")]
		public ushort b { get; set; }

		[DataMember(Name = "value")]
		public string value { get; set; }
	}

	[DataContract]
	public class rgbFraction
	{
		[DataMember(Name = "r")]
		public decimal r { get; set; }

		[DataMember(Name = "g")]
		public decimal g { get; set; }

		[DataMember(Name = "b")]
		public decimal b { get; set; }
	}

	#endregion

	#region HSL

	[DataContract]
	public class hslData
	{
		[DataMember(Name = "fraction")]
		public hslFraction fraction { get; set; }

		[DataMember(Name = "h")]
		public ushort h { get; set; }

		[DataMember(Name = "s")]
		public ushort s { get; set; }

		[DataMember(Name = "l")]
		public ushort l { get; set; }

		[DataMember(Name = "value")]
		public string value { get; set; }
	}

	[DataContract]
	public class hslFraction
	{
		[DataMember(Name = "h")]
		public decimal h { get; set; }

		[DataMember(Name = "s")]
		public decimal s { get; set; }

		[DataMember(Name = "l")]
		public decimal l { get; set; }
	}

	#endregion

	#region HSV

	[DataContract]
	public class hsvData
	{
		[DataMember(Name = "fraction")]
		public hsvFraction fraction { get; set; }

		[DataMember(Name = "h")]
		public ushort h { get; set; }

		[DataMember(Name = "s")]
		public ushort s { get; set; }

		[DataMember(Name = "v")]
		public ushort v { get; set; }

		[DataMember(Name = "value")]
		public string value { get; set; }
	}

	[DataContract]
	public class hsvFraction
	{
		[DataMember(Name = "h")]
		public decimal h { get; set; }

		[DataMember(Name = "s")]
		public decimal s { get; set; }

		[DataMember(Name = "v")]
		public decimal v { get; set; }
	}

	#endregion

	#region CMYK

	[DataContract]
	public class cmykData
	{
		[DataMember(Name = "fraction")]
		public cmykFraction fraction { get; set; }

		[DataMember(Name = "c")]
		public ushort c { get; set; }

		[DataMember(Name = "m")]
		public ushort m { get; set; }

		[DataMember(Name = "y")]
		public ushort y { get; set; }

		[DataMember(Name = "k")]
		public ushort k { get; set; }

		[DataMember(Name = "value")]
		public string value { get; set; }
	}

	public class cmykFraction
	{
		[DataMember(Name = "c")]
		public decimal c { get; set; }

		[DataMember(Name = "m")]
		public decimal m { get; set; }

		[DataMember(Name = "y")]
		public decimal y { get; set; }

		[DataMember(Name = "k")]
		public decimal k { get; set; }
	}

	#endregion

	#region XYZ

	[DataContract]
	public class xyzData
	{
		[DataMember(Name = "fraction")]
		public xyzFraction fraction { get; set; }

		[DataMember(Name = "X")]
		public ushort x { get; set; }

		[DataMember(Name = "Y")]
		public ushort y { get; set; }

		[DataMember(Name = "Z")]
		public ushort z { get; set; }

		[DataMember(Name = "value")]
		public string value { get; set; }
	}

	[DataContract]
	public class xyzFraction
	{
		[DataMember(Name = "X")]
		public decimal x { get; set; }

		[DataMember(Name = "Y")]
		public decimal y { get; set; }

		[DataMember(Name = "Z")]
		public decimal z { get; set; }
	}

	#endregion

	#region Misc

	[DataContract]
	public class hexData
	{
		[DataMember(Name = "value")]
		public string value { get; set; }

		[DataMember(Name = "clean")]
		public string clean { get; set; }
	}

	[DataContract]
	public class nameData
	{
		[DataMember(Name = "value")]
		public string value { get; set; }

		[DataMember(Name = "closest_named_hex")]
		public string closestNamedHex { get; set; }

		[DataMember(Name = "exact_match_name")]
		public bool exactNameMatch { get; set; }

		[DataMember(Name = "distance")]
		public int distance { get; set; }
	}

	[DataContract]
	public class imageData
	{
		[DataMember(Name = "bare")]
		public string Bare { get; set; }

		[DataMember(Name = "named")]
		public string Named { get; set; }
	}

	[DataContract]
	public class contrastData
	{
		[DataMember(Name = "value")]
		public string value { get; set; }
	}

	[DataContract]
	public class linksData
	{
		[DataMember(Name = "self")]
		public selfData Self { get; set; }
	}

	public class selfData
	{
		[DataMember(Name = "href")]
		public string href { get; set; }
	}
	#endregion
}
