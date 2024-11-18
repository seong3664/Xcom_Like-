using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy_Ai
{
<<<<<<< HEAD
    int EnemyMoveorAtk(Transform Unit);
=======
    bool EnemyMoveorAtk(Transform Unit);
>>>>>>> parent of 5ecb678 (Revert "xcomlike")
   List<A_Nodes> EnemyMove(Transform Unit,UnitStat Unitstat);
    Transform EnemyAtk(Transform Unit);

}
