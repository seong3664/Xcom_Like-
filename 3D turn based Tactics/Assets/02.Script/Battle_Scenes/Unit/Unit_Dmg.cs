using States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Unit_Dmg : MonoBehaviour, OnDamege
{
    UnitStat Unitstat;
    Unit_AniCtrl Unit_AniCtrl;
<<<<<<< HEAD
    CapsuleCollider unitCollider;
    Rigidbody unitRigidbody;
=======
    CapsuleCollider Collider;
    Rigidbody Rigidbody;
>>>>>>> parent of 5ecb678 (Revert "xcomlike")
    Animator animator;

    GameObject MissTxt;
    ParticleSystem Bloodeff;
<<<<<<< HEAD
    GameObject Unit_UI;
=======
>>>>>>> parent of 5ecb678 (Revert "xcomlike")
    void Awake()
    {
        Unitstat = GetComponent<UnitStat>();
        Unit_AniCtrl = GetComponent<Unit_AniCtrl>();
<<<<<<< HEAD
        unitCollider = GetComponent<CapsuleCollider>();
        unitRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        MissTxt = transform.GetChild(0).GetChild(0).GetChild(2).gameObject;
        Bloodeff = transform.GetChild(2).GetComponent<ParticleSystem>();
        Unit_UI = transform.GetChild(0).gameObject;
=======
        Collider = GetComponent<CapsuleCollider>();
        Rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        MissTxt = transform.GetChild(0).GetChild(0).GetChild(2).gameObject;
        Bloodeff = transform.GetChild(2).GetComponent<ParticleSystem>();
>>>>>>> parent of 5ecb678 (Revert "xcomlike")
    }
    private void Start()
    {
        MissTxt.SetActive(false);
    }
    public void OnDamege(int Damege, Vector3 hitPoint, Vector3 hitnom)
    {
        Unitstat.stat.Hp -= Damege;
        Bloodeff.transform.position = hitPoint;
        Bloodeff.transform.rotation = Quaternion.LookRotation(hitnom);
        Bloodeff.Play();
        SoundManager.instance.PlayerSound("Hit");
        if (Unitstat.stat.Hp <= 0)
        {
            animator.enabled = false;
            UnitDie(hitPoint,hitnom);
        }
        else
        {
            Unit_AniCtrl.HitAniSet();
        }
    }
    public void OnMissed()
    {
        StartCoroutine(MissTxtOn());
<<<<<<< HEAD
=======
        if (Unitstat.stat.Hp <= 0)
        {
            animator.enabled = false;
        }
        else
        {
        }
>>>>>>> parent of 5ecb678 (Revert "xcomlike")
    }
    IEnumerator MissTxtOn()
    {
        SoundManager.instance.PlayerSound("Miss");
        MissTxt.SetActive (true);
        yield return new WaitForSeconds(2f);
        MissTxt.SetActive(false);
    }
<<<<<<< HEAD
    IEnumerator EnableRagdoll()
    {
        // 위치를 물리엔진에 동기화하여 Rigidbody가 현재 위치와 회전을 인식하도록 함
        Physics.SyncTransforms();

        // 약간 더 긴 지연 시간을 둬서 물리 엔진이 완전히 준비되도록 함
        yield return new WaitForSeconds(0.2f);

        // Rigidbody의 isKinematic 해제
        unitRigidbody.isKinematic = false;

        // Collider 활성화
        unitCollider.enabled = false;
    }

    void UnitDie(Vector3 hitPoint, Vector3 hitnom)
    {
        animator.enabled = false;
        Unit_UI.SetActive(false);
        // 해당 노드에서 유닛 제거
        GetComponent<SetNode>().OutNode();

        // 게임 매니저에서 유닛 제거
        if (Unitstat.stat.UnitCode == UnitCode.Player)
        {
            GameManager.gamemaneger.PlayerList.Remove(gameObject);
        }
        else if (Unitstat.stat.UnitCode == UnitCode.Enemy)
        {
            GameManager.gamemaneger.EnemyList.Remove(gameObject);
        }

        // 레이어 및 태그 초기화
        gameObject.layer = LayerMask.NameToLayer("Default");
        gameObject.tag = "Untagged";

        // Rigidbody 초기화
        unitRigidbody.velocity = Vector3.zero;
        unitRigidbody.angularVelocity = Vector3.zero;
        hitnom.y = 0;
        unitRigidbody.AddForce(hitnom * 0.5f, ForceMode.Impulse);
        // Rigidbody를 일시적으로 kinematic으로 설정
        unitRigidbody.isKinematic = true;
        
        // Collider 비활성화
        //unitCollider.enabled = false;

        // Ragdoll 전환을 위한 코루틴 실행
        StartCoroutine(EnableRagdoll());

        // 게임 종료 체크
        turnManager.TurnManager.CheckEndGame();
    }

=======
    void UnitDie(Vector3 hitPoint, Vector3 hitnom)
    {
        transform.GetComponent<SetNode>().OutNode();
        if (Unitstat.stat.UnitCode == UnitCode.Player)
        {
            GameManager.gamemaneger.PlayerList.Remove(gameObject);

        }
        else if (Unitstat.stat.UnitCode == UnitCode.Enemy)
        {
           GameManager.gamemaneger.EnemyList.Remove(gameObject);
        }
        this.gameObject.layer = LayerMask.NameToLayer("Default");
        this.gameObject.tag = "Untagged";
        Collider.enabled = false;
        Rigidbody.isKinematic = false;
        Rigidbody.useGravity = false;
        Vector3 horizontalHitnom = new Vector3(hitnom.x, 0, hitnom.z);
        Rigidbody.AddForce(horizontalHitnom);
        turnManager.TurnManager.CheckEndGame();
    }

    
>>>>>>> parent of 5ecb678 (Revert "xcomlike")
}
