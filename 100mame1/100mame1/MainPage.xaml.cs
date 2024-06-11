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

        public MainPage()
        {
            InitializeComponent();
            _steps = 0;
            _random = new Random();
        }

        private void OnSimulateStepClicked(object sender, EventArgs e)
        {
            // Simulate step data
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