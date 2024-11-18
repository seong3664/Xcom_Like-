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
        direction.y = 0; // Y�� ȸ���� �����ϹǷ� Y ���� 0���� ����

        // Ÿ�� ���������� Y�� ȸ���� ���
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // ���� ������ ȸ������ Y�ุ Ÿ�� �������� ����
        transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);

        Ray ray = new Ray(Firepos.position, (Target.position + Vector3.up * 2f - Firepos.position).normalized);
        UnitStat stat = transform.GetComponent<UnitStat>();
        stat.stat.Action -= 2; // �߻� �� �ൿ�� 2 ����
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

                lineRenderer.SetPosition(0, Firepos.position); // ���� ������ ���� ��ġ ����
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
                break; // Ÿ���� ã���� ���� ����
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
