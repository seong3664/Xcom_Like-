using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideobj : MonoBehaviour
{
    Transform target;  // ClearShot ī�޶� ���󰡴� ���
    LayerMask obstacleMask;  // ������ ��ü�� ���̾� ����
    public float transparentAlpha = 0.3f;  // ����ȭ ���� �� ����

    private Transform[] obstacles;  // ������ ��ü��

    private void Start()
    {
        target = transform.parent;
        obstacleMask = 1 << 10 | 1 << 11;
    }

    void Update()
    {
        // ���� �������� ���� ��ü���� ������� ����
        if (obstacles != null)
        {
            foreach (var obstacle in obstacles)
            {
                SetTransparency(obstacle, 1f);  // ���� ������ ���·� ����
            }
        }

        // ī�޶�� ��� ������ ��ü�� ����
        Vector3 direction = target.position - transform.position;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, direction.magnitude, obstacleMask);

        obstacles = new Transform[hits.Length];
        for (int i = 0; i < hits.Length; i++)
        {
            obstacles[i] = hits[i].transform;
            SetTransparency(obstacles[i], transparentAlpha);  // �����ϰ� ����
        }
    }

  void SetTransparency(Transform obj, float alpha)
{
    Renderer renderer = obj.GetComponent<Renderer>();
    if (renderer != null)
    {
        // MaterialPropertyBlock�� ����Ͽ� Transparency ���� ����
        MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(materialPropertyBlock);
        materialPropertyBlock.SetFloat("_Transparency", alpha);
        renderer.SetPropertyBlock(materialPropertyBlock);  // Transparency ���� ����

        //// ���İ��� ���� �����Ͽ� ���� ����
        //Material material = renderer.material;
        //Color color = material.color;
        //color.a = alpha; // ���� ���� ���ĸ� ����
        //material.color = color; // ���� ����
    }
}
}
