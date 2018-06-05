//
//  SpectraDetector.h
//  SpectraDetector
//
//  Created by Banafsheh on 5/7/18.
//  Copyright © 2018 DIYSpec. All rights reserved.
//




@interface SpectraDetector : NSObject
- (id) initWithSpectraCascade:(NSString*) filePath;
- (NSArray*) detectSpectras:(UIImage*) image;
@end


