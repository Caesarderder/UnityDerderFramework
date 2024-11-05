using UnityEngine;
using Newtonsoft.Json;

public static class DataFabUtil 
{
	#region Fileds 亻尔 女子
	#endregion

	#region Methods
	public static void LocalSave(string key,System.Object Data)
	{
        Debug.Log($"Save LocalData:{key} data:{JsonConvert.SerializeObject(Data)}");
		PlayerPrefs.SetString(key, JsonConvert.SerializeObject(Data));
	}

	public static T LocalLoad<T>(string key) where T: new()
	{
		var data=PlayerPrefs.GetString(key);
        Debug.Log($"Load LocalData:{key} data:{data}");
		if(string.IsNullOrEmpty(data))
		{
			return new T(); 
		}
		return JsonConvert.DeserializeObject<T>(data);
	}
	#endregion
}
