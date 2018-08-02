package com.mob.unity3d.listener;

import android.content.Context;

import com.mob.pushsdk.MobPushCustomMessage;
import com.mob.pushsdk.MobPushNotifyMessage;
import com.mob.pushsdk.MobPushReceiver;
import com.mob.tools.utils.Hashon;
import com.unity3d.player.UnityPlayer;

import java.util.HashMap;

/**
 * Created by jychen on 2018/2/24.
 */

public class MobPushListener implements MobPushReceiver {

	private String u3dCallback;
	private String u3dGameObject;
	private Hashon hashon;
	//自定义action : 0:透传  1:接收通知  2:打开通知  3:Tags  4:Alias

	public MobPushListener(String u3dGameObject, String u3dCallback){
		this.u3dGameObject = u3dGameObject;
		this.u3dCallback = u3dCallback;
		hashon = new Hashon();
	}

	@Override
	public void onCustomMessageReceive(Context context, MobPushCustomMessage mobPushCustomMessage) {
		HashMap<String, Object> map = new HashMap<String, Object>();
		map.put("action", 0);
		map.put("result", mobPushCustomMessageToMap(mobPushCustomMessage));
		System.out.println("onCustomMessageReceive:" + map);
		UnityPlayer.UnitySendMessage(u3dGameObject, u3dCallback, hashon.fromHashMap(map));
	}

	@Override
	public void onNotifyMessageReceive(Context context, MobPushNotifyMessage mobPushNotifyMessage) {
		HashMap<String, Object> map = new HashMap<String, Object>();
		map.put("action", 1);
		map.put("result", mobPushNotifyMessageToMap(mobPushNotifyMessage));
		System.out.println("onNotifyMessageReceive:" + map);
		UnityPlayer.UnitySendMessage(u3dGameObject, u3dCallback, hashon.fromHashMap(map));
	}

	@Override
	public void onNotifyMessageOpenedReceive(Context context, MobPushNotifyMessage mobPushNotifyMessage) {
		HashMap<String, Object> map = new HashMap<String, Object>();
		map.put("action", 2);
		map.put("result", mobPushNotifyMessageToMap(mobPushNotifyMessage));
		System.out.println("onNotifyMessageOpenedReceive:" + map);
		UnityPlayer.UnitySendMessage(u3dGameObject, u3dCallback, hashon.fromHashMap(map));
	}

	@Override
	public void onTagsCallback(Context context, String[] tags, int operation, int errorCode) {
		HashMap<String, Object> map = new HashMap<String, Object>();
		map.put("action", 3);
		map.put("tags", arrayToStr(tags));
		map.put("operation", operation);
		map.put("errorCode", errorCode);
		System.out.println("onTagsCallback:" + map);
		UnityPlayer.UnitySendMessage(u3dGameObject, u3dCallback, hashon.fromHashMap(map));
	}

	@Override
	public void onAliasCallback(Context context, String alias, int operation, int errorCode) {
		HashMap<String, Object> map = new HashMap<String, Object>();
		map.put("action", 4);
		map.put("alias", alias);
		map.put("operation", operation);
		map.put("errorCode", errorCode);
		System.out.println("onAliasCallback:" + map);
		UnityPlayer.UnitySendMessage(u3dGameObject, u3dCallback, hashon.fromHashMap(map));
	}

	private HashMap<String, Object> mobPushCustomMessageToMap(MobPushCustomMessage message){
		HashMap<String, Object> map = new HashMap<String, Object>();
		map.put("messageId", message.getMessageId());
		map.put("content", message.getContent());
		map.put("extrasMap", message.getExtrasMap());
		map.put("timestamp", message.getTimestamp());
		return map;
	}

	private HashMap<String, Object> mobPushNotifyMessageToMap(MobPushNotifyMessage message){
		HashMap<String, Object> map = new HashMap<String, Object>();
		map.put("messageId", message.getMessageId());
		map.put("content", message.getContent());
		map.put("title", message.getTitle());
		map.put("style", message.getStyle());
		map.put("styleContent", message.getStyleContent());
		map.put("extrasMap", message.getExtrasMap());
		map.put("timestamp", message.getTimestamp());
		map.put("inboxStyleContent", message.getInboxStyleContent());
		map.put("channel", message.getChannel());
		return map;
	}

	/**
	 * 数组转成逗号分隔的字符串
	 * @param array
	 * @return
	 */
	public String arrayToStr(String[] array) {
		StringBuffer sb = new StringBuffer();
		for (int i = 0; i < array.length; i++) {
			sb.append(array[i]);
			if ((i + 1) != array.length) {
				sb.append(",");
			}
		}
		return sb.toString();
	}
}
