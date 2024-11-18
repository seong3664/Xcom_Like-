using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.GlobalIllumination;

public class MoveCtrl : MonoBehaviour
{   
    private float moveSpeed = 10f;
    
    Unit_AniCtrl Unitani;
    private bool isMove = true;
    private Transform Cam;
   [SerializeField] SetNode setnode;
    public bool IsMove
    {get { return isMove; } }
    [SerializeField] GameObject Btn;

    private void Start()
    {
        Cam =GameObject.Find("Turn_Manager").GetComponent<Transform>();
    }
    public IEnumerator MoveNode(Transform selectUnit, List<A_Nodes> path)
    {
        setnode = selectUnit.GetComponent<SetNode>();
        isMove = false;
        Btn.gameObject.SetActive(false);
        setnode.OutNode();
        SoundManager.instance.PlayerSound("footstep");
        foreach (A_Nodes node in path)
        {
            Vector3 targetPosition = node.WorldPos;
           
            // Ÿ�� ��ġ���� �̵�
            while (Vector3.Distance(selectUnit.position, targetPosition) > 0.1f)
            {
                //Debug.Log("����� �̵��ϸ� ������ ��µɰ�");
                Vector3 Targetdist = targetPosition - selectUnit.position;
                selectUnit.rotation = Quaternion.LookRotation(new Vector3(Targetdist.x, 0, Targetdist.z));
                selectUnit.position = Vector3.MoveTowards(selectUnit.position, targetPosition, moveSpeed * Time.deltaTime);
               yield return null;
                Cam.position = selectUnit.position;
               
            }

        }
        setnode.SetInNode();
        Btn.gameObject.SetActive(true);
        isMove = true;

    }
    //IEnumerator Move(Transform selectUnit, List<A_Nodes> path)
    //{
    //    isMove = false;
      
    //}
  
    

}
