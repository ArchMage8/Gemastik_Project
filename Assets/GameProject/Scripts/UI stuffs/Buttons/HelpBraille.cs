using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpBraille : MonoBehaviour
{
    [Header("System references : ")]

    [SerializeField] GameObject HelpPopUp;
    [SerializeField] GameObject Board;
    [SerializeField] GameObject Card;

    private Animator CardAnimator;
    private Animator BoardAnimator;
    [SerializeField] Animator HelpAnimator;

    [Space(10)]
    [Header("ButtonReferences : ")]

    [SerializeField] GameObject HelpButton;
    [SerializeField] GameObject CloseButton;

    [Header("Braille Buttons : ")]
    [SerializeField] GameObject SubmitButton;
    [SerializeField] GameObject ResetButton;


    private void Awake()
    {
        CardAnimator = Card.GetComponent<Animator>();
        BoardAnimator = Board.GetComponent<Animator>();
        
        HelpPopUp.SetActive(false);
        CloseButton.SetActive(false);
    }
    public void Appear()
    {
        
        HelpButton.SetActive(false);
        CloseButton.SetActive(true);

        StartCoroutine(MoveUp());
    }

    public void Remove()
    {
        StartCoroutine(MoveDown());
    }

    private IEnumerator MoveUp()
    {
        CardAnimator.SetTrigger("MoveDown");
        BoardAnimator.SetTrigger("MoveDown");

        ResetButton.SetActive(false);
        SubmitButton.SetActive(false);

        yield return new WaitForSeconds(1f);

        Card.SetActive(false);
        Board.SetActive(false);

        HelpPopUp.SetActive(true);
    }

    private IEnumerator MoveDown()
    {
        
        HelpAnimator.SetTrigger("MoveDown");
        CloseButton.SetActive(false);

        yield return new WaitForSeconds(1f);

        ResetButton.SetActive(true);
        SubmitButton.SetActive(true);
        Card.SetActive(true);
        Board.SetActive(true);

        Time.timeScale = 1.0f;
        HelpPopUp.SetActive(false);
        HelpButton.SetActive(true);
      
    }
}
