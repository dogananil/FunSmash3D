using UnityEngine;

namespace Voodoo.Sauce.Internal {
	[CreateAssetMenu(fileName = "Assets/Resources/TinySauce/TinySettings", menuName = "TinySauce/Settings file")]
	public class TinySettings : ScriptableObject {
		public  const string   tenjinApiKey = "E3OTCZ3UQ1OUEWUD3W9SO8JT6REPCZ5U" ;
		
		[Header("Tiny Sauce version 3.0.0", order = 0)]
		[Header("GameAnalytics",  order = 1)]
		[Tooltip("Your GameAnalytics Ios Game Key - copy/paste from the GA website")]
		public string gameAnalyticsIosGameKey;

		[Tooltip("Your GameAnalytics Ios Secret Key - copy/paste from the GA website")]
		public string gameAnalyticsIosSecretKey;
		
		[Tooltip("Your GameAnalytics Android Game Key - copy/paste from the GA website")]
		public string gameAnalyticsAndroidGameKey;

		[Tooltip("Your GameAnalytics Android Secret Key - copy/paste from the GA website")]
		public string gameAnalyticsAndroidSecretKey;
		
		[Header("Facebook")]
		[Tooltip("The Facebook App Id of your game - copy/paste from Facebook website")]
		public string facebookAppId;
		
		
	}
}
