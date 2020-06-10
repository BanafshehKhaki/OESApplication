using Xamarin.Forms;

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
