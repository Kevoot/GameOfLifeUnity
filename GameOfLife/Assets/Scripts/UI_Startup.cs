using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Startup : MonoBehaviour {
    public GameObject Level_Select_UI;
    public GameObject Game_UI;
    public GameObject Staging;
    public GameObject SeedMenu;
    public GameObject Level_Complete_UI;

	// Use this for initialization
	void Start ()
    {
        Level_Select_UI.SetActive(false);
        Game_UI.SetActive(false);
        Staging.SetActive(false);
        SeedMenu.SetActive(false);
        Level_Complete_UI.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

    }
}
