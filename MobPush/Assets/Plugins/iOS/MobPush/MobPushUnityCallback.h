//
//  MobPushUnityCallback.h
//  MobPushDemo
//
//  Created by LeeJay on 2018/5/11.
//  Copyright © 2018年 com.mob. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface MobPushUnityCallback : NSObject

+ (instancetype)defaultCallBack;

- (void)addPushObserver:(NSString *)observer;

@end
