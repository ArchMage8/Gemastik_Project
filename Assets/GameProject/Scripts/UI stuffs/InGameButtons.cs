using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameButtons : MonoBehaviour
{

    [SerializeField] private PlayerController playerController;
    [SerializeField] private SimulationManager simulationManager;
    [SerializeField] private int HomeSceneIndex;
    [SerializeField] private Sprite ActivatedSprite;

    public void RestartThisScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartSim()
    {
        playerController.enabled = false;
        simulationManager.isSimulating = true;

        GetComponent<SpriteRenderer>().sprite = ActivatedSprite;
    }

    public void GoToHomeScreen()
    {
        SceneManager.LoadScene(HomeSceneIndex);
    }
}
