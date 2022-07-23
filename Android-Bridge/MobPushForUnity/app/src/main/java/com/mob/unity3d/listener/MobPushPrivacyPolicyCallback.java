package com.mob.unity3d.listener;

import android.util.Log;

import com.mob.PrivacyPolicy;
import com.mob.tools.utils.Hashon;
import com.unity3d.player.UnityPlayer;

import java.util.HashMap;

public class MobPushPrivacyPolicyCallback implements PrivacyPolicy.OnPolicyListener {
    private static final String TAG = "getPrivacyPolicy";
    private String onPrivacyPolicyCallback;
    private String u3dGameObject;
    private Hashon hashon;

    public MobPushPrivacyPolicyCallback(String onPrivacyPolicyCallback, String u3dGameObject) {
        this.onPrivacyPolicyCallback = onPrivacyPolicyCallback;
        this.u3dGameObject = u3dGameObject;
        hashon = new Hashon();
    }

    @Override
    public void onComplete(PrivacyPolicy privacyPolicy) {
        try {
            if (privacyPolicy != null) {
                String privacy = privacyPolicyToJson(privacyPolicy);
                UnityPlayer.UnitySendMessage(u3dGameObject, onPrivacyPolicyCallback, privacy);
            } else {
                Log.d(TAG, "onComplete privacy is empty");
            }
        } catch (Exception e) {
            Log.d(TAG, "onComplete err:" + (e == null ? "" : e.getMessage()));
        }
    }

    @Override
    public void onFailure(Throwable throwable) {
        try {
            Log.d(TAG, "onFailure");
            UnityPlayer.UnitySendMessage(u3dGameObject, onPrivacyPolicyCallback, getErrorPrivacyPolicyJson());
        } catch (Exception e) {
            Log.d(TAG, "onFailure err:" + (e == null ? "" : e.getMessage()));
        }
    }

    private final String privacyPolicyToJson(PrivacyPolicy privacyPolicy) {
        if (null == privacyPolicy) {
            return null;
        }
        HashMap<String, Object> privacyMap = new HashMap<>();
        privacyMap.put("title", privacyPolicy.getTitle());
        privacyMap.put("content", privacyPolicy.getContent());
        privacyMap.put("ppVersion", privacyPolicy.getPpVersion());
        privacyMap.put("timestamp", privacyPolicy.getTimestamp());
        HashMap<String, Object> map = new HashMap<>();
        map.put("data", hashon.fromHashMap(privacyMap));
        map.put("errorCode", 0);
        return hashon.fromHashMap(map);
    }

    private final String getErrorPrivacyPolicyJson() {
        HashMap<String, Object> map = new HashMap<>();
        map.put("data", "");
        map.put("errorCode", -1);
        return hashon.fromHashMap(map);
    }
}
