using States;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gamemaneger { get; private set; }

    [SerializeField]
    private List<Stat> stats = new List<Stat>();
    public List<Stat> Stats {
        get { return stats; }
        set { stats = value; }
                            }

    public GameObject playerPrefab;
    public List<GameObject> PlayerList = new List<GameObject>();
    public GameObject Enemyprefab;
    [SerializeField]
    public List<GameObject> EnemyList = new List<GameObject>();

    public bool isnewGame = false;
    private void Awake()
    {
        if (gamemaneger == null)
        {
            gamemaneger = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
        playerPrefab = Resources.Load("Player 1") as GameObject;
        Enemyprefab = Resources.Load("Enemy") as GameObject;
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; 
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Battle_Scenes" && isnewGame)
        {
            CreateUnit();
            isnewGame = false;
        }
    }
    private void CreateUnit()
    {

        Transform[] playerSpawnTransforms = GameObject.Find("Player_Spawn").GetComponentsInChildren<Transform>();

        for (int i = 1; i < playerSpawnTransforms.Length; i++) // 1���� �����Ͽ� �θ� �ǳʶ�
        {
            Transform t = playerSpawnTransforms[i];
            GameObject newUnit = Instantiate(playerPrefab, t.position, Quaternion.identity);
            PlayerList.Add(newUnit);
            
            UnitStat unitStat = newUnit.GetComponent<UnitStat>();
            if(unitStat.stat != null)
            {
                unitStat.stat = null;
            }
            unitStat.stat = stats[i - 1];
        }
<<<<<<< HEAD
        Transform[] EnemySpawnTransforms = GameObject.Find("Enemy_Spawn").GetComponentsInChildren<Transform>();
        for (int i = 1; i < EnemySpawnTransforms.Length; i++)
        {
            EnemyList.Add(Instantiate(Enemyprefab, EnemySpawnTransforms[i].position, Enemyprefab.transform.rotation));
=======
        for (int i = 0; i < 6; i++)
        {
            Vector3 Randompos = new Vector3(Random.Range(-29,18),0f,Random.Range(16,24));
            EnemyList.Add(Instantiate(Enemyprefab, Randompos, Enemyprefab.transform.rotation));
>>>>>>> parent of 5ecb678 (Revert "xcomlike")
        }
    }
}
