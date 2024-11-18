using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy_Ai : Enemy_Select,IEnemy_Ai
{

    AimCheck HitchanceCheck;
    int HitChance;
    int GethitChance;

    A_Pathfinding pathfinding;
    [SerializeField] GameObject[] PlayerUnits;

   

    private void Awake()
    {
        HitchanceCheck = GetComponent<AimCheck>();
        pathfinding = GameObject.Find("Grid_Manager").GetComponent<A_Pathfinding>();
    }
    private void Start()
    {
        
    }
    /// <summary>
    /// 에너미가 공격할지 이동할지 결정하는 메서드
    /// true면 이동 false면 공격
    /// </summary>
    /// <param name="Unit"></param>
    /// <returns></returns>

    public int EnemyMoveorAtk(Transform Unit)

    public bool EnemyMoveorAtk(Transform Unit)
>>>>>>> parent of 5ecb678 (Revert "xcomlike")
    {
        PlayerUnits = GameObject.FindGameObjectsWithTag("Player");
        foreach (var Player in PlayerUnits)
        {


            HitChance = HitchanceCheck.HitChanceCheckTransform(Unit, Player.transform);
            GethitChance = HitchanceCheck.HitChanceCheckTransform(Player.transform, Unit);
            RaycastHit hit;

            if (HitChance >= 30 && GethitChance <= 50 || HitChance >= 60 && Vector3.Distance(Unit.position, Player.transform.position) < 15f)
            { 
                if (Physics.Raycast(new Ray(Unit.position + Vector3.up * 2f, (Player.transform.position - Unit.position).normalized), out hit, 20f))
                {
                    Debug.DrawRay(Unit.position + Vector3.up * 2f, (Player.transform.position - Unit.position).normalized * 10f, Color.green, 10f);
                    if (hit.collider.gameObject.layer == 3)
                    {
                        return 1;
                    }
                } 
             }
            else if (Vector3.Distance(Unit.position, Player.transform.position) < 30f)
                    return 0;
                
            
        }
       
        return 2;
    }

    public List<A_Nodes> EnemyMove(Transform Unit, UnitStat unitStat)
    {
        List<A_Nodes> ReachableNodes = pathfinding.GetReachableNodes(Unit.position, unitStat.stat.MovePoint);
        List<int> priority = new List<int>(new int[ReachableNodes.Count]);

=======
            InfiniteLoopDetector.Run();
            HitChance = HitchanceCheck.HitChanceCheckTransform(Unit,Player.transform);
            GethitChance = HitchanceCheck.HitChanceCheckTransform(Player.transform,Unit);
          
           if(HitChance >= 30 && GethitChance <= 50 || HitChance >= 60 && Vector3.Distance(Unit.position, Player.transform.position) < 15f)
            {
                return false;
            }
        }
        return true;
    }

    public List<A_Nodes> EnemyMove(Transform Unit,UnitStat unitStat)
    {
        List<A_Nodes> ReachableNodes = pathfinding.GetReachableNodes(Unit.position, unitStat.stat.MovePoint);
        List<int> priority = new List<int>(new int[ReachableNodes.Count]);
>>>>>>> parent of 5ecb678 (Revert "xcomlike")
        for (int i = 0; i < ReachableNodes.Count; i++)
        {
            foreach (GameObject Player in PlayerUnits)
            {
                UnitStat PlayerStat = Player.GetComponent<UnitStat>();
<<<<<<< HEAD

                // 명중률과 피격률 계산
                HitChance = HitchanceCheck.HitChanceCheckVector(ReachableNodes[i].WorldPos, Player.transform.position, unitStat, PlayerStat);
                GethitChance = HitchanceCheck.HitChanceCheckVector(Player.transform.position, ReachableNodes[i].WorldPos, unitStat, PlayerStat);

=======
                //명중률 계산
                HitChance = HitchanceCheck.HitChanceCheckVector(ReachableNodes[i].WorldPos, Player.transform.position, unitStat, PlayerStat);
                //피격률 계산
                GethitChance = HitchanceCheck.HitChanceCheckVector(Player.transform.position, ReachableNodes[i].WorldPos, unitStat, PlayerStat);
                //명중률과 피격확률이 모두 0일때,즉 거리가 30 이상일 때에는 무조건 우선도를 0으로 설정
>>>>>>> parent of 5ecb678 (Revert "xcomlike")
                if (HitChance == 0 && GethitChance == 0)
                {
                    priority[i] = 0;
                    continue;
                }
                else
                {
<<<<<<< HEAD
                    // 명중률에 따른 우선도 부여
                    if (HitChance >= 70)
                        priority[i] += 4;
                    else if (HitChance >= 50)
                        priority[i] += 3;
                    else if (HitChance >= 30)
                        priority[i] += 1;

                    // 피격률에 따른 우선도 부여 (엄폐 여부 확인)
=======
                    //명중 확률 따라 우선도 부여(높을 수록 우선도 업)
                    {
                        if (HitChance >= 70)
                            priority[i] += 4;
                        else if (HitChance >= 50)
                            priority[i] += 3;
                        else if (HitChance >= 30)
                            priority[i] += 1;
                        else
                            priority[i] += 0;
                    }
                    //피격 확률 따라 우선도 부여(낮을 수록 우선도 업) 
>>>>>>> parent of 5ecb678 (Revert "xcomlike")
                    if (ReachableNodes[i].Nodetype == NodeType.Cover || ReachableNodes[i].Nodetype == NodeType.SemiCover)
                    {
                        if (GethitChance >= 70)
                            priority[i] += 0;
                        else if (GethitChance >= 50)
                            priority[i] += 1;
                        else if (GethitChance >= 30)
                            priority[i] += 2;
                        else
                            priority[i] += 3;
<<<<<<< HEAD
=======

>>>>>>> parent of 5ecb678 (Revert "xcomlike")
                    }
                }
            }
        }
<<<<<<< HEAD

        // 모든 우선도가 0이면 제자리에 머무르게 설정
        if (priority.All(p => p == 0))
        {
            return null;// 빈 경로 반환 (즉, 이동하지 않음)
        }

        // 우선도가 가장 높은 노드 선택
        List<int> bestNodesIndices = BestNodeSelec(priority);
        List<int> coverNodesIndices = bestNodesIndices
            .Where(i => ReachableNodes[i].Nodetype == NodeType.Cover || ReachableNodes[i].Nodetype == NodeType.SemiCover)
            .ToList();

        int selectedIndex;
=======
        //우선도 비교해서 우선도가 가장 높은 노드로 이동. 우선도가 같은 노드들이 있을시 랜덤으로 결정
        List<int> bestNodesIndices = BestNodeSelec(priority);

        List<int> coverNodesIndices = bestNodesIndices.Where(i => ReachableNodes[i].Nodetype == NodeType.Cover || ReachableNodes[i].Nodetype == NodeType.SemiCover).ToList();

        int selectedIndex;
        // 랜덤으로 하나의 노드 선택
>>>>>>> parent of 5ecb678 (Revert "xcomlike")
        if (coverNodesIndices.Count > 0)
        {
            selectedIndex = coverNodesIndices[Random.Range(0, coverNodesIndices.Count)];
        }
        else
        {
            selectedIndex = bestNodesIndices[Random.Range(0, bestNodesIndices.Count)];
        }
<<<<<<< HEAD

        List<A_Nodes> TargetNodepath = pathfinding.A_StarAlgorithm(Unit.position, ReachableNodes[selectedIndex].WorldPos, unitStat.stat.MovePoint, false);
        return TargetNodepath;
    }

    // 최고 우선도 노드 선택
=======
        List<A_Nodes> TargetNodepath = new List<A_Nodes>();
        TargetNodepath = pathfinding.A_StarAlgorithm(Unit.position, ReachableNodes[selectedIndex].WorldPos, unitStat.stat.MovePoint, false);
        return TargetNodepath;
        // 선택한 노드 반환
    }

>>>>>>> parent of 5ecb678 (Revert "xcomlike")
    private static List<int> BestNodeSelec(List<int> priority)
    {
        int maxPriority = -1;
        List<int> bestNodesIndices = new List<int>();
<<<<<<< HEAD

=======
        bestNodesIndices.Clear();
>>>>>>> parent of 5ecb678 (Revert "xcomlike")
        for (int i = 0; i < priority.Count; i++)
        {
            if (priority[i] > maxPriority)
            {
                maxPriority = priority[i];
<<<<<<< HEAD
                bestNodesIndices.Clear();
                bestNodesIndices.Add(i);
            }
            else if (priority[i] == maxPriority)
            {
                bestNodesIndices.Add(i);
=======

                bestNodesIndices.Add(i); // 새로운 최고 우선도 노드의 인덱스 추가
            }
            else if (priority[i] == maxPriority)
            {
                bestNodesIndices.Add(i); // 같은 우선도 노드 인덱스 추가
>>>>>>> parent of 5ecb678 (Revert "xcomlike")
            }
        }

        return bestNodesIndices;
    }

<<<<<<< HEAD

=======
>>>>>>> parent of 5ecb678 (Revert "xcomlike")
    public Transform EnemyAtk(Transform Unit)
    {
        int temp = 0;
        HitChance = 0;
        Transform AtkTarget = null ;
        foreach(var player in PlayerUnits)
        {
           temp = HitchanceCheck.HitChanceCheckTransform(Unit, player.transform);
            if (HitChance <= temp)
            {
                HitChance = temp;
            }
            AtkTarget = player.transform;
        }
        return AtkTarget;
    }
}

