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
    [Register ("mainViewController")]
    partial class mainViewController
    {
        [Outlet]
        UIKit.UIButton captureSpectraButton { get; set; }


        [Outlet]
        UIKit.UIImageView CrossHair { get; set; }


        [Outlet]
        UIKit.UIImageView ImagePreview { get; set; }


        [Outlet]
        UIKit.UIButton takeimageButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView liveCameraStream { get; set; }


        [Action("captureSpectraTouchUpInside:")]
        partial void captureSpectraTouchUpInside(Foundation.NSObject sender);

        void ReleaseDesignerOutlets ()
        {
            if (liveCameraStream != null) {
                liveCameraStream.Dispose ();
                liveCameraStream = null;
            }
        }
    }
}