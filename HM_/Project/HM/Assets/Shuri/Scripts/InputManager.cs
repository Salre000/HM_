using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using static JsonDataModule;
public class InputManager : MonoBehaviour
{

    public static InputManager instance;

    [HideInInspector] public ConfigData data;

    private string _filepath = "/KeyConfig.json";
    private string _configBasePath = "/KeyConfig_Base.json";

    public enum InputType
    {
        None,
        Key,
        AxisPlus,
        AxisMinus,
    }

    public enum InputKeys
    {
        A,
        B,
        X,
        Y,
        RT,
        LT,
        RB,
        LB,
        Right,
        Left,
        Up,
        Down,
        Max,
    }

    public Key[] keys = new Key[(int)InputKeys.Max];

    public struct Key
    {
        public string keyName;
        public InputType type;
    }

    const float DeadZone = 0.3f;

    private void Awake()
    {
        TextMeshProUGUI text = GameObject.Find("Text").GetComponent<TextMeshProUGUI>();

        text.text = Application.dataPath;


        if (instance != null) Destroy(this.gameObject);

        instance = this;

        DontDestroyOnLoad(this.gameObject);

        // パス名取得
        _filepath = Application.dataPath + _filepath;
        _configBasePath = Application.dataPath + _configBasePath;

        // ファイルを読み込んでdataに格納
        data = Load<ConfigData>(_filepath);
        
        for (int i = 0; i < (int)InputKeys.Max; i++)
        {
            keys[i].keyName = data.name[i];
            keys[i].type = (InputType)int.Parse(data.types[i]);
        }

        // ファイルがないとき、ファイル作成
        if (!File.Exists(_filepath)) Save(data,_filepath);

    }

    private void Update()
    {
        IsOnButton(InputKeys.RB);
    }

    public bool IsOnButton(InputKeys inputKey)
    {
        Key key = keys[(int)inputKey];

        InputType type = key.type;
        string keyName = key.keyName;


        // タイプごとの取得方法で押されているか判定
        switch (type)
        {
            case InputType.Key:
                return IsOnKey(keyName);
            case InputType.AxisPlus:
                if (Input.GetAxis(keyName) > DeadZone) return true; break;
            case InputType.AxisMinus:
                if (Input.GetAxis(keyName) < -DeadZone) return true; break;
        }
        return false;
    }

    bool IsOnKey(string key)
    {
        if (Input.GetKey(key)) return true;
        return false;
    }

    public async void KeyChange(int index)
    {
        keys[index] = await CheckChangeKeyConfig.ChangeKey();

        for (int i = 0; i < (int)InputKeys.Max; i++)
        {
            if (i == index) continue;
            if (keys[i].keyName == keys[index].keyName && keys[i].type == keys[index].type)
            {
                keys[i].keyName = null;
                keys[i].type = InputType.None;
            }
        }
    }

    public bool EnableAllKey()
    {
        for (int i = 0; i < (int)InputKeys.Max; i++)
        {
            if (keys[i].type == InputType.None) return false;
        }
        return true;
    }

    //// 保存
    //public void Save()
    //{
    //    for (int i = 0; i < ConfigData.ButtonNum; i++)
    //    {
    //        data.name[i] = keys[i].keyName;
    //        data.types[i] = ((int)keys[i].type).ToString();
    //    }
    //    // json変換
    //    string json = JsonUtility.ToJson(data);

    //    // 書き込み指定
    //    StreamWriter wr = new(_filepath, false);

    //    // 書き込み
    //    wr.WriteLine(json);

    //    // ファイルを閉じる
    //    wr.Close();
    //}

    //// jsonファイル読み込み
    //private ConfigData Load(string path)
    //{
    //    // 読み込み指定
    //    StreamReader rd = new(path);

    //    // ファイル内容全て読み込む
    //    string json = rd.ReadToEnd();

    //    // ファイルを閉じる
    //    rd.Close();

    //    // jsonファイルを型に戻して返す
    //    return JsonUtility.FromJson<ConfigData>(json);
    //}

    public void ConfigReset()
    {
        data = Load<ConfigData>(_configBasePath);
        for (int i = 0; i < (int)InputKeys.Max; i++)
        {
            keys[i].keyName = data.name[i];
            keys[i].type = (InputType)int.Parse(data.types[i]);
        }
        Save(data, _filepath);
    }
}
