using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class InGameButtons : MonoBehaviour
{

    [SerializeField] private PlayerController playerController;
    [SerializeField] private SimulationManager simulationManager;
    [SerializeField] private int HomeSceneIndex;
    [SerializeField] private Sprite ActivatedSprite;
    [SerializeField] private AudioSource buttonSound;

    private Image buttonImage;

    private void Start()
    {
        buttonImage = GetComponent<Image>();
    }

    public void StartSim()
    {
        buttonSound.Play();
        playerController.enabled = false;
        simulationManager.isSimulating = true;

        buttonImage.sprite = ActivatedSprite;
        EventSystem.current.SetSelectedGameObject(null);
    }
}
