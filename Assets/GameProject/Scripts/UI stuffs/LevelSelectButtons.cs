using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectButtons : MonoBehaviour
{
    [SerializeField] private GameObject LevelPreview;

    private void Start()
    {
        //LevelPreview.SetActive(false);
    }

    public void TurnOnLevelPreview()
    {
        LevelPreview.SetActive(true);
    }

     public void TurnOffLevelPreview()
    {
        LevelPreview.SetActive(false);
    }

    public void QuitTheGame()
    {
	Application.Quit();
    }
}
