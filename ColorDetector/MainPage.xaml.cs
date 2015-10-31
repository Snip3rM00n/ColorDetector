/*
	ColorDetector
	By: Anthony McKeever (GitHub: Snip3rM00n - http://github.com/snip3rm00n)

	App: Designed to work with RGB Sensor from Adafruit and provide color codes and values (RGB, CMYK, HSL, HSV, XYZ) about a color.
	Based on: Microsoft IoT Lesson 205 - "What Color Is It"
	API Used: The Color API by Josh Beckman (http://thecolorapi.com).

	File: MainPage.xaml.cs
	Coded in: C# (Microsoft .Net 5.0 Framework)
	Namespace: ColorDetector
	Purpose: Used to power the UI of the page and interface with breadboard's button.
*/

using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Devices.Gpio;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace ColorDetector
{
	public sealed partial class MainPage : Page
	{
		TCS34725 colorSensor;
		SpeechSynthesizer Synthia;
		MediaElement audio;
		GpioPin buttonPin;

		int gpioPin = 4;

		public MainPage()
		{
			InitializeComponent();
		}

		protected override async void OnNavigatedTo(NavigationEventArgs navArgs)
		{
			ApiHelpers.MakePinWebAPICall();

			try
			{
				colorSensor = new TCS34725();
				await colorSensor.Initialize();

				Synthia = new SpeechSynthesizer();
				Synthia.Voice = SpeechSynthesizer.AllVoices[1];

				audio = new MediaElement();

				InitializeGPIO();
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.Message);
			}
		}

		private void InitializeGPIO()
		{
			GpioController gpioController = GpioController.GetDefault();
			buttonPin = gpioController.OpenPin(gpioPin);

			buttonPin.DebounceTimeout = new TimeSpan(1000);
			buttonPin.SetDriveMode(GpioPinDriveMode.Input);
			buttonPin.ValueChanged += buttonPin_ValueChanged;
		}

		private async void buttonPin_ValueChanged(object sender, GpioPinValueChangedEventArgs e)
		{
			if (e.Edge == GpioPinEdge.RisingEdge)
			{
				ColorfulRestProperty color = await colorSensor.getColorfulRestData();
				await SpeakColor(color.Name.Value);

				Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
				{
					updateUI(color, updText: true);
				}).AsTask().Wait();
			}
		}
		
		//	Gets Colorful Rest Data from user input.
		private async void hitApi_Click(object sender, RoutedEventArgs e)
		{
			int r = Convert.ToInt32(redInt.Text);
			int g = Convert.ToInt32(greenInt.Text);
			int b = Convert.ToInt32(blueInt.Text);

			object json = await ApiHelpers.getColorApiJson(r, g, b);

			ColorfulRestProperty cProp = new ColorfulRestProperty(json);
			updateUI(cProp);
		}

		private async Task SpeakColor(string colorRead)
		{
			var stream = await Synthia.SynthesizeTextToStreamAsync("This color appears to be " + colorRead);
			var ignored = Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
				{
					audio.SetSource(stream, stream.ContentType);
					audio.Play();
				});
		}

		//	Updates the color history combobox
		private void updateHistory(ColorfulRestProperty color)
		{
			if (!ColorHistory.Items.Contains(color))
			{
				ColorHistory.Items.Add(color);
				ColorHistory.SelectedIndex = ColorHistory.Items.Count - 1;
			}
		}

		//	Updates UI with the history item selected
		private void ColorHistory_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ColorfulRestProperty cProp = (ColorfulRestProperty)ColorHistory.SelectedItem;
			updateUI(cProp, false);
		}

		//	Updates the UI with colorful rest data.
		private void updateUI(ColorfulRestProperty colorData, bool updHistory = true, bool updText = false)
		{
			byte r = (byte)colorData.RGB.Values.Red;
			byte g = (byte)colorData.RGB.Values.Green;
			byte b = (byte)colorData.RGB.Values.Blue;

			colorDetail.Text = colorData.ToString(true);
			ColorPreview.Fill = new SolidColorBrush(Windows.UI.Color.FromArgb(255, r, g, b));

			if (updHistory)	updateHistory(colorData);

			if (updText)
			{
				redInt.Text = colorData.RGB.Values.Red.ToString();
				greenInt.Text = colorData.RGB.Values.Green.ToString();
				blueInt.Text = colorData.RGB.Values.Blue.ToString();
            }
		}
	}
}
