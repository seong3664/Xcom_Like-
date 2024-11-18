using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum TurnState { PlayerTurn, WaitingTurn, EnemyTurn, GameOver }
public class turnManager : MonoBehaviour
{

    static turnManager turnmanager;
    GameObject GameOver;
    GameObject GameClear;
   
    public static turnManager TurnManager
    {
        private set
        {
            if (turnmanager == null)
                turnmanager = value;
            else 
                Destroy(value.gameObject);
        }
        get
        {
            return turnmanager;
        }
    }
    public Transform SelectUnit;
    private TurnState State = TurnState.PlayerTurn;
    public TurnState state
    {
        get { return State; }
        set { State = value; }
    }
   

    public delegate void TurnEndEvent();
    public static TurnEndEvent EndTurn; //턴 종료시 공통으로 발생하는 이벤트
    public delegate void PlayerTurnEndEvent();
    public static PlayerTurnEndEvent PlayerEndTurn; //플레이어 턴 종료시에만 발생하는 이벤트
    public delegate void EnemyTurnEndEvent();
    public static EnemyTurnEndEvent EnemyEndTurn;   //적 턴 종료시에만 발생하는 이벤트


    private void Awake()
    {
        TurnManager = this;
        Transform GameEnd = GameObject.Find("Game_End_Canvas").GetComponent<Transform>();
        GameOver = GameEnd.GetChild(0).gameObject;
        GameClear = GameEnd.GetChild(1).gameObject;
    }
    private void Start()
    {
        state = TurnState.PlayerTurn;
        Data_Manager.Instance.nowSlot = 0;
        Data_Manager.Instance.SaveUnitData();
    }
    public void EndPlayerTunr() //플레이어 턴 종료
    {
        state = TurnState.EnemyTurn;
        EndTurn();
        PlayerEndTurn();
    }
    public void EndEnemyturn()  //적 턴 종료
    {
        state = TurnState.PlayerTurn;
        EndTurn();
        EnemyEndTurn();
    }
    public void CheckEndGame()  //게임 종료 체크(적이나 아군 유닛 수가 0일시)
    {
        if (GameManager.gamemaneger.PlayerList.Count <= 0)
        {
            GameOver.SetActive(true);
        }
        else if (GameManager.gamemaneger.EnemyList.Count <= 0)
        {
            GameClear.SetActive(true);
        }
    }
}

    


