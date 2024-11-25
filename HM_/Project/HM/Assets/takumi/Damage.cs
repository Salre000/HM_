using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
   [SerializeField] float _damage = 0;

    public void SetDamage(float Damage) {  _damage = Damage; }
    public float GetDamage() { return _damage; }

}
