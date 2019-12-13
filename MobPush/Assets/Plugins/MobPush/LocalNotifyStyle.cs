using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace com.mob.mobpush{
	public class LocalNotifyStyle {


		public Hashtable styleParams = new Hashtable();

		public void setContent(string content){
			styleParams ["content"] = content;
		}

		public void setTitle(string title){
			styleParams ["title"] = title;
		}
		
		/*延迟的时间（毫秒）*/
		public void setTimestamp(long timeStamp){
			styleParams["timeStamp"] = timeStamp;
		}

        // id 唯一，否则会覆盖上一条消息，id 作为删除通知的标志
        public void setId(string id) {
            styleParams["id"] = id;
        }

        public void setExtras(Hashtable extras){
			string extrasStr = MiniJSON.jsonEncode (extras);
			styleParams["extras"] = extrasStr;
		}

#if UNITY_IPHONE

			public void setSubTitle(string subTitle){
				styleParams["subTitle"] = subTitle;
			}

			public void setSound(string sound){
				styleParams["sound"] = sound;
			}

			public void setBadge(int badge){
				styleParams["badge"] = badge;
			}


#endif

#if UNITY_ANDROID

			public void setStyle(int style){
				styleParams ["style"] = style;
			}

			public void setStyleContent(string styleContent){
				styleParams["styleContent"] = styleContent;
			}


			public void setVoice(bool isVoice){
				styleParams["isVoice"] = isVoice;
			}

			public void setShark(bool isShark){
				styleParams["isShark"] = isShark;
			}

			public void setLinght(bool isLight){
				styleParams["isLight"] = isLight;
			}

#endif

        public String getStyleParamsStr() {
			String jsonStr = MiniJSON.jsonEncode (styleParams);
			Debug.Log("StyleParams  ===>>> " + jsonStr );
			return jsonStr;
		}

	}
}
