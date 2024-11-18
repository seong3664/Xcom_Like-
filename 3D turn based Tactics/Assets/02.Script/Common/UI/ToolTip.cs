using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject tooltipObject;  // ���� �ؽ�Ʈ ������Ʈ
    public TextMeshProUGUI tooltipText;  // TextMeshPro�� ����� ���
    // public Text tooltipText;  // Unity �⺻ Text ��� ��

    public string description;  // ������ �ؽ�Ʈ ����

    private void Start()
    {
        tooltipObject.SetActive(false);  // �ʱ� ���¸� ��Ȱ��ȭ
    }

    // ���콺�� UI ���� �ö��� �� ����
    public void OnPointerEnter(PointerEventData eventData)
    {

        tooltipText.text = description;
        tooltipObject.SetActive(true);
    }

    // ���콺�� UI���� ����� �� ����
    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipObject.SetActive(false);
    }

}
