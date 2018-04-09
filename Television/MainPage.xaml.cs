using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Gpio;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web;
using Windows.Web.Http;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Television
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private GpioController _gpc;

        private GpioPin _GpioPin05;
        private GpioPin _GpioPin13;
        private GpioPin _GpioPin19;
        private GpioPin _GpioPin26;

        private string pr = "http://www.francetelevisions.fr/programme-tv";
        private string c2 = "https://mobile.france.tv/france-2/direct.html";
        private string c3 = "https://mobile.france.tv/france-3/direct.html";
        private string c4 = "https://mobile.france.tv/france-4/direct.html";
        private string c5 = "https://mobile.france.tv/france-5/direct.html";
        private string co = "https://mobile.france.tv/france-o/direct.html";
         

        
        public MainPage()
        {
            this.InitializeComponent();
            this.initGpio();
            this.travauxTimer();
        }

        private void initGpio()
        {

            _gpc = GpioController.GetDefault();

            // Bouton Blanc sur GPIO05
            _GpioPin05 = _gpc.OpenPin(5);
            _GpioPin05.SetDriveMode(GpioPinDriveMode.Input);

            // Bouton Vert sur GPIO13
            _GpioPin13 = _gpc.OpenPin(13);
            _GpioPin13.SetDriveMode(GpioPinDriveMode.Input);

            // Bouton Bleu sur GPIO19
            _GpioPin19 = _gpc.OpenPin(19);
            _GpioPin19.SetDriveMode(GpioPinDriveMode.Input);

            // Bouton Jaune sur GPIO26
            _GpioPin26 = _gpc.OpenPin(26);
            _GpioPin26.SetDriveMode(GpioPinDriveMode.Input);

        }

        private void travauxTimer()
        {

            DispatcherTimer dispatcherTimerButtun = new DispatcherTimer();
            dispatcherTimerButtun.Tick += dispatcherTimerButtun_Tick;
            dispatcherTimerButtun.Interval = new TimeSpan(0, 0, 0, 0, 1);
            dispatcherTimerButtun.Start();
        }
        
        private async void dispatcherTimerButtun_Tick(object sender, object e)
        {

            // Bouton Blanc P+
            if (_GpioPin05.Read() == GpioPinValue.High)
            {

                MediaElement mediaElement = new MediaElement();
                var synth = new SpeechSynthesizer();
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("White, programm plus");
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();

                if ( myWebView.Source.ToString().Equals( pr ) )
                {

                    myWebView.Navigate(new Uri(c2));
                }
                else if ( myWebView.Source.ToString().Equals( c2 ) )
                {

                    myWebView.Navigate( new Uri( c3 ) );
                }
                else if ( myWebView.Source.ToString().Equals( c3 ) )
                {

                    myWebView.Navigate( new Uri( c4 ) );
                }
                else if (myWebView.Source.ToString().Equals(c4))
                {

                    myWebView.Navigate(new Uri(co));
                }
                else if (myWebView.Source.ToString().Equals(co))
                {

                    myWebView.Navigate(new Uri(c2));
                }

            }

            // Bouton Vert P-
            if (_GpioPin13.Read() == GpioPinValue.High)
            {

                MediaElement mediaElement = new MediaElement();
                var synth = new SpeechSynthesizer();
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Green, programm menus");
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();

                if (myWebView.Source.ToString().Equals(pr))
                {

                    myWebView.Navigate         (new Uri(co));
                }
                else if (myWebView.Source.ToString().Equals(co))
                {

                    myWebView.Navigate(new Uri(c4));
                }
                else if (myWebView.Source.ToString().Equals(c4))
                {

                    myWebView.Navigate(new Uri(c3));
                }
                else if (myWebView.Source.ToString().Equals(c3))
                {

                    myWebView.Navigate(new Uri(c2));
                }
                else if (myWebView.Source.ToString().Equals(c2))
                {

                    myWebView.Navigate(new Uri(co)) ;
                }

            }

            // Bouton Bleu
            if (_GpioPin19.Read() == GpioPinValue.High)
            {

                MediaElement mediaElement = new MediaElement();
                var synth = new SpeechSynthesizer();
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Blue, volume plus");
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();
            }

            // Bouton Jaune
            if (_GpioPin26.Read() == GpioPinValue.High)
            {

                MediaElement mediaElement = new MediaElement();
                var synth = new SpeechSynthesizer();
                SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync("Yellow, volume menus");
                mediaElement.SetSource(stream, stream.ContentType);
                mediaElement.Play();
            }

        }

    }

}
