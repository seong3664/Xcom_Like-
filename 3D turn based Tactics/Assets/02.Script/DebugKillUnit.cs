using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugKillUnit : MonoBehaviour
{
    public void KillPlayerUnit()
    {
        GameManager.gamemaneger.PlayerList[0].GetComponent<OnDamege>().OnDamege(100, Vector3.zero, Vector3.zero);
       
    }
    public void KillEnemyUnit()
    {
        GameManager.gamemaneger.EnemyList[0].GetComponent<OnDamege>().OnDamege(100, Vector3.zero, Vector3.zero);

    }
}
