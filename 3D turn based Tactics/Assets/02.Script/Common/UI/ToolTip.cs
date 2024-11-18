using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject tooltipObject;  // 설명 텍스트 오브젝트
    public TextMeshProUGUI tooltipText;  // TextMeshPro를 사용할 경우
    // public Text tooltipText;  // Unity 기본 Text 사용 시

    public string description;  // 설명할 텍스트 내용

    private void Start()
    {
        tooltipObject.SetActive(false);  // 초기 상태를 비활성화
    }

    // 마우스가 UI 위에 올라갔을 때 실행
    public void OnPointerEnter(PointerEventData eventData)
    {

        tooltipText.text = description;
        tooltipObject.SetActive(true);
    }

    // 마우스가 UI에서 벗어났을 때 실행
    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipObject.SetActive(false);
    }

}
