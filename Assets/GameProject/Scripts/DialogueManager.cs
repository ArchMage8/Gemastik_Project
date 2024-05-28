using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DialogueManager : MonoBehaviour
{
   public enum Speaker
    {
        OldMan,
        Dog
    }

    public Speaker[] speaker;
    [Space(15)]
    public TMP_Text[] dialogues;
    [Space(15)]
    public Image[] speakerPictures;
    [Space(15)]

    [Space(15)]
    [Header("Dialogue Boxes")]
    public GameObject TopBox;
    public GameObject BottomBox;


    private GameObject topBoxShader;
    private GameObject bottomBoxShader;

    private int indexValue = 0;
    private float delayTime = 0f;
    private bool canAdvance = true;

    public void Start()
    {
        topBoxShader = TopBox.transform.GetChild(0).gameObject;
        bottomBoxShader = BottomBox.transform.GetChild(0).gameObject;

        Time.timeScale = 0f;

        foreach (var dialogue in dialogues)
        {
            dialogue.gameObject.SetActive(false);
        }

        foreach (var speakerPicture in speakerPictures)
        {
            speakerPicture.gameObject.SetActive(false);
        }

        EnableObjectsAtIndex(indexValue);

    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAdvance)
        {
            indexValue++;
            StartCoroutine(AdvanceDialogueWithDelay(delayTime));
        }
    }

    void EnableObjectsAtIndex(int index)
    {
        if (index < dialogues.Length && index < speakerPictures.Length)
        {
            dialogues[index].gameObject.SetActive(true);
            speakerPictures[index].gameObject.SetActive(true);

            if (speaker[index] == Speaker.OldMan)
            {
                bottomBoxShader.SetActive(true);
                topBoxShader.SetActive(false);
            }
            else if (speaker[index] == Speaker.Dog)
            {
                bottomBoxShader.SetActive(false);
                topBoxShader.SetActive(true);
            }
            else
            {
                bottomBoxShader.SetActive(false);
                topBoxShader.SetActive(false);
            }
        }
        else
        {
            EndDialogue();
        }
    }

    private void DisableHandler()
    {
        //Check for the next object's enum
        //Disable the previous game object in the array with the same enum
    }

    IEnumerator AdvanceDialogueWithDelay(float delay)
    {

        canAdvance = false;
        yield return new WaitForSeconds(delay);
        EnableObjectsAtIndex(indexValue);
        canAdvance = true;
    }

    void EndDialogue()
    {
        Time.timeScale = 1.0f;
        foreach (var dialogue in dialogues)
        {
            dialogue.gameObject.SetActive(false);
        }

        foreach (var speakerPicture in speakerPictures)
        {
            speakerPicture.gameObject.SetActive(false);
        }
        TopBox.SetActive(false);
        BottomBox.SetActive(false);
    }
}
