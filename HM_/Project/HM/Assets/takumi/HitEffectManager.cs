using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.TextCore.Text;
using UnityEngine;
using UnityEngine.UIElements;

public class HitEffectManager : MonoBehaviour
{
    public static HitEffectManager instance;



    [Header("�����X�^�[�̍U���ɂ��q�b�g�G�t�F�N�g���f��")]
    [SerializeField] GameObject MonsterEffect;
    [Header("�����X�^�[�̍U���G�t�F�N�g�̃v�[��")]
    [SerializeField] GameObject[] MonsterEffectPool=new GameObject[20];

    [Header("�n���^�[�̍U���ɂ��q�b�g�G�t�F�N�g���f��")]
    [SerializeField] GameObject []HunterEffect=new GameObject[HunterCount];
    [Header("�n���^�[�̍U���G�t�F�N�g�̃v�[��")]
    [SerializeField] GameObject[][] HunterEffectPool=new GameObject[HunterCount][];


    const int HunterCount = 4;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        for(int i = 0; i < HunterCount; i++) 
        {
            HunterEffectPool[i]=new GameObject[10];
            for(int j = 0; j < 10; j++) 
            {
                HunterEffectPool[i][j] = Instantiate(HunterEffect[i],this.transform);
                HunterEffectPool[i][j].SetActive(false);
                HunterEffectPool[i][j].name = "hunter";
            }
        }

        for(int i = 0; i < 20; i++) 
        {

            MonsterEffectPool[i] = Instantiate(MonsterEffect,this.transform);
            MonsterEffectPool[i].SetActive(false);  
                MonsterEffectPool[i].name = "Monster";
        }
    }
    [SerializeField] const int EffectTime = 2;

    //��莞�Ԍ�ɃG�t�F�N�g��������悤�ɂ���
    private async UniTask InvisibleObject(GameObject EffectObject,int Time) 
    {

        await UniTask.DelayFrame(Time*10);

        //�G�t�F�N�g�̏�Ԃ��ŏ��ɖ߂�


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


    //vector�R�̓G�t�F�N�g���o����������W
    public  void HitEffectShow(Vector3 pos,CharacterType type,int time= EffectTime) 
    {
        GameObject Effect;

        if (type == CharacterType.Monster)
        {
            Effect = GetPoolObject(MonsterEffectPool);
            Effect.transform.position = pos;
            InvisibleObject(Effect,time);


            return;
        }
        if (type != CharacterType.None)
        {
            Effect = GetPoolObject(HunterEffectPool[(int)type]);
            Effect.transform.position = pos;
            InvisibleObject(Effect,time);

            return;
        }

        return;




    }

}
