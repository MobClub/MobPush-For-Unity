using System;
using System.Collections;

namespace com.mob.mobpush
{
#if UNITY_IPHONE || UNITY_IOS
	public enum AuthorizationType {
		None = 0,
		Badge = 1,
		Sound = 2,
		Alert = 4
	}
#endif


}