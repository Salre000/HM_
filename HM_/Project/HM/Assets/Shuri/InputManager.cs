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

    //public UseKey config;

    const float DeadZone = 0.3f;

    private void Awake()
    {
        if (instance != null) Destroy(this.gameObject);

        instance = this;

        DontDestroyOnLoad(this.gameObject);

        // �p�X���擾
        _filepath = Application.dataPath + "/KeyConfig2.json";

        // �t�@�C�����Ȃ��Ƃ��A�t�@�C���쐬
        if (!File.Exists(_filepath)) Save(data);

        // �t�@�C����ǂݍ����data�Ɋi�[
        data = Load(_filepath);

        string jsonText = _keyConfig.ToString();

        JsonNode json = JsonNode.Parse(jsonText);

        for (int i = 0; i < (int)InputKeys.Max; i++)
        {
            keys[i].keyName = json["Name" + (i + 1).ToString()].Get<string>();
            keys[i].type = (KeyType)int.Parse(json["Type" + (i + 1).ToString()].Get<string>());
        }
    }

    public bool IsOnButton(Key key)
    {
        KeyType type = key.type;
        string keyName = key.keyName;

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
        keys[index] = await ChangeCheckKeyConfig.ChangeKey();

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

    // �ۑ�
    void Save(ConfigData data)
    {
        // json�ϊ�
        string json = JsonUtility.ToJson(data);

        // �������ݎw��
        StreamWriter wr = new(_filepath, false);

        // ��������
        wr.WriteLine(json);

        // �t�@�C�������
        wr.Close();
    }

    // json�t�@�C���ǂݍ���
    ConfigData Load(string path)
    {
        // �ǂݍ��ݎw��
        StreamReader rd = new(path);

        // �t�@�C�����e�S�ēǂݍ���
        string json = rd.ReadToEnd();

        // �t�@�C�������
        rd.Close();

        // json�t�@�C�����^�ɖ߂��ĕԂ�
        return JsonUtility.FromJson<ConfigData>(json);
    }
}
