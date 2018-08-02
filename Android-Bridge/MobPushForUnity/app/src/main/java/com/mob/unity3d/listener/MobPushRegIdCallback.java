package com.mob.unity3d.listener;

import com.mob.pushsdk.MobPushCallback;
import com.mob.tools.utils.Hashon;
import com.unity3d.player.UnityPlayer;

import java.util.HashMap;

/**
 * Created by jychen on 2018/5/8.
 */

public class MobPushRegIdCallback implements MobPushCallback<String> {

	private String u3dDemoCallback;
	private String u3dGameObject;
	private Hashon hashon;

	public MobPushRegIdCallback(String u3dGameObject, String u3dDemoCallback){
		this.u3dGameObject = u3dGameObject;
		this.u3dDemoCallback = u3dDemoCallback;
		hashon = new Hashon();
	}

	@Override
	public void onCallback(String regId) {
		HashMap<String, Object> result = new HashMap<String, Object>();
		result.put("RegId", regId);
		System.out.println("MobPushRegIdCallback-getRegId:" + result);
		UnityPlayer.UnitySendMessage(u3dGameObject, u3dDemoCallback, hashon.fromHashMap(result));
	}
}
