// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
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
		[GeneratedCode ("iOS Designer", "1.0")]
		UIKit.UIView liveCameraStream { get; set; }

		[Outlet]
		UIKit.UIButton takeimageButton { get; set; }

		[Action ("captureSpectraTouchUpInside:")]
		partial void captureSpectraTouchUpInside (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (ImagePreview != null) {
				ImagePreview.Dispose ();
				ImagePreview = null;
			}

			if (captureSpectraButton != null) {
				captureSpectraButton.Dispose ();
				captureSpectraButton = null;
			}

			if (CrossHair != null) {
				CrossHair.Dispose ();
				CrossHair = null;
			}

			if (liveCameraStream != null) {
				liveCameraStream.Dispose ();
				liveCameraStream = null;
			}

			if (takeimageButton != null) {
				takeimageButton.Dispose ();
				takeimageButton = null;
			}
		}
	}
}
