using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class seed1_action : MonoBehaviour {
	public Button yourButton;
	public accounting acc;
    public GameObject gameCamera;
    public GameObject OverheadCamPosition;
    public GameObject Seed_Menu_UI;
    public GameObject Staging;
    public GameObject Game_UI;
    public GameObject seedCamera;
    public GameObject FinishPastingButton;
    public GameObject FinishTurnButton;
    public GameObject PaintButton;
    public Seed seed;
    public bool[] seedArray;
	public int cost = 5;

	// Use this for initialization
	void Start () {
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
		GameObject.Find("seed1").GetComponentInChildren<Text>().text = cost + " seeds";
        // seed = Globals.factory.getSeed(SeedType.DIAMOND);
	}

	void Update () {
		Button btn = yourButton.GetComponent<Button>();
		if ((acc.balance - cost) < 0 && btn.IsInteractable () == true) {
			btn.interactable = false;
		} 
		else if (btn.IsInteractable () == false && (acc.balance - cost) >= 0) {
			btn.interactable = true;
		}
    }
	
	void TaskOnClick()
	{
		// if not enough balance, don't do anything.
		if ((acc.balance - cost) < 0) {
			return;
		}
		acc.update_balance (0 - cost);
        // Move camera to overhead view
        gameCamera.transform.position = OverheadCamPosition.transform.position;
        gameCamera.transform.eulerAngles = OverheadCamPosition.transform.eulerAngles;
        Seed_Menu_UI.SetActive(false);
        Staging.SetActive(true);
        Game_UI.SetActive(true);
        gameCamera.SetActive(true);
        seedCamera.SetActive(false);
        FinishPastingButton.SetActive(true);
        FinishTurnButton.SetActive(false);
        Globals.whichSeed = SeedType.DIAMOND;
        Globals.initPasteGrid = true;

        // Now that the UI is set up, begin rendering the current Seed on top of the game board for replacement

        // This must be set to allow camera to stay in overhead mode
        Globals.pasting = true;
    }
}
