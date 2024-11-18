using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granede_Ctrl : MonoBehaviour
{
    [SerializeField] private Transform throwPoint;
    [SerializeField] private float explosionRadius = 5.0f;
    [SerializeField] private float explosionForce = 500f;
    [SerializeField] private int enemyLayer = 9;
    [SerializeField] private int destroyableLayer = 11;
    [SerializeField] private ParticleSystem Expeff;
    private Vector3 targetPosition;

    public void ThrowGrenade(GameObject grenade, Vector3 target)
    {

        Rigidbody rb = grenade.GetComponent<Rigidbody>();

        // ��ǥ �������� �ʿ��� �ʱ� �ӵ� ���
        Vector3 velocity = CalculateThrowVelocity(target, throwPoint.position, 45f);
        rb.velocity = velocity;

        // ��ǥ ��ġ ���� �� �Ÿ� üũ ����
        targetPosition = target;
        StartCoroutine(CheckDistanceToTarget(grenade));
    }

    private IEnumerator CheckDistanceToTarget(GameObject grenade)
    {
        // �׷����̵尡 ��ǥ ������ ������ ������ �ݺ�
        while (Vector3.Distance(grenade.transform.position, targetPosition) > 0.5f)
        {
            yield return null; // �� ������ ���
        }

        // ��ǥ ���� ���� �� ���� �߻�
        Explode();
    }

    private void Explode()
    {
        // ���� ȿ�� ���
        Expeff.Play();

        // �ֺ� ������Ʈ Ž��
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider hitCollider in hitColliders)
        {
            GameObject obj = hitCollider.gameObject;

            if (obj.layer == enemyLayer)
            {
                var damageable = obj.GetComponent<OnDamege>();
                if (damageable != null)
                {
                    Vector3 hitPoint = obj.transform.position; // ���� ������Ʈ ��ġ ���
                    Vector3 hitNormal = (obj.transform.position - transform.position).normalized; // ���� �߽ɺο��� ���� ����
                    damageable.OnDamege(4, hitPoint, hitNormal);
                }
            }
            else if (obj.layer == destroyableLayer)
            {
                // ���̾� ���� �� ���� ȿ���� ���ư��� �ϱ�
                obj.layer = 0; // �⺻ ���̾�� ����
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }
            }
        }

        // ���� �� �׷����̵� ����
        Destroy(gameObject);
    }

    private Vector3 CalculateThrowVelocity(Vector3 target, Vector3 origin, float angle)
    {
        float gravity = Physics.gravity.y;
        float radianAngle = angle * Mathf.Deg2Rad;

        Vector3 direction = target - origin;
        float horizontalDistance = new Vector3(direction.x, 0, direction.z).magnitude;
        float verticalDistance = direction.y;

        float initialVelocity = Mathf.Sqrt(Mathf.Abs(gravity * horizontalDistance * horizontalDistance) /
                                           (2 * (horizontalDistance * Mathf.Tan(radianAngle) - verticalDistance)));

        Vector3 velocity = new Vector3(direction.x, 0, direction.z).normalized * initialVelocity * Mathf.Cos(radianAngle);
        velocity.y = initialVelocity * Mathf.Sin(radianAngle);

        return velocity;
    }
}



