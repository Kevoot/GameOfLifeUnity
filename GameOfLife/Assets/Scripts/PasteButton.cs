using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasteButton : MonoBehaviour {
    public Button yourButton;
    public GameObject FinishTurnButton;
    public GameObject FinishPastingButton;
    public GameObject BottomCamPosObject;
    public GameObject StagingCamera;
    public GameObject DoNotEnter;

    // Use this for initialization
    void Start () {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void TaskOnClick()
    {
        // Do the actual pasting here

        // Release to the flag on pasting, camera should rotate back to current state
        Globals.pasting = false;
        FinishTurnButton.SetActive(true);
        FinishPastingButton.SetActive(false);
        StagingCamera.transform.position = BottomCamPosObject.transform.position;
        StagingCamera.transform.eulerAngles = BottomCamPosObject.transform.eulerAngles;
        DoNotEnter.SetActive(false);
        Globals.paintAllowed = false;
        Globals.panningAllowed = true;
        var paintButton = GameObject.Find("Paint_Button");
        paintButton.GetComponentInChildren<Text>().text = "Paint";
        paintButton.GetComponent<Button>().interactable = true;

        for (var i = 0; i < 24; i++) //convert preview to permanent
        {
            for (var j = 0; j < 24; j++)
            {
                
                if(Equals(Globals.cubeGrid[i, j].GetPlayerType(), PlayerType.PLAYERNEXT))
                {
                    Globals.cubeGrid[i, j].SetPlayerType(PlayerType.PLAYER);
                }
            }
        }

    }
}
