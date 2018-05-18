//
//  UnityAppController+MobPushInit.m
//  MobPushDemo
//
//  Created by LeeJay on 2018/5/11.
//  Copyright © 2018年 com.mob. All rights reserved.
//

#import "UnityAppController+MobPushInit.h"
#import "MobPushUnityCallback.h"

@implementation UnityAppController (MobPushInit)

+ (void)initialize
{
    [[NSNotificationCenter defaultCenter] addObserver:[MobPushUnityCallback defaultCallBack]
                                             selector:@selector(didFinishLaunching)
                                                 name:UIApplicationDidFinishLaunchingNotification
                                               object:nil];
}

- (void)dealloc
{
    [[NSNotificationCenter defaultCenter] removeObserver:[MobPushUnityCallback defaultCallBack]];
}

@end
