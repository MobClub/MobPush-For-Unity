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
				javaObj = new AndroidJavaObject("com.mob.unity3d.MobPushUtils", go.name, "_MobPushCallback", "_MobPushDemoCallback", "_MobPushRegIdCallback");
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

		public override void getRegistrationId (){
			Debug.Log("AndroidImpl  ===>>>  getRegistrationId === ");
			if(javaObj != null){
				javaObj.Call ("getRegistrationId");
			}
		}

		public override void addTags (string tags){
			Debug.Log("AndroidImpl  ===>>>  addTags === ");
			if(javaObj != null){
				javaObj.Call ("addTags", tags);
			}
		}

		public override void getTags (){
			Debug.Log("AndroidImpl  ===>>>  getTags === ");
			if(javaObj != null){
				javaObj.Call ("getTags");
			}
		}

		public override void deleteTags (string tags){
			Debug.Log("AndroidImpl  ===>>>  deleteTags === ");
			if(javaObj != null){
				javaObj.Call ("deleteTags", tags);
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
				javaObj.Call ("addAlias", alias);
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

		public override void req (int type, string content, int space, string extras, int iosProduction){
			Debug.Log("AndroidImpl  ===>>>  req === ");
			if(javaObj != null){
				javaObj.Call ("req", type, content, space, extras);
			}
		}
	}
	#endif
}