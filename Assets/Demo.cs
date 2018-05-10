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
			mobPush.req(1, "content-Send Notify", 0, null);
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
			style.setContent ("Content");
			style.setTitle ("Title");
			style.setTickerText ("TickerText");
			mobPush.setCustomNotification(style);
		}

		if (GUI.Button(new Rect((Screen.width - btnGap) / 2 + btnGap, btnTop, btnWidth, btnHeight), "getRegistrationId"))
		{
			mobPush.getRegistrationId ();
		}
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
		Debug.Log ("OnTagsHandler");
	}

	void OnAliasHandler (int action, string alias, int operation, int errorCode)
	{
		Debug.Log ("OnAliasHandler");
	}

	void OnRegIdHandler (string regId)
	{
		Debug.Log ("OnRegIdHandler-regId:" + regId);
	}

	void OnDemoReqHandler (bool isSuccess)
	{
		Debug.Log ("OnDemoReqHandler:" + isSuccess);
	}
}
