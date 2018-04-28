using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotate_Button : MonoBehaviour {
    public Button yourButton;
    public GameObject cam;
    public GameObject plane;
    public static int state = 0; // 0 = front, 1 = left, 2 = back, 3 = right

    public GameObject BottonCamPosObject;
    public GameObject RightCamPosObject;
    public GameObject TopCamPosObject;
    public GameObject LeftCamPosObject;
    public GameObject OverheadCamPosObject;

    public static Vector3 startLocation;
    public static Vector3 endLocation;

    public static Vector3 startRotation;
    public static Vector3 endRotation;


    // Use this for initialization
    void Start () {
        startLocation = cam.transform.position;
        endLocation = cam.transform.position;
        startRotation = cam.transform.eulerAngles;
        endRotation = cam.transform.eulerAngles;
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void Update()
    {
        if(Globals.pasting)
        {
            return;
        }
        if(!CheckCameraDistance() && !CheckCameraRotation() /*&& !Globals.panningAllowed*/ && !Globals.paintAllowed && !Globals.pasting)
        {
            Debug.Log("Currently changing position/rotation");
            cam.transform.position = Vector3.Lerp(cam.transform.position, endLocation, Time.deltaTime * 3f);
            // cam.transform.eulerAngles = Vector3.Lerp(cam.transform.eulerAngles, endRotation, Time.deltaTime * 3f);
            // cam.transform.eulerAngles = Vector3.LerpUnclamped(cam.transform.eulerAngles, endRotation, Time.deltaTime * 3f);
            var x = Mathf.LerpAngle(cam.transform.eulerAngles.x, endRotation.x, Time.deltaTime * 9f);
            var y = Mathf.LerpAngle(cam.transform.eulerAngles.y, endRotation.y, Time.deltaTime * 9f);
            var z = Mathf.LerpAngle(cam.transform.eulerAngles.z, endRotation.z, Time.deltaTime * 9f);
            cam.transform.eulerAngles = Vector3.Lerp(cam.transform.eulerAngles, new Vector3(x, y, z), Time.deltaTime * 18f);
        }
        else
        {
            if(!Globals.panningAllowed) Globals.panningAllowed = true;
        }
    }

    bool CheckCameraDistance()
    {
        // Floating point math is funny sometimes. Just make sure the camera is in the correct range.
        if(cam.transform.position.x > endLocation.x - .1 && cam.transform.position.x < endLocation.x + .1)
        {
            if(cam.transform.position.y > endLocation.y - .1 && cam.transform.position.y < endLocation.y + .1)
            {
                if(cam.transform.position.z > endLocation.z - .1 && cam.transform.position.z < endLocation.z + .1)
                {
                    // Only return true if the camera is within the general bounds of the position
                    // with a small degree of error
                    return true;
                }
            }
        }
        return false;
    }

    bool CheckCameraRotation()
    {
        // Floating point math is funny sometimes. Just make sure the camera is in the correct range.
        if (cam.transform.rotation.eulerAngles.x > endRotation.x - .1 && cam.transform.rotation.eulerAngles.x < endRotation.x + .1)
        {
            if (cam.transform.rotation.eulerAngles.y > endRotation.y - .1 && cam.transform.rotation.eulerAngles.y < endRotation.y + .1)
            {
                if (cam.transform.rotation.eulerAngles.z > endLocation.z - .1 && cam.transform.rotation.eulerAngles.z < endRotation.z + .1)
                {
                    if (cam.transform.rotation.eulerAngles.z > endLocation.z - .1 && cam.transform.rotation.eulerAngles.z < endRotation.z + .1)
                    {
                        // Only return true if the camera is within the general bounds of the rotation
                        // with a small degree of error
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public void TaskOnClick()
    {
        Debug.Log("Paint Button Clicked");
        Globals.panningAllowed = false;
        state = (state + 1) % 4;
        switch(state)
        {
            // Bottom
            case 0:
                startLocation = cam.transform.position;
                endLocation = BottonCamPosObject.transform.position;
                startRotation = cam.transform.eulerAngles;
                endRotation = new Vector3(BottonCamPosObject.transform.eulerAngles.x, BottonCamPosObject.transform.eulerAngles.y, BottonCamPosObject.transform.eulerAngles.z);
                break;
            // Left
            case 1:
                startLocation = cam.transform.position;
                endLocation = LeftCamPosObject.transform.position;
                startRotation = cam.transform.eulerAngles;
                endRotation = new Vector3(LeftCamPosObject.transform.eulerAngles.x, LeftCamPosObject.transform.eulerAngles.y, LeftCamPosObject.transform.eulerAngles.z);
                break;
            // Top
            case 2:
                startLocation = cam.transform.position;
                endLocation = TopCamPosObject.transform.position;
                startRotation = cam.transform.eulerAngles;
                endRotation = new Vector3(TopCamPosObject.transform.eulerAngles.x, TopCamPosObject.transform.eulerAngles.y, TopCamPosObject.transform.eulerAngles.z);
                break;
            // Right
            case 3:
                startLocation = cam.transform.position;
                endLocation = RightCamPosObject.transform.position;
                startRotation = cam.transform.eulerAngles;
                endRotation = new Vector3(RightCamPosObject.transform.eulerAngles.x, RightCamPosObject.transform.eulerAngles.y, RightCamPosObject.transform.eulerAngles.z);
                break;
        }
    }
}
