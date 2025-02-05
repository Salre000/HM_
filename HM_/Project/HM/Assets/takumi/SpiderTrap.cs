using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class SpiderTrap : MonoBehaviour
{
    GameObject Hunter;

    Hunter_AI []Hunter_AI=new Hunter_AI[2];

    [SerializeField] Material Material;
    [SerializeField] Gradient Gradient;
    List<GameObject> DestroyObject=new List<GameObject>(4);
    int NUmber = 0;

    const float range = 3;

    [SerializeField] GameObject Spider;
    int capacity = 0;
    public void OnTriggerEnter(Collider other)
    {
        if (capacity <= 0) return;

        if (other.transform.tag != "Hunter") return;

        if (Hunter == other.gameObject) return;

        Hunter = other.gameObject;

        Hunter_AI[NUmber]=Hunter.GetComponent<Hunter_AI>();




        //拘束の関数を呼ぶ
        Hunter_AI[NUmber].StartRestraining();

        NUmber++;

        //蜘蛛の繭を生成


        List<Vector3> MinSpioderPos = GetSpiderPosition(other.transform.position);

        for(int i = 0; i < MinSpioderPos.Count; i++) 
        {
            GameObject game = new GameObject();
            game.name = "aaa";

            game.transform.SetParent(this.transform);

            LineRenderer line= game.AddComponent<LineRenderer>();

            line.positionCount = 0;


            line.startWidth = line.endWidth = 0.1f;

            line.material = Material;
            line.colorGradient = Gradient;

            MinSpider spider = new MinSpider(MinSpioderPos[i],other.transform.position, Instantiate(Spider), Object => Destroy(Object), (position,Start) => 
            {
                if (Vector3.Distance(Start, position) < 0.1f) return;

                line.positionCount++;
                line.SetPosition(line.positionCount-1,position);


            },async INT=> 
            {
               await Remove(line);


            }

            );


            //other.GetComponentInChildren<ChainPoint>()

            DestroyObject.Add(game);

            spider.StartSpider();


        }



        capacity--;
        time = 0;


    }

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

        for(int i = 0; i < 4; i++) 
        {

            Vector3 RandPos = pos + new Vector3(Mathf.Sin(Rnad+(i*90) * Mathf.Deg2Rad) * range, 0, Mathf.Cos(Rnad + (i * 90) * Mathf.Deg2Rad) * range);

            list.Add(RandPos);





        }

        return list;


    }

    private void Awake()
    {
        capacity = 1;
    }

    bool capacityCount = false;

    [SerializeField]float time = 0;

    private void FixedUpdate()
    {
        if (this.gameObject.transform.localScale.x >= 15 && capacityCount == false) { capacity++; capacityCount = true; }

        time += Time.deltaTime;

        if (time < 13) return;

        capacityCount = false;
        capacity = 1;


        //ハンターの拘束攻撃を終了する処理をかく

        for (int i = 0; i < NUmber; i++)
        {
            Hunter_AI[i].StopRestraining();

        }
        for (int i = 0; i < DestroyObject.Count; i++) Destroy(DestroyObject[i]);
        DestroyObject.Clear();
        

        Hunter = null;
        time = 0;
        this.gameObject.transform.localScale=Vector3.one;

        //ナビメッシュのベイクのし直しをするスプリクトをデリート//まだ


        



        NUmber = 0;

        this.gameObject.SetActive(false);


    }

}

class MinSpider 
{
    System.Action<GameObject> DestroyObjcet;
    System.Action<Vector3,Vector3> AddPos;
    System.Action<int> RemoveLine;
    public MinSpider(Vector3 pos,Vector3 target,GameObject Object,System.Action<GameObject> func,System.Action<Vector3,Vector3>addpos, System.Action<int> remove)
    {
        RemoveLine = remove;
        DestroyObjcet = func;
        AddPos = addpos;
        this.Object = Object;
        this.pos = pos;
        this.Target = target;

        Vec =Target-this.pos;
        Vec /= 50;

    }
    Vector3 pos;
    Vector3 Target;
    Vector3 Vec;
    GameObject Object;

    float maxH = 0.8f;

    public async UniTask StartSpider() 
    {

        while (true) 
        {

            this.Object.transform.position = pos;

            pos += Vec;

            if (Vector3.Distance(pos, Target) <= 0.2f) break;

           await UniTask.DelayFrame(1);

        }

        await UpSpider();
    
    }

    private async UniTask UpSpider() 
    {
        Vector3 Vecs = pos - Target;
        float Rnage = Vector3.Distance(pos, Target);
        float Angle = Mathf.Atan2(Vecs.x,Vecs.z);

        while (true)
        {
            pos.x=Target.x+Mathf.Sin(Angle)*Rnage;
            pos.z=Target.z+Mathf.Cos(Angle)*Rnage;

            pos.y += 0.004f;

            Angle += 3 * Mathf.Deg2Rad;
            this.Object.transform.position = pos;
            AddPos(pos, Target);

            if (pos.y >= maxH) break;   
            await UniTask.DelayFrame(1);

        }

        await RandMove(Angle, Rnage);


    }

    private async UniTask RandMove(float Angle,float Rnage) 
    {
        int RondCount=Random.Range(20, 50);
        int Count = 0;
        int EndCount =0;
        float RondUp = Random.Range(0,8)-4;
        while (true)
        {
            pos.x = Target.x + Mathf.Sin(Angle) * Rnage;
            pos.z = Target.z + Mathf.Cos(Angle) * Rnage;

            pos.y += RondUp / 1000;

            Angle += 3 * Mathf.Deg2Rad;
            this.Object.transform.position = pos;
            Count++;
            AddPos(pos, Target);

            await UniTask.DelayFrame(1);
            if (EndCount >= 5) break; 
            if (Count < RondCount) continue;

            EndCount++;
            RondCount= Random.Range(1, 4);
            Count = 0;
            RondUp = Random.Range(0, 8)-4;
        }

        DestroyObjcet(this.Object);
        await UniTask.DelayFrame(150);
        RemoveLine(-1);

    }

}