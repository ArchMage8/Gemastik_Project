using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSkip : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private AudioSource buttonSound;

    private void Update()
    {
        checkComplete();
    }

    public void SkipDialogue()
    {
        buttonSound.Play();
        StartCoroutine(disableSelf());
    }

    private IEnumerator disableSelf()
    {
        
        dialogueManager.EndDialogues();
        yield return new WaitForSeconds(0.001f);
        this.gameObject.SetActive(false);
    }

    private void checkComplete()
    {
        if (dialogueManager.indexValue == dialogueManager.refLength)
        {
            StartCoroutine(disableSelf());
        }
    }
}
