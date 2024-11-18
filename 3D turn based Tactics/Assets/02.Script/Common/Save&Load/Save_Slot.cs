using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Save_Slot : MonoBehaviour
{
    public TMP_Text[] slotText;
    private Button[] slotButton = new Button[4];
    [SerializeField]
    bool[] savefile = new bool[4];
    [SerializeField]
    GameObject Loadslots;
    private void Awake()
    {
        for (int i = 0; i < slotText.Length; i++)
        {
            slotButton[i] = slotText[i].transform.parent.GetComponent<Button>();

        }
    }
    public void OnoffSaveSlot()
    {
        Loadslots.SetActive(!Loadslots.activeSelf);
    }
    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            if (File.Exists(Data_Manager.Instance.path + $"{i}" +".json"))    
            {   //게임 시작시 세이브 파일이 있는지 확인
                savefile[i] = true;         //있다면 해당 슬롯에 세이브 파일 존재함 체크
                Data_Manager.Instance.nowSlot = i;   //해당 슬롯 데이터 불러오기 위함
                Data_Manager.Instance.LoadUnits(false);    //flase로 설정해 기본 데이터만 가져와서 읽기 씬 로드등은 x
                DateTime lastModified = File.GetLastWriteTime(Data_Manager.Instance.path + $"{i}" + ".json");
                slotText[i].text = $"Slot {i} - {lastModified:MM-dd HH:mm:ss}"; 
            }
            else   
            {
                savefile[i] = false;
                slotText[i].text = "No data";
            }
        }
        Data_Manager.Instance.DataClear();  
    }
    public void Slot(int number)	
    {
        //슬롯 클릭시
        Data_Manager.Instance.nowSlot = number;
        //슬롯 넘버를 매개변수로 받아 데이터 매니저 나우슬롯에 전달
        if (savefile[number])
        {
            GameManager.gamemaneger.PlayerList.Clear();
            GameManager.gamemaneger.EnemyList.Clear();
            Data_Manager.Instance.LoadUnits(true);	
            //데이터 파일이 있다면, 로드하기 전 게임 매니저 리스트를 클리어하고 로드
        }
        else
        {
            if (SceneManager.GetActiveScene().name == "Battle_Scenes")
            {//없다면 세이브. 배틀 씬에서만 세이브 되도록
                Data_Manager.Instance.SaveUnitData();
                savefile[number] = true;
                slotText[number].text = $"Slot{Data_Manager.Instance.nowSlot} - " + DateTime.Now.ToString("_MM-dd_HH-mm-ss");
            }
        }
    }
    public void DeleteSavedData(int slot)
    {
        string filePath = Data_Manager.Instance.path + slot.ToString() + ".json";

        //파일 지우기
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            slotText[slot].text = "No data";
            savefile[slot] = false;
        }
        else
        {
        }
    }
}
