using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class seed4_action : MonoBehaviour {
	public Button yourButton;
	public accounting acc;
	public int cost = 5;

	// Use this for initialization
	void Start () {
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
		GameObject.Find("seed4").GetComponentInChildren<Text>().text = cost + " seeds";
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
	}
}
