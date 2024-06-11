using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneSystem : MonoBehaviour
{
    [SerializeField] private Animator LevelLoader;
    [SerializeField] private TMP_Text indicator;
    [SerializeField] private float timeDelay;
    [SerializeField] private bool auto;
    [SerializeField] private int NextScene;

    private bool canMoveNext = false;
    private bool isSkipping = false;

    private void Start()
    {
        indicator.enabled = false;

        if (auto)
        {
            StartCoroutine(ToggleNextAuto());
        }

        else //if manual
        {
            StartCoroutine(ToggleNextManual());
        }
    }

    private IEnumerator ToggleNextManual()
    {
        yield return new WaitForSeconds(timeDelay);
        indicator.enabled = true;

        if (Input.anyKey && canMoveNext && !isSkipping)
        {
            StartCoroutine(Toggler());
        }
    }

    private IEnumerator ToggleNextAuto()
    {
        yield return new WaitForSeconds(timeDelay);
        StartCoroutine(Toggler());
    }

    private IEnumerator Toggler()
    {
        LevelLoader.SetTrigger("Start");
        yield return null;
        SceneManager.LoadScene(NextScene);
    }
}
