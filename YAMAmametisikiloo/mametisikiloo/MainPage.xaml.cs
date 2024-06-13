using Microsoft.Maui.Devices.Sensors;

namespace mametisikiloo
{
    public partial class MainPage : ContentPage
       
    {
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


    }

}
