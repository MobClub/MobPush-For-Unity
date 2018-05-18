using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace com.mob.mobpush{
	public class MobPush : MonoBehaviour {

		//public string appKey = "moba6b6c6d6";
		//public string appSecret = "b89d2427a3bc7ad1aea1e1e8c1d36bf3";
		public MobPushImpl mobPushImpl;
		public OnNotifyCallback onNotifyCallback;
		public OnTagsCallback onTagsCallback;
		public OnAliasCallback onAliasCallback;
		public OnRegIdCallback onRegIdCallback;
		public OnDemoReqCallback onDemoReqCallback;

		void Awake() {
			#if UNITY_IPHONE

				mobPushImpl = new iOSMobPushImpl (gameObject);

			#elif UNITY_ANDROID
				mobPushImpl = new AndroidImpl (gameObject);
				//mobPushImpl.initPushSDK (appKey, appSecret);
			#endif

				mobPushImpl.addPushReceiver ();
			
		}

		private void _MobPushCallback (string data) {
			if (data == null) {
				return;
			}
			Debug.Log ("_MobPushCallback:" + data);
			Hashtable res = (Hashtable)MiniJSON.jsonDecode (data);

			if(res == null || res.Count <= 0){
				return;
			}
			int action = Convert.ToInt32(res["action"]);
			// 0 自定义消息 1 收到 2 点击
			if (action == ResponseState.CoutomMessage || action == ResponseState.MessageRecvice || action == ResponseState.MessageOpened) {
				if (onNotifyCallback != null) {
					Hashtable result = (Hashtable) res["result"];
					onNotifyCallback (action, result);
				}
			} else if (action == 3) {
				if (onTagsCallback != null) {
					string stringTags =  Convert.ToString(res ["tags"]);
					ArrayList array = new ArrayList( stringTags.Split(',') ) ;
					string[] tags = (string[])array.ToArray (typeof(string));
					int operation = Convert.ToInt32(res["operation"]);
					int errorCode =  Convert.ToInt32(res["errorCode"]);
					onTagsCallback (action, tags, operation, errorCode);
				}
			} else if (action == 4) {
				if (onAliasCallback != null) {
					string alias = Convert.ToString(res ["alias"]);
					int operation = Convert.ToInt32 (res ["operation"]);
					int errorCode = Convert.ToInt32 (res ["errorCode"]);
					onAliasCallback (action, alias, operation, errorCode);
				}
			}
		}

		private void _MobPushDemoCallback (string data) {
			if (data == null) {
				return;
			}
			Debug.Log ("_MobPushDemoCallback:" + data);
			Hashtable res = (Hashtable)MiniJSON.jsonDecode (data);
			if(res == null || res.Count <= 0){
				return;
			}
			int action = Convert.ToInt32(res["action"]);
			bool isSuccess;
			if (action == 1) {
				isSuccess = true;
			} else {
				isSuccess = false;
			}
			if (onDemoReqCallback != null) {
				onDemoReqCallback (isSuccess);
			}
		}

		private void _MobPushRegIdCallback (string regId) {
			if (regId == null) {
				return;
			}
			Debug.Log ("_MobPushRegIdCallback-regId:" + regId);
			if (onRegIdCallback != null) {
				onRegIdCallback (regId);
			}
		}

		#if UNITY_IPHONE
		public void setAPNsForProduction (bool isPro)
		{
			mobPushImpl.setAPNsForProduction(isPro);
			
		}
		#endif

		public void addPushReceiver() {
			mobPushImpl.addPushReceiver ();
		}
	
		#if UNITY_ANDROID
		public void initPushSDK(string appKey, string appScrect) {
			mobPushImpl.initPushSDK (appKey, appScrect);
		}

		public void stopPush() {
			mobPushImpl.stopPush ();
		}

		public void restartPush() {
			mobPushImpl.restartPush ();
		}

		public bool isPushStopped() {
			return mobPushImpl.isPushStopped ();
		}

		public void setClickNotificationToLaunchPage(bool isOpen) {
			mobPushImpl.setClickNotificationToLaunchPage (isOpen);
		}
		#endif
			
		public void getRegistrationId() {
			mobPushImpl.getRegistrationId ();
		}

		public void addTags(string[] tags) {
			mobPushImpl.addTags (tags);
		}

		public void getTags() {
			mobPushImpl.getTags ();
		}

		public void deleteTags(string[] tags) {
			mobPushImpl.deleteTags (tags);
		}

		public void cleanAllTags() {
			mobPushImpl.cleanAllTags ();
		}

		public void addAlias(string alias) {
			mobPushImpl.addAlias (alias);
		}

		public void getAlias() {
			mobPushImpl.getAlias ();
		}

		public void cleanAllAlias() {
			mobPushImpl.cleanAllAlias ();
		}

		public void setMobPushLocalNotification(LocalNotifyStyle style) {
			mobPushImpl.setMobPushLocalNotification (style);
		}

		public void setCustomNotification(CustomNotifyStyle style){
			mobPushImpl.setCustomNotification (style);
		}

		public void req(int type, string content, int space, Hashtable extras) {
			String jsonStr = MiniJSON.jsonEncode (extras);
			Debug.Log("extras  ===>>> " + jsonStr );
			mobPushImpl.req (type, content, space, jsonStr);
		}

		public delegate void OnNotifyCallback (int action, Hashtable resulte);

		public delegate void OnTagsCallback (int action, string[] tags, int operation, int errorCode);

		public delegate void OnAliasCallback(int action, string alias, int operation, int errorCode);

		public delegate void OnRegIdCallback(string regId);

		public delegate void OnDemoReqCallback(bool isSuccess);
	
	}
}
