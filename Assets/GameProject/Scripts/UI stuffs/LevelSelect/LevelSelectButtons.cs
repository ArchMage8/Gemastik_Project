using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectButtons : MonoBehaviour
{
    [SerializeField] private GameObject LevelPreview;
    [SerializeField] private AudioSource buttonSound;
    private Animator animator;

    private void Start()
    {
        //LevelPreview.SetActive(false);
       
    }

    public void TurnOnLevelPreview()
    {
        buttonSound.Play();
        LevelPreview.SetActive(true);
    }

     public void TurnOffLevelPreview()
    {
        buttonSound.Play();
        StartCoroutine(DisablePreview());

    }

    public void QuitTheGame()
    {
	Application.Quit();
    }

    private IEnumerator DisablePreview()
    {
        animator = LevelPreview.GetComponent<Animator>();

        animator.SetTrigger("MoveDown");
        yield return new WaitForSeconds(1f);
        LevelPreview.SetActive(false);
    }
}
