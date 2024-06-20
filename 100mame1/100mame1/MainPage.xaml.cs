using Microsoft.Maui.ApplicationModel;
using System;
using System.Collections.Generic;
using System.Resources;
using System.Security.Cryptography;

namespace _100mame1
{
    public partial class MainPage : ContentPage
    {
        Location location;
        private CancellationTokenSource _cancelTokenSource;
        private bool _isCheckingLocation;

        private Random _random = new Random();

        public async Task GetCurrentLocation()
        {
            try
            {
                _isCheckingLocation = true;

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                _cancelTokenSource = new CancellationTokenSource();

                location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                if (location != null)
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                CounterBtn.Text = (location.Latitude).ToString();
                Washlet.Text = (location.Longitude).ToString();


            }

            // Catch one of the following exceptions:
            //   FeatureNotSupportedException
            //   FeatureNotEnabledException
            //   PermissionException
            catch (Exception ex)
            {
                // Unable to get location
            }
            finally
            {
                _isCheckingLocation = false;
            }
        }

        public void CancelRequest()
        {
            if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
                _cancelTokenSource.Cancel();
        }

        private int _steps;
       

        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            GetCurrentLocation();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;
            GetCurrentLocation();
        }
        private void mameClicked(object sender, EventArgs e)
        {
            // Simulate step data
            SimulateStepData();
        }

        private void WashletClicked(object sender, EventArgs e)
        {
            GetCurrentLocation();
        }

        private void SimulateStepData()
        {
            _steps++;
            aruku2Label.Text = $"歩数: {_steps}";

            if (_steps % 1 == 0)
            {
                mameMessage();
            }
        }

        private void mameMessage()
        {
            string[] messages = {
                "ところ天の助の値段は100円",
                "ネオアームストロング砲は江戸城" +
                "の天守閣を吹き飛ばし江戸を開国" +
                "させちまった戌磯族の決戦兵器だ",
                "蘭姉ちゃんの髪の「角」は初期の" +
                "コナンでは存在しなかった(現在" +
                "は凶器)",
                "ドラえもんがネズミに驚いて逃げた時の速さは時速129.3km",
                "ラッコの肉を食べると、なんだか無性にムラムラするらしい",
                "味噌はオソマではない",
                "カレーもオソマではない",
                "クリオネは不味い(シンナー臭い)",
                "パスタを折るとイタリア人に殺される",
                "誰なの？怖いよおッ！！！",
                "実は白石で魚が釣れる",
                "ぼざろの喜多ちゃんの初期案はツンデレ"+
                "ツン目はその名残り",
                "アムロは当初の髪型が「アフロ」として企画されていた",
                "杉本さぁーーーーーーん",
                "はっ！！！",
            };

            string[] images = {
                "tokoro_ten.jpg",//ところてん1
                "neo_hou.jpg",//ネオ砲2
                "ran_konan.png",//蘭角3
                "dorae_mon.png",//ドラえもん4
                "rako_niku.jpg",//ラッコ肉5
                "oso_ma.jpg",//味噌オソマ6
                "kare_osoma.webp",//カレーオソマ7
                "kuri_one.jpg",//クリオネ8
                "pa_suta.jpg",//パスタ9
                "dare_kowai.jpg",//二階堂10
                "sira_ishi.jpg",//白石魚11
                "kita.jpg",//喜多12
                "ahuro.jpg",//アムロ13
                "iwaiki.jpg",//杉本さぁーん14
                "ha.jpg",//はっ！15
            };

            int index = _random.Next(messages.Length);
            string message = messages[index];
            string imagePath = images[index];

            AlertMessage.Text = message;
            AlertImage.Source = ImageSource.FromFile(imagePath);
            AlertMessage.IsVisible = true;
            AlertImage.IsVisible = true;
        }

        void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            var acceleration = e.Reading.Acceleration;
            if (Math.Abs(acceleration.X) > 1 || Math.Abs(acceleration.Y) > 1 || Math.Abs(acceleration.Z) > 1)
            {
                _steps++;
                aruku2Label.Text = $"Steps: {_steps}";

                if (_steps % 100 == 0)
                {
                    mameMessage();
                }
            }
        }
    }
}