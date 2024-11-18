using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Turn_UiCtrl : MonoBehaviour
{
    Image TurnUI;
    TMP_Text TurnTxt;

    private void Start()
    {
        TurnUI = transform.GetChild(2).GetComponent<Image>();
        TurnTxt = TurnUI.transform.GetChild(1).GetComponent<TMP_Text>();
        turnManager.EndTurn += EndTurn;
        EndTurn();
    }
    void EndTurn()
    {
        if (turnManager.TurnManager.state == TurnState.EnemyTurn)
        {
            TurnUI.color = new Color(1, 0, 0, 0.6f);
            StartCoroutine(TurnUIImagOnff());
            TurnTxt.text = "Enemy Turn";
        }
        else if(turnManager.TurnManager.state == TurnState.PlayerTurn)
        {
            TurnUI.color = new Color(0, 0, 1, 0.6f);
            StartCoroutine(TurnUIImagOnff());
            TurnTxt.text = "Player Turn";
        }
    }
    IEnumerator TurnUIImagOnff()
    {
        TurnUI.enabled = true;
        yield return new WaitForSeconds(3f);
        TurnUI.enabled = false;
    }

}
