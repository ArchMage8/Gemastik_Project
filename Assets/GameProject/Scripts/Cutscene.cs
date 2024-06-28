using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    public int DesinationScene;
    public int SkipDestination;
    public Animator animator;

    private void Start()
    {
        StartCoroutine(loadNextScene());
    }

    private IEnumerator loadNextScene()
    {
        yield return new WaitForSeconds (3f);
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(DesinationScene);
    }

    public void Skip()
    {
        StartCoroutine(GoingSkip());
    }

    private IEnumerator GoingSkip()
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SkipDestination);
    }
}
