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
	[Register ("resultViewController")]
	partial class resultViewController
	{
		[Outlet]
		UIKit.UIButton blankControl { get; set; }

		[Outlet]
		UIKit.UIImageView detectedSpectra { get; set; }

		[Outlet]
		UIKit.UIButton measureNitrate { get; set; }

		[Outlet]
		UIKit.UIButton measurePH { get; set; }

		[Outlet]
		UIKit.UILabel resultOutput { get; set; }

		[Outlet]
		UIKit.UITextField resultOutputBox { get; set; }

		[Action ("measureNitrateTouchUpInside:")]
		partial void measureNitrateTouchUpInside (Foundation.NSObject sender);

		[Action ("measurePHTouchUpInside:")]
		partial void measurePHTouchUpInside (Foundation.NSObject sender);

		[Action ("setBlankControlTouchUpInside:")]
		partial void setBlankControlTouchUpInside (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (blankControl != null) {
				blankControl.Dispose ();
				blankControl = null;
			}

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

			if (resultOutputBox != null) {
				resultOutputBox.Dispose ();
				resultOutputBox = null;
			}
		}
	}
}
