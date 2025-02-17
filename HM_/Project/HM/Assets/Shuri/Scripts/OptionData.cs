using UnityEngine;

[System.Serializable]
public class OptionData : MonoBehaviour
{
    [SerializeField] TextAsset _option;
    public int sensibility;
    public int volumeBGM;
    public int volumeSE;

    OptionData() 
    {
        string jsonText = _option.ToString();

        JsonNode json = JsonNode.Parse(jsonText);

        sensibility = int.Parse(json["sensibility"].Get<string>());
        volumeBGM = int.Parse(json["BGMvolume"].Get<string>());
        volumeSE = int.Parse(json["SEvolume"].Get<string>());
    }
}
