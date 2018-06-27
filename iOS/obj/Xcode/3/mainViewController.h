// WARNING
// This file has been generated automatically by Visual Studio to
// mirror C# types. Changes in this file made by drag-connecting
// from the UI designer will be synchronized back to C#, but
// more complex manual changes may not transfer correctly.


#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>


@interface mainViewController : UIViewController {
	UIButton *_captureSpectraButton;
	UIImageView *_CrossHair;
	UIImageView *_ImagePreview;
	UIView *_liveCameraStream;
	UIButton *_takeimageButton;
}

@property (nonatomic, retain) IBOutlet UIButton *captureSpectraButton;

@property (nonatomic, retain) IBOutlet UIImageView *CrossHair;

@property (nonatomic, retain) IBOutlet UIImageView *ImagePreview;

@property (nonatomic, retain) IBOutlet UIView *liveCameraStream;

@property (nonatomic, retain) IBOutlet UIButton *takeimageButton;

- (IBAction)captureSpectraTouchUpInside:(id)sender;

@end
