package com.mob.unity3d.listener;

import com.mob.pushsdk.MobPushCallback;
import com.mob.tools.utils.Hashon;
import com.unity3d.player.UnityPlayer;

import java.util.HashMap;

public class MobPushBindPhoneNumCallback implements MobPushCallback<Boolean> {
	private String u3dBindPhoneNumCallback;
	private String u3dGameObject;
	private Hashon hashon;

	public MobPushBindPhoneNumCallback(String u3dGameObject, String u3dBindPhoneNumCallback) {
		this.u3dGameObject = u3dGameObject;
		this.u3dBindPhoneNumCallback = u3dBindPhoneNumCallback;
		hashon = new Hashon();
	}

	@Override
	public void onCallback(Boolean b) {
		HashMap<String, Object> result = new HashMap<String, Object>();
		result.put("result", b ? 1 : 0);
		System.out.println("MobPushBindPhoneNumCallback-result:" + result);
		UnityPlayer.UnitySendMessage(u3dGameObject, u3dBindPhoneNumCallback, hashon.fromHashMap(result));
	}
}
