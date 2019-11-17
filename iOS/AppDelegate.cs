using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

using Foundation;
using UIKit;

namespace OESApplication.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        UIWindow window;
        public static UIStoryboard Storyboard = UIStoryboard.FromName("Main", null);
        public static UIViewController initialViewController;


        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            //global::Xamarin.Forms.Forms.Init();

            //LoadApplication(new App());

            //return base.FinishedLaunching(app, options);
			AppCenter.Start("89e5378d-4591-4c6a-aafe-7b8757891d15",
                   typeof(Analytics), typeof(Crashes));
            //AppCenter.Start("89e5378d-4591-4c6a-aafe-7b8757891d15", typeof(Analytics), typeof(Crashes));

            window = new UIWindow(UIScreen.MainScreen.Bounds);
			noOpClass.Init();

            initialViewController = Storyboard.InstantiateInitialViewController() as UIViewController;

            window.RootViewController = initialViewController;
            window.MakeKeyAndVisible();
            return true;
        }
    }
}
