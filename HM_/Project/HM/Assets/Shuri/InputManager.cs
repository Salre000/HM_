using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] TextAsset _keyConfig;

    public static InputManager instance;

    public enum KeyType
    {
        None,
        Key,
        AxisPlus,
        AxisMinus,
    }

    //public struct UseKey
    //{
    //    public Key a;
    //    public Key b;
    //    public Key x;
    //    public Key y;
    //    public Key rt;
    //    public Key lt;
    //    public Key rb;
    //    public Key lb;
    //    public Key right;
    //    public Key left;
    //    public Key up;
    //    public Key down;
    //}

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

        string jsonText = _keyConfig.ToString();

        JsonNode json = JsonNode.Parse(jsonText);

        for (int i = 0; i < (int)InputKeys.Max; i++)
        {
            keys[i].keyName = json["Name" + (i + 1).ToString()].Get<string>();
            keys[i].type = (KeyType)int.Parse(json["Type" + (i + 1).ToString()].Get<string>());
        }

        //config.a.keyName = json["a"].Get<string>();
        //config.b.keyName = json["b"].Get<string>();
        //config.x.keyName = json["x"].Get<string>();
        //config.y.keyName = json["y"].Get<string>();
        //config.rt.keyName = json["rt"].Get<string>();
        //config.lt.keyName = json["lt"].Get<string>();
        //config.rb.keyName = json["rb"].Get<string>();
        //config.lb.keyName = json["lb"].Get<string>();
        //config.right.keyName = json["pad_right"].Get<string>();
        //config.left.keyName = json["pad_left"].Get<string>();
        //config.up.keyName = json["pad_up"].Get<string>();
        //config.down.keyName = json["pad_down"].Get<string>();


        //config.a.type = (KeyType)int.Parse(json["TypeA"].Get<string>());
        //config.b.type = (KeyType)int.Parse(json["TypeB"].Get<string>());
        //config.x.type = (KeyType)int.Parse(json["TypeX"].Get<string>());
        //config.y.type = (KeyType)int.Parse(json["TypeY"].Get<string>());
        //config.rt.type = (KeyType)int.Parse(json["TypeRT"].Get<string>());
        //config.lt.type = (KeyType)int.Parse(json["TypeLT"].Get<string>());
        //config.rb.type = (KeyType)int.Parse(json["TypeRB"].Get<string>());
        //config.lb.type = (KeyType)int.Parse(json["TypeLB"].Get<string>());
        //config.right.type = (KeyType)int.Parse(json["TypeRight"].Get<string>());
        //config.left.type = (KeyType)int.Parse(json["TypeLeft"].Get<string>());
        //config.up.type = (KeyType)int.Parse(json["TypeUp"].Get<string>());
        //config.down.type = (KeyType)int.Parse(json["TypeDown"].Get<string>());

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
}
