using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TagData")]
public class Tag : ScriptableObject
{
    [SerializeField] string EnemyTag;
    [SerializeField] string PlayerTag;

    [SerializeField] string EnemyAttackTag;
    [SerializeField] string PlayerAttackTag;

    public string GetEnemyTag() {  return EnemyTag; }
    public string GetEnemyAttackTag() {  return EnemyAttackTag; }
    public string GetPlayerTag() {  return PlayerTag; }
    public string GetPlayerAttackTag() { return PlayerAttackTag; }


}
