using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HItTest : MonoBehaviour
{
    private PlayerStatus _status;

    // Start is called before the first frame update
    void Start()
    {
        _status = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
    }

    //‰½‚©‚É“–‚½‚Á‚½
    //(ƒgƒŠƒK[“¯m‚àŠl“¾‚µ‚Ä‚­‚ê‚é)
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("“–‚½‚Á‚½");

        //“G‚ÌUŒ‚‚ğó‚¯‚½
        if (other.gameObject.tag == "EnemyAttack")
        {


            //“G‚ÌUŒ‚—Í‚ğ—˜—p‚µ‚½‹““®
            Damage _damage=other.GetComponent<Damage>();

            //HP‚ğŒ¸‚ç‚·
            _status.Damage(_damage.GetDamage());
        }
    }
}
