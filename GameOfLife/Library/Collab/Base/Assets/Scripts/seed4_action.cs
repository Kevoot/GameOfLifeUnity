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

	void TaskOnClick()
	{
		acc.update_balance (0 - cost);
	}
}
