using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition_To_Level_1 : MonoBehaviour {
    public Button yourButton;
    public GameObject Level_Select_UI;
    public GameObject Game_UI;
    public GameObject Staging;
    public GameObject cam;
    public GameObject FinishPastingButton;
    public GameObject DoNotEnter;
    public GameObject Level_Complete_UI;

    // Use this for initialization
    void Start () {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Game_UI.SetActive(true);
        Level_Select_UI.SetActive(false);
        Staging.SetActive(true);
        FinishPastingButton.SetActive(false);
        Level_Complete_UI.SetActive(false);
        DoNotEnter.SetActive(false);
        TurnCounter.turnsLeft = Globals.level1turns;
        UnitCounter.Enemies = 0;
        UnitCounter.Players = 0;
        UnitCounter.resetMoney = true;
    }
}
