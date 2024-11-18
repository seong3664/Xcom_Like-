using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Unit_Fire : MonoBehaviour
{
    LineRenderer lineRenderer;
<<<<<<< HEAD
=======
    float F_Time;
>>>>>>> parent of 5ecb678 (Revert "xcomlike")
    public Transform Firepos;
    public ParticleSystem MuzleFire;
    Light Light;
    Color startColor;
    Quaternion StartRot;

    AimCheck aimCheck;
    int HitChance;

    [SerializeField]
    Image BulletCount;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        startColor = lineRenderer.material.color;
        StartRot = Firepos.rotation;
        Firepos = transform.GetChild(1).GetComponent<Transform>();
        //MuzleFire = transform.GetChild(1).GetComponent<ParticleSystem>();
        aimCheck = GameObject.Find("Enemy_Manager").GetComponent<AimCheck>();
        Light = MuzleFire.gameObject.transform.GetChild(1).GetComponent<Light>();
    }
    private void Start()
    {

        MuzleFire.Stop();
        Light.enabled = false;
    }
    public void FireRay(Transform Target, int Damage)
    {
        RaycastHit[] hits;
        int aiming = transform.GetComponent<UnitStat>().stat.Aiming;
        Vector3 direction = Target.position - transform.position;
        direction.y = 0; // Y축 회전만 적용하므로 Y 값을 0으로 고정

        // 타겟 방향으로의 Y축 회전만 계산
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // 기존 유닛의 회전에서 Y축만 타겟 방향으로 변경
        transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);

        Ray ray = new Ray(Firepos.position, (Target.position + Vector3.up * 2f - Firepos.position).normalized);
        UnitStat stat = transform.GetComponent<UnitStat>();
        stat.stat.Action -= 2; // 발사 시 행동력 2 감소
        stat.BulletCount -= 1;
        SoundManager.instance.PlayerSound("gunshoot");
        StartCoroutine(MuzleFireOn());

        hits = Physics.RaycastAll(ray, 30f);
        bool hitFound = false;

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject == Target.gameObject)
            {
                hitFound = true;

                lineRenderer.SetPosition(0, Firepos.position); // 라인 렌더러 시작 위치 설정
                HitChance = aimCheck.HitChanceCheckTransform(transform, Target);
                OnDamege target = hit.collider.GetComponent<OnDamege>();

                if (Random.Range(0, 101) < HitChance)
                {
                    
                    if (target != null)
<<<<<<< HEAD
                        target.OnDamege(Damage+Random.Range(-1,2), hit.point, hit.normal);
=======
                        target.OnDamege(Damage+Random.Range(-1,1), hit.point, hit.normal);
>>>>>>> parent of 5ecb678 (Revert "xcomlike")
                    lineRenderer.SetPosition(1, hit.point);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y + 5f, 0);
                    lineRenderer.SetPosition(1, Firepos.position + Firepos.forward * 30f);
                    if (target != null)
                        target.OnMissed();
                }

                StartCoroutine(Firing());
                break; // 타겟을 찾으면 루프 종료
            }
        }
        if (!hitFound)
        {
            lineRenderer.SetPosition(1, Firepos.position + Firepos.forward * 30f);
        }
    }


    IEnumerator MuzleFireOn()
    {
        MuzleFire.Play();
        Light.enabled = true;
        yield return new WaitForSeconds(0.1f);
        MuzleFire.Stop();
        Light.enabled = false;
    }
    IEnumerator Firing()
    {
<<<<<<< HEAD
=======
        F_Time = 0f;
>>>>>>> parent of 5ecb678 (Revert "xcomlike")
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.1f);
        lineRenderer.enabled = false;

    }

}
