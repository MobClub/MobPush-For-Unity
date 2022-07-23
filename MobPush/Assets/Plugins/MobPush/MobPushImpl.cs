using System;
using System.Collections;

namespace com.mob.mobpush
{
	public abstract class MobPushImpl
	{
		/// <summary>
		/// Init the PushSDK.
		/// <summary>
		public abstract void initPushSDK(string appKey, string appSecret);

		/// <summary>
		/// Add Push Receiver.
		/// <summary>
		public abstract void addPushReceiver();

		/// <summary>
		/// Stop Push.
		/// <summary>
		public abstract void stopPush();

		/// <summary>
		/// restart Push.
		/// <summary>
		public abstract void restartPush();

		/// <summary>
		/// isPushStop.
		/// <summary>
		public abstract bool isPushStopped();

		/// <summary>
		/// Set whether the app hides notifications when it runs in the foreground
		/// <summary>
		public abstract void setAppForegroundHiddenNotification(bool hidden);

		/// <summary>
		/// Set whether the app user agree mobtech privacy Protocol.
		/// <summary>
		public abstract void updatePrivacyPermissionStatus(bool agree);

		/// <summary>
		/// getRegistrationId.
		/// <summary>
		public abstract void getRegistrationId();

		/// <summary>
		/// add Tags.
		/// <summary>
		public abstract void addTags(string[] tags);

		/// <summary>
		/// Get Tags.
		/// <summary>
		public abstract void getTags();

		/// <summary>
		/// delete tags.
		/// <summary>
		public abstract void deleteTags(string[] tags);

		/// <summary>
		/// Determine weather the APP-Client of platform is valid.
		/// <summary>
		public abstract void cleanAllTags();

		/// <summary>
		/// add Alias.
		/// <summary>
		public abstract void addAlias(string alias);

		/// <summary>
		/// get Alias.
		/// <summary>
		public abstract void getAlias();

		/// <summary>
		/// Determine weather the APP-Client of platform is valid.
		/// <summary>
		public abstract void cleanAllAlias();

		/// <summary>
		/// Init the PushSDK.
		/// <summary>
		public abstract void setMobPushLocalNotification(LocalNotifyStyle style);

		/// <summary>
		/// CustomNotification.
		/// <summary>
		public abstract void setCustomNotification(CustomNotifyStyle style);

		/// <summary>
		/// bind phone num.
		/// <summary>
		public abstract void bindPhoneNum(string phoneNum);

		public abstract void req(int type, string content, int space, string extras);

		/// <summary>
		/// 删除通知中心的通知.
		/// <summary>
		public abstract void deleteLocalNotification(string[] ids);
		
		/// <summary>
		/// get mobtech privacy policy content(url/rich content)
		/// type 协议类型 (1= url类型, 2= 富文本类型)
		/// </summary>
		public abstract void getPrivacyPolicy (string type, string language); 


#if UNITY_IPHONE || UNITY_IOS
		/// <summary>
		/// set APNs 环境（ios only）
		/// <summary>
		public abstract void setAPNsForProduction(bool isPro);

		/// <summary>
		/// 设置角标（ios only）.
		/// <summary>
		public abstract void setBadge(int badge);

		/// <summary>
		/// 清除角标（ios only）
		/// <summary>
		public abstract void clearBadge();


#endif

#if UNITY_ANDROID
		/// <summary>
		/// Open the launch activity after click notification（Android Only）
		/// <summary>
		public abstract void setClickNotificationToLaunchPage(bool isOpen);

		/// <summary>
		/// set notification icon（Android Only）
		/// <summary>
		public abstract void setNotifyIcon(string resIcon);

		/// <summary>
		/// Set whether show badge（Android Only）.
		/// <summary>
		public abstract void setShowBadge(bool show);


#endif
	}
}