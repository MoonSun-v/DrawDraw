using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// -----------------------------------------------------------------------------------------------------
// ★ Unity의 JsonUtility는 Dictionary를 직렬화하거나 역직렬화할 수 없기 때문에 ★
// Dictionary<TKey, TValue>를 JSON 형식으로 직렬화(Serialization)하거나,
// JSON에서 Dictionary<TKey, TValue>로 역직렬화(Deserialization)할 수 있도록 도와주는
// 유틸리티 클래스를 만들어서 사용한다.
// -----------------------------------------------------------------------------------------------------


[Serializable]                                    // 이 클래스가 JSON으로 직렬화될 수 있음을 의미
public class DataDictionary<TKey, TValue>         // 한 쌍의 키와 값을 저장하는 용도로 사용
{
    public TKey Key;
    public TValue Value;
}

[Serializable]
public class JsonDataArray<TKey, TValue>          // 모든 키-값 쌍을 저장하는 리스트를 포함
{
    public List<DataDictionary<TKey, TValue>> data;
}

public static class DictionaryJsonUtility
{

    // ★ [ Dictionary -> Json으로 파싱 ]
    public static string ToJson<TKey, TValue>(Dictionary<TKey, TValue> jsonDicData, bool pretty = false)
    {
        List<DataDictionary<TKey, TValue>> dataList = new List<DataDictionary<TKey, TValue>>();
        DataDictionary<TKey, TValue> dictionaryData;
        foreach (TKey key in jsonDicData.Keys)
        {
            dictionaryData = new DataDictionary<TKey, TValue>();
            dictionaryData.Key = key;
            dictionaryData.Value = jsonDicData[key];
            dataList.Add(dictionaryData);
        }
        JsonDataArray<TKey, TValue> arrayJson = new JsonDataArray<TKey, TValue>();
        arrayJson.data = dataList;

        return JsonUtility.ToJson(arrayJson, pretty); // 반환값: JSON 문자열
    }


    // ★ [ Json Data -> Dictionary로 파싱 ]
    public static Dictionary<TKey, TValue> FromJson<TKey, TValue>(string jsonData)
    {
        JsonDataArray<TKey, TValue> arrayJson = JsonUtility.FromJson<JsonDataArray<TKey, TValue>>(jsonData);
        List<DataDictionary<TKey, TValue>> dataList = arrayJson.data;

        Dictionary<TKey, TValue> returnDictionary = new Dictionary<TKey, TValue>();

        for (int i = 0; i < dataList.Count; i++)
        {
            DataDictionary<TKey, TValue> dictionaryData = dataList[i];
            returnDictionary[dictionaryData.Key] = dictionaryData.Value;
        }

        return returnDictionary;
    }
}