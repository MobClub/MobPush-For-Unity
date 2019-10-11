package com.mob.unity3d;

import android.content.Context;
import android.text.TextUtils;

import com.mob.MobSDK;
import com.mob.pushsdk.MobPush;
import com.mob.pushsdk.MobPushLocalNotification;
import com.mob.tools.utils.Hashon;
import com.mob.tools.utils.ResHelper;
import com.mob.unity3d.listener.MobPushBindPhoneNumCallback;
import com.mob.unity3d.listener.MobPushDemoListener;
import com.mob.unity3d.listener.MobPushListener;
import com.mob.unity3d.listener.MobPushRegIdCallback;
import com.unity3d.player.UnityPlayer;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Random;
import java.util.Set;

/**
 * Created by jychen on 2018/2/24.
 */

public class MobPushUtils {
	private static boolean DEBUG = false;
	private static String u3dGameObject;
	private static String u3dCallback;
	private static String u3dDemoCallback;
	private static String u3dRegIdCallback;
	private static String u3dBindPhoneNumCallback;
	private static Context context;
	private Hashon hashon = new Hashon();

	public MobPushUtils(final String gameObject,final String u3dCallback, String u3dDemoCallback, String u3dRegIdCallback, String u3dBindPhoneNumCallback) {
		if (DEBUG) {
			System.out.println("MobPushUtils.prepare: gameObject:" + gameObject + " u3dCallback:" + u3dCallback + " u3sDemoCallback:" + u3dDemoCallback + " u3dBindPhoneNumCallback:" + u3dBindPhoneNumCallback);
		}
		if (context == null) {
			context = UnityPlayer.currentActivity.getApplicationContext();
		}

		if(!TextUtils.isEmpty(gameObject)) {
			u3dGameObject = gameObject;
		}
		if(!TextUtils.isEmpty(u3dCallback)) {
			this.u3dCallback = u3dCallback;
		}
		if(!TextUtils.isEmpty(u3dDemoCallback)) {
			this.u3dDemoCallback = u3dDemoCallback;
		}
		if(!TextUtils.isEmpty(u3dRegIdCallback)) {
			this.u3dRegIdCallback = u3dRegIdCallback;
		}
		if(!TextUtils.isEmpty(u3dBindPhoneNumCallback)) {
			this.u3dBindPhoneNumCallback = u3dBindPhoneNumCallback;
		}
		hashon = new Hashon();
	}

	public void isDebug(boolean isDebug){
		DEBUG = isDebug;
	}

	/**
	 * 初始化PushSDK，此方法必要时才调用
	 * @param appKey
	 * @param screct
	 */
	public void initPushSDK(String appKey, String screct){
		if (DEBUG) {
			System.out.println("initSDK appkey ==>>" + appKey + "appscrect ==>>" + screct);
		}
		if (!TextUtils.isEmpty(appKey) && !TextUtils.isEmpty(screct)) {
			MobSDK.init(context, appKey,screct);
		} else if(!TextUtils.isEmpty(appKey)){
			MobSDK.init(context, appKey);
		} else {
			MobSDK.init(context);
		}
	}

	/**
	 * 添加推送的监听
	 */
	public void addPushReceiver(){
		if (DEBUG) {
			System.out.println("addPushReceiver");
		}
		MobPushListener listener = new MobPushListener(u3dGameObject, u3dCallback);
		MobPush.addPushReceiver(listener);
	}

	/**
	 * 停止推送
	 */
	public void stopPush(){
		if (DEBUG) {
			System.out.println("stopPush");
		}
		MobPush.stopPush();
	}

	//重启推送
	public void restartPush(){
		if (DEBUG) {
			System.out.println("restartPush");
		}
		MobPush.restartPush();
	}

	//推送是否停止
	public boolean isPushStopped(){
		if (DEBUG) {
			System.out.println("isPushStopped");
		}
		return MobPush.isPushStopped();
	}

	/**
	 * 点击通知后是否启动Launch Activity
	 * @param isOpen
	 */
	public void setClickNotificationToLaunchMainActivity(boolean isOpen){
		if (DEBUG) {
			System.out.println("setClickNotificationToLaunchMainActivity:" + isOpen);
		}
		MobPush.setClickNotificationToLaunchMainActivity(isOpen);
	}

	/**
	 * 获取注册ID
	 */
	public void getRegistrationId(){
		MobPushRegIdCallback mobPushRegIdCallback = new MobPushRegIdCallback(u3dGameObject, u3dRegIdCallback);
		MobPush.getRegistrationId(mobPushRegIdCallback);
	}

	public void addTags(String tagsReq){
		if (DEBUG) {
			System.out.println("addTags:" + tagsReq);
		}
		String[] tags = tagsReq.split(",");
		MobPush.addTags(tags);
	}

	/**
	 * 获取设置的tags
	 */
	public void getTags(){
		if (DEBUG) {
			System.out.println("getTags");
		}
		MobPush.getTags();
	}

	/**
	 * 清除某组标签
	 * @param tagsReq
	 */
	public void deleteTags(String tagsReq) {
		if (DEBUG) {
			System.out.println("deleteTags:" + tagsReq);
		}
		String[] tags = tagsReq.split(",");
		MobPush.deleteTags(tags);
	}


	/**
	 * 清空全部的标签
	 */
	public void cleanAllTags(){
		if (DEBUG) {
			System.out.println("cleanAllTags");
		}
		MobPush.cleanTags();
	}

	/**
	 * 添加Alias
	 */
	public void setAlias(String aliasReq) {
		if (DEBUG) {
			System.out.println("setAlias:" + aliasReq);
		}
		MobPush.setAlias(aliasReq);
	}

	/**
	 * 获取设置的Alias
	 */
	public void getAlias(){
		if (DEBUG) {
			System.out.println("getAlias");
		}
		MobPush.getAlias();
	}

	/**
	 * 清空Alias
	 */
	public void cleanAllAlias(){
		if (DEBUG) {
			System.out.println("cleanAllAlias");
		}
		MobPush.deleteAlias();
	}

	/**
	 * 添加本地通知
	 */
	public void setMobPushLocalNotification(String reqJson){
		if (DEBUG) {
			System.out.println("setMobPushLocalNotification:content" + reqJson);
		}
		Hashon hashon = new Hashon();
		HashMap<String, Object> map = hashon.fromJson(reqJson);
		MobPushLocalNotification mobPushLocalNotification = new MobPushLocalNotification();
		if(map.containsKey("content")){
			mobPushLocalNotification.setContent((String)map.get("content"));
		}
		if(map.containsKey("title")){
			mobPushLocalNotification.setTitle((String)map.get("title"));
		}
		if(map.containsKey("styleContent")){
			mobPushLocalNotification.setStyleContent((String)map.get("styleContent"));
		}
		if(map.containsKey("extras")){
			HashMap<String, String> extrasMap = hashon.fromJson((String)map.get("extras"));
			mobPushLocalNotification.setExtrasMap(extrasMap);
		}
		if(map.containsKey("timestamp")){
			mobPushLocalNotification.setTimestamp(Long.valueOf((String)map.get("timestamp")) + System.currentTimeMillis());
		}
		if(map.containsKey("isVoice")){
			mobPushLocalNotification.setVoice(Boolean.valueOf((String)map.get("isVoice")));
		}
		if(map.containsKey("isShake")){
			mobPushLocalNotification.setShake(Boolean.valueOf((String)map.get("isShake")));
		}
		if(map.containsKey("isLight")){
			mobPushLocalNotification.setLight(Boolean.valueOf((String)map.get("isLight")));
		}
		if(map.containsKey("style")){
			mobPushLocalNotification.setStyle(Integer.valueOf((String)map.get("style")));
		}
        if(map.containsKey("id")){
            mobPushLocalNotification.setNotificationId(Integer.valueOf((String)map.get("id")));
        } else {
            mobPushLocalNotification.setNotificationId(new Random().nextInt());
        }
		System.out.println("LocalNotify style:" + mobPushLocalNotification.toString());
		MobPush.addLocalNotification(mobPushLocalNotification);
	}

	/**
	 * 移除本地通知
	 *
	 * @param strIds 本地通知的ID集合字符串
	 * */
	public void deleteLocalNotification(String strIds) {
		if (DEBUG) {
			System.out.println("deleteLocalNotification");
		}
		String[] ids = strIds.split(",");
		try {
			for (String id : ids) {
				MobPush.removeLocalNotification(Integer.valueOf(id));
			}
		} catch (Exception e) {
			System.out.println("deleteLocalNotification error");
			e.printStackTrace();
		}
	}

	/**
	 * 自定义通知栏
	 * @param reqJson
	 */
	public void setCustomNotification(String reqJson){
		if (DEBUG) {
			System.out.println("setCustomNotification:" + reqJson);
		}
		MobPush.setCustomNotification(new CustomNotification(reqJson));
	}

	/**
	 * 设置通知图标
	 * @param resIcon 图标资源名称
	 */
	public void setNotifyIcon(String resIcon){
		if(DEBUG) {
			System.out.println("setNotifyIcon:" + resIcon);
		}

		if (!TextUtils.isEmpty(resIcon)) {
			MobPush.setNotifyIcon(ResHelper.getBitmapRes(MobSDK.getContext(), resIcon));
		}
	}

	/**
	 * 设置应用前台时通知是否显示
	 * @param hidden 是否隐藏，true为隐藏不显示通知，false为显示不隐藏通知
	 */
	public void setAppForegroundHiddenNotification(boolean hidden){
		if(DEBUG) {
			System.out.println("setAppForegroundHiddenNotification:" + hidden);
		}

		MobPush.setAppForegroundHiddenNotification(hidden);
	}

	/**
	 * 绑定手机号
	 * @param phoneNum 手机号
	 */
	public void bindPhoneNum(String phoneNum){
		if(DEBUG) {
			System.out.println("bindPhoneNum:" + phoneNum);
		}
		MobPushBindPhoneNumCallback callback = new MobPushBindPhoneNumCallback(u3dGameObject, u3dBindPhoneNumCallback);
		MobPush.bindPhoneNum(phoneNum, callback);
	}

	/**
	 * 绑定手机号
	 * @param show 手机号
	 */
	public void setShowBadge(boolean show){
		if(DEBUG) {
			System.out.println("setShowBadge:" + show);
		}
		MobPush.setShowBadge(show);
	}

	/**
	 * demo的请求接口
	 * @param type 消息类型：1、通知测试；2、内推测试；3、定时
	 * @param content 文本
	 * @param space 延时
	 * @param extras 扩展参数
	 */
	public void req(int type,  String content, int space, String extras){
		if (DEBUG) {
			System.out.println("req:type" + type + " content:" + content + " space:" + space + " extras:" + extras);
		}
		HashMap<String, String> hashmap = hashon.fromJson(extras);
		JSONObject object = new JSONObject();
		try {
			Set<String> set = hashmap.keySet();
			for (String key : set) {
				System.out.println("Key = " + key + ", Value = " + hashmap.get(key));
				object.put(key, hashmap.get(key));//URLEncoder.encode(url, "utf-8")
			}
		} catch (JSONException e) {
			e.printStackTrace();
		}
		if(object != null){
			extras = object.toString();
		} else{
			extras = null;
		}
		MobPushDemoListener listener = new MobPushDemoListener(u3dGameObject, u3dDemoCallback);
		SimulateRequest.sendPush(type, content, space, extras, listener);
	}
}
