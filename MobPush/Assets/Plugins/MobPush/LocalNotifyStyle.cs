using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace com.mob.mobpush
{
	public class LocalNotifyStyle {
		public Hashtable styleParams = new Hashtable();

		public void setContent(string content) {
			styleParams ["content"] = content;
		}

		public void setTitle(string title) {
			styleParams ["title"] = title;
		}

		public void setTimestamp(long timestamp) {
			styleParams ["timestamp"] = timestamp;
		}

		public void setId(string id) {
			styleParams ["id"] = id;
		}

		public void setExtras(Hashtable extras) {
			string extrasStr = MiniJSON.jsonEncode(extras);
			styleParams["extras"] = extrasStr;
		}

		public string getStyleParamsStr() {
			String jsonStr = MiniJSON.jsonEncode(styleParams);
			return jsonStr;
		}
#if UNITY_IPHONE || UNITY_IOS
		public void setSubTitle(string subTitle) {
			styleParams ["subTitle"] = subTitle;
		}

		public void setSound(long sound) {
			styleParams ["sound"] = sound;
		}

		public void setBadge(int badge) {
			styleParams ["badge"] = badge;
		}
#endif

#if UNITY_ANDROID
		public void setStyle(int style) {
			styleParams ["style"] = style;
		}

		public void setStyleContent(string styleContent) {
			styleParams ["styleContent"] = styleContent;
		}

		public void setVoice(bool isVoice) {
			styleParams ["isVoice"] = isVoice;
		}

		public void setShark(bool isShark) {
			styleParams ["isShark"] = isShark;
		}

		public void setLinght(bool isLight) {
			styleParams ["isLight"] = isLight;
		}
#endif

	}
}