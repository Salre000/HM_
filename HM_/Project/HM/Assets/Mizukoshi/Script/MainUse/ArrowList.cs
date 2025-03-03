using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowList : MonoBehaviour
{
    [SerializeField] GameObject Hunter;
    public GameObject aro;
    private GameObject[] _arrowList =new GameObject[3];

    private void Start()
    {

        for(int i = 0; i < 3; i++) 
        {
            //_arrowList[i] = Instantiate(aro);
            _arrowList[i]=Instantiate(aro);

            _arrowList[i].SetActive(false);

            TestCollision test = _arrowList[i].transform.GetChild(1).GetComponent<TestCollision>();
            test.SetGameObject(Hunter);
            
        }
        foreach (GameObject arrow in _arrowList)
        {
            if (arrow != null)
            {
                arrow.SetActive(false);
            }
        }
    }

    public void SetArrow(Vector3 startPos,Vector3 dir)
    {
        for (int i = 0; i < 3; i++)
        {
            bool isActivate = _arrowList[i].activeSelf;
            if (!isActivate)
            {
                _arrowList[i].SetActive(true);
                _arrowList[i].transform.position = startPos;
                _arrowList[i].transform.LookAt(dir);
                return;
            }
        }
    }
}
