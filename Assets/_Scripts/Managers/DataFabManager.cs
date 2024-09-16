using UnityEngine;
using Newtonsoft.Json;

public static class DataFabManager 
{
	#region Fileds 亻尔 女子
	#endregion

	#region Methods
	public static void LocalSave(string key,System.Object Data)
	{
		PlayerPrefs.SetString(key, JsonConvert.SerializeObject(Data));
	}

	public static T LocalLoad<T>(string key)
	{
		var data=PlayerPrefs.GetString(key);
		return JsonConvert.DeserializeObject<T>(data);
	}
	#endregion
}
