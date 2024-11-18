
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Scenes_Manager : MonoBehaviour
{
    [SerializeField]
    Button StartButton;
    [SerializeField]
    Button QuitButton;

    private void Awake()
    {
        if(QuitButton == null)
        {
            QuitButton = GameObject.Find("Seting_Ui").transform.GetChild(0).GetChild(1).GetChild(3).GetComponent<Button>();
        }
    }
    private void Start()
    {
        StartButton.onClick.RemoveAllListeners();
        StartButton.onClick.AddListener(LoadBattleScenes);
        QuitButton.onClick.RemoveAllListeners();
        QuitButton.onClick.AddListener(QuitGame);
    }
    void LoadBattleScenes()
    {
        if(SceneManager.GetActiveScene().name == "Ready_Scenes")
        {
            GameManager.gamemaneger.isnewGame = true;
            GameManager.gamemaneger.Stats = UnitSet_Manager.instance.unitstat;
        }
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            // ´ÙÀ½ ¾À ·Îµå
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("¸¶Áö¸· ¾À");
        }
    }
    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
     Application.Quit();
#endif
    }
}
