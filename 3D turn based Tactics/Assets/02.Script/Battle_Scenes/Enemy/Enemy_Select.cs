using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Enemy_Select : MonoBehaviour,IEnemy_Select
{
    Transform Cam;
    A_Grid grid;
 
   [SerializeField] LayerMask EnemyLayer;
    [SerializeField] List<GameObject> EnemyList;
    [SerializeField] List<UnitStat> EnemyListStat;

    Transform EnemyseletUnitUI;
    protected Transform SelectEnemy { get; private set; } //외부에서 접근 가능하도록

    

    private void Awake()
    {
        grid = GameObject.Find("Grid_Manager").GetComponent<A_Grid>();
        EnemyLayer = LayerMask.NameToLayer("Enemy");
        
        EnemyseletUnitUI = transform.GetChild(0).GetComponent<Transform>();
        
    }
    private void Start()
    {
        Cam = GameObject.Find("Turn_Manager").GetComponent<Transform>();
        Vector3 boxSize = new Vector3(grid.gridWorldSize.x, 1.0f, grid.gridWorldSize.y);
       
        EnemyseletUnitUI.gameObject.SetActive(false);
        
    }
    public Transform SelectcanMoveEnemy()
    {
        EnemyList = GameManager.gamemaneger.EnemyList;
        //게임 매니저에 저장된 인 게임내 생존한 모든 에너미의 리스트를 가져옴
        for (int i = 0; i < EnemyList.Count; i++)
        {

            EnemyListStat.Add(EnemyList[i].GetComponent<UnitStat>());
            //해당 에너미들의 스탯 가져옴

                EnemyListStat.Add(EnemyList[i].GetComponent<UnitStat>());
                //해당 에너미들의 스탯 가져옴

        }
        for (int i = 0; i < EnemyList.Count; i++)
        {
            if (EnemyListStat[i].stat.Action > 0)
            {
                SelectEnemy = EnemyList[i].transform;
                Cam.position = SelectEnemy.position;
                return SelectEnemy;
                //가져온 유닛중 행동력이 남아 있는 유닛 하나 선택후 반환
            }

        }
        EnemyListStat.Clear();
        turnManager.TurnManager.EndEnemyturn();
        //셀렉트할 유닛이 없다면(모든 적 유닛의 행동력이 0이라면) 턴 종료
<<<<<<< HEAD
        EnemyseletUnitUI.gameObject.SetActive(false);

=======
>>>>>>> parent of 5ecb678 (Revert "xcomlike")
        return null;
    }
}

