using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

namespace com.mob.mobpush
{
	#if UNITY_IPHONE
	public class iOSMobPushImpl : MobPushImpl {

		private static string _gameObjectName;

		[DllImport("__Internal")]
		private static extern void __iosMobPushSetAPNsForProduction (bool isPro);

		[DllImport("__Internal")]
		private static extern void __iosMobAddPushReceiver (string observer);

		[DllImport("__Internal")]
		private static extern void __iosMobPushSetupNotification (string notification);

		[DllImport("__Internal")]
		private static extern void __iosMobPushAddLocalNotification (string message);

		[DllImport("__Internal")]
		private static extern void __iosMobPushGetTags (string observer);

		[DllImport("__Internal")]
		private static extern void __iosMobPushAddTags (string tags, string observer);

		[DllImport("__Internal")]
		private static extern void __iosMobPushDeleteTags (string tags, string observer);

		[DllImport("__Internal")]
		private static extern void __iosMobPushCleanAllTags (string observer);

		[DllImport("__Internal")]
		private static extern void __iosMobPushGetAlias (string observer);

		[DllImport("__Internal")]
		private static extern void __iosMobPushSetAlias (string alias, string observer);

		[DllImport("__Internal")]
		private static extern void __iosMobPushDeleteAlias (string observer);

		[DllImport("__Internal")]
		private static extern void __iosMobPushGetRegistrationID (string observer);

		[DllImport("__Internal")]
		private static extern void __iosMobPushSetBadge(int badge);

		[DllImport("__Internal")]
		private static extern void __iosMobPushClearBadge();

		[DllImport("__Internal")]
		private static extern void __iosMobPushBindPhoneNum(string phoneNum, string observer);

		[DllImport("__Internal")]
		private static extern void __iosMobPushStopPush();

		[DllImport("__Internal")]
		private static extern void __iosMobPushRestartPush();

		[DllImport("__Internal")]
		private static extern bool __iosMobPushIsPushStopped();

		[DllImport("__Internal")]
		private static extern void __iosMobPushInitPushSDK(string appKey, string appScrect);

		[DllImport("__Internal")]
		private static extern void __iosMobPushSendMessage (int type, string content, int space, string extras, string observer);

		[DllImport("__Internal")]
		private static extern void __iosMobPushSetAppForegroundHidden(bool hidden);

		[DllImport("__Internal")]
		private static extern void __iosMobPushDeleteLocalNotification(string[] ids);

		[DllImport("__Internal")]
		private static extern void __iosUpdatePrivacyPermissionStatus(bool agree);

		public iOSMobPushImpl (GameObject go)
		{
			Debug.Log("iOSMobPushImpl  ===>>>  iOSMobPushImpl" + go.name);
			_gameObjectName = go.name;
		}

		public override void deleteLocalNotification (string[] ids)
		{
			__iosMobPushDeleteLocalNotification(ids);
		}

		public override void setAppForegroundHiddenNotification (bool hidden)
		{
			__iosMobPushSetAppForegroundHidden(hidden);
		}

		public override void updatePrivacyPermissionStatus (bool agree)
		{
			__iosUpdatePrivacyPermissionStatus(agree);
		}

		public override void initPushSDK (string appKey, string appScrect)
		{
			 __iosMobPushInitPushSDK(appKey, appScrect);
		}
		
		public override void setAPNsForProduction (bool isPro)
		{
			__iosMobPushSetAPNsForProduction(isPro);
		}

		public override void addPushReceiver ()
		{
			Debug.Log("iOSImpl  ===>>>  addPushReceiver === ");
			__iosMobAddPushReceiver(_gameObjectName);
		}

		public override void getRegistrationId ()
		{
			__iosMobPushGetRegistrationID(_gameObjectName);
		}

		public override void addTags (string[] tags)
		{
			string stringTags = String.Join (",", tags);
			__iosMobPushAddTags(stringTags, _gameObjectName);
		}

		public override void getTags ()
		{
			__iosMobPushGetTags(_gameObjectName);
		}

		public override void deleteTags (string[] tags)
		{
			string stringTags = String.Join (",", tags);
			__iosMobPushDeleteTags(stringTags, _gameObjectName);
		}

		public override void cleanAllTags ()
		{
			__iosMobPushCleanAllTags(_gameObjectName);
		}
			
		public override void addAlias (string alias)
		{
			__iosMobPushSetAlias(alias, _gameObjectName);
		}

		public override void getAlias ()
		{
			__iosMobPushGetAlias(_gameObjectName);
		}

		public override void cleanAllAlias ()
		{
			__iosMobPushDeleteAlias(_gameObjectName);
		}

		public override void setMobPushLocalNotification (LocalNotifyStyle style)
		{
			String reqJson = style.getStyleParamsStr ();
			Debug.Log("iOSImpl  ===>>>  setMobPushLocalNotification === " + reqJson);
			__iosMobPushAddLocalNotification(reqJson);
		}

		public override void setCustomNotification (CustomNotifyStyle style)
		{
			String reqJson = style.getStyleParamsStr ();
			Debug.Log("iOSImpl  ===>>>  setCustomNotification === " + reqJson);
			__iosMobPushSetupNotification(reqJson);
		}

		public override void req (int type, string content, int space, string extras)
		{
			__iosMobPushSendMessage(type, content, space, extras, _gameObjectName);
		}

		public override void setBadge(int badge) 
		{
			__iosMobPushSetBadge(badge);
		}

		public override void clearBadge() 
		{
			__iosMobPushClearBadge();
		} 

		public override void bindPhoneNum(string phoneNum)
		{
			__iosMobPushBindPhoneNum(phoneNum, _gameObjectName);
		}

		public override void stopPush()
		{
			__iosMobPushStopPush();
		}

		public override void restartPush()
		{
			__iosMobPushRestartPush();
		}

		public override bool isPushStopped()
		{
			return __iosMobPushIsPushStopped();
		}

	}
	#endif
}