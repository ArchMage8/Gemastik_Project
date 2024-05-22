using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public Image[] dialogueImages;
    public float spamDelay = 1f;
    public bool sceneTrigger = false;
    public int targetScene;

    private int currentImageIndex = 0;
    private bool canClick = true;

    private void Awake()
    {
        foreach (Image img in dialogueImages)
        {
            img.gameObject.SetActive(false);
        }

        if (dialogueImages.Length > 0)
        {
            dialogueImages[0].gameObject.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canClick)
        {
            StartCoroutine(HandleClick());

        }
    }

    private IEnumerator HandleClick()
    {
        canClick = false;
        
        if(currentImageIndex == 0)
        {
            yield return new WaitForSecondsRealtime(spamDelay);
        }

        if (currentImageIndex < dialogueImages.Length)
        {
            dialogueImages[currentImageIndex].gameObject.SetActive(false);
        }

        currentImageIndex++;

        if (currentImageIndex < dialogueImages.Length)
        {
            dialogueImages[currentImageIndex].gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;

            foreach (Image img in dialogueImages)
            {
                img.gameObject.SetActive(false);
            }

            if (sceneTrigger)
            {
                SceneManager.LoadScene(targetScene);
            }
        }

        yield return new WaitForSecondsRealtime(spamDelay);
        canClick = true;
    }
}
