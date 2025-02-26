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

        // �p�X���擾
        _filepath = Application.dataPath + _filepath;
        _configBasePath = Application.dataPath + _configBasePath;

        // �t�@�C����ǂݍ����data�Ɋi�[
        data = Load<ConfigData>(_filepath);
        
        for (int i = 0; i < (int)InputKeys.Max; i++)
        {
            keys[i].keyName = data.name[i];
            keys[i].type = (InputType)int.Parse(data.types[i]);
        }

        // �t�@�C�����Ȃ��Ƃ��A�t�@�C���쐬
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


        // �^�C�v���Ƃ̎擾���@�ŉ�����Ă��邩����
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

    //// �ۑ�
    //public void Save()
    //{
    //    for (int i = 0; i < ConfigData.ButtonNum; i++)
    //    {
    //        data.name[i] = keys[i].keyName;
    //        data.types[i] = ((int)keys[i].type).ToString();
    //    }
    //    // json�ϊ�
    //    string json = JsonUtility.ToJson(data);

    //    // �������ݎw��
    //    StreamWriter wr = new(_filepath, false);

    //    // ��������
    //    wr.WriteLine(json);

    //    // �t�@�C�������
    //    wr.Close();
    //}

    //// json�t�@�C���ǂݍ���
    //private ConfigData Load(string path)
    //{
    //    // �ǂݍ��ݎw��
    //    StreamReader rd = new(path);

    //    // �t�@�C�����e�S�ēǂݍ���
    //    string json = rd.ReadToEnd();

    //    // �t�@�C�������
    //    rd.Close();

    //    // json�t�@�C�����^�ɖ߂��ĕԂ�
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
