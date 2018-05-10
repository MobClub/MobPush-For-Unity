using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace com.mob.mobpush{
	public class MobPush : MonoBehaviour {

		public string appKey = "moba6b6c6d6";
		public string appSecret = "b89d2427a3bc7ad1aea1e1e8c1d36bf3";

		public MobPushImpl mobPushImpl;
		public OnNotifyCallback onNotifyCallback;
		public OnTagsCallback onTagsCallback;
		public OnAliasCallback onAliasCallback;
		public OnRegIdCallback onRegIdCallback;
		public OnDemoReqCallback onDemoReqCallback;

		void Awake() {
			mobPushImpl = new AndroidImpl (gameObject);
			mobPushImpl.initPushSDK (appKey, appSecret);
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
			if (action == 0 || action == 1 || action == 2) {
				if (onNotifyCallback != null) {
					Hashtable result = (Hashtable) res["result"];
					onNotifyCallback (action, result);
				}
			} else if (action == 3) {
				if (onTagsCallback != null) {
					string[] tags = (string[])(res ["tags"]);
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


		public void initPushSDK(string appKey, string appScrect) {
			mobPushImpl.initPushSDK (appKey, appSecret);
		}

		public void addPushReceiver() {
			mobPushImpl.addPushReceiver ();
		}

		public void setAPNSForProduction(bool isOpen) {
			#if UNITY_IOS
			mobPushImpl.setAPNSForProduction (isOpen);
			#endif
		}

		public void getRegistrationId() {
			mobPushImpl.getRegistrationId ();
		}

		public void addTags(string tags) {
			mobPushImpl.addTags (tags);
		}

		public void getTags() {
			mobPushImpl.getTags ();
		}

		public void deleteTags(string tags) {
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

		public void req(int type, string content, int space, string extras) {
			#if UNITY_ANDROID
			mobPushImpl.req (type, content, space, extras, 0);
			#endif
		}

		public delegate void OnNotifyCallback (int action, Hashtable resulte);

		public delegate void OnTagsCallback (int action, string[] tags, int operation, int errorCode);

		public delegate void OnAliasCallback(int action, string alias, int operation, int errorCode);

		public delegate void OnRegIdCallback(string regId);

		public delegate void OnDemoReqCallback(bool isSuccess);
	
	}
}
