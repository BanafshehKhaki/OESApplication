using Xamarin.Forms;
//using Microsoft.AppCenter;
//using Microsoft.AppCenter.Analytics;
//using Microsoft.AppCenter.Crashes;

namespace OESApplication
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new OESApplicationPage();
        }

        protected override void OnStart()
        {
			// Handle when your app starts
			//AppCenter.Start("ios=89e5378d-4591-4c6a-aafe-7b8757891d15;" +
			//	  "uwp={Your UWP App secret here};" +
			//	  "android={Your Android App secret here}",
			//				typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
