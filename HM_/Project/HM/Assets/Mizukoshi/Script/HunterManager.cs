using UnityEngine;
public class HunterManager : MonoBehaviour
{

    private GameObject[] gameObjects;
    private GameObject _spear;

    private HPManager _hpManager;

    int deathCount = 0;
    bool deathAnimationNow = false;
    bool[] isDeath = new bool[4];
    float[] time = new float[4];


    Vector3 respawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("Hunter");
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i].GetComponent<Hunter_AI>() == null) continue;
            gameObjects[i].GetComponent<Hunter_ID>().SetHunterID(i);
        }
        respawnPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDeath();
        DebugCommand();

    }

    public int GetHunterDeathAmount()
    {
        return deathCount;
    }

    public void Respawn(int i)
    {
        _hpManager.HunterHeel(100, i);  
        gameObjects[i].transform.position = respawnPosition;
        gameObjects[i].GetComponent<Hunter_AI>().WaitForCount();
    }

    /// <summary>
    /// ���ɂ܂�����GetHunterLostNumber()��n�b�セ�̒l��-1   ���X�|�[����1�b�ҋ@
    /// </summary>
    /// <param name="hunterNum"></param>
    void CheckDeath()
    {
        int deathNum=_hpManager.GetHunterLostNumber();
        gameObjects[deathNum].GetComponent<Hunter_AI>().Death();
    }

    float AmountDamaged(int hunterNum)
    {
        return gameObjects[hunterNum].GetComponent<HunterHPManager>().collider.gameObject.GetComponent<Damage>().GetDamage();
    }

    /// <summary>
    /// �n���^�[�������X�^�[���������Ƃ��ɌĂԊ֐�
    /// </summary>
    public void SetDisapper()
    {
        // �n���^�[�ɂ��Ă��郂���X�^�[��������֐����Ăяo���B
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].GetComponent<Hunter_AI>().monsterDisplay=true;
        }
    }

    /// <summary>
    /// �n���^�[�̊댯��Ԃ��m�F
    /// </summary>
    public void SetDengerousState()
    {
        // 
    }



    // �����I�ɃQ�[�����肠�ɂ���B
    public void ForceDie()
    {
        deathCount = 4;
    }

    public void DebugCommand()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            gameObjects[0].GetComponent<Hunter_AI>().DeathAnimation();
            Debug.Log(gameObjects[0].GetComponent<Hunter_AI>().GetHunterID());
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            gameObjects[1].GetComponent<Hunter_AI>().DeathAnimation();
            Debug.Log(gameObjects[1].GetComponent<Hunter_AI>().GetHunterID());
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            gameObjects[2].GetComponent<Hunter_AI>().DeathAnimation();
            Debug.Log(gameObjects[2].GetComponent<Hunter_AI>().GetHunterID());
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            gameObjects[0].GetComponent<Hunter_AI>().StartRestraining();
            Debug.Log(gameObjects[0].GetComponent<Hunter_AI>().GetHunterID());
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            gameObjects[1].GetComponent<Hunter_AI>().StartRestraining();
            Debug.Log(gameObjects[1].GetComponent<Hunter_AI>().GetHunterID());
        }
        if (Input.GetKey(KeyCode.Alpha6))
        {
            gameObjects[2].GetComponent<Hunter_AI>().StartRestraining();
            Debug.Log(gameObjects[2].GetComponent<Hunter_AI>().GetHunterID());
        }
        if (Input.GetKey(KeyCode.R))
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                gameObjects[i].GetComponent<Hunter_AI>().StopRestraining();
                gameObjects[i].GetComponent<Hunter_AI>().ResetAnimation();
            }
        }
    }
}
