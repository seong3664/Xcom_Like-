using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideobj : MonoBehaviour
{
    Transform target;  // ClearShot 카메라가 따라가는 대상
    LayerMask obstacleMask;  // 가리는 물체의 레이어 설정
    public float transparentAlpha = 0.3f;  // 투명화 알파 값 설정

    private Transform[] obstacles;  // 가리는 물체들

    private void Start()
    {
        target = transform.parent;
        obstacleMask = 1 << 10 | 1 << 11;
    }

    void Update()
    {
        // 이전 프레임의 가림 물체들을 원래대로 복원
        if (obstacles != null)
        {
            foreach (var obstacle in obstacles)
            {
                SetTransparency(obstacle, 1f);  // 원래 불투명 상태로 복원
            }
        }

        // 카메라와 대상 사이의 물체들 감지
        Vector3 direction = target.position - transform.position;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, direction.magnitude, obstacleMask);

        obstacles = new Transform[hits.Length];
        for (int i = 0; i < hits.Length; i++)
        {
            obstacles[i] = hits[i].transform;
            SetTransparency(obstacles[i], transparentAlpha);  // 투명하게 설정
        }
    }

  void SetTransparency(Transform obj, float alpha)
{
    Renderer renderer = obj.GetComponent<Renderer>();
    if (renderer != null)
    {
        // MaterialPropertyBlock을 사용하여 Transparency 값을 설정
        MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(materialPropertyBlock);
        materialPropertyBlock.SetFloat("_Transparency", alpha);
        renderer.SetPropertyBlock(materialPropertyBlock);  // Transparency 값을 적용

        //// 알파값을 직접 수정하여 투명도 설정
        //Material material = renderer.material;
        //Color color = material.color;
        //color.a = alpha; // 직접 색상에 알파를 적용
        //material.color = color; // 색상 변경
    }
}
}
