using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnCounter : MonoBehaviour {
    public static int turnsLeft;
    public GameObject Staging;
    public GameObject Game_UI;
    public GameObject Level_Complete_UI;
    public GameObject Turns_Left_Label;
    public Transform CubePrefab;

    // Use this for initialization
    void Start () {
        Turns_Left_Label.GetComponentInChildren<Text>().text = "Turns Left: " + TurnCounter.turnsLeft.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        Turns_Left_Label.GetComponentInChildren<Text>().text = "Turns Left: " + TurnCounter.turnsLeft.ToString();
        if (TurnCounter.turnsLeft == 0)
        {
            Debug.Log("No more turns left!");
            Staging.SetActive(false);
            Game_UI.SetActive(false);

            for (var i = 0; i < 24; i++)
            {
                for (var j = 0; j < 24; j++)
                {
                    if(Globals.cubeGrid[i, j].playerType == PlayerType.PLAYER)
                    {
                        UnitCounter.Players++;
                    }
                    else if(Globals.cubeGrid[i, j].playerType == PlayerType.ENEMY)
                    {
                        UnitCounter.Enemies++;
                    }
                }
            }

            Debug.Log("Turn Counter, Players: " + UnitCounter.Players + " Enemies: " + UnitCounter.Enemies);

            for (var i = 0; i < 24; i++)
            {
                for (var j = 0; j < 24; j++)
                {
                    Object.Destroy(Globals.cubeGrid[i, j].prefab.gameObject);
                }
            }
            Globals.cubeGrid = null;
            Globals.completeTurnRequested = false;
            Globals.paintAllowed = false;
            Globals.panningAllowed = false;
            Globals.pasting = false;

            Globals.initPasteGrid = false;

            Debug.Log("Resetting Grid");
            Globals.cubeGrid = new InitialCube[24, 24];
            for (var i = 0; i < 24; i++)
            {
                for (var j = 0; j < 24; j++)
                {
                    Globals.cubeGrid[i, j] = new InitialCube(CubePrefab);
                    Globals.cubeGrid[i, j].prefab = Instantiate(CubePrefab, new Vector3(i * 1.5f, 0.5f, j * 1.5f), Quaternion.identity);
                    if ((j == 0 || j == 23) || (i == 0 || i == 23))
                    {
                        Globals.cubeGrid[i, j].SetPlayerType(PlayerType.WALL);
                    }
                    else
                        Globals.cubeGrid[i, j].SetPlayerType(PlayerType.DEAD);
                }
            }

            Globals.cubeGrid[5, 20].SetPlayerType(PlayerType.ENEMY);
            Globals.cubeGrid[4, 19].SetPlayerType(PlayerType.ENEMY);
            Globals.cubeGrid[6, 19].SetPlayerType(PlayerType.ENEMY);
            Globals.cubeGrid[5, 18].SetPlayerType(PlayerType.ENEMY);

            Globals.cubeGrid[12, 20].SetPlayerType(PlayerType.ENEMY);
            Globals.cubeGrid[11, 19].SetPlayerType(PlayerType.ENEMY);
            Globals.cubeGrid[13, 19].SetPlayerType(PlayerType.ENEMY);
            Globals.cubeGrid[12, 18].SetPlayerType(PlayerType.ENEMY);

            Globals.cubeGrid[19, 20].SetPlayerType(PlayerType.ENEMY);
            Globals.cubeGrid[18, 19].SetPlayerType(PlayerType.ENEMY);
            Globals.cubeGrid[20, 19].SetPlayerType(PlayerType.ENEMY);
            Globals.cubeGrid[19, 18].SetPlayerType(PlayerType.ENEMY);

            Level_Complete_UI.SetActive(true);
        }
	}
}
