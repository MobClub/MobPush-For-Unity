package com.mob.unity3d.listener;

import com.mob.pushsdk.MobPushCallback;
import com.mob.tools.utils.Hashon;
import com.unity3d.player.UnityPlayer;

import java.util.HashMap;

/**
 * Created by jychen on 2018/5/4.
 */

public class MobPushDemoListener implements MobPushCallback<Boolean> {

	private String u3dDemoCallback;
	private String u3dGameObject;
	private Hashon hashon;

	public MobPushDemoListener(String u3dGameObject, String u3dDemoCallback){
		this.u3dGameObject = u3dGameObject;
		this.u3dDemoCallback = u3dDemoCallback;
		hashon = new Hashon();
	}

	@Override
	public void onCallback(Boolean b) {
		HashMap<String, Object> result = new HashMap<String, Object>();
		if(b){
			result.put("action", 1);
		} else{
			result.put("action", 0);
		}
		System.out.println("MobPushDemoListener-Action:" + result);
		UnityPlayer.UnitySendMessage(u3dGameObject, u3dDemoCallback, hashon.fromHashMap(result));
	}
}
