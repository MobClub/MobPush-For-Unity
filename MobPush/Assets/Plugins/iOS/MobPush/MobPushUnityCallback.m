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

            if (message.extraInfomation)
            {
                [reslut setObject:message.extraInfomation forKey:@"extra"];
            }
            
            if (message.content)
            {
                [reslut setObject:message.content forKey:@"content"];
            }
            
            if (message.messageID)
            {
                [reslut setObject:message.messageID forKey:@"messageId"];
            }
            
            if (message.currentServerTimestamp)
            {
                [reslut setObject:@(message.currentServerTimestamp) forKey:@"timeStamp"];
            }
        }
            break;
        case MPushMessageTypeAPNs:
        {// APNs 回调
            /*
            {
                1 = 2;
                aps =     {
                    alert =         {
                        body = 1;
                        subtitle = 1;
                        title = 1;
                    };
                    "content-available" = 1;
                    "mutable-content" = 1;
                };
                mobpushMessageId = 159346875878223872;
            }
             */
            if (message.msgInfo)
            {
                NSDictionary *aps = message.msgInfo[@"aps"];
                if ([aps isKindOfClass:[NSDictionary class]])
                {
                    NSDictionary *alert = aps[@"alert"];
                    if ([alert isKindOfClass:[NSDictionary class]])
                    {
                        NSString *body = alert[@"body"];
                        if (body)
                        {
                            [reslut setObject:body forKey:@"content"];
                        }
                        
                        NSString *subtitle = alert[@"subtitle"];
                        if (subtitle)
                        {
                            [reslut setObject:subtitle forKey:@"subtitle"];
                        }
                        
                        NSString *title = alert[@"title"];
                        if (title)
                        {
                            [reslut setObject:title forKey:@"title"];
                        }
                    }
                    
                    NSString *sound = aps[@"sound"];
                    if (sound)
                    {
                        [reslut setObject:sound forKey:@"sound"];
                    }
                    
                    NSInteger badge = [aps[@"badge"] integerValue];
                    if (badge)
                    {
                        [reslut setObject:@(badge) forKey:@"badge"];
                    }
                    
                }
            }
            
            NSString *messageId = message.msgInfo[@"mobpushMessageId"];
            if (messageId)
            {
                [reslut setObject:messageId forKey:@"messageId"];
            }
            
            NSMutableDictionary *extra = [NSMutableDictionary dictionary];
            [message.msgInfo enumerateKeysAndObjectsUsingBlock:^(id  _Nonnull key, id  _Nonnull obj, BOOL * _Nonnull stop) {
               
                if (![key isEqualToString:@"aps"] && ![key isEqualToString:@"mobpushMessageId"])
                {
                    [extra setObject:obj forKey:key];
                }
                
            }];
            
            if (extra.count)
            {
                [reslut setObject:extra forKey:@"extra"];
            }
            
            [resultDict setObject:@1 forKey:@"action"];
            
            //            if ([UIApplication sharedApplication].applicationState == UIApplicationStateActive)
            //            {
            //                // 前台收到
            //                [resultDict setObject:@1 forKey:@"action"];
            //            }
            //            else
            //            {
            //                // 点击通知
            //                [resultDict setObject:@2 forKey:@"action"];
            //            }
        }
            break;
        case MPushMessageTypeLocal:
        { // 本地通知回调
            NSString *body = message.notification.body;
            NSString *title = message.notification.title;
            NSString *subtitle = message.notification.subTitle;
            NSInteger badge = message.notification.badge;
            NSString *sound = message.notification.sound;
            if (body)
            {
                [reslut setObject:body forKey:@"content"];
            }
            
            if (title)
            {
                [reslut setObject:title forKey:@"title"];
            }
            
            if (subtitle)
            {
                [reslut setObject:subtitle forKey:@"subtitle"];
            }
            
            if (badge)
            {
                [reslut setObject:@(badge) forKey:@"badge"];
            }
            
            if (sound)
            {
                [reslut setObject:sound forKey:@"sound"];
            }
            
            
            [resultDict setObject:@1 forKey:@"action"];
            
//            if ([UIApplication sharedApplication].applicationState == UIApplicationStateActive)
//            {
//                // 前台收到
//                [resultDict setObject:@1 forKey:@"action"];
//            }
//            else
//            {
//                // 点击通知
//                [resultDict setObject:@2 forKey:@"action"];
//            }
        }
            break;
            
        case MPushMessageTypeClicked:
        {
//            action = 2;
            
            if (message.msgInfo)
            {
                NSDictionary *aps = message.msgInfo[@"aps"];
                if ([aps isKindOfClass:[NSDictionary class]])
                {
                    NSDictionary *alert = aps[@"alert"];
                    if ([alert isKindOfClass:[NSDictionary class]])
                    {
                        NSString *body = alert[@"body"];
                        if (body)
                        {
                            [reslut setObject:body forKey:@"content"];
                        }
                        
                        NSString *subtitle = alert[@"subtitle"];
                        if (subtitle)
                        {
                            [reslut setObject:subtitle forKey:@"subtitle"];
                        }
                        
                        NSString *title = alert[@"title"];
                        if (title)
                        {
                            [reslut setObject:title forKey:@"title"];
                        }
                    }
                    
                    NSString *sound = aps[@"sound"];
                    if (sound)
                    {
                        [reslut setObject:sound forKey:@"sound"];
                    }
                    
                    NSInteger badge = [aps[@"badge"] integerValue];
                    if (badge)
                    {
                        [reslut setObject:@(badge) forKey:@"badge"];
                    }
                    
                }
                
                NSString *messageId = message.msgInfo[@"mobpushMessageId"];
                if (messageId)
                {
                    [reslut setObject:messageId forKey:@"messageId"];
                }
                
                NSMutableDictionary *extra = [NSMutableDictionary dictionary];
                [message.msgInfo enumerateKeysAndObjectsUsingBlock:^(id  _Nonnull key, id  _Nonnull obj, BOOL * _Nonnull stop) {
                    
                    if (![key isEqualToString:@"aps"] && ![key isEqualToString:@"mobpushMessageId"])
                    {
                        [extra setObject:obj forKey:key];
                    }
                    
                }];
                
                if (extra.count)
                {
                    [reslut setObject:extra forKey:@"extra"];
                }
                
                [resultDict setObject:@2 forKey:@"action"];
            }
            else
            {
                NSString *body = message.notification.body;
                NSString *title = message.notification.title;
                NSString *subtitle = message.notification.subTitle;
                NSInteger badge = message.notification.badge;
                NSString *sound = message.notification.sound;
                if (body)
                {
                    [reslut setObject:body forKey:@"content"];
                }
                
                if (title)
                {
                    [reslut setObject:title forKey:@"title"];
                }
                
                if (subtitle)
                {
                    [reslut setObject:subtitle forKey:@"subtitle"];
                }
                
                if (badge)
                {
                    [reslut setObject:@(badge) forKey:@"badge"];
                }
                
                if (sound)
                {
                    [reslut setObject:sound forKey:@"sound"];
                }
                
                [resultDict setObject:@2 forKey:@"action"];
            }
            
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
