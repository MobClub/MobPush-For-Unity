//
//  MOBFHookService.h
//  MOBFoundation
//
//  Created by liyc on 2017/12/7.
//  Copyright © 2017年 MOB. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <objc/message.h>

@interface MOBFHookService : NSObject

/**
 Hook钩子

 @param cls 要钩取的类
 @param dulRawSEL 要钩取的类的方法
 @param targetCls 目标类：实现本地方法的类
 @param newSEL 新方法
 @param holderSEL 占位方法
 */
+ (void)hookRawClass:(Class)cls
              rawSEL:(SEL)dulRawSEL
         targetClass:(Class)targetCls
              newSEL:(SEL)newSEL
      placeHolderSEL:(SEL)holderSEL;


@end
