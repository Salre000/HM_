
using UnityEngine;

//‰H‚Ì“–‚½‚è”»’è‚ÌƒNƒ‰ƒX
public class HitTestWing : MonoBehaviour
{

    [SerializeField]GameObject []WingRoot=new GameObject[2];
    [SerializeField]GameObject []WingEnd=new GameObject[2];
    [SerializeField] GameObject HitTestPosition;
    [SerializeField] Vector3 []HitTestPositionAngle=new Vector3[2];
    GameObject []Test=new GameObject[2];
    [SerializeField] float RnageY = 0;
    [SerializeField] float RnageX = 0;
    [SerializeField] float Angle = 0;
    // Start is called before the first frame update
    void Start()
    {
        Test[0] = new GameObject();
        Test[1] = new GameObject();
        HitTestPosition = new GameObject();
        BoxCollider boxCollider=HitTestPosition.AddComponent<BoxCollider>();
        boxCollider.size = new Vector3(0.01f, RnageY, RnageX);
        boxCollider.center = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        SetHitTestPosition();
    }

    float Angles = 0;
    void SetHitTestPosition() 
    {
        Vector3 AngleVec=WingEnd[1].transform.position-WingEnd[0].transform.position;
        Test[0].transform.position=HitTestPositionAngle[0] = (AngleVec/5)*4 + WingEnd[0].transform.position;
        AngleVec=WingRoot[1].transform.position- WingRoot[0].transform.position;
        Test[1].transform.position=HitTestPositionAngle[1] = (AngleVec) + WingRoot[1].transform.position;


        Angles++;

        Vector3 Vec = HitTestPositionAngle[0]- HitTestPositionAngle[1];

        HitTestPosition.transform.position = (Vec/2)+HitTestPositionAngle[1];

        HitTestPosition.transform.LookAt(HitTestPositionAngle[1]);
        Vector3.Dot(Vec,AngleVec);
        HitTestPosition.transform.Rotate(new Vector3(0,0, Angle));


    }

}
