using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public partial class ConfigDataManager : MonoBehaviour {

	public const string AssetPath = "Assets/Resources/ConfigData";
	const int DataStartRow = 3;
	const char ArraySplitMark = '|';

	public static ConfigDataManager Instance;

	public void Awake()
	{
        Debug.Log("ConfigDataManager:Awake");
		Instance = this;
        Instance.Init();
        Instance.PostInit();
    }

	public void Init()
	{
		var csvs = Resources.LoadAll<TextAsset>("ConfigData/");
		foreach(var csv in csvs)
		{
			var name = csv.name;
			string str = System.Text.Encoding.GetEncoding("utf-8").GetString(csv.bytes);
			var strGrid = CsvParser.Parse(str);
			var initMethodName = string.Format("Init{0}ConfigDic", name);
			var initMethod = this.GetType().GetMethod(initMethodName, BindingFlags.NonPublic | BindingFlags.Instance);
			if (initMethod != null)
			{
				initMethod.Invoke(this, new object[] { strGrid });
			}
		}
	}

	private void ParseConfigDataDic<T>(List<string[]> content, Dictionary<int,T> dic) where T : ConfigDataBase
	{
		dic.Clear();
		if (content.Count < DataStartRow)
			return;
		var fieldNames = content[0];
		for(int i = DataStartRow; i < content.Count; i++)
		{
			var rowContent = content[i];
			int id = int.Parse(rowContent[0]);
			var configData = ParseConfigData<T>(content[i]);
			dic.Add(id, configData);
		}
	}

	private T ParseConfigData<T>(string[] content) where T : ConfigDataBase
	{
		var type = typeof(T);
		var fields = type.GetFields();
		List<FieldInfo> realFields = new List<FieldInfo>();
		for(int i = 0; i < fields.Length; i++)
		{
			var field = fields[i];
			var customAttris = field.GetCustomAttributes(typeof(FieldAttribute), false);
			if (customAttris.Length != 0)
			{
				realFields.Add(field);
			}
		}
		var instance = System.Activator.CreateInstance<T>();
		for(int i = 0; i < realFields.Count; i++)
		{
			var fieldInfo = realFields[i];
			var fieldName = fieldInfo.Name;
			var data = GetData(fieldInfo.FieldType, content[i]);
			fieldInfo.SetValue(instance, data);
		}
		return instance;
	}

	private bool IsFieldTypeValid(Type t)
	{
		var res = s_FieldTypes.Contains(t);
		return res;
	}

	private static HashSet<Type> s_FieldTypes = new HashSet<Type>() { typeof(int), typeof(float), typeof(string),typeof(int[]) };

	private object GetData(Type t,string content)
	{
		if (!IsFieldTypeValid(t))
		{
			return null;
		}
		object res = null;
		if (t == typeof(int))
		{
			if (string.IsNullOrEmpty(content))
				res = 0;
			else
			    res = int.Parse(content);
		}else if(t == typeof(float))
		{
			if (string.IsNullOrEmpty(content))
				res = 0;
			else
			    res = float.Parse(content);
		}
		else if(t == typeof(string))
		{
			res = content;
		}else if(t == typeof(int[]))
		{
			var splits = content.Split(new char[] { ArraySplitMark });
			var data = new int[splits.Length];
			for(int i = 0; i < splits.Length; i++)
			{
				data[i] = (int)GetData(typeof(int), splits[i]);
			}
			res = data;
		}
		return res;
	}
}
