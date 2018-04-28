using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour {
    public GameObject WinLabel;
    public GameObject LoseLabel;
    public GameObject DrawLabel;


    // Use this for initialization
    void Start()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Level Complete, Players: " + UnitCounter.Players + " Enemies: " + UnitCounter.Enemies);
        WinLabel.SetActive(false);
        LoseLabel.SetActive(false);
        DrawLabel.SetActive(false);


        // Enemy wins
        if (UnitCounter.Enemies > 0)
        {
            LoseLabel.SetActive(true);
        }
        // Player wins
        // else if (UnitCounter.Enemies < UnitCounter.Players)
        // {
        //     WinLabel.SetActive(true);
        // }
        // Win
        else
        {
            WinLabel.SetActive(true);
        }
    }
}
