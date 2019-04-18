using System;
using System.Collections;


namespace com.mob.mobpush{
	public abstract class MobPushImpl {

		/// <summary>
		/// Init the PushSDK.
		/// </summary>
		public abstract void initPushSDK (string appKey, string appScrect);


		/// <summary>
		/// Add push receiver
		/// </summary>
		public abstract void addPushReceiver ();

		/// <summary>
		/// 设置通知是否在前台展示
		/// </summary>
        public abstract void setAppForegroundHiddenNotification (bool hidden);
       
#if UNITY_IPHONE
		
		/// <summary>
        /// set APNs 环境（ios only）
        /// </summary>
		public abstract void setAPNsForProduction (bool isPro);

		/// <summary>
        /// 设置角标（ios only）
        /// </summary>
		public abstract void setBadge(int badge);

		/// <summary>
        /// 清除角标 （ios only）
        /// </summary>
		public abstract void clearBadge();

		/// <summary>
        /// 删除通知栏指定通知和未发送的定时通知，ids 是数组，如果 ids 为空，则清空所有通知 （ios only）
        /// </summary>
		public abstract void deleteLocalNotification(string[] ids);
 

#elif UNITY_ANDROID
		

		/// <summary>
		/// Open the launch activity after click notification（Android Only）
		/// default true.
		/// </summary>
		public abstract void setClickNotificationToLaunchPage (bool isOpen);
        /// <summary>
		/// set notification icon（Android Only）
		/// </summary>
        public abstract void setNotifyIcon (string resIcon);

#endif

        /// <summary>
		/// stop push 
		/// </summary>
		public abstract void stopPush ();

		/// <summary>
		/// restart Push
		/// </summary>
		public abstract void restartPush ();

		/// <summary>
		/// isPushStop
		/// </summary>
		public abstract bool isPushStopped ();

        /// <summary>
        /// getRegistrationId.
        /// </summary>
        public abstract void getRegistrationId ();

		/// <summary>
		/// add Tags.
		/// </summary>
		public abstract void addTags (string[] tags);
		
		/// <summary>
		/// get Tags.
		/// </summary>
		public abstract void getTags ();

		/// <summary>
		/// delete tags.
		/// </summary>
		public abstract void deleteTags (string[] tags);

		/// <summary>
		/// Determine weather the APP-Client of platform is valid.
		/// </summary>
		public abstract void cleanAllTags ();
		
		/// <summary>
		/// add Alias.
		/// </summary>
		public abstract void addAlias (string alias);
		
		/// <summary>
		/// get Alias.
		/// </summary>
		public abstract void getAlias ();

		/// <summary>
		/// Determine weather the APP-Client of platform is valid.
		/// </summary>
		public abstract void cleanAllAlias();

		/// <summary>
		/// LocalNotification.
		/// </summary>
		public abstract void setMobPushLocalNotification (LocalNotifyStyle style);

		/// <summary>
		/// CustomNotification.
		/// </summary>
		public abstract void setCustomNotification (CustomNotifyStyle style);

        /// <summary>
        /// bind phone num.
        /// </summary>
        public abstract void bindPhoneNum(string phoneNum);

        /// <summary>
        /// demo Req.
        /// type notify:1  AppNotify:2  Delayed:3
        /// iosProduction (ios only)
        /// </summary>
        public abstract void req (int type,  String content, int space, String extras);

	}
}

