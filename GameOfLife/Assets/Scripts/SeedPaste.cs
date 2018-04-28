using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class SeedPaste : MonoBehaviour
{

    Seed mySeed;

    public Transform prefab;

    // Use this for initialization
    void Start()
    {
        
    }

    Vector3 touchPosWorld;

    //Change me to change the touch phase used.
    TouchPhase touchPhase = TouchPhase.Ended;

    void Update()
    {

        if(Globals.initPasteGrid)
        {
            mySeed = Globals.factory.getSeed(Globals.whichSeed);
            Globals.initPasteGrid = false;
        }
        

        if (Globals.pasting)
        {
            if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {

                // focusObj = null;
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.gameObject.name.Contains("CubePrefab"))
                {
                    //Reset the grid if not pasting
                    for (var i = 0; i < 24; i++) 
                    {
                        for (var j = 0; j < 24; j++)
                        {
                            if (Equals(Globals.cubeGrid[i, j].GetPlayerType(), PlayerType.PLAYERNEXT))
                            {
                                Globals.cubeGrid[i, j].SetPlayerType(PlayerType.DEAD);
                            }
                        }
                    }
                    //find the cube to start pasting from
                    for (int y = 0; y < Globals.cubeGrid.GetLength(0); y++)
                    {
                        for(int x = 0; x < Globals.cubeGrid.GetLength(1); x++)
                        if (hit.transform == Globals.cubeGrid[y, x].prefab)
                        {
                            if (hit.transform.gameObject.GetComponent<Renderer>().material.GetColor("_Color") != Color.black)
                            {
                                    pasteSeed(y, x);
                            }
                        }
                    }
                }
            }
        }


    }

    void pasteSeed(int bottomY, int leftX)
    {
        if(((leftX + mySeed.Grid.GetLength(0) - 1) > Globals.cubeGrid.GetLength(0)) 
            || ((bottomY + mySeed.Grid.GetLength(1) - 1) > Globals.cubeGrid.GetLength(1))) // don't paste if out of bounds
        {
            return;
        }

        //double check not writing over existing player seeds
        for (int y = 0; y < mySeed.Grid.GetLength(0); y++)
        {
            for (int x = 0; x < mySeed.Grid.GetLength(1); x++)
            {
                if (mySeed.Grid[y, x] == 1)
                {
                    if (!Equals(Globals.cubeGrid[y + bottomY, x + leftX].GetPlayerType(), PlayerType.DEAD))
                    {
                        return;
                    }
                }
            }
        }


        //paste preview to cube grid
        for (int y = 0; y < mySeed.Grid.GetLength(0); y++)
        {
            for(int x = 0; x < mySeed.Grid.GetLength(1); x++)
            {
                if(mySeed.Grid[y, x] == 1)
                {
                    Globals.cubeGrid[y + bottomY, x + leftX].SetPlayerType(PlayerType.PLAYERNEXT);
                }
            }
        }

    }
}

