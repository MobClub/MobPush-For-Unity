package com.mob.unity3d;

import android.app.Notification;
import android.app.NotificationChannel;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageManager;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Color;
import android.os.Build;
import android.text.TextUtils;

import com.mob.MobSDK;
import com.mob.pushsdk.MobPushCustomNotification;
import com.mob.pushsdk.MobPushNotifyMessage;
import com.mob.tools.utils.Hashon;
import com.mob.tools.utils.ResHelper;
import com.unity3d.player.UnityPlayerActivity;

import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;

/**
 * Created by jychen on 2018/2/24.
 */

public class CustomNotification implements MobPushCustomNotification {

	private static final String ChannelId = "mobpush_notify";
	private static final String ChannelName = "Channel";
	private String iconName;
	private Hashon hashon;
	private HashMap<String, Object> result;
	private Context context;

	public CustomNotification(String jsonReq) {
		System.out.println("CustomNotification Builder: -->" + jsonReq);
		hashon = new Hashon();
		result = hashon.fromJson(jsonReq);
		result = mapScreen(result);
	}

	/**
	 * @param context           上下文环境
	 * @param when              消息时间
	 * @param tickerText        状态栏显示内容
	 * @param title             标题
	 * @param content           内容
	 * @param flag              PendingIntent的flag
	 * @param style             样式: {@link MobPushNotifyMessage#STYLE_NORMAL}
	 *                          {@link MobPushNotifyMessage#STYLE_BIG_TEXT}
	 *                          {@link MobPushNotifyMessage#STYLE_BIG_PICTURE}
	 *                          {@link MobPushNotifyMessage#STYLE_INBOX}
	 * @param styleContent      大段文本和大图模式的样式内容
	 * @param inboxStyleContent 收件箱样式的内容
	 * @param voice             声音
	 * @param shake             震动
	 * @param light             呼吸灯
	 * @return 返回Notification对象
	 */
	public Notification getNotification(Context context, NotificationManager manager, long when, String tickerText, String title, String content, int flag, int style,
	                                    String styleContent, String[] inboxStyleContent, boolean voice, boolean shake, boolean light) {
		this.context = context;
		when = result.containsKey("when") ? Long.valueOf((String)result.get("when")) : when;
		tickerText = result.containsKey("tickerText") ? (String)result.get("tickerText") : tickerText;
		title = result.containsKey("title") ? (String)result.get("title") : title;
		content = result.containsKey("content") ? (String)result.get("content") : content;
		flag = result.containsKey("flag") ? Integer.valueOf((String)result.get("flag")) : flag;
		style = result.containsKey("style") ? Integer.valueOf((String)result.get("style")) : style;
		styleContent = result.containsKey("styleContent") ? (String)result.get("styleContent") : styleContent;
		String str = (String)result.get("inboxStyleContent");
		String[] strs = str.split(",");
		inboxStyleContent = result.containsKey("inboxStyleContent") ? strs : inboxStyleContent;
		voice = result.containsKey("voice") ? Boolean.valueOf((String)result.get("voice")) : voice;
		shake = result.containsKey("shake") ? Boolean.valueOf((String)result.get("shake"))  : shake;
		light = result.containsKey("light") ? Boolean.valueOf((String)result.get("light"))  : light;
		iconName = result.containsKey("iconName") ? iconName : "";

		return getMyNotification(manager, iconName, when, tickerText, title, content, flag, style, styleContent, inboxStyleContent, voice, shake, light);
	}

	private Notification getMyNotification(NotificationManager manager, String iconName, long when, String tickerText, String title, String content, int flag,
										   int styleInt, String styleContent, String[] inboxStyleContent, boolean voice, boolean shake, boolean light) {
		//TODO 此处设置点击要启动的app
		PendingIntent pi = PendingIntent.getActivity(context, 1001, new Intent(context, UnityPlayerActivity.class), flag);
		//通知必须设置：小图标、标题、内容
		Notification.Builder builder = null;
		if (Build.VERSION.SDK_INT >= 26) {
			NotificationChannel notificationChannel = new NotificationChannel(ChannelId,
					ChannelName, NotificationManager.IMPORTANCE_DEFAULT);
			notificationChannel.enableLights(true); //是否在桌面icon右上角展示小红点
			notificationChannel.setLightColor(Color.GREEN); //小红点颜色
			notificationChannel.setShowBadge(true); //是否在久按桌面图标时显示此渠道的通知
			manager.createNotificationChannel(notificationChannel);
			builder = new Notification.Builder(MobSDK.getContext(), ChannelId);
		} else{
			builder = new Notification.Builder(MobSDK.getContext());
		}

		String packageName = MobSDK.getContext().getPackageName();
		PackageManager pm = MobSDK.getContext().getPackageManager();
		int icon;
		String appName = "";
		try {
			ApplicationInfo info = pm.getApplicationInfo(packageName, 0);
			icon = info.icon;
			appName = info.name;
		} catch (PackageManager.NameNotFoundException e) {
			icon = 0;
		}
		if(!TextUtils.isEmpty(iconName)){
			builder.setSmallIcon(ResHelper.getBitmapRes(context, iconName));
		} else{
			builder.setSmallIcon(icon);
		}
		if (TextUtils.isEmpty(title)) {
			builder.setContentTitle(appName);
		} else {
			builder.setContentTitle(title);
		}
		builder.setContentText(content);
		builder.setTicker(tickerText);
		builder.setWhen(when);
		if (Build.VERSION.SDK_INT >= 21) {
			builder.setColor(0x00000000);
		}
		builder.setContentIntent(pi);
		builder.setAutoCancel(true);
		if (voice && shake && light) {
			builder.setDefaults(Notification.DEFAULT_SOUND | Notification.DEFAULT_VIBRATE | Notification.DEFAULT_LIGHTS);
		} else if (voice && shake) {
			builder.setDefaults(Notification.DEFAULT_SOUND | Notification.DEFAULT_VIBRATE);
		} else if (voice && light) {
			builder.setDefaults(Notification.DEFAULT_SOUND | Notification.DEFAULT_LIGHTS);
		} else if (shake && light) {
			builder.setDefaults(Notification.DEFAULT_VIBRATE | Notification.DEFAULT_LIGHTS);
		} else if (voice) {
			builder.setDefaults(Notification.DEFAULT_SOUND);
		} else if (shake) {
			builder.setDefaults(Notification.DEFAULT_VIBRATE);
		} else {
			builder.setSound(null);
			builder.setVibrate(null);
			if (light) {
				builder.setDefaults(Notification.DEFAULT_LIGHTS);
			} else {
				builder.setLights(0, 0, 0);
			}
		}
		builder.setContentText(content);
		if (Build.VERSION.SDK_INT >= 16) {
			if (styleInt == MobPushNotifyMessage.STYLE_BIG_TEXT) {    //大段文本
				Notification.BigTextStyle style = new Notification.BigTextStyle();
				style.setBigContentTitle(title).bigText(styleContent);
				builder.setStyle(style);
			} else if (styleInt == MobPushNotifyMessage.STYLE_INBOX) {//收件箱
				Notification.InboxStyle style = new Notification.InboxStyle();
				style.setBigContentTitle(title);
				if (inboxStyleContent != null && inboxStyleContent.length > 0) {
					for (String item : inboxStyleContent) {
						if (item == null) {
							item = "";
						}
						style.addLine(item);
					}
				}
				builder.setStyle(style);
			} else if (styleInt == MobPushNotifyMessage.STYLE_BIG_PICTURE) {//大图类型
				Notification.BigPictureStyle style = new Notification.BigPictureStyle();
				Bitmap bitmap = BitmapFactory.decodeFile(styleContent);
				if (bitmap != null) {
					style.setBigContentTitle(title).bigPicture(bitmap);
				}
				builder.setStyle(style);
			}
		}
		System.out.println("CustomNotification style:" + builder.toString());
		return getNotification(builder);
	}

	private Notification getNotification(Notification.Builder builder) {
		return Build.VERSION.SDK_INT >= 16 ? builder.build() : builder.getNotification();
	}

	private HashMap<String, Object> mapScreen(HashMap<String, Object> map){
		Iterator iter = result.entrySet().iterator();
		while (iter.hasNext()) {
			Map.Entry entry = (Map.Entry) iter.next();
			Object key = entry.getKey();
			Object value = entry.getValue();
			try {
				if(value == null){
					map.remove(key);
				} else{
					if(value instanceof Integer){
						int i = Integer.valueOf(value.toString());
					} else if(value instanceof Long){
						Long l = Long.valueOf(value.toString());
					} else if(value instanceof Boolean){
						boolean b = Boolean.parseBoolean(value.toString());
					}
				}
			} catch (Exception e){
				e.printStackTrace();
				map.remove(key);
			}
		}
		return map;
	}
}