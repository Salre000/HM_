using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HunterSoundTest:MonoBehaviour
{
    public GameObject Manager;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha0))
        {
            Manager.GetComponent<HunterSoundManager>().SoundHunmerAttack();
        }
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            Manager.GetComponent<HunterSoundManager>().SoundSwordAttack();
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            Manager.GetComponent<HunterSoundManager>().SoundSpearAttack();
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            Manager.GetComponent<HunterSoundManager>().SoundArchAttack();
        }
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            Manager.GetComponent<HunterSoundManager>().SoundPreArchAttack();
        }
        if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            Manager.GetComponent<HunterSoundManager>().SoundWalk();
        }
    }

}
