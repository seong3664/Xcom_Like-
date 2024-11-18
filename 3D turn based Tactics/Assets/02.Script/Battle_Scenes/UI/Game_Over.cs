using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Over : MonoBehaviour
{
    [SerializeField]
    Button LoadBtn;
    [SerializeField]
    Button EndBtn;

    private void Awake()
    {
        
    }
    private void Start()
    {
        LoadBtn.onClick.RemoveAllListeners();
        LoadBtn.onClick.AddListener(() => Data_Manager.Instance.LoadUnits(true));
        EndBtn.onClick.RemoveAllListeners();
        EndBtn.onClick.AddListener(QuitBtn);
    }
    public void QuitBtn()
    {
#if UNITY_EDITOR    
        UnityEditor.EditorApplication.isPlaying = false;
#else
     Application.Quit();
#endif
    }
}
