//
//  MobPushUnity3DBridge.m
//  MobPush
//
//  Created by LeeJay on 2018/5/10.
//  Copyright © 2018年 com.mob. All rights reserved.
//

#import "MobPushUnity3DBridge.h"
#import <MobPush/MobPush.h>
#import <MOBFoundation/MOBFJson.h>
#import <MobPush/MPushNotificationConfiguration.h>
#import <MobPush/MobPush+Test.h>
#import <MobPush/MPushMessage.h>
#import "MobPushUnityCallback.h"
#import <MOBFoundation/MobSDK.h>
#import <MOBFoundation/MobSDK+Privacy.h>

#if defined (__cplusplus)
extern "C" {
#endif
    
    extern void __iosMobPushSetAPNsForProduction (bool iosPro);
    
    extern void __iosMobAddPushReceiver (void *observer);
    
    extern void __iosMobPushSetupNotification (void *notification);

    extern void __iosMobPushAddLocalNotification (void *message);
    
    extern void __iosMobPushGetTags (void *observer);
    
    extern void __iosMobPushAddTags (void *tags, void *observer);
    
    extern void __iosMobPushDeleteTags (void *tags, void *observer);
    
    extern void __iosMobPushCleanAllTags (void *observer);
    
    extern void __iosMobPushGetAlias (void *observer);
    
    extern void __iosMobPushSetAlias (void *alias, void *observer);

    extern void __iosMobPushDeleteAlias (void *observer);

    extern void __iosMobPushGetRegistrationID (void *observer);
    
    extern void __iosMobPushSetBadge (int badge);
    
    extern void __iosMobPushClearBadge ();
    
    extern void __iosMobPushBindPhoneNum(void *phoneNum, void *observer);
    
    extern void __iosMobPushStopPush();
    
    extern void __iosMobPushRestartPush();
    
    extern bool __iosMobPushIsPushStopped();
    
    extern void __iosMobPushSendMessage (int type, void *content, int space, void *extras, void *observer);
    
    extern void __iosMobPushInitPushSDK (void *appKey, void *appScrect);
    
    extern void __iosMobPushDeleteLocalNotification (void *ids);
    
    extern void __iosMobPushSetAppForegroundHidden (bool hidden);
    
    extern void __iosUpdatePrivacyPermissionStatus (bool agree);

    extern void __iosGetPrivacyPolicy(void *type, void*language, void *observer);
    
    BOOL _iosPro;
    
    MPushNotificationConfiguration *__parseNotiConfigHashtable (void *notificationInfo);
    MPushMessage *__parseMessageHashtable (void *messageInfo);
    
    void __iosMobPushSetAppForegroundHidden (bool hidden)
    {
        if (hidden)
        {
            [MobPush setAPNsShowForegroundType:MPushAuthorizationOptionsNone];
        }
        else
        {
            [MobPush setAPNsShowForegroundType:MPushAuthorizationOptionsSound | MPushAuthorizationOptionsAlert | MPushAuthorizationOptionsBadge];
        }
    }
    
    void __iosUpdatePrivacyPermissionStatus (bool agree)
    {
        [MobSDK uploadPrivacyPermissionStatus:YES onResult:^(BOOL success) {
            if (success)
            {
                NSLog(@"[MobPush_Plugin]:隐私协议许可状态上传成功");
            }else{
                NSLog(@"[MobPush_Plugin]:隐私协议许可状态上传失败");
            }
        }];
    }
    
    void __iosMobPushDeleteLocalNotification (void *ids)
    {
        if (ids)
        {
            NSString *theParamsStr = [NSString stringWithCString:ids encoding:NSUTF8StringEncoding];
            NSArray *idParams = nil;
            
            if (theParamsStr)
            {
                idParams = [theParamsStr componentsSeparatedByString:@","];
            }
            [MobPush removeNotificationWithIdentifiers:idParams];
        }
        else
        {
            [MobPush removeNotificationWithIdentifiers:nil];
        }
    }
    
    void __iosMobPushInitPushSDK (void *appKey, void *appScrect)
    {
        NSString *appKeyStr = nil, *appScrectStr = nil;
        if (appKey)
        {
            appKeyStr = [NSString stringWithCString:appKey encoding:NSUTF8StringEncoding];
        }
        
        if (appScrect)
        {
            appScrectStr = [NSString stringWithCString:appScrect encoding:NSUTF8StringEncoding];
        }
        
        [MobSDK registerAppKey:appKeyStr appSecret:appScrectStr];
    }
    
    void __iosMobPushBindPhoneNum (void *phoneNum, void *observer)
    {
        NSString *observerStr = nil;
        if (observer)
        {
            observerStr = [NSString stringWithCString:observer encoding:NSUTF8StringEncoding];
        }
        
        NSString *phoneNumStr = nil;
        if (phoneNum)
        {
            phoneNumStr = [NSString stringWithCString:phoneNum encoding:NSUTF8StringEncoding];
        }
        [MobPush bindPhoneNum:phoneNumStr result:^(NSError *error) {
            NSMutableDictionary *resultDict = [NSMutableDictionary dictionary];
            
            if (error)
            {
                resultDict[@"result"] = @"0";
            }
            else
            {
                resultDict[@"result"] = @"1";
            }
            
            // 转成 json 字符串
            NSString *resultStr = [MOBFJson jsonStringFromObject:resultDict];
            
            UnitySendMessage([observerStr UTF8String], "_MobPushBindPhoneNumCallback", [resultStr UTF8String]);
        }];
    }
    
    void __iosMobPushStopPush ()
    {
        [MobPush stopPush];
    }
    
    void __iosMobPushRestartPush ()
    {
        [MobPush restartPush];
    }
    
    bool __iosMobPushIsPushStopped ()
    {
        return [MobPush isPushStopped] ? true : false;
    }
    
    void __iosMobPushSetBadge (int badge)
    {
        [UIApplication sharedApplication].applicationIconBadgeNumber = badge;
        [MobPush setBadge:(NSInteger)badge];
    }
    
    void __iosMobPushClearBadge ()
    {
        [MobPush clearBadge];
    }
    
    void __iosMobPushSetAPNsForProduction (bool iosPro)
    {
        _iosPro = iosPro == true ? YES : NO;
        [MobPush setAPNsForProduction:_iosPro];

        NSLog(@"[MobPush_Plugin]: %@", [MobSDK version]);
    }
    
    void __iosMobAddPushReceiver(void *observer)
    {
        NSString *observerStr = nil;
        if (observer)
        {
            observerStr = [NSString stringWithCString:observer encoding:NSUTF8StringEncoding]; 
        }
        [[MobPushUnityCallback defaultCallBack] addPushObserver:observerStr];
    }
    
    void __iosMobPushSetupNotification (void *notification)
    {
        MPushNotificationConfiguration *config = __parseNotiConfigHashtable(notification);
        [MobPush setupNotification:config];
    }
    
    extern void __iosMobPushAddLocalNotification (void *messageInfo)
    {
        MPushMessage *message = __parseMessageHashtable(messageInfo);
        [MobPush addLocalNotification:message];
    }
    
    extern void __iosMobPushGetTags (void *observer)
    {
        NSString *observerStr = nil;
        if (observer)
        {
            observerStr = [NSString stringWithCString:observer encoding:NSUTF8StringEncoding];
        }
        
        [MobPush getTagsWithResult:^(NSArray *tags, NSError *error) {
            
            NSMutableDictionary *resultDict = [NSMutableDictionary dictionary];
            // action = 3 ，操作 tag
            [resultDict setObject:@3 forKey:@"action"];
            // operation = 0 获取
            [resultDict setObject:@0 forKey:@"operation"];
            if (error)
            {
                [resultDict setObject:@(error.code) forKey:@"errorCode"];
            }
            else
            {
                if (tags.count)
                {
                    NSString *tagStr = [tags componentsJoinedByString:@","];
                    
                    [resultDict setObject:tagStr forKey:@"tags"];
                }
                [resultDict setObject:@(0) forKey:@"errorCode"];
            }
            // 转成 json 字符串
            NSString *resultStr = [MOBFJson jsonStringFromObject:resultDict];
            UnitySendMessage([observerStr UTF8String], "_MobPushCallback", [resultStr UTF8String]);
        }];
    }
    
    extern void __iosMobPushAddTags (void *tags, void *observer)
    {
        NSString *theParamsStr = [NSString stringWithCString:tags encoding:NSUTF8StringEncoding];
        NSArray *tagParams = nil;
        
        if (theParamsStr)
        {
            tagParams = [theParamsStr componentsSeparatedByString:@","];
        }
        
        NSString *observerStr = nil;
        if (observer)
        {
            observerStr = [NSString stringWithCString:observer encoding:NSUTF8StringEncoding];
        }
        [MobPush addTags:tagParams result:^(NSError *error) {
            
            NSMutableDictionary *resultDict = [NSMutableDictionary dictionary];
            // action = 3 ，操作 tag
            [resultDict setObject:@3 forKey:@"action"];
            // operation = 1 设置
            [resultDict setObject:@1 forKey:@"operation"];
            if (error)
            {
                [resultDict setObject:@(error.code) forKey:@"errorCode"];
            }
            else
            {
                [resultDict setObject:@(0) forKey:@"errorCode"];
            }
            // 转成 json 字符串
            NSString *resultStr = [MOBFJson jsonStringFromObject:resultDict];
            UnitySendMessage([observerStr UTF8String], "_MobPushCallback", [resultStr UTF8String]);
            
        }];
    }
    
    extern void __iosMobPushDeleteTags (void *tags, void *observer)
    {
        NSString *theParamsStr = [NSString stringWithCString:tags encoding:NSUTF8StringEncoding];
        NSArray *tagParams = nil;
        
        if (theParamsStr)
        {
            tagParams = [theParamsStr componentsSeparatedByString:@","];
        }
        
        NSString *observerStr = nil;
        if (observer)
        {
            observerStr = [NSString stringWithCString:observer encoding:NSUTF8StringEncoding];
        }
        
        [MobPush deleteTags:tagParams result:^(NSError *error) {
            
            NSMutableDictionary *resultDict = [NSMutableDictionary dictionary];
            // action = 3 ，操作 tag
            [resultDict setObject:@3 forKey:@"action"];
            // operation = 2 删除
            [resultDict setObject:@2 forKey:@"operation"];
            if (error)
            {
                [resultDict setObject:@(error.code) forKey:@"errorCode"];
            }
            else
            {
                [resultDict setObject:@(0) forKey:@"errorCode"];
            }
            // 转成 json 字符串
            NSString *resultStr = [MOBFJson jsonStringFromObject:resultDict];
            UnitySendMessage([observerStr UTF8String], "_MobPushCallback", [resultStr UTF8String]);
            
        }];
    }
    
    extern void __iosMobPushCleanAllTags (void *observer)
    {
        NSString *observerStr = nil;
        if (observer)
        {
            observerStr = [NSString stringWithCString:observer encoding:NSUTF8StringEncoding];
        }
        
        [MobPush cleanAllTags:^(NSError *error) {
            
            NSMutableDictionary *resultDict = [NSMutableDictionary dictionary];
            // action = 3 ，操作 tag
            [resultDict setObject:@3 forKey:@"action"];
            // operation = 3 清空
            [resultDict setObject:@3 forKey:@"operation"];
            if (error)
            {
                [resultDict setObject:@(error.code) forKey:@"errorCode"];
            }
            else
            {
                [resultDict setObject:@(0) forKey:@"errorCode"];
            }
            // 转成 json 字符串
            NSString *resultStr = [MOBFJson jsonStringFromObject:resultDict];
            UnitySendMessage([observerStr UTF8String], "_MobPushCallback", [resultStr UTF8String]);
        }];
    }
    
    extern void __iosMobPushGetAlias (void *observer)
    {
        NSString *observerStr = nil;
        if (observer)
        {
            observerStr = [NSString stringWithCString:observer encoding:NSUTF8StringEncoding];
        }
        
        [MobPush getAliasWithResult:^(NSString *alias, NSError *error) {
            
            NSMutableDictionary *resultDict = [NSMutableDictionary dictionary];
            // action = 4 ，操作 alias
            [resultDict setObject:@4 forKey:@"action"];
            // operation = 0 获取
            [resultDict setObject:@0 forKey:@"operation"];
            if (error)
            {
                [resultDict setObject:@(error.code) forKey:@"errorCode"];
            }
            else
            {
                if (alias)
                {
                    [resultDict setObject:alias forKey:@"alias"];
                }
                [resultDict setObject:@(0) forKey:@"errorCode"];
            }
            // 转成 json 字符串
            NSString *resultStr = [MOBFJson jsonStringFromObject:resultDict];
            UnitySendMessage([observerStr UTF8String], "_MobPushCallback", [resultStr UTF8String]);
        }];
    }
    
    extern void __iosMobPushSetAlias (void *alias, void *observer)
    {
        NSString *aliasParam = [NSString stringWithCString:alias encoding:NSUTF8StringEncoding];
        
        NSString *observerStr = nil;
        if (observer)
        {
            observerStr = [NSString stringWithCString:observer encoding:NSUTF8StringEncoding];
        }
        
        [MobPush setAlias:aliasParam result:^(NSError *error) {
            
            NSMutableDictionary *resultDict = [NSMutableDictionary dictionary];
            // action = 4 ，操作 alias
            [resultDict setObject:@4 forKey:@"action"];
            // operation = 1 设置
            [resultDict setObject:@1 forKey:@"operation"];
            if (error)
            {
                [resultDict setObject:@(error.code) forKey:@"errorCode"];
            }
            else
            {
                [resultDict setObject:@(0) forKey:@"errorCode"];
            }
            // 转成 json 字符串
            NSString *resultStr = [MOBFJson jsonStringFromObject:resultDict];
            UnitySendMessage([observerStr UTF8String], "_MobPushCallback", [resultStr UTF8String]);
        }];
    }
    
    extern void __iosMobPushDeleteAlias (void *observer)
    {
        NSString *observerStr = nil;
        if (observer)
        {
            observerStr = [NSString stringWithCString:observer encoding:NSUTF8StringEncoding];
        }
        
        [MobPush deleteAlias:^(NSError *error) {
            NSMutableDictionary *resultDict = [NSMutableDictionary dictionary];
            // action = 4 ，操作 alias
            [resultDict setObject:@4 forKey:@"action"];
            // operation = 2 删除
            [resultDict setObject:@2 forKey:@"operation"];
            if (error)
            {
                [resultDict setObject:@(error.code) forKey:@"errorCode"];
            }
            else
            {
                [resultDict setObject:@(0) forKey:@"errorCode"];
            }
            // 转成 json 字符串
            NSString *resultStr = [MOBFJson jsonStringFromObject:resultDict];
            UnitySendMessage([observerStr UTF8String], "_MobPushCallback", [resultStr UTF8String]);
        }];
    }
    
    extern void __iosMobPushGetRegistrationID (void *observer)
    {
        NSString *observerStr = nil;
        if (observer)
        {
            observerStr = [NSString stringWithCString:observer encoding:NSUTF8StringEncoding];
        }
        
        [MobPush getRegistrationID:^(NSString *registrationID, NSError *error) {
            if (registrationID)
            {
                UnitySendMessage([observerStr UTF8String], "_MobPushRegIdCallback", [registrationID UTF8String]);
            }
        }];
    }

    extern void __iosMobPushSendMessage (int type, void *content, int space, void *extras, void *observer)
    {
        NSString *contentParam = [NSString stringWithCString:content encoding:NSUTF8StringEncoding];

        NSDictionary *extrasDict = nil;
        if (extras)
        {
            NSString *theParam = [NSString stringWithCString:extras encoding:NSUTF8StringEncoding];
            extrasDict = [MOBFJson objectFromJSONString:theParam];
        }
        
        NSString *observerStr = nil;
        if (observer)
        {
            observerStr = [NSString stringWithCString:observer encoding:NSUTF8StringEncoding];
        }
        
        [MobPush sendMessageWithMessageType:type
                                    content:contentParam
                                      space:@(space)
                                      sound:nil
                    isProductionEnvironment:_iosPro
                                     extras:extrasDict
                                 linkScheme:@""
                                   linkData:@""
                                    coverId:nil
                                     result:^(NSString *workId, NSError *error) {
                            
                                         NSMutableDictionary *resultDict = [NSMutableDictionary dictionary];
                                        
                                         if (error)
                                         {
                                             [resultDict setObject:@(0) forKey:@"action"];
                                         }
                                         else
                                         {
                                             [resultDict setObject:@(1) forKey:@"action"];
                                         }
                                         // 转成 json 字符串
                                         NSString *resultStr = [MOBFJson jsonStringFromObject:resultDict];
                                         UnitySendMessage([observerStr UTF8String], "_MobPushDemoCallback", [resultStr UTF8String]);
                                         
                                     }];
    }
    
    MPushNotificationConfiguration *__parseNotiConfigHashtable (void *notificationInfo)
    {
        NSString *theParamsStr = [NSString stringWithCString:notificationInfo encoding:NSUTF8StringEncoding];
        NSDictionary *eventParams = [MOBFJson objectFromJSONString:theParamsStr];
        
        MPushNotificationConfiguration *config = [[MPushNotificationConfiguration alloc] init];
        config.types = (MPushAuthorizationOptions)[eventParams[@"type"] integerValue];
        return config;
    }

    MPushMessage *__parseMessageHashtable (void *messageInfo)
    {
        NSString *theParamsStr = [NSString stringWithCString:messageInfo encoding:NSUTF8StringEncoding];
        NSDictionary *eventParams = [MOBFJson objectFromJSONString:theParamsStr];
        
        MPushMessage *message = [[MPushMessage alloc] init];
        message.messageType = MPushMessageTypeLocal;
        message.identifier = eventParams[@"id"];
        message.extraInfomation = [MOBFJson objectFromJSONString:eventParams[@"extras"]];
        MPushNotification *noti = [[MPushNotification alloc] init];
        
        noti.title = eventParams[@"title"];
        noti.body = eventParams[@"content"];
        noti.sound = eventParams[@"sound"];
        noti.badge = [eventParams[@"badge"] integerValue];
        noti.subTitle = eventParams[@"subTitle"];
        
        long timeStamp = [eventParams[@"timeStamp"] longValue];
        if (timeStamp == 0)
        {
            message.isInstantMessage = YES;
        }
        else
        {
            NSDate *currentDate = [NSDate dateWithTimeIntervalSinceNow:0];
            NSTimeInterval nowtime = [currentDate timeIntervalSince1970] * 1000;
            message.taskDate = nowtime + (NSTimeInterval)timeStamp;
        }
        
        message.notification = noti;
        
        return message;
    }

    extern void __iosGetPrivacyPolicy(void *type, void *language, void *observer)
    {
        NSString *observerStr = nil;
        if (observer)
        {
            observerStr = [NSString stringWithCString:observer encoding:NSUTF8StringEncoding];
        }
        
        NSString *typeParam = [NSString stringWithCString:type encoding:NSUTF8StringEncoding];
        NSString *languageParam = [NSString stringWithCString:language encoding:NSUTF8StringEncoding];

        [MobSDK getPrivacyPolicy:typeParam language:languageParam compeletion:^(NSDictionary * _Nullable data, NSError * _Nullable error) {
            if (error == nil)
            {
                NSMutableDictionary *resultDict = [NSMutableDictionary dictionary];
                // action = 5 ，获取隐私协议
                // [resultDict setObject:@5 forKey:@"action"];
                if (error)
                {
                    [resultDict setObject:@(error.code) forKey:@"errorCode"];
                }
                else
                {
                    [resultDict setObject:@(0) forKey:@"errorCode"];
                }

                if (data)
                {
                    // 转成 json 字符串
                    NSString *dataStr = [MOBFJson jsonStringFromObject:data];
                    [resultDict setObject:dataStr forKey:@"data"];
                }
                // 转成 json 字符串
                NSString *resultStr = [MOBFJson jsonStringFromObject:resultDict];
                UnitySendMessage([observerStr UTF8String], "_MobPushPrivacyPolicyCallback", [resultStr UTF8String]);
            }
        }];
    }
    
#if defined (__cplusplus)
}
#endif

@implementation MobPushUnity3DBridge

@end
