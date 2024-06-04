using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionFunctions : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private int destinationScene;
    [SerializeField] private AudioSource buttonSound;
    [SerializeField] private bool Reload;
    

    public void LoadNextScene()
    {
        buttonSound.Play();
        StartCoroutine(Toggler());
    }

    private IEnumerator Toggler()
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(1.2f);

        if (!Reload)
        {
            SceneManager.LoadScene(destinationScene);
        }

        else if (Reload)
        {
            int temp = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(temp);
        }

    }
}
