using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    public Button yourButton;
    public GameObject TitleUI;
    public GameObject LevelCompleteUI;

    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    public void TaskOnClick()
    {
        LevelCompleteUI.SetActive(false);
        TitleUI.SetActive(true);
    }
}
