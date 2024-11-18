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
    /// ���ʹ̰� �������� �̵����� �����ϴ� �޼���
    /// true�� �̵� false�� ����
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

                // ���߷��� �ǰݷ� ���
                HitChance = HitchanceCheck.HitChanceCheckVector(ReachableNodes[i].WorldPos, Player.transform.position, unitStat, PlayerStat);
                GethitChance = HitchanceCheck.HitChanceCheckVector(Player.transform.position, ReachableNodes[i].WorldPos, unitStat, PlayerStat);

=======
                //���߷� ���
                HitChance = HitchanceCheck.HitChanceCheckVector(ReachableNodes[i].WorldPos, Player.transform.position, unitStat, PlayerStat);
                //�ǰݷ� ���
                GethitChance = HitchanceCheck.HitChanceCheckVector(Player.transform.position, ReachableNodes[i].WorldPos, unitStat, PlayerStat);
                //���߷��� �ǰ�Ȯ���� ��� 0�϶�,�� �Ÿ��� 30 �̻��� ������ ������ �켱���� 0���� ����
>>>>>>> parent of 5ecb678 (Revert "xcomlike")
                if (HitChance == 0 && GethitChance == 0)
                {
                    priority[i] = 0;
                    continue;
                }
                else
                {
<<<<<<< HEAD
                    // ���߷��� ���� �켱�� �ο�
                    if (HitChance >= 70)
                        priority[i] += 4;
                    else if (HitChance >= 50)
                        priority[i] += 3;
                    else if (HitChance >= 30)
                        priority[i] += 1;

                    // �ǰݷ��� ���� �켱�� �ο� (���� ���� Ȯ��)
=======
                    //���� Ȯ�� ���� �켱�� �ο�(���� ���� �켱�� ��)
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
                    //�ǰ� Ȯ�� ���� �켱�� �ο�(���� ���� �켱�� ��) 
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

        // ��� �켱���� 0�̸� ���ڸ��� �ӹ����� ����
        if (priority.All(p => p == 0))
        {
            return null;// �� ��� ��ȯ (��, �̵����� ����)
        }

        // �켱���� ���� ���� ��� ����
        List<int> bestNodesIndices = BestNodeSelec(priority);
        List<int> coverNodesIndices = bestNodesIndices
            .Where(i => ReachableNodes[i].Nodetype == NodeType.Cover || ReachableNodes[i].Nodetype == NodeType.SemiCover)
            .ToList();

        int selectedIndex;
=======
        //�켱�� ���ؼ� �켱���� ���� ���� ���� �̵�. �켱���� ���� ������ ������ �������� ����
        List<int> bestNodesIndices = BestNodeSelec(priority);

        List<int> coverNodesIndices = bestNodesIndices.Where(i => ReachableNodes[i].Nodetype == NodeType.Cover || ReachableNodes[i].Nodetype == NodeType.SemiCover).ToList();

        int selectedIndex;
        // �������� �ϳ��� ��� ����
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

    // �ְ� �켱�� ��� ����
=======
        List<A_Nodes> TargetNodepath = new List<A_Nodes>();
        TargetNodepath = pathfinding.A_StarAlgorithm(Unit.position, ReachableNodes[selectedIndex].WorldPos, unitStat.stat.MovePoint, false);
        return TargetNodepath;
        // ������ ��� ��ȯ
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

                bestNodesIndices.Add(i); // ���ο� �ְ� �켱�� ����� �ε��� �߰�
            }
            else if (priority[i] == maxPriority)
            {
                bestNodesIndices.Add(i); // ���� �켱�� ��� �ε��� �߰�
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

