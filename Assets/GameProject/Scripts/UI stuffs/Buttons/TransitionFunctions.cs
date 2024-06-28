using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking.Types;
using UnityEngine.SceneManagement;

public class TransitionFunctions : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public int destinationScene;
    [SerializeField] private AudioSource buttonSound;
    [SerializeField] private AudioSource BGMusic;
    [SerializeField] private bool Reload;
    

    public void LoadNextScene()
    {
        buttonSound.Play();
        StartCoroutine(Toggler());
    }

    private IEnumerator Toggler()
    {
        //StartCoroutine(FadeOutMusic());
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(1.2f);

        if (!Reload)
        {
            EventSystem.current.SetSelectedGameObject(null);
            SceneManager.LoadScene(destinationScene);

        }

        else if (Reload)
        {
            EventSystem.current.SetSelectedGameObject(null);
            int temp = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(temp);
        }

    }

    private IEnumerator FadeOutMusic()
    {
        float startVolume = BGMusic.volume;

        for (float t = 0; t < 1.2f; t += Time.deltaTime)
        {
            BGMusic.volume = Mathf.Lerp(startVolume, 0, t / 1.2f);
            yield return null;
        }

        BGMusic.volume = 0;
    }
}

