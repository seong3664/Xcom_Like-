using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SetingMenuQuitBtn : MonoBehaviour
{
    List<Button> quitBoxOnOffBtn = new List<Button>();
    Button quitbtn;
    [SerializeField]
    GameObject quitcheckBox;
    Transform SetingMenuBtn;
    private void Awake()
    {
        SetingMenuBtn = transform.parent.GetChild(1);  //��ư�� ����� �޷��ִ� �г� ��Ƴ��� ������Ʈ
        quitcheckBox = transform.GetChild(3).gameObject;   //���� ��ư�� ��¥ �������� �ƴ��� Ȯ���ϴ� â
        //quitBoxOnOffBtn.Add(SetingMenuBtn.GetChild(3).GetComponent<Button>());  //quit��ư ������ Ȯ��â on �������� �� ������ off
        //quitBoxOnOffBtn.Add(quitcheckBox.transform.GetChild(0).GetChild(2).GetComponent<Button>()); //quit ��ư ������ ������ Ȯ��â���� no��ư
        quitbtn = quitcheckBox.transform.GetChild(0).GetChild(1).GetComponent<Button>();



    }
    private void Start()
    {
        //foreach (var button in quitBoxOnOffBtn)
        //{
        //    button.onClick.RemoveAllListeners();
        //    button.onClick.AddListener(QuitBtnOnOff);
        //}
        quitbtn.onClick.RemoveAllListeners();
        quitbtn.onClick.AddListener(QuitBtn);
    }

    public void QuitBtnOnOff()
    {
        quitcheckBox.SetActive(!quitcheckBox.activeSelf);
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
