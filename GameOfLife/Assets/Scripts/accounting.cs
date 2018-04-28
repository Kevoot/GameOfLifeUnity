using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class accounting : MonoBehaviour {
	public int balance = 0;

	// Use this for initialization
	void Start () {
		update_balance (5);
	}

	void Update() {
        if(UnitCounter.resetMoney)
        {
            balance = 5;
            UnitCounter.resetMoney = false;
        }
		GameObject.Find("curr_balance").GetComponentInChildren<Text>().text = "Balance: " + balance;
	}

	// Updates balance with given amount
	public void update_balance (int amount) {
		balance += amount;
	}

	public int get_balance () {
		string curr;
		int total;
		curr = GameObject.Find ("curr_balance").GetComponentInChildren<Text>().text;
		curr = curr.Replace ("Balance: ", "");
		//	Console.WriteLine ("this is curr:" + curr);
		total = int.Parse(curr);
		return total;
	}
}
