using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.mob.mobpush
{
#if UNITY_ANDROID
	public class AndroidImpl : MobPushImpl {
		private AndroidJavaObject javaObj;

		public AndroidImpl (GameObject go) {
			Debug.Log("AndroidImpl  ===>>>  AndroidImpl" );
			try{
				javaObj = new AndroidJavaObject("com.mob.unity3d.MobPushUtils", go.name, "_MobPushCallback", "_MobPushDemoCallback", "_MobPushRegIdCallback", "_MobPushBindPhoneNumCallback");
			} catch(Exception e) {
				Console.WriteLine("{0} Exception caught.", e);
			}
		}

		public override void initPushSDK (string appKey, string appScrect){
			Debug.Log("AndroidImpl  ===>>>  InitPushSDK === appKey:" + appKey + "   AppSecrect:" + appScrect);
			if(javaObj != null){
				javaObj.Call ("initPushSDK", appKey, appScrect);
			}
		}

		public override void addPushReceiver (){
			Debug.Log("AndroidImpl  ===>>>  addPushReceiver === ");
			if(javaObj != null){
				javaObj.Call ("addPushReceiver");
			}
		}

		public override void stopPush (){
			Debug.Log("AndroidImpl  ===>>>  stopPush === ");
			if(javaObj != null){
				javaObj.Call ("stopPush");
			}
		}

		public override void restartPush (){
			Debug.Log("AndroidImpl  ===>>>  restartPush === ");
			if(javaObj != null){
				javaObj.Call ("restartPush");
			}
		}

		public override bool isPushStopped (){
			Debug.Log("AndroidImpl  ===>>>  isPushStopped === ");
			if(javaObj != null){
				return javaObj.Call <bool>("isPushStopped");
			}
			return false;
		}

		public override void setClickNotificationToLaunchPage (bool isOpen){
			Debug.Log("AndroidImpl  ===>>>  setClickNotificationToLaunchPage === ");
			if(javaObj != null){
				javaObj.Call ("setClickNotificationToLaunchMainActivity", isOpen);
			}
		}

		public override void getRegistrationId (){
			Debug.Log("AndroidImpl  ===>>>  getRegistrationId === ");
			if(javaObj != null){
				javaObj.Call ("getRegistrationId");
			}
		}

		public override void addTags (string[] tags){
			Debug.Log("AndroidImpl  ===>>>  addTags === ");
			if(javaObj != null){
				string stringTags = String.Join (",", tags);
				javaObj.Call ("addTags", stringTags);
			}
		}

		public override void getTags (){
			Debug.Log("AndroidImpl  ===>>>  getTags === ");
			if(javaObj != null){
				javaObj.Call ("getTags");
			}
		}

		public override void deleteTags (string[] tags){
			Debug.Log("AndroidImpl  ===>>>  deleteTags === ");
			if(javaObj != null){
				string stringTags = String.Join (",", tags);
				javaObj.Call ("deleteTags", stringTags);
			}
		}

		public override void cleanAllTags (){
			Debug.Log("AndroidImpl  ===>>>  cleanAllTags === ");
			if(javaObj != null){
				javaObj.Call ("cleanAllTags");
			}
		}
			
		public override void addAlias (string alias){
			Debug.Log("AndroidImpl  ===>>>  addAlias === ");
			if(javaObj != null){
				javaObj.Call ("setAlias", alias);
			}
		}

		public override void getAlias (){
			Debug.Log("AndroidImpl  ===>>>  getAlias === ");
			if(javaObj != null){
				javaObj.Call ("getAlias");
			}
		}

		public override void cleanAllAlias (){
			Debug.Log("AndroidImpl  ===>>>  cleanAllAlias === ");
			if(javaObj != null){
				javaObj.Call ("cleanAllAlias");
			}
		}

		public override void setMobPushLocalNotification (LocalNotifyStyle style){
			String reqJson = style.getStyleParamsStr ();
			Debug.Log("AndroidImpl  ===>>>  setMobPushLocalNotification === " + reqJson);
			if(javaObj != null){
				javaObj.Call ("setMobPushLocalNotification", reqJson);
			}
		}

		public override void setCustomNotification (CustomNotifyStyle style){
			String reqJson = style.getStyleParamsStr ();
			Debug.Log("AndroidImpl  ===>>>  setCustomNotification === " + reqJson);
			if(javaObj != null){
				javaObj.Call ("setCustomNotification", reqJson);
			}
		}

        public override void setNotifyIcon (string resIcon){
			Debug.Log("AndroidImpl  ===>>>  setNotifyIcon === ");
			if(javaObj != null){
				javaObj.Call ("setNotifyIcon", resIcon);
			}
		}

        public override void setAppForegroundHiddenNotification (bool hidden){
			Debug.Log("AndroidImpl  ===>>>  setAppForegroundHiddenNotification === ");
			if(javaObj != null){
				javaObj.Call ("setAppForegroundHiddenNotification", hidden);
			}
		}

        public override void bindPhoneNum (string phoneNum){
			Debug.Log("AndroidImpl  ===>>>  bindPhoneNum === "+phoneNum);
			if(javaObj != null){
				javaObj.Call ("bindPhoneNum", phoneNum);
			}
		}

        public override void setShowBadge (bool show){
			Debug.Log("AndroidImpl  ===>>>  setShowBadge === "+show);
			if(javaObj != null){
				javaObj.Call ("setShowBadge", show);
			}
		}

		public override void req (int type, string content, int space, string extras){
			Debug.Log("AndroidImpl  ===>>>  req === ");
			if(javaObj != null){
				javaObj.Call ("req", type, content, space, extras);
			}
		}
	}
#endif
}