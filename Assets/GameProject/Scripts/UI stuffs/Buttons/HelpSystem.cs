using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpSystem : MonoBehaviour
{
    [Header("System references : ")]
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject HelpPopUp;
    [SerializeField] Animator HelpAnimator;

    [Space(10)]
    [Header("ButtonReferences : ")]

    [SerializeField] GameObject HelpButton;
    [SerializeField] GameObject CloseButton;

    private void Awake()
    {
        HelpPopUp.SetActive(false);
        CloseButton.SetActive(false);
    }
    public void Appear()
    {
        playerController.enabled = false;
        
        HelpPopUp.SetActive(true);

        HelpButton.SetActive(false);
        CloseButton.SetActive(true);
    }

    public void Remove()
    {
        StartCoroutine(MoveDown());
    }

    private IEnumerator MoveDown()
    {
        
        HelpAnimator.SetTrigger("MoveDown");
        CloseButton.SetActive(false);

        yield return new WaitForSeconds(1.5f);

        Time.timeScale = 1.0f;
        HelpPopUp.SetActive(false);
        HelpButton.SetActive(true);
        playerController.enabled = true;
    }
}
