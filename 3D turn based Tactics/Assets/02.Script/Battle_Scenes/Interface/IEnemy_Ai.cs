using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy_Ai
{

    int EnemyMoveorAtk(Transform Unit);

    bool EnemyMoveorAtk(Transform Unit);

   List<A_Nodes> EnemyMove(Transform Unit,UnitStat Unitstat);
    Transform EnemyAtk(Transform Unit);

}
