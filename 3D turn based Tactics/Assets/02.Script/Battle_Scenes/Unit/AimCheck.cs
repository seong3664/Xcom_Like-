using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AimCheck : MonoBehaviour
{
    public LayerMask coverLayer;
    float detectionRadius = 3f;
    Vector3 Target = Vector3.zero;
    int HitChance;
    int coverStrength;
    LayerMask layerMask = ~(1 << 3 | 1 << 8 | 1<< 9);

    private void Awake()
    {
        coverLayer = 1 << 10 | 1 << 11;
    }
    
    public int HitChanceCheckVector(Vector3 Unit,Vector3 Target,UnitStat Unitstat, UnitStat TargetStat)
    {
        return HitChanceCheck(Unit, Target, Unitstat, TargetStat);
    }
    public int HitChanceCheckTransform(Transform Unit,Transform Target)
    {
        UnitStat Unitstat = Unit.GetComponent<UnitStat>();
        UnitStat TargetStat = Target.GetComponent<UnitStat>();
        return HitChanceCheck(Unit.position, Target.position, Unitstat, TargetStat);
    }
    private int HitChanceCheck(Vector3 Unit, Vector3 Target, UnitStat Unitstat, UnitStat TargetStat)
    {
        HitChance = 0;
  
        if (!Physics.Raycast(Unit + Vector3.up * 2f, (Target - Unit).normalized * 30f,30f, layerMask))
        {
            if (Vector3.Distance(Unit, Target) > 32)
            return 0;
            HitChance = Unitstat.stat.Aiming - TargetStat.stat.Evasion;

            coverStrength = CheckTargetCover(Unit, Target);

            if (coverStrength == 2)
                HitChance -= 30;
            else if (coverStrength == 1)
                HitChance -= 15;
            else
                HitChance += 10;

            HitChance = Mathf.Max(HitChance, 0);
        }
        return HitChance;
    }

public int CheckTargetCover(Vector3 unit, Vector3 target)
    {
        Collider[] Covers = Physics.OverlapSphere(target, detectionRadius, coverLayer);
        Target = target;
        foreach (Collider cover in Covers)
        {
            // Ÿ�ٿ��� �����ڿ� ���󹰱����� ���� ���
            Vector3 directionAttacker = (unit - target).normalized;
            Vector3 directionCover = (cover.transform.position - target).normalized;

            // Ÿ�ٿ��� �� ���󹰰� �������� ��ġ �� ���� ���
            float angleAttackerAndCover = Vector3.Angle(directionAttacker, directionCover);



            // Ÿ�ٰ� ������ ���̿� ������ �ִ��� Ȯ��
            if (angleAttackerAndCover < 90f)
            {

                if (angleAttackerAndCover < 30f)
                {
                    //Debug.Log("���� ����");
                    return 2;
                }
                else if (angleAttackerAndCover < 60f)
                {
                    //Debug.Log("���� ����");
                    return 1;
                }
                else
                {
                    //Debug.Log("���� ����");
                    return 0;

                }

            }
        }
        return 0;
    }
    void OnDrawGizmos()
    {

        Gizmos.color = Color.red;

        if (Target != null)
        {
            Gizmos.DrawWireSphere(Target, detectionRadius);
        }
    }
}
