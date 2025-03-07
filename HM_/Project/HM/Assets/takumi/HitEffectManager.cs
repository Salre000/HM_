using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HitEffectManager : MonoBehaviour
{
    public static HitEffectManager instance;



    [Header("モンスターの攻撃によるヒットエフェクトモデル")]
    [SerializeField] GameObject MonsterEffect;
    [Header("モンスターの攻撃エフェクトのプール")]
    [SerializeField] GameObject[] MonsterEffectPool=new GameObject[20];
    
    [Header("モンスターの血によるヒットエフェクトモデル")]
    [SerializeField] GameObject BloodEffect;
    [Header("モンスターの血による攻撃エフェクトのプール")]
    [SerializeField] GameObject[] MonsterBloodEffectPool=new GameObject[20];


    [Header("ハンターの攻撃によるヒットエフェクトモデル")]
    [SerializeField] GameObject []HunterEffect=new GameObject[HunterCount];
    [Header("ハンターの攻撃エフェクトのプール")]
    [SerializeField] GameObject[][] HunterEffectPool=new GameObject[HunterCount][];


    const int HunterCount = 4;

    [SerializeField] float MonsterEffectSize=1.5f;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        for(int i = 0; i < HunterCount; i++) 
        {
            HunterEffectPool[i]=new GameObject[30];
            for(int j = 0; j < 30; j++) 
            {
                HunterEffectPool[i][j] = Instantiate(HunterEffect[i],this.transform);
                HunterEffectPool[i][j].SetActive(false);
                HunterEffectPool[i][j].name = "Hunter";
            }
        }

        for(int i = 0; i < 20; i++) 
        {

            MonsterEffectPool[i] = Instantiate(MonsterEffect,this.transform);
            MonsterEffectPool[i].SetActive(false);  
                MonsterEffectPool[i].name = "Monster";
            MonsterEffectPool[i].transform.localScale = new Vector3(MonsterEffectSize, MonsterEffectSize, MonsterEffectSize);


            MonsterBloodEffectPool[i] = Instantiate(BloodEffect, this.transform);
            MonsterBloodEffectPool[i].SetActive(false);
            MonsterBloodEffectPool[i].name = "MonsterBlood";

        }
    }
    [SerializeField] const int EffectTime = 2;

    //一定時間後にエフェクトが消えるようにする
    private async UniTask InvisibleObject(GameObject EffectObject,int Time) 
    {

        await UniTask.DelayFrame(Time*10);

        //エフェクトの状態を最初に戻す


        EffectObject.SetActive(false);


    



    }
    public enum CharacterType 
    {
        Monster=0,
        Hammer,
        Bow,
        Spear,
        sword,
        None=-1


    }
     
    private GameObject GetPoolObject(GameObject[] Object) 
    {
        for(int i = 0; i < Object.Length; i++) 
        {
            if (Object[i].activeSelf == true) continue;

            Object[i].SetActive(true);

            return Object[i];


        }

        return null;



    }

    //vector３はエフェクトを出現させる座標
    public  void HitEffectShow(Vector3 pos ,CharacterType type,int time= EffectTime) 
    {
        GameObject Effect;

        if (type == CharacterType.Monster)
        {
            Effect = GetPoolObject(MonsterEffectPool);
            Effect.transform.position = pos;

            Effect.transform.LookAt(Camera.main.transform.position);

            Effect.GetComponent<ParticleSystem>()?.Play();


            InvisibleObject(Effect,time);
           


            return;
        }
        if (type != CharacterType.None)
        {
            if ((int)type != 1) 
            {
                int ssss = 0;
            }
            Effect = GetPoolObject(HunterEffectPool[(int)type-1]);

            Effect.transform.position = pos;
            GameObject Blood = GetPoolObject(MonsterBloodEffectPool);

            Blood.transform.position = pos;
            Blood.transform.LookAt(Camera.main.transform.position);
            Blood.GetComponent<ParticleSystem>()?.Play();

            InvisibleObject(Blood, time);

            InvisibleObject(Effect,time);

            return;
        }

        return;




    }


}
