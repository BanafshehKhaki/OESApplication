//
//  SpectraDetector.m
//  SpectraDetector
//
//  Created by Banafsheh on 5/7/18.
//  Copyright © 2018 DIYSpec. All rights reserved.
//

#import "SpectraDetector.h"
#import "OpenCVUtils.h"


@interface SpectraDetector() {
    cv::CascadeClassifier _spectraCascade;
}

@end

@implementation SpectraDetector
- (id) initWithSpectraCascade:(NSString*) filePath {
    self = [super init];
    if (self)   {
        _spectraCascade = cv::CascadeClassifier([filePath UTF8String]);
    }
    return self;
}
- (NSArray*) detectSpectras:(UIImage*) image   {
    //detect faces with C code
    std::vector<cv::Rect> faces;
    cv::Mat matImage = [OpenCVUtils cvMatFromUIImage:image];
    cv::Size minSize = cv::Size(60, 60);
    _spectraCascade.detectMultiScale(matImage, faces, 1.1, 2, CV_HAAR_FIND_BIGGEST_OBJECT | CV_HAAR_DO_ROUGH_SEARCH, minSize);
    
    //convert cv::vector<cv::Rect> to NSArray of CGRect
    NSMutableArray *arrFaces = [[NSMutableArray alloc] init];
    
    for(std::vector<cv::Rect>::iterator it = faces.begin(); it != faces.end(); ++it) {
        CGRect rect = [OpenCVUtils cgRectFromCVRect:*it];
        NSValue *value = [NSValue valueWithCGRect:rect];
        [arrFaces addObject:value];
    }
    
    
//    [arrFaces addObject:[NSValue valueWithCGPoint:CGPointMake(0, 50)]];
    return arrFaces;
}

@end
