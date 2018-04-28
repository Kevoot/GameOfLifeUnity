using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishTurn : MonoBehaviour {
    public Button yourButton;
	public accounting acc;
    public GameObject TurnsLeftLabel;
    public GameObject DoNotEnter;
    public GameObject Staging;
    public GameObject Game_UI;
    public GameObject Level_Complete_UI;
    public static bool pasting;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Globals.completeTurnRequested = true;
		acc.update_balance (5);
        TurnCounter.turnsLeft--;

        //Check if we have defeated the enemy
        int total_enemy = 0;
        for (var i = 0; i < 24; i++)
        {
            for (var j = 0; j < 24; j++)
            {
                if (Globals.cubeGrid[i, j].playerType == PlayerType.ENEMY)
                {
                    total_enemy++;
                }
            }
        }
        if(total_enemy == 0)
        {
            Staging.SetActive(false);
            Game_UI.SetActive(false);
            Level_Complete_UI.SetActive(true);
        }

        DoNotEnter.SetActive(false);
    }
}
