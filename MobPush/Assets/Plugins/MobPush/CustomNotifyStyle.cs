using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace com.mob.mobpush
{
	public class CustomNotifyStyle {

#if UNITY_IPHONE || UNITY_IOS
		public Hashtable styleParams = new Hashtable();
#endif


#if UNITY_ANDROID
		public static int TYPE_NORMAL = 0;
		public static int TYPE_BIG_TEXT = 1;
		public static int TYPE_BIG_PICTURE = 2;
		public static int TYPE_INBOX = 3;
		public Hashtable styleParams = new Hashtable();
#endif




#if UNITY_IPHONE || UNITY_IOS
		public void setType(AuthorizationType type) {
			styleParams ["type"] = type;
		}

		public void setCategorys(string categorys) {
			styleParams ["categorys"] = categorys;
		}

		public string getStyleParamsStr() {
			String jsonStr = MiniJSON.jsonEncode(styleParams);
			return jsonStr;
		}
#endif

#if UNITY_ANDROID
		public void setContent(string content) {
			styleParams ["content"] = content;
		}

		public void setTitle(string title) {
			styleParams ["title"] = title;
		}

		public void setTickerText(string tickerText) {
			styleParams ["tickerText"] = tickerText;
		}

		public void setStyle(int style) {
			styleParams ["style"] = style;
		}

		public void setStyleContent(string styleContent) {
			styleParams ["styleContent"] = styleContent;
		}

		public void setInboxStyleContent(string[] inboxStyleContent) {
			styleParams["inboxStyleContent"] = String.Join(",", inboxStyleContent);
		}

		public void setTimestamp(long timeStamp) {
			styleParams ["timeStamp"] = timeStamp;
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

		public string getStyleParamsStr() {
			String jsonStr = MiniJSON.jsonEncode(styleParams);
			return jsonStr;
		}
#endif

	}
}