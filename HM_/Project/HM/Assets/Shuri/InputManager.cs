using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] TextAsset _keyConfig;

    static InputManager instance;

    public struct UseKey
    {
        public string a;
        public string b;
        public string x;
        public string y;
        public string start;
        public string back;
        public string rt;
        public string lt;
        public string rb;
        public string lb;
    }

    public UseKey config;

    private void Start()
    {
        if (instance != null) Destroy(this.gameObject);

        instance = this;

        DontDestroyOnLoad(this.gameObject);

        string jsonText = _keyConfig.ToString();

        JsonNode json = JsonNode.Parse(jsonText);

        config.a = json["a"].Get<string>();
        config.b = json["b"].Get<string>();
        config.x = json["x"].Get<string>();
        config.y = json["y"].Get<string>();
        config.start = json["start"].Get<string>();
        config.back = json["back"].Get<string>();
        config.rt = json["rt"].Get<string>();
        config.lt = json["lt"].Get<string>();
        config.rb = json["rb"].Get<string>();
        config.lb = json["lb"].Get<string>();
    }
}
