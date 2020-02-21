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
		/// Set whether the app hides notifications when it runs in the foreground
		/// </summary>
        public abstract void setAppForegroundHiddenNotification (bool hidden); 

       	/// <summary>
		/// Set whether the app user agree mobtech privacy Protocol
		/// </summary>
        public abstract void updatePrivacyPermissionStatus (bool agree); 

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
        /// 清除角标（ios only）
        /// </summary>
		public abstract void clearBadge();

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
   
        /// <summary>
		/// Set whether show badge（Android Only）
		/// </summary>
        public abstract void setShowBadge (bool show);
#endif

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

        /// <summary>
        /// 删除通知中心的通知
        /// </summary>
        public abstract void deleteLocalNotification(string[] ids);

    }
}

