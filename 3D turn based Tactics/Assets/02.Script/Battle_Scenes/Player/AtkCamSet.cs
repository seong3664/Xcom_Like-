using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkCamSet : MonoBehaviour
{
    [SerializeField] private GameObject TurnManager;
    public CinemachineVirtualCamera NomCam;
    public CinemachineVirtualCamera AimCam;
    private Coroutine lookTargetCoroutine;
    private bool isAtkCamOn;
    public bool IsAtkCamOn
    { get { return isAtkCamOn; } }

    [SerializeField]
    RectTransform TargetingUI;
    private Canvas TargetingUIcanvas;
    private void Awake()
    {
        TurnManager = GameObject.Find("Turn_Manager");
        AimCam = GameObject.Find("Aim_Cam").GetComponent<CinemachineVirtualCamera>();
        NomCam = TurnManager.transform.GetChild(0).GetComponent<CinemachineVirtualCamera>();
        TargetingUIcanvas = TargetingUI.GetComponentInParent<Canvas>();
    }
    private void Start()
    {
        AimCam.enabled = false;
        NomCam.enabled = true;
        TargetingUI.gameObject.SetActive(false);
    }
    /// <summary>
    /// 선택한 유닛과 공격할 유닛을 받아
    /// 공격 시점을 설정함.
    /// </summary>
    /// <param name="Unit"></param>
    /// <param name="Target"></param>
    public void SetSelectUnitCam(Transform Unit, Transform Target)
    {
        isAtkCamOn = true;
        AimCam.enabled = true;
        NomCam.enabled = false;
        Vector3 direction = Target.position - Unit.position;
        direction.y = 0; 
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        if (lookTargetCoroutine != null)
        {
            StopCoroutine(lookTargetCoroutine);
        }
        lookTargetCoroutine = StartCoroutine(LookTarget(Unit, targetRotation));
        StartCoroutine(WaitingTurn(Unit, Target));

    }
    IEnumerator WaitingTurn(Transform Unit, Transform Target)
    {
        AimCam.Follow = Unit;
        AimCam.LookAt = Unit;

        // 카메라가 목표 위치에 도달할 때까지 기다립니다.
        yield return new WaitUntil(() => IsCameraAtTargetPosition());
        yield return new WaitForSeconds(1f);
        UpdateTargetingUIPosition(Target);
    }

    private bool IsCameraAtTargetPosition()
    {
        float positionThreshold = 2.1f;
        float positionDistance = Vector3.Distance(AimCam.transform.position, AimCam.Follow.position);

        return positionDistance < positionThreshold;
    }

    private void UpdateTargetingUIPosition(Transform Target)
    {
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(Target.position + Vector3.up);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            TargetingUIcanvas.transform as RectTransform,
            screenPoint,
            TargetingUIcanvas.worldCamera,
            out Vector2 localPoint
        );

        TargetingUI.localPosition = localPoint;
        TargetingUI.gameObject.SetActive(true);

        // Debug 메시지로 활성화 여부 확인
    }
    IEnumerator LookTarget(Transform Unit,Quaternion TargetRot)
    {
        float R_Time = 0f;
        while (Unit.rotation != TargetRot)
        {
            R_Time += Time.deltaTime;
            Unit.rotation = Quaternion.Slerp(Unit.rotation, TargetRot, R_Time*0.25f);
            yield return null;
        }
        lookTargetCoroutine = null;
    }
    public void OffAtkCam(Transform Unit)
    {
        TargetingUI.gameObject.SetActive(false);
        isAtkCamOn = false;
        AimCam.enabled=false;
        NomCam.enabled = true;
        TurnManager.transform.position = Unit.position;

    }

}
