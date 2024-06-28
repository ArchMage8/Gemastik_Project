using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsEnable : MonoBehaviour
{
    [SerializeField] private GameObject CreditsHUD;
    [SerializeField] private GameObject ButtonsHolder;

    private void Awake()
    {
        CreditsHUD.SetActive(false);
    }
    public void TurnOn()
    {
        ButtonsHolder.SetActive(false);
        CreditsHUD.SetActive(true);
    }

    public void TurnOff()
    {
        ButtonsHolder.SetActive(true);
        CreditsHUD.SetActive(false);
    }
}
