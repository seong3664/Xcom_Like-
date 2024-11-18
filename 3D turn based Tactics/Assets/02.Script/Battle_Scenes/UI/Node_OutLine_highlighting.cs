using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class Node_OutLine_highlighting : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Color UiColor;

    public float rayDistance = 3f;  // ����ĳ��Ʈ�� �Ÿ�
    public LayerMask targetLayer;   // ������ ������Ʈ�� ���̾�
    [SerializeField]
    RectTransform CoverUi;

    Vector3 CoverUiPos;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UiColor = Color.white;
        targetLayer = 1 << 11;
        CoverUi = transform.GetChild(0).GetComponent<RectTransform>();

    }
    private void OnEnable()
    {
        StartCoroutine(highlightNode());
        CoverUiPos = new Vector3(0f, 0.9f, -0.79f);
        CoverUi.gameObject.SetActive (false);
    }
    private void FixedUpdate()
    {
        bool coverDetected = false;
        RaycastHit[] hits;
        Vector3 origin = transform.position + Vector3.up * 0.5f;
       hits = Physics.RaycastAll(origin, transform.up, rayDistance);
        foreach (RaycastHit hit in hits)
        {
            if ((1 << hit.collider.gameObject.layer & targetLayer) != 0)
            {
             
                CoverUiPos.x = 0f;
                CoverUiPos.y = 0.9f;
                CoverUi.localPosition = CoverUiPos;
                CoverUi.localRotation = Quaternion.Euler(new Vector3(90f, -90f, 90f));
                coverDetected = true;
                break;
            }
        }

        hits = Physics.RaycastAll(origin, -transform.up, rayDistance);
        foreach (RaycastHit hit in hits)
        {
            if ((1 << hit.collider.gameObject.layer & targetLayer) != 0)
            {
                CoverUiPos.x = 0f;
                CoverUiPos.y = -0.9f;
                CoverUi.localPosition = CoverUiPos;
                CoverUi.localRotation = Quaternion.Euler(new Vector3(90f, -90f, 90f));
                coverDetected = true;
                break;
            }
        }

        hits = Physics.RaycastAll(origin, -transform.right, rayDistance);
        foreach (RaycastHit hit in hits)
        {
            if ((1 << hit.collider.gameObject.layer & targetLayer) != 0)
            {
                CoverUiPos.x = -0.9f;
                CoverUiPos.y = 0f;
                CoverUi.localPosition = CoverUiPos;
                CoverUi.localRotation = Quaternion.Euler(new Vector3(0f, -90f, 90f));
                coverDetected = true;
                break;
            }
        }

        hits = Physics.RaycastAll(origin, transform.right, rayDistance);
        foreach (RaycastHit hit in hits)
        {
            if ((1 << hit.collider.gameObject.layer & targetLayer) != 0)
            {
                CoverUiPos.x = 0.9f;
                CoverUiPos.y = 0f;
                CoverUi.localPosition = CoverUiPos;
                CoverUi.localRotation = Quaternion.Euler(new Vector3(0f, -90f, 90f));
                coverDetected = true;
                break;
            }
        }

        CoverUi.gameObject.SetActive(coverDetected);

    }
    IEnumerator highlightNode()
    {
        float a = 0.009f;
        while (true)
        {
            for (int i = 0; i < 150; i++)
            {
                UiColor.a = Mathf.Clamp(UiColor.a + a, 0f, 0.7f); 
                spriteRenderer.color = UiColor;
                yield return null;
            }
            a = -a; 
            yield return null;
        }
    }
    }
