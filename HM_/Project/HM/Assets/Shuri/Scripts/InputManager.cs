using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] TextAsset _keyConfig;

    public static InputManager instance;

    [HideInInspector] public ConfigData data;

    string _filepath;

    public enum KeyType
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
        public KeyType type;
    }

    const float DeadZone = 0.3f;

    private void Awake()
    {
        if (instance != null) Destroy(this.gameObject);

        instance = this;

        DontDestroyOnLoad(this.gameObject);

        // パス名取得
        _filepath = Application.dataPath + "/KeyConfig2.json";

        // ファイルがないとき、ファイル作成
        if (!File.Exists(_filepath)) Save(data);

        // ファイルを読み込んでdataに格納
        data = Load(_filepath);

        string jsonText = _keyConfig.ToString();

        JsonNode json = JsonNode.Parse(jsonText);

        for (int i = 0; i < (int)InputKeys.Max; i++)
        {
            keys[i].keyName = json["name"][i].Get<string>();
            keys[i].type = (KeyType)int.Parse(json["types"][i].Get<string>());
        }
    }

    public bool IsOnButton(Key key)
    {
        KeyType type = key.type;
        string keyName = key.keyName;

        // タイプごとの取得方法で押されているか判定
        switch (type)
        {
            case KeyType.Key:
                return IsOnKey(keyName);
            case KeyType.AxisPlus:
                if (Input.GetAxis(keyName) > DeadZone) return true; break;
            case KeyType.AxisMinus:
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
                keys[i].type = KeyType.None;
            }
        }
    }

    public bool EnableAllKey()
    {
        for (int i = 0; i < (int)InputKeys.Max; i++)
        {
            if (keys[i].type == KeyType.None) return false;
        }
        return true;
    }

    // 保存
    public void Save(ConfigData data)
    {
        //for (int i = 0; i < ConfigData.ButtonNum; i++)
        //{
        //    data.name[i]=keys[i].keyName;
        //    data.types[i] = ((int)keys[i].type).ToString();
        //}
        // json変換
        string json = JsonUtility.ToJson(data);

        // 書き込み指定
        StreamWriter wr = new(_filepath, false);

        // 書き込み
        wr.WriteLine(json);

        // ファイルを閉じる
        wr.Close();
    }

    // jsonファイル読み込み
    ConfigData Load(string path)
    {
        // 読み込み指定
        StreamReader rd = new(path);

        // ファイル内容全て読み込む
        string json = rd.ReadToEnd();

        // ファイルを閉じる
        rd.Close();

        // jsonファイルを型に戻して返す
        return JsonUtility.FromJson<ConfigData>(json);
    }
}
