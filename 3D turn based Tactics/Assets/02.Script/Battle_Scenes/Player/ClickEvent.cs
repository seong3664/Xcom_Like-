using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;
using UnityEngine.UI;
public class ClickEvent : MonoBehaviour
{
    public A_Pathfinding A_Pathfinding;
    public A_Grid grid;
    public List<A_Nodes> reachableNodes = new List<A_Nodes>();
    List<A_Nodes> toMoveNodes = new List<A_Nodes>();
    [SerializeField]
    Draw_movearea draw_Movearea;
    MoveCtrl moveCtrl;
    private Transform selectUnit;
    private UnitStat unitStat;



    bool canMove = true;
    LayerMask Player;
    LayerMask WalkAble;
    LayerMask Enemy;
    Unit_AniCtrl aniCtrl;

    private BtnEvent btnEvent;
    public AtkCamSet AtkCam;
    private GameObject GameManager;
    private Transform MoveUi;

    Quaternion UIrot;

    [SerializeField]
    Image BulletCount;

    private void Awake()
    {
        moveCtrl = GetComponent<MoveCtrl>();
        //draw_Movearea =Camera.main.transform.GetComponent<Draw_movearea>();
        btnEvent = GameObject.Find("UI").GetComponent<BtnEvent>();
        GameManager = GameObject.Find("Game_Manager");
        MoveUi = transform.GetChild(0).GetComponent<Transform>();
        AtkCam = btnEvent.transform.GetComponent<AtkCamSet>();
    }
    private void Start()
    {
        turnManager.EndTurn += EndTurn;
        MoveUi.gameObject.SetActive(false);
        Player = 1 << 3;
        WalkAble = 1 << 7;
        Enemy = 1 << 8;
        UIrot = MoveUi.rotation;
        BulletCount.enabled = false;
    }
    void EndTurn()
    {//턴종료시 셀렉트 유닛 없앰
        selectUnit = null;
    }
    private void Update()
    {

        if (turnManager.TurnManager.state != TurnState.PlayerTurn)
        {//자신의 턴이 아니라면 UI를 끔
            draw_Movearea.isDraw = false;
            BulletCount.enabled = false;
            MoveUi.gameObject.SetActive(false);
            return;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //화면의 마우스 위치에서 월드로 레이
        RaycastHit hit;
        if (unitStat != null && unitStat.stat.Action == 0)
        {   //셀렉트 유닛의 스탯이 있고, 해당 유닛의 행동력이 0이라면 UI를 끈다.
            draw_Movearea.isDraw = false;
            MoveUi.gameObject.SetActive(false);
        }
        if (EventSystem.current.IsPointerOverGameObject() == false)
        {   //UI 클릭시 레이캐스트 막기
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, Player | WalkAble | Enemy) && canMove)
            {//이동 중이 아니고 레이캐스트에 검출된 레이어가 플레이어,이동가능 영역,적 중 하나라면
                if (((1 << hit.collider.gameObject.layer) & WalkAble) != 0 && selectUnit != null && canMove && unitStat.stat.Action > 0)
                {//레이캐스트와 부딫힌 레이어가 있고,셀렉트 유닛이 존재하며 이동중이 아니고 셀렉트 유닛의 행동력이 양수일 때 이동 UI 활성화
                    toMoveNodes = A_Pathfinding.A_StarAlgorithm(selectUnit.position, hit.point, unitStat.stat.MovePoint, false);
                    draw_Movearea.Draw_MoveLine(selectUnit.position, toMoveNodes);

                }

                if (Input.GetMouseButtonDown(0))
                {

                    if (((1 << hit.collider.gameObject.layer) & Player) != 0)
                    {//플레이어 레이어와 충돌시
                        selectUnit = hit.collider.transform;
                        //셀렉트 유닛을 충돌한 오브젝트의 트랜스폼으로
                        btnEvent.BtnEventSetUnit(selectUnit);
                        //행동 버튼이 조작할 유닛을 셀렉트 유닛으로 설정
                        MoveUi.SetParent(selectUnit);
<<<<<<< HEAD
                        MoveUi.position = selectUnit.position + Vector3.up * 0.2f;
=======
                        MoveUi.position = selectUnit.position + Vector3.up * 0.01f;
>>>>>>> parent of 5ecb678 (Revert "xcomlike")
                        MoveUi.localRotation = UIrot;
                        MoveUi.gameObject.SetActive(true);
                        //이동 UI 설정. 선택한 유닛 따라다니게
                        unitStat = selectUnit.GetComponent<UnitStat>();
                        //선택한 유닛의 스탯 가져옴
                        BulletCount.enabled = true;
                        BulletCount.fillAmount = (float)unitStat.BulletCount / 3;
                        //선택 유닛의 잔탄량 표시
                        aniCtrl = selectUnit.GetComponent<Unit_AniCtrl>();
                        //선택 유닛의 애니메이션 재생 스크립트
                        if (unitStat.stat.Action > 0)
                        {//행동력이 0보다 높다면 이동 가능으로 설정
                            canMove = true;
                            DrawMoveArea();
                            //현재 셀렉트 유닛의 이동력을 기준으로 이동 범위 그리기
                        }
                    }

                    else if (((1 << hit.collider.gameObject.layer) & WalkAble) != 0 && selectUnit != null && canMove && toMoveNodes != null && unitStat.stat.Action > 0)
                    {//셀렉트 유닛이 존재하는 상태에서 행동력이 남아있고 이동이 가능하며 이동 가능 범위 내의 이동 영역을 클릭했다면
                        canMove = false;
                        //이동 시작(이동중엔 추가행동 못하게 막아놓음)
                        aniCtrl.MoveAniSet(true);
                        //이동 애니메이션 시작
                        unitStat.stat.Action -= 1;
                        //행동력 1 감소
                        draw_Movearea.isDraw = false;
                        //이동영역 그만 그림
                        StartCoroutine(moveCtrl.MoveNode(selectUnit, toMoveNodes));
                        //이동 끝날때 까지 대기

                    }
                }

            }
            //}
            
            }
        if (!canMove && moveCtrl.IsMove)
        {//이동이 끝났다면
            aniCtrl.MoveAniSet(false);
            //무브 애니 종료
            canMove = true;
            //행동 가능
            AtkCam.OffAtkCam(selectUnit);
            //공격했었다면 공격시점 해제
            if (unitStat.stat.Action > 0)
            {
                DrawMoveArea();
                //행동력이 0이상(이동가능)이라면 이동 가능 영역 그리기
            }
            else
            {
                btnEvent.BtnEventSetUnit(selectUnit);
                BulletCount.enabled = false;
                //아니라면 행동 버튼 비활성화
            }
            if (selectUnit == null)
            {//잔탄수 UI  활성화,비활성화
                BulletCount.enabled = false;
            }
            else
            {
                BulletCount.enabled = true;
                BulletCount.fillAmount = (float)unitStat.BulletCount / 3;
            }

        }
        void DrawMoveArea()
        {//이동 영역 그리는 매서드에 경로탐색한 이동 가능 영역 노드 리스트 보내줌

            reachableNodes = A_Pathfinding.GetReachableNodes(selectUnit.position, unitStat.stat.MovePoint);
            draw_Movearea.reachableNodes = reachableNodes;
            grid.reachableNodes = reachableNodes;

            draw_Movearea.boundarydraw();
            draw_Movearea.isDraw = true;
        }
    }
}
