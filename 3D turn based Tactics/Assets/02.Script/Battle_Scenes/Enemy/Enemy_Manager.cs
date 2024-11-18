using States;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.ProBuilder.MeshOperations;

public class Enemy_Manager : MonoBehaviour
{
    IEnemy_Select enemy_select;
    IEnemy_Ai enemy_ai;
    IEnemy_Ctrl enemy_ctrl;

    Transform SelectEnemy;
    UnitStat SelectEnemy_stat;
    Unit_AniCtrl AniCtrl;


    private void Awake()
    {
        enemy_select = GetComponent<IEnemy_Select>();
        enemy_ai = GetComponent<IEnemy_Ai>();
        enemy_ctrl = GetComponent<IEnemy_Ctrl>();
        AniCtrl = GetComponent<Unit_AniCtrl>();

        turnManager.PlayerEndTurn += SetSelectEnemy;
        enemy_ctrl.OnEnemyActionCompleted += SetSelectEnemy;
    }


    void SetSelectEnemy()
    {
        if (SelectEnemy == null)
        {//선택 유닛이 없다면
            SelectEnemy = enemy_select.SelectcanMoveEnemy();
            //유닛 선택
            if (SelectEnemy != null)
=======
   
    void SetSelectEnemy()
    {
        if(SelectEnemy == null)
        {//선택 유닛이 없다면
            SelectEnemy = enemy_select.SelectcanMoveEnemy();
            //유닛 선택
            if(SelectEnemy != null)
>>>>>>> parent of 5ecb678 (Revert "xcomlike")
            {
                SelectEnemy_stat = SelectEnemy.GetComponent<UnitStat>();
                //샐렉트 에너미가 널이 아니라면 스탯 가져옴
            }
        }
<<<<<<< HEAD
        if (SelectEnemy != null)
        {

            if (SelectEnemy_stat.stat.Action > 0)
            {//선택된 적의 행동력이 0이 될 때 까지
                if (enemy_ai.EnemyMoveorAtk(SelectEnemy) == 0)
                {//ai에서 선택된 적이 공격할지 이동할지 판별(트루면 이동 false면공격)
                    List<A_Nodes> MoveNode = enemy_ai.EnemyMove(SelectEnemy, SelectEnemy_stat);
                    if (MoveNode != null)
                    {
                        enemy_ctrl.EnemyMove(SelectEnemy, MoveNode);
                        SelectEnemy_stat.stat.Action--;
                        //이동 실행,이동 후 행동력 -
                    }
                    else
                    {
                        SelectEnemy_stat.stat.Action = 0;
                        SelectEnemy = null;
                        SetSelectEnemy();
                    }
                }
                else if(enemy_ai.EnemyMoveorAtk(SelectEnemy) == 1) 
                {
                    Transform Target = enemy_ai.EnemyAtk(SelectEnemy);
                    enemy_ctrl.EnemyAtk(SelectEnemy, Target, SelectEnemy_stat);
                    //공격
                }
                else
                {
                    SelectEnemy_stat.stat.Action = 0;
                    SelectEnemy = null;
                    SetSelectEnemy();
=======
        if(SelectEnemy != null)
        {
            
            if (SelectEnemy_stat.stat.Action > 0)
            {//선택된 적의 행동력이 0이 될 때 까지
                if (enemy_ai.EnemyMoveorAtk(SelectEnemy))
                {//ai에서 선택된 적이 공격할지 이동할지 판별(트루면 이동 false면공격)

                    List<A_Nodes> MoveNode = enemy_ai.EnemyMove(SelectEnemy, SelectEnemy_stat);
                    enemy_ctrl.EnemyMove(SelectEnemy, MoveNode);
                    SelectEnemy_stat.stat.Action--;
                    //이동 실행,이동 후 행동력 -
                }
                else
                {
                   Transform Target = enemy_ai.EnemyAtk(SelectEnemy);
                    enemy_ctrl.EnemyAtk(SelectEnemy,Target,SelectEnemy_stat); 
                    //공격
>>>>>>> parent of 5ecb678 (Revert "xcomlike")
                }
            }
            else
            {
                SelectEnemy = null;
                SetSelectEnemy();
                //선택 유닛의 행동력이 없다면 셀렉트 유닛을 null로 한뒤 해당 함수 다시 호출해 반복
            }
        }
<<<<<<< HEAD
        //만일 셀렉트 에너미가 널을 반환한다면 함수 반복 종료.
    }

=======
       //만일 셀렉트 에너미가 널을 반환한다면 함수 반복 종료.
    }
>>>>>>> parent of 5ecb678 (Revert "xcomlike")
}
