// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Threading.Tasks;
using AVFoundation;
using Foundation;
using UIKit;
using AssetsLibrary;
using CoreImage;
using CoreMedia;

//developed using https://blog.xamarin.com/custom-camera-display-avfoundation/


namespace OESApplication.iOS
{
    public partial class mainViewController : UIViewController
    {


        AVCaptureSession captureSession;
        AVCaptureDeviceInput captureDeviceInput;
        AVCaptureStillImageOutput stillImageOutput;
        AVCaptureVideoPreviewLayer videoPreviewLayer;
        NSData jpegAsByteArray; //pixel values that will be passed to resultViewController. 


        public mainViewController(IntPtr handle) : base(handle)
        {
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();
            await AuthorizeCameraUse();
            SetupLiveCameraStream();
        }

        //Connected to button to take the image
        partial void captureSpectraTouchUpInside(Foundation.NSObject sender)
        {
            /*
             * Erasing previous image because if user moves fast between pages, 
             * resultViewController could be calculating the previous image data
            */
            jpegAsByteArray = null;

            //when i used await, it crashed and hence not using it.  
            takeThepictureAsync();
        }


        /*Performs before image sent to resultViewController due to its Seque connection
         * Checks to see there is image taken by looking at length of jpegAsByteArray and making sure its not null        
         */
        public override bool ShouldPerformSegue(string segueIdentifier, NSObject sender)
        {
            if (segueIdentifier == "SequetoResultsPage")
            {
                if (jpegAsByteArray != null && jpegAsByteArray.Length > 100)
                {
                    Console.WriteLine("good to go ");
                    return true;
                }

                return false;
            }
            return base.ShouldPerformSegue(segueIdentifier, sender);
        }


        /*
         * Passing few values to resultViewController at this step
         * 
        */
        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {

            base.PrepareForSegue(segue, sender);

            var resultViewController = segue.DestinationViewController as resultViewController;


            if (resultViewController != null)
            {

                // Pixel values passed to PixelArray varibale in the resultViewController
                resultViewController.PixelArray = jpegAsByteArray;
                /*
                CrossHair is the green rectangular used to define spectra positioning
                Send the location of green ractangle and frame hight and width to resultViewController 
                resultViewController uses these informations to crop the image
                */
                resultViewController.x_crop_loc = (int)CrossHair.Frame.X;
                resultViewController.y_crop_loc = (int)CrossHair.Frame.Y;

                Console.WriteLine("CrossHair..X: " + CrossHair.Frame.X);
                Console.WriteLine("CrossHair..Y: " + CrossHair.Frame.Y);

                resultViewController.widthOfCrossHair = (int)CrossHair.Frame.Width;
                resultViewController.HeightOfCrossHair = (int)CrossHair.Frame.Height;
                resultViewController.liveCameraWidth = (int)liveCameraStream.Frame.Width;
                resultViewController.liveCameraHeight = (int)liveCameraStream.Frame.Height;

                Console.WriteLine("widthOfCrossHair: " + CrossHair.Frame.Width);
                Console.WriteLine("HeightOfCrossHair: " + CrossHair.Frame.Height);
                Console.WriteLine("liveCameraWidth: " + (int)liveCameraStream.Frame.Width);
                Console.WriteLine("liveCameraHeight: " + (int)liveCameraStream.Frame.Height);

            }

        }



        /*
         * Setting the connection to be from camera feed and capture the image
         * jpegAsByteArray is used to save image pixel values to send to resultViewController to calculate  concentration of sample     
         * Asynchoronouse task needs async 
         */
        public async Task takeThepictureAsync()
        {
            var videoConnection = stillImageOutput.ConnectionFromMediaType(AVMediaType.Video);
            var sampleBuffer = await stillImageOutput.CaptureStillImageTaskAsync(videoConnection);

            var jpegImageAsNsData = AVCaptureStillImageOutput.JpegStillToNSData(sampleBuffer);

            jpegAsByteArray = jpegImageAsNsData;

            var imageMeta = CIImage.FromData(jpegImageAsNsData);

            // creating an image and putting it in ImagePreview frame on top of the screen

            var imagePrev = new UIImage(jpegImageAsNsData); //UIImage.FromImage(imageMeta);
            ImagePreview.Image = imagePrev;

            sampleBuffer.Dispose();


            // Saving the complete image into the phone gallery with addition to its meta data 
            var meta = imageMeta.Properties.Dictionary.MutableCopy() as NSMutableDictionary;
            var library = new ALAssetsLibrary();
            library.WriteImageToSavedPhotosAlbum(jpegImageAsNsData, meta, (assetUrl, error) =>
            {
                if (error == null)
                {
                    Console.WriteLine("saved: ");//+jpegImageAsNsData);
                }
                else
                {
                    Console.WriteLine(error);
                    UIAlertView alert = new UIAlertView()
                    {
                        Title = "Alert!",
                        Message = "There was a problem with saving your image, please take a new picture"
                    };
                    alert.AddButton("OK");
                    alert.Show();

                }
            });

        }

        async Task AuthorizeCameraUse()
        {
            var authorizationStatus = AVCaptureDevice.GetAuthorizationStatus(AVMediaType.Video);
            if (authorizationStatus != AVAuthorizationStatus.Authorized)
            {
                await AVCaptureDevice.RequestAccessForMediaTypeAsync(AVMediaType.Video);
            }
        }

        public void SetupLiveCameraStream()
        {
            captureSession = new AVCaptureSession();

            //.PresetPhoto for camera image feed
            captureSession.SessionPreset = AVCaptureSession.PresetPhoto;

            var viewLayer = liveCameraStream.Layer;
            videoPreviewLayer = new AVCaptureVideoPreviewLayer(captureSession)
            {
                Frame = this.View.Frame,
            };

            liveCameraStream.Layer.AddSublayer(videoPreviewLayer);
            var captureDevice = AVCaptureDevice.GetDefaultDevice(AVMediaType.Video);

            ConfigureCameraForDevice(captureDevice);
            captureDeviceInput = AVCaptureDeviceInput.FromDevice(captureDevice);
            captureSession.AddInput(captureDeviceInput);

            var dictionary = new NSMutableDictionary();
            dictionary[AVVideo.CodecKey] = new NSNumber((int)AVVideoCodec.JPEG);
            stillImageOutput = new AVCaptureStillImageOutput()
            {
                OutputSettings = new NSDictionary()
            };

            captureSession.AddOutput(stillImageOutput);
            captureSession.StartRunning();

        }


        /*
         * Setting up camera setting for EOSpec:
         * Focus: 0.3 locked : Empirically chosen for set distance to current version of EOSpec
         * ISO : Avg between max and min ISO of device          
         * Exposure : Empirically set to 1/10       
         * RGB gain: R and G normalized using max Gain values, 
         * however Blue is normalized using min Gain value due to it saturating more than green and red channels (my phone: 1.858154 , 1 ,  1)
        */
        void ConfigureCameraForDevice(AVCaptureDevice device)
        {
            var error = new NSError();
            if (device.IsFocusModeSupported(AVCaptureFocusMode.ContinuousAutoFocus))
            {
                device.LockForConfiguration(out error);
                device.FocusMode = AVCaptureFocusMode.Locked;
                device.SetFocusModeLocked((float)0.3, null);

                Console.WriteLine("devidce device.FocusMode: " + device.FocusMode);
                device.UnlockForConfiguration();
            }

            if (device.IsExposureModeSupported(AVCaptureExposureMode.ContinuousAutoExposure))
            {
                device.LockForConfiguration(out error);
                device.ExposureMode = AVCaptureExposureMode.Custom;

                var iso = (device.ActiveFormat.MaxISO + device.ActiveFormat.MinISO) / 2;
                CMTime exposureTime = new CMTime(1, 10);
                device.LockExposure(exposureTime, iso, null);
                Console.WriteLine("deviceexposure: " + device.ExposureDuration + " = " + exposureTime + " iso: " + iso);

                device.UnlockForConfiguration();

                //Setting RGB gains using WhiteBalance
                device.LockForConfiguration(out error);
                AVCaptureWhiteBalanceGains gains = device.DeviceWhiteBalanceGains;
                //normalizign Gains
                gains.RedGain = Math.Max(1, gains.RedGain);
                gains.GreenGain = Math.Max(1, gains.GreenGain);
                gains.BlueGain = Math.Min(1, gains.BlueGain);

                float maxGain = device.MaxWhiteBalanceGain;
                gains.RedGain = Math.Min(maxGain, gains.RedGain);
                gains.GreenGain = Math.Min(maxGain, gains.GreenGain);
                gains.BlueGain = Math.Min(maxGain, gains.BlueGain);
                Console.WriteLine("devidce device.WhiteBalanceMode gains RGB: " + gains.RedGain + " " + gains.GreenGain + " " + gains.BlueGain);

                device.SetWhiteBalanceModeLockedWithDeviceWhiteBalanceGains(gains, null);
                device.UnlockForConfiguration();
            }
        }


        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        protected override void Dispose(bool disposing)
        {
            captureSession.Dispose();
            captureDeviceInput.Dispose();
            stillImageOutput.Dispose();
            jpegAsByteArray.Dispose();
            base.Dispose(disposing);
        }


    }
}
