using System;
using Foundation;

// @interface SpectraDetector
interface SpectraDetector
{
	// -(id)initWithSpectraCascade:(id)filePath;
	[Export ("initWithSpectraCascade:")]
	IntPtr Constructor (NSObject filePath);

	// -(id)detectSpectras:(id)image;
	[Export ("detectSpectras:")]
	NSObject DetectSpectras (NSObject image);
}
