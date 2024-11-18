using States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System;
public class Data_Manager : MonoBehaviour
{
    private static Data_Manager instance;
    public static Data_Manager Instance 
    {  
        get { return instance; }
        private set 
        { if (instance == null)
                 instance = value;
          else if(instance != null)
                Destroy(instance.gameObject);
        }
    }
    UnitSaveData saveData = new UnitSaveData();
    public Dictionary<string, GameObject> modelPrefabs;
    GameObject playerPrefab;
    GameObject enemyPrefab;
    public string path;
    public int nowSlot;
    UnitSaveData loadedData = new UnitSaveData();
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        path = Application.persistentDataPath + "/save";
    }
    void Start()
    {
        playerPrefab = GameManager.gamemaneger.playerPrefab;
        enemyPrefab = GameManager.gamemaneger.Enemyprefab;
        modelPrefabs = new Dictionary<string, GameObject>()
    {
        { "Player", playerPrefab },
        { "Enemy", enemyPrefab },
    };
    }
    public void SaveUnitData()
    {
        
        saveData.sceneName = SceneManager.GetActiveScene().name;
        //씬 이름 저장
        saveData.units.Clear();
        UnitStat[] Units = FindObjectsOfType<UnitStat>();
       //저장시 모든 유닛 스탯을 가져와 저장함.
        foreach (UnitStat unitStat in Units)
        {
            Stat stat = unitStat.stat;
            Transform UnitTr= unitStat.transform;
            if (stat != null && UnitTr != null)
            {
                UnitData unistdata = new UnitData(stat, UnitTr);
                //유닛 스탯과 위치 데이터를 저장할 수 있게 바꾸어줌
                saveData.units.Add(unistdata);
                //바꾼 데이터를 세이브 데이터 목록에 추가
            }
        }
       
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(path + nowSlot.ToString() + ".json", json);
    }
    public void LoadUnits(bool loadGame)
    {
        if (File.Exists(path + nowSlot.ToString() + ".json"))
        {
            string json = File.ReadAllText(path + nowSlot.ToString() + ".json");
            loadedData = JsonUtility.FromJson<UnitSaveData>(json);
            if (loadGame)
                LoadGame();
                //파일 이름으로 로드
        }
        else
        {
            Debug.Log("파일 없음");
            
        }
       
    }
    public void LoadGame()
    {
        string saveFilePath = path + nowSlot.ToString() + ".json";

        if (File.Exists(saveFilePath))
        {
           //로드 완료시에 데이터 불러오기
            SceneManager.sceneLoaded += OnSceneLoaded;

            
            //배틀 씬으로 전환
            SceneManager.LoadScene("Battle_Scenes", LoadSceneMode.Single);
        }
        else
        {
            Debug.Log("저장 데이터 없음");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //데이터 불러오기 시작
        ApplyLoadedUnits(loadedData, modelPrefabs);

        // 이벤트 구독 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void ApplyLoadedUnits(UnitSaveData loadedData, Dictionary<string, GameObject> modelPrefabs)
    {
        Debug.Log(loadedData.units.Count);
        foreach (UnitData unitData in loadedData.units)
        {
            //가져온 데이터를 유닛 스탯에 다시 적용
            if (modelPrefabs.TryGetValue(unitData.statData.unitCode, out GameObject prefab))
            {
                GameObject unit = Instantiate(prefab, unitData.position, unitData.rotation);
                Stat stat = ScriptableObject.CreateInstance<Stat>();
                if (stat != null)
                {
                    stat.dmg = unitData.statData.dmg;
                    stat.Hp = unitData.statData.hp;
                    stat.MovePoint = unitData.statData.movePoint;
                    stat.Action = unitData.statData.actionPoint;
                    stat.Aiming = unitData.statData.aiming;
                    stat.Evasion = unitData.statData.evasion;
                    stat.Crit = unitData.statData.crit;
                    stat.ScopeOnoff = unitData.statData.scopeOnoff;
                    stat.VestOnoff = unitData.statData.vestOnoff;
                    stat.MuzzleOnoff = unitData.statData.muzzleOnoff;
                    
                }
                if (unitData.statData.unitCode == "Player")
                {
                    //플레이어라면 플레이어 프리팹 적용+게임 매니저 플레이어 리스트에 추가
                    stat.UnitCode = UnitCode.Player;
                    GameManager.gamemaneger.PlayerList.Add(unit);

                }
                else if (unitData.statData.unitCode == "Enemy")
                {
                    //적도 마찬가지로 적프리팹,적 리스트에 추가
                    stat.UnitCode = UnitCode.Enemy;
                    GameManager.gamemaneger.EnemyList.Add(unit);
                }
                unit.GetComponent<UnitStat>().stat = stat;

               
            }
            else
            {
                Debug.LogWarning($"유닛 프리팹 없음");
            }
        }
       
    }
    
    public void DataClear()
    {
        nowSlot = -1;
        loadedData = new UnitSaveData();
    }
}
