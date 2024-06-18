using Microsoft.Maui.ApplicationModel;
using System;
using System.Resources;
using System.Security.Cryptography;

namespace _100mame1
{
    public partial class MainPage : ContentPage
    {
        private int _steps;
        private Random _random;
        Location location;
        private CancellationTokenSource _cancelTokenSource;
        private bool _isCheckingLocation;

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

        int count = 0;
        int randommame;
        Random r = new Random();

        public MainPage()
        {
            InitializeComponent();
            GetCurrentLocation();
            _steps = 0;
            _random = new Random();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;
            GetCurrentLocation();
        }

        private void WashletClicked(object sender, EventArgs e)
        {
            GetCurrentLocation();
        }

        private void mameClicked(object sender, EventArgs e)
        {
            SimulateStepData();
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
                "ネオアームストロングサイクロンジェットアームストロング砲は" +
                "江戸城の天守閣を吹き飛ばし江戸を開国させちまった戌磯族の決戦兵器だ",
                "蘭姉ちゃんの髪の「角」は初期のコナンでは存在しなかった(現在は凶器)",
                "ドラえもんがネズミに驚いて逃げた時の速さは時速129.3km",
                "ラッコの肉を食べると、なんだか無性にムラムラするらしい",
                "水族館のマンボウは唇が大きい",
                "クリオネは実は不味い(シンナー臭い)",
                "エヴァンゲリオンの第11話はスタジオジブリが作画を担当している",
                "アムロは当初の髪型が「アフロ」として企画されていた",
                "ヱヴァの主要キャラの名前は旧日本軍の戦艦名が由来",
                "　　　　　　　「そいつが切り札だ！！」　　　　 　　　　　　"+
                "  ～ジュランダル～ ※たまにプラ製がはいっているので注意",
                "「俺を選べsp」(77.0%)"+"※ただしコバケンは選ばれなかった...",
                "味噌はオソマではない",
                "カレーもオソマではない",
                "　　　　　　　　　銀魂の年号覚え方　　　　　　　　　　　　　"+
                "1588年　いっこハンパねーバナナあったゴリね母さん　バナナ狩り",
                "ぶりぶり～ケツだけ星人よッ！！！",
                "グルメスパイザーの値段は国家予算！！！"+
                "ちなみにトリコは最終回で暴走したトリコを小松が泣きながら調理して連載終了",
                "コバは燃え尽きた...",
                "名前は「ウンコティンティン」"+
                "けして「ウンコチンチン」と下品な名前を言えと言ってるんじゃないんだ",
                "やりました！やったんですよ！必死に！"+
                "その結果がこれなんですよ！モビルスーツに乗って、殺し合いをして、今はこうして砂漠を歩いている。"+
                "これ以上何をどうしろって言うんです？何と戦えって言うんですか！",
                "それでも！！！！！！！！",
            };

            string[] images = {
                "tokoro_ten.jpg",//ところ天の助
                "neo_hou.jpg",//アームストロング...
                "ran_konan.png",//蘭姉ちゃん
                "dorae_mon.png",//ドラえもん
                "rako_niku.jpg",//ラッコ肉
                "man_bo.jpg",//マンボウ
                "kuri_one.jpg",//クリオネ
                "",
                "",
                "",
                "zyuran_daru.webp",//ジュランダル
                "era_be.jpg",//リゼロ
                "oso_ma.jpg",//オソマ
                "kare_osoma.webp",//カレーオソマ
                "ginta_ma1.jpg",//銀魂
                "画像.jpg",//みさえ
                "guru_me.jpg",//トリコ
                "moe_tukita.jpg",//燃え尽きた
                "CePEVWZUMAApBmS.jpg",
                "o1652092915227046724.jpg",
                "o0500038615131724015.jpg",
            };

            int index = _random.Next(messages.Length);
            string message = messages[index];
            string imagePath = images[index];

            AlertMessage.Text = message;
            AlertImage.Source = imagePath;
            AlertMessage.IsVisible = true;
            AlertImage.IsVisible = true;
        }
    }
}