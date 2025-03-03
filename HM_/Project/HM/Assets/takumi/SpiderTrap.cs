using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class SpiderTrap : MonoBehaviour
{
    GameObject Hunter;

    Hunter_AI[] Hunter_AI = new Hunter_AI[2];

    [SerializeField] Material Material;
    [SerializeField] Gradient Gradient;
    List<GameObject> DestroyObject = new List<GameObject>(4);
    int Number = 0;

    const float range = 3;

    [SerializeField] GameObject Spider;
    int capacity = 0;
    public void OnTriggerEnter(Collider other)
    {
        if (capacity <= 0) return;

        if (other.transform.tag != "Hunter") return;

        if (Hunter == other.gameObject) return;

        Hunter = other.gameObject;

        Hunter_AI[Number] = Hunter.GetComponent<Hunter_AI>();




        //拘束の関数を呼ぶ
        Hunter_AI[Number].StartRestraining();

        Number++;


        capacity--;
        time = 0;


    }
    public void ResetTime() {time = 0;}

    async UniTask Remove(LineRenderer line)
    {


        while (line.positionCount >= 1)
        {

            line.positionCount--;

            await UniTask.DelayFrame(1);
        }
    }


    private List<Vector3> GetSpiderPosition(Vector3 pos)
    {
        List<Vector3> list = new List<Vector3>();

        float Rnad = Random.Range(0, 360);

        for (int i = 0; i < 4; i++)
        {

            Vector3 RandPos = pos + new Vector3(Mathf.Sin(Rnad + (i * 90) * Mathf.Deg2Rad) * range, 0, Mathf.Cos(Rnad + (i * 90) * Mathf.Deg2Rad) * range);

            list.Add(RandPos);





        }

        return list;


    }

    private void Awake()
    {
        capacity = 1;
    }

    bool capacityCount = false;

    [SerializeField] float time = 0;

    private void FixedUpdate()
    {
        if (this.gameObject.transform.localScale.x >= 15 && capacityCount == false) { capacity++; capacityCount = true; }

        time += Time.deltaTime;

        if (time < 13) return;

        capacityCount = false;
        capacity = 1;


        //ハンターの拘束攻撃を終了する処理をかく

        for (int i = 0; i < Number; i++)
        {
            Hunter_AI[i].StopRestraining();

        }
        for (int i = 0; i < DestroyObject.Count; i++) Destroy(DestroyObject[i]);
        DestroyObject.Clear();


        Hunter = null;
        time = 0;
        this.gameObject.transform.localScale = Vector3.one;

        //ナビメッシュのベイクのし直しをするスプリクトをデリート//まだ






        Number = 0;

        this.gameObject.SetActive(false);


    }

}
