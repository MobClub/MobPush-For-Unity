using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using com.mob.mobpush;

public class Demo : MonoBehaviour {

	public GUISkin demoSkin;
	public MobPush mobPush;

	void Start ()
	{	
		mobPush = gameObject.GetComponent<MobPush>();
		mobPush.onNotifyCallback = OnNitifyHandler;
		mobPush.onTagsCallback = OnTagsHandler;
		mobPush.onAliasCallback = OnAliasHandler;
		mobPush.onDemoReqCallback = OnDemoReqHandler;
		mobPush.onRegIdCallback = OnRegIdHandler;
		mobPush.onBindPhoneNumCallback = OnBindPhoneNumHandler;
		mobPush.onPrivacyPolicyCallback = OnPrivacyPolicyHandler;
		
		// IPHONE 要想收到 APNs 和本地通知，必须先要 setCustom (only ios)
		#if UNITY_IPHONE

			// 真机调试 false , 上线 true
			mobPush.setAPNsForProduction(false);

			CustomNotifyStyle style = new CustomNotifyStyle ();
		style.setType(CustomNotifyStyle.AuthorizationType.Badge | CustomNotifyStyle.AuthorizationType.Sound | CustomNotifyStyle.AuthorizationType.Alert);
			mobPush.setCustomNotification(style);

		#endif
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}
	
	void OnGUI ()
	{
		GUI.skin = demoSkin;
		
		float scale = 1.0f;

		if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			scale = Screen.width / 320;
		}
		
		//float btnWidth = 165 * scale;
		float btnWidth= Screen.width / 5 * 2;
		float btnHeight = Screen.height / 25;
		float btnTop = 30 * scale;
		float btnGap = 20 * scale;
		GUI.skin.button.fontSize = Convert.ToInt32(13 * scale);

		if (GUI.Button(new Rect((Screen.width - btnGap) / 2 - btnWidth, btnTop, btnWidth, btnHeight), "Send Notify"))
		{
			Hashtable extras = new Hashtable ();
			extras.Add ("key1", "value1");
			extras.Add ("key2", "value2");
			extras.Add ("key3", "value3");
			mobPush.req(1, "content-Send Notify", 0, extras);
		}
			
		if (GUI.Button(new Rect((Screen.width - btnGap) / 2 + btnGap, btnTop, btnWidth, btnHeight), "Send App Notify"))
		{
			mobPush.req(2, "App Notify", 0, null);
		}

		btnTop += btnHeight + 20 * scale;
		if (GUI.Button(new Rect((Screen.width - btnGap) / 2 - btnWidth, btnTop, btnWidth, btnHeight), "Send Delayed Notify"))
		{
			mobPush.req(3, "Delayed Notify", 1, null);
		}
			
		if (GUI.Button(new Rect((Screen.width - btnGap) / 2 + btnGap, btnTop, btnWidth, btnHeight), "Send Locat Notify"))
		{
			LocalNotifyStyle style = new LocalNotifyStyle ();
			style.setContent ("Text");
			style.setTitle ("title");
			Hashtable extras = new Hashtable ();
			extras["key1"] = "value1";
			extras["key2"] = "value1";
			style.setExtras (extras);
			mobPush.setMobPushLocalNotification (style);	
		}

		btnTop += btnHeight + 20 * scale;
		if (GUI.Button(new Rect((Screen.width - btnGap) / 2 - btnWidth, btnTop, btnWidth, btnHeight), "CustomNotify"))
		{
			CustomNotifyStyle style = new CustomNotifyStyle ();

		#if UNITY_IPHONE

			style.setType(CustomNotifyStyle.AuthorizationType.Badge | CustomNotifyStyle.AuthorizationType.Sound | CustomNotifyStyle.AuthorizationType.Alert);

		#elif UNITY_ANDROID

			style.setContent ("Content");
			style.setTitle ("Title");
			style.setTickerText ("TickerText");

		#endif
			
			mobPush.setCustomNotification(style);
		}

		if (GUI.Button(new Rect((Screen.width - btnGap) / 2 + btnGap, btnTop, btnWidth, btnHeight), "getRegistrationId"))
		{
			mobPush.getRegistrationId ();
		}

		//Test Code
		btnTop += btnHeight + 20 * scale;
		if (GUI.Button(new Rect((Screen.width - btnGap) / 2 - btnWidth, btnTop, btnWidth, btnHeight), "addTags"))
		{
			String[] tags = { "tags1", "tags2", "tags3" };
			mobPush.addTags (tags);
		}

		if (GUI.Button(new Rect((Screen.width - btnGap) / 2 + btnGap, btnTop, btnWidth, btnHeight), "getTags"))
		{
			mobPush.getTags ();
		}


		btnTop += btnHeight + 20 * scale;
		if (GUI.Button(new Rect((Screen.width - btnGap) / 2 - btnWidth, btnTop, btnWidth, btnHeight), "deleteTags"))
		{
			String[] tags = { "tags1", "tags2", "tags3" };
			mobPush.deleteTags (tags);
		}

		if (GUI.Button(new Rect((Screen.width - btnGap) / 2 + btnGap, btnTop, btnWidth, btnHeight), "cleanAllTags"))
		{
			mobPush.cleanAllTags ();
		}


		btnTop += btnHeight + 20 * scale;
		if (GUI.Button(new Rect((Screen.width - btnGap) / 2 - btnWidth, btnTop, btnWidth, btnHeight), "addAlias"))
		{
			mobPush.addAlias ("alias");
		}

		if (GUI.Button(new Rect((Screen.width - btnGap) / 2 + btnGap, btnTop, btnWidth, btnHeight), "getAlias"))
		{
			mobPush.getAlias ();
		}


		btnTop += btnHeight + 20 * scale;
		if (GUI.Button(new Rect((Screen.width - btnGap) / 2 - btnWidth, btnTop, btnWidth, btnHeight), "cleanAllAlias"))
		{
			mobPush.cleanAllAlias ();
		}

        if (GUI.Button(new Rect((Screen.width - btnGap) / 2 + btnGap, btnTop, btnWidth, btnHeight), "bindPhoneNum"))
		{
			mobPush.bindPhoneNum ("12345678988");
		}

		#if UNITY_IPHONE
		btnTop += btnHeight + 20 * scale;
		if (GUI.Button(new Rect((Screen.width - btnGap) / 2 - btnWidth, btnTop, btnWidth, btnHeight), "查询隐私协议(URL)"))
		{
			mobPush.getPrivacyPolicy ("1", "");
		}

		if (GUI.Button(new Rect((Screen.width - btnGap) / 2 + btnGap, btnTop, btnWidth, btnHeight), "查询隐私协议(富文本)"))
		{
			mobPush.getPrivacyPolicy ("2", "");
		}

		btnTop += btnHeight + 20 * scale;
		if (GUI.Button(new Rect((Screen.width - btnGap) / 2 - btnWidth, btnTop, btnWidth, btnHeight), "上报隐私授权"))
		{
			mobPush.updatePrivacyPermissionStatus (true);
		}
		#endif

#if UNITY_ANDROID

        btnTop += btnHeight + 20 * scale;
		if (GUI.Button(new Rect((Screen.width - btnGap) / 2 - btnWidth, btnTop, btnWidth, btnHeight), "stopPush"))
		{
			mobPush.stopPush ();
		}

		
		if (GUI.Button(new Rect((Screen.width - btnGap) / 2 + btnGap, btnTop, btnWidth, btnHeight), "restartPush"))
		{
			mobPush.restartPush ();
		}
        btnTop += btnHeight + 20 * scale;
		if (GUI.Button(new Rect((Screen.width - btnGap) / 2 - btnWidth, btnTop, btnWidth, btnHeight), "isPushStop"))
		{
			mobPush.isPushStopped();
		}

		if (GUI.Button(new Rect((Screen.width - btnGap) / 2 + btnGap, btnTop, btnWidth, btnHeight), "setClickNotificationToLaunchPage"))
		{
			mobPush.setClickNotificationToLaunchPage(false);
		}


         btnTop += btnHeight + 20 * scale;
		if (GUI.Button(new Rect((Screen.width - btnGap) / 2 - btnWidth, btnTop, btnWidth, btnHeight), "setShowBadge"))
		{
			mobPush.setShowBadge(true);
		}

#endif
    }

    void OnNitifyHandler (int action, Hashtable resulte)
	{
		Debug.Log ("OnNitifyHandler");
		if (action == ResponseState.CoutomMessage)
		{
			Debug.Log ("CoutomMessage:" + MiniJSON.jsonEncode(resulte));
		}
		else if (action == ResponseState.MessageRecvice)
		{
			Debug.Log ("MessageRecvice:" + MiniJSON.jsonEncode(resulte));
		}
		else if (action == ResponseState.MessageOpened) 
		{
			Debug.Log ("MessageOpened:" + MiniJSON.jsonEncode(resulte));
		}
	}
	
	void OnTagsHandler (int action, string[] tags, int operation, int errorCode)
	{
		
		Debug.Log ("OnTagsHandler  action:" + action + " tags:" + String.Join (",", tags) + " operation:" + operation + "errorCode:" + errorCode);
	}

	void OnAliasHandler (int action, string alias, int operation, int errorCode)
	{
		Debug.Log ("OnAliasHandler action:" + action + " alias:" + alias + " operation:" + operation + "errorCode:" + errorCode);
	}

	void OnRegIdHandler (string regId)
	{
		Debug.Log ("OnRegIdHandler-regId:" + regId);
	}

    void OnBindPhoneNumHandler(bool isSuccess)
	{
		Debug.Log ("OnBindPhoneNumHandler-result:" + isSuccess);
    }

	void OnPrivacyPolicyHandler(string content, int errorCode)
	{
		Debug.Log ("OnPrivacyPolicyHandler  content:" + content + " errorCode:" + errorCode);
	}

	void OnDemoReqHandler (bool isSuccess)
	{
		Debug.Log ("OnDemoReqHandler:" + isSuccess);
	}
}
