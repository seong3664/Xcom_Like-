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
        // ��ġ�� ���������� ����ȭ�Ͽ� Rigidbody�� ���� ��ġ�� ȸ���� �ν��ϵ��� ��
        Physics.SyncTransforms();

        // �ణ �� �� ���� �ð��� �ּ� ���� ������ ������ �غ�ǵ��� ��
        yield return new WaitForSeconds(0.2f);

        // Rigidbody�� isKinematic ����
        unitRigidbody.isKinematic = false;

        // Collider Ȱ��ȭ
        unitCollider.enabled = false;
    }

    void UnitDie(Vector3 hitPoint, Vector3 hitnom)
    {
        animator.enabled = false;
        Unit_UI.SetActive(false);
        // �ش� ��忡�� ���� ����
        GetComponent<SetNode>().OutNode();

        // ���� �Ŵ������� ���� ����
        if (Unitstat.stat.UnitCode == UnitCode.Player)
        {
            GameManager.gamemaneger.PlayerList.Remove(gameObject);
        }
        else if (Unitstat.stat.UnitCode == UnitCode.Enemy)
        {
            GameManager.gamemaneger.EnemyList.Remove(gameObject);
        }

        // ���̾� �� �±� �ʱ�ȭ
        gameObject.layer = LayerMask.NameToLayer("Default");
        gameObject.tag = "Untagged";

        // Rigidbody �ʱ�ȭ
        unitRigidbody.velocity = Vector3.zero;
        unitRigidbody.angularVelocity = Vector3.zero;
        hitnom.y = 0;
        unitRigidbody.AddForce(hitnom * 0.5f, ForceMode.Impulse);
        // Rigidbody�� �Ͻ������� kinematic���� ����
        unitRigidbody.isKinematic = true;
        
        // Collider ��Ȱ��ȭ
        //unitCollider.enabled = false;

        // Ragdoll ��ȯ�� ���� �ڷ�ƾ ����
        StartCoroutine(EnableRagdoll());

        // ���� ���� üũ
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
