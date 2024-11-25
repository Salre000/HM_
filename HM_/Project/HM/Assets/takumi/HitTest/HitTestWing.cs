
using UnityEngine;

//羽の当たり判定のクラス
public class HitTestWing : MonoBehaviour
{
    SphereCollider atama;
    [SerializeField] private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
        //atama=this.gameObject.AddComponent<SphereCollider>();



        ////トランスフォームコンポーネントの変数を宣言
        //Transform leftLowerLeg;

        ////左膝に設定されているTransformを取得
        //leftLowerLeg = anim.GetBoneTransform(HumanBodyBones.LeftLowerLeg);

        //Debug.Log(leftLowerLeg.position);


    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
