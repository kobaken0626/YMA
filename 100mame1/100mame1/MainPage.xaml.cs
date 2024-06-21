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
                "ところ天の助の値段は100円",//1
                "ネオアームストロング砲は江戸城の天" +
                "守閣を吹き飛ばし江戸を開国させち" +
                "まった戌磯族の決戦兵器だ",//2
                "蘭姉ちゃんの髪の「角」は初期の" +
                "コナンでは存在しなかった(現在" +
                "は凶器)",//3
                "ドラえもんがネズミに驚いて逃げた時の速さは時速129.3km",//4
                "ラッコの肉を食べると、なんだか無性にムラムラするらしい",//5
                "味噌はオソマではない",//6
                "カレーもオソマではない",//7
                "クリオネは不味い(シンナー臭い)",//8
                "パスタを折るとイタリア人に殺される",//9
                "誰なの？怖いよおッ！！！",//10
                "実は白石で魚が釣れる",//11
                "ぼざろの喜多ちゃんの初期案はツンデレ"+
                "ツン目はその名残り",//12
                "アムロは当初の髪型が「アフロ」として企画されていた",//13
                "杉本さぁーーーーーーん",//14
                "はっ！！！",//15
                "それでも！！",//16
                "やりましたよ、やったんですよ必死に！！",//17
                "俺を選べsp(77.0%)," +
                "※コバケンは選ばれなかった",//18
                "そいつが切り札だ！！",//19
                "炎炎ノ炎ニ帰セ",//20
                "イエーイ",//21
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
                "rx_000.jpg",//それでも16
                "o1652092915227046724.jpg",//やったんですよ17
                "era_be.jpg",//選べ18
                "zyuran_daru.webp",//ジュランダル19
                "ra_tom.jpg",//ラートム20
                "20230324140630.jpeg",//イエーイ21
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