using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintButton : MonoBehaviour {
    public Button yourButton;
	public accounting acc;
    public GameObject DoNotEnter;
	public int cost = 1;

    // Use this for initialization
    void Start () {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

	void Update () {
		Button btn = yourButton.GetComponent<Button>();
		if ((acc.balance - cost) < 0 && btn.IsInteractable () == true) {
			btn.interactable = false;
			Globals.paintAllowed = false;
			Globals.panningAllowed = true;
			GameObject.Find("Paint_Button").GetComponentInChildren<Text>().text = "Paint";
		} 
		else if (btn.IsInteractable () == false && (acc.balance - cost) >= 0) {
			btn.interactable = true;
		}
	}
	
    void TaskOnClick()
    {
        if (!Globals.paintAllowed)
        {
            Globals.paintAllowed = true;
            Globals.panningAllowed = false;
            DoNotEnter.SetActive(true);
            GameObject.Find("Paint_Button").GetComponentInChildren<Text>().text = "Finish Painting";
        }
        else
        {
            Globals.paintAllowed = false;
            Globals.panningAllowed = true;
            DoNotEnter.SetActive(false);
            GameObject.Find("Paint_Button").GetComponentInChildren<Text>().text = "Paint";
        }
    }
}
