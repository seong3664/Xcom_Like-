using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookatCam : MonoBehaviour
{
    private void FixedUpdate()
    {
        //transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position, Vector3.up);
        Vector3 targetPosition = Camera.main.transform.position;
        targetPosition.y = transform.position.y;  // UI�� Y�� ��ġ ����
        transform.LookAt(targetPosition);         // ī�޶� ������ �ٶ󺸰� ȸ��
    }

}
