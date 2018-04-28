using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTest : MonoBehaviour {

    

	// Use this for initialization
	void Start () {
       
	}

    Vector3 touchPosWorld;

    //Change me to change the touch phase used.
    TouchPhase touchPhase = TouchPhase.Ended;

    void Update()
    {
        if (Globals.paintAllowed)
        {
            if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
            {
                // focusObj = null;
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.transform.gameObject.name.Contains("CubePrefab"))
                {
                    foreach(var cube in Globals.cubeGrid)
                    {
                        if(hit.transform == cube.prefab)
                        {
                            if(hit.transform.gameObject.GetComponent<Renderer>().material.GetColor("_Color") != Color.black)
                            {
                                cube.SetPlayerType(PlayerType.PLAYER);
                            }
                        }
                    }
                }
            }
        }

        
    }
}
