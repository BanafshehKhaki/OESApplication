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
		UIKit.UIImageView detectedSpectra { get; set; }

		[Outlet]
		UIKit.UILabel resultOutput { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (resultOutput != null) {
				resultOutput.Dispose ();
				resultOutput = null;
			}

			if (detectedSpectra != null) {
				detectedSpectra.Dispose ();
				detectedSpectra = null;
			}
		}
	}
}
