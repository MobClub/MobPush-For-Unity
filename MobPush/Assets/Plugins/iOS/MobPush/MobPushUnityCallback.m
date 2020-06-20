//
//  MobPushUnityCallback.m
//  MobPushDemo
//
//  Created by LeeJay on 2018/5/11.
//  Copyright © 2018年 com.mob. All rights reserved.
//

#import "MobPushUnityCallback.h"
#import <MobPush/MobPush.h>
#import <MOBFoundation/MOBFJson.h>

@interface MobPushUnityCallback ()

@property (nonatomic, copy) NSString *observerStr;

@end

@implementation MobPushUnityCallback

+ (instancetype)defaultCallBack
{
    static id instance = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        instance = [[self alloc] init];
    });
    return instance;
}

- (void)addPushObserver:(NSString *)observer
{
    _observerStr = observer;
    [[NSNotificationCenter defaultCenter] addObserver:self
                                             selector:@selector(didReceiveMessage:) name:MobPushDidReceiveMessageNotification
                                               object:nil];
}

// 收到通知回调
- (void)didReceiveMessage:(NSNotification *)notification
{
    MPushMessage *message = notification.object;
    NSMutableDictionary *resultDict = [NSMutableDictionary dictionary];
    NSMutableDictionary *reslut = [NSMutableDictionary dictionary];

    switch (message.messageType)
    {
        case MPushMessageTypeCustom:
        {// 自定义消息
            [resultDict setObject:@0 forKey:@"action"];

//            if (message.notification.userInfo)
//            {
//                [reslut setObject:message.notification.userInfo forKey:@"extra"];
//            }
//
//            if (message.notification.body)
//            {
//                [reslut setObject:message.notification.body forKey:@"content"];
//            }
//
//            if (message.messageID)
//            {
//                [reslut setObject:message.messageID forKey:@"messageId"];
//            }
            [reslut addEntriesFromDictionary:message.convertDictionary];
        }
            break;
        case MPushMessageTypeAPNs:
        {// APNs 回调
//            [reslut addEntriesFromDictionary:message.notification.convertDictionary];
//            [reslut setObject:message.notification.body forKey:@"content"];
//
//            NSString *messageId = message.messageID;
//            if (messageId)
//            {
//                [reslut setObject:messageId forKey:@"messageId"];
//            }
//
//            if (message.notification.userInfo)
//            {
//                [reslut setObject:message.notification.userInfo forKey:@"extra"];
//            }
//            reslut[@"identifier"] = message.identifier;
            
            [reslut addEntriesFromDictionary:message.convertDictionary];
            
            [resultDict setObject:@1 forKey:@"action"];
        }
            break;
        case MPushMessageTypeLocal:
        { // 本地通知回调
            
//            [reslut addEntriesFromDictionary:message.notification.convertDictionary];
//            [reslut setObject:message.notification.body forKey:@"content"];
//
//            if (message.notification.userInfo)
//            {
//                [reslut setObject:message.notification.userInfo forKey:@"extras"];
//            }
//            reslut[@"identifier"] = message.identifier;
            
            [reslut addEntriesFromDictionary:message.convertDictionary];
            
            [resultDict setObject:@1 forKey:@"action"];
        }
            break;
            
        case MPushMessageTypeClicked:
        {
//            [reslut addEntriesFromDictionary:message.notification.convertDictionary];
//            [reslut setObject:message.notification.body forKey:@"content"];
//            if (message.notification.userInfo)
//            {
//                [reslut setObject:message.notification.userInfo forKey:@"extras"];
//            }
//            reslut[@"identifier"] = message.identifier;
            
            [reslut addEntriesFromDictionary:message.convertDictionary];
            
            [resultDict setObject:@2 forKey:@"action"];
            
        }
            break;
            
        default:
            break;
    }
    
    if (reslut.count)
    {
        [resultDict setObject:reslut forKey:@"result"];
    }
    
    if (resultDict.count)
    {
        NSString *resultStr = [MOBFJson jsonStringFromObject:resultDict];
        UnitySendMessage([_observerStr UTF8String], "_MobPushCallback", [resultStr UTF8String]);
    }
}

- (void)dealloc
{
    [[NSNotificationCenter defaultCenter] removeObserver:self];
}

@end
