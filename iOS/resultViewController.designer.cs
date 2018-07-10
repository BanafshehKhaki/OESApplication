// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace OESApplication.iOS
{
    [Register ("resultViewController")]
    partial class resultViewController
    {
        [Outlet]
        UIKit.UIImageView detectedSpectra { get; set; }


        [Outlet]
        UIKit.UIButton measureNitrate { get; set; }


        [Outlet]
        UIKit.UIButton measurePH { get; set; }


        [Outlet]
        UIKit.UILabel resultOutput { get; set; }


        [Outlet]
        UIKit.UIStackView StackView { get; set; }


        [Action ("measureNitrateTouchUpInside:")]
        partial void measureNitrateTouchUpInside (Foundation.NSObject sender);


        [Action ("measurePHTouchUpInside:")]
        partial void measurePHTouchUpInside (Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
            if (detectedSpectra != null) {
                detectedSpectra.Dispose ();
                detectedSpectra = null;
            }

            if (measureNitrate != null) {
                measureNitrate.Dispose ();
                measureNitrate = null;
            }

            if (measurePH != null) {
                measurePH.Dispose ();
                measurePH = null;
            }

            if (resultOutput != null) {
                resultOutput.Dispose ();
                resultOutput = null;
            }
        }
    }
}