/*
	ColorDetector
	By: Anthony McKeever (GitHub: Snip3rM00n - http://github.com/snip3rm00n)

	App: Designed to work with RGB Sensor from Adafruit and provide color codes and values (RGB, CMYK, HSL, HSV, XYZ) about a color.
	Based on: Microsoft IoT Lesson 205 - "What Color Is It"
	API Used: The Color API by Josh Beckman (http://thecolorapi.com).

	File: ApiHelpers.cs
	Coded in: C# (Microsoft .Net 5.0 Framework)
	Namespace: ColorDetector
	Purpose: Provides a static class of API Helpers.  These make calls to various APIs (TheColorAPI, AdaFruitSamples).
*/

using System;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace ColorDetector
{
	public static class ApiHelpers
	{
		const string apiUrl = "http://www.thecolorapi.com/id?{0}";
		const string rgbUrlParams = "rgb=rgb({0},{1},{2})";

		//	Returns a ColorfulJsonParser object from call to The Color API.
		public static async Task<object> getColorApiJson(int r, int g, int b)
		{
			string url = string.Format(apiUrl, rgbUrlParams);
			url = string.Format(url, r, g, b);

			try
			{
				HttpClient client = new HttpClient();
				System.IO.Stream s = await client.GetStreamAsync(url);

				DataContractJsonSerializer jSonSerializer = new DataContractJsonSerializer(typeof(ColorfulJsonParser));
				object jObject = jSonSerializer.ReadObject(s);

				return jObject;
			}
			catch (Exception e)
			{
				Debug.WriteLine(string.Format("Call to TheColorAPI (URL: {0}) failed: {1}", url, e.Message));
				return null;
            }
		}

		//	Makes call to the Adafruit Sample API for Lesson 205
		public static void MakePinWebAPICall()
		{
			try
			{
				HttpClient client = new HttpClient();
				client.GetStringAsync("http://adafruitsample.azurewebsites.net/api?Lesson=205");
			}
			catch (Exception e)
			{
				Debug.WriteLine("API call failed: " + e.Message);
			}
		}
	}
}
