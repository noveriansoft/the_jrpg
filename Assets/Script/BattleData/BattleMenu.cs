using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class BattleMenu : MonoBehaviour
{
    public Button attackButton;
    public Button runButton;

    void Start()
    {
        attackButton.interactable = false;
        runButton.interactable = false;

        StartCoroutine(EnableBattleButtons());
    }

    public void enableButton()
    {
        StartCoroutine(EnableBattleButtons());
    }

    IEnumerator EnableBattleButtons()
    {
        yield return new WaitForSeconds(1f);

        attackButton.interactable = true;
        runButton.interactable = true;

        EventSystem.current.SetSelectedGameObject(attackButton.gameObject);
    }
}
