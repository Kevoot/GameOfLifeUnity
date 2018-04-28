using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenSeedMenu : MonoBehaviour {
    public Button yourButton;
    public GameObject Seed_Menu_UI;
    public GameObject Staging;
    public GameObject Game_UI;
    public GameObject StagingCamera;
    public GameObject SeedCamera;
    public GameObject FinishTurnButton;
    public GameObject FinishPastingButton;
    public GameObject DoNotEnter;

    // Use this for initialization
    void Start () {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Globals.paintAllowed = false;
        Globals.panningAllowed = true;
        DoNotEnter.SetActive(false);
        Seed_Menu_UI.SetActive(true);
        Staging.SetActive(false);
        Game_UI.SetActive(false);
        StagingCamera.SetActive(false);
        SeedCamera.SetActive(true);
        FinishTurnButton.SetActive(false);
        FinishPastingButton.SetActive(true);
        DoNotEnter.SetActive(true);
    }
}
