using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinHandler : MonoBehaviour
{
    //What happens when we win the wordle

    [Space(10)]
    [Header("Animator: ")]
    public Animator Bus;
    public Animator LeftDoor;
    public Animator RightDoor;
    public Animator Peoples;

    [Header("Level Loader: ")]
    public Animator LevelTrigger;
    public int destinationScene;
    [SerializeField] private AudioSource BGMusic;

    //Sequence:
    // Bus Arrives          -> Bus
    // Open Bus Doors       -> Bus
    // Open Station Doors   -> Doors
    // Character walk cycle -> People

    public void Complete()
    {
        BusAnims();
    }

    private void BusAnims()
    {
        
        Bus.SetTrigger("BusEntry");
        
    }

    public void OpenStationDoors()
    {
       
        LeftDoor.SetTrigger("OpenDoor");
        RightDoor.SetTrigger("OpenDoor");
    }

    public void MovePeoples()
    {
        StartCoroutine(FadeOutMusic());
        Peoples.SetTrigger("MovePeople");
    }

    public void LoadNextScene()
    {
        StartCoroutine(Toggler());
    }

    private IEnumerator Toggler()
    {
        
        LevelTrigger.SetTrigger("Start");
        yield return new WaitForSeconds(1.2f);

        
            SceneManager.LoadScene(destinationScene);
        

       

    }

    private IEnumerator FadeOutMusic()
    {
        float startVolume = BGMusic.volume;

        yield return new WaitForSeconds(1.9f);

        for (float t = 0; t < 1.2f; t += Time.deltaTime)
        {
            BGMusic.volume = Mathf.Lerp(startVolume, 0, t / 1f);
            yield return null;
        }

        BGMusic.volume = 0;
    }

}
