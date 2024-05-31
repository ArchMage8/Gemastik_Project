using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public enum Speaker
    {
        Dog,
        OldMan
    }

    [System.Serializable]
    public class CharacterPicture
    {
        public Image Picture;
        public Speaker Type;
    }

    [Space(20)]
    public CharacterPicture[] CharacterPictures;
    [Space(20)]
    public TMP_Text[] Dialogues;
    [Space(20)]
    public Speaker[] Speakers;

    [Space(10)]
    [Header("Boxes:")]
    public Image DogBox;
    public Image ManBox;
    public GameObject ScreenFilter;

    [Space(20)]
    [Header("TimeDelay:")]
    public float delay = 2f; // Time delay before allowing the next dialogue index

    private int indexValue = 0;
    private bool canProgress = false; // Flag to control the ability to move to the next dialogue index

    void Start()
    {
        StartCoroutine(InitializeDialogues());
        UpdateCharacterPictures();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canProgress)
        {
            StartCoroutine(NextDialogueWithDelay());
        }
    }

    IEnumerator NextDialogueWithDelay()
    {
        canProgress = false; // Disable progression while coroutine is active
       
        indexValue++;

        if (indexValue < Dialogues.Length)
        {
            UpdateDialogues();
        }
        else
        {
            Debug.Log("End of dialogues reached.");
            EndDialogues();
        }

        yield return new WaitForSecondsRealtime(delay); // Use WaitForSecondsRealtime for unscaled delay
        canProgress = true; // Re-enable progression after delay
    }

    IEnumerator InitializeDialogues()
    {
        foreach (CharacterPicture characterPicture in CharacterPictures)
        {
            characterPicture.Picture.gameObject.SetActive(false);
        }

        foreach (TMP_Text dialogue in Dialogues)
        {
            dialogue.gameObject.SetActive(false);
        }

        if (Dialogues.Length > 0)
            Dialogues[0].gameObject.SetActive(true);

        if (CharacterPictures.Length > 0)
            CharacterPictures[0].Picture.gameObject.SetActive(true);

        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(delay); // Use WaitForSecondsRealtime for unscaled delay
        canProgress = true; // Re-enable progression after delay
    }

    void UpdateDialogues()
    {
        Dialogues[indexValue - 1].gameObject.SetActive(false);
        Dialogues[indexValue].gameObject.SetActive(true);
        UpdateCharacterPictures();
    }

    void EndDialogues()
    {
        Time.timeScale = 1f;

        foreach (CharacterPicture characterPicture in CharacterPictures)
        {
            characterPicture.Picture.gameObject.SetActive(false);
        }

        foreach (TMP_Text dialogue in Dialogues)
        {
            dialogue.gameObject.SetActive(false);
        }

        DogBox.enabled = false;
        ManBox.enabled = false;
        ScreenFilter.SetActive(false);
    }

    void UpdateCharacterPictures()
    {
        if (indexValue >= CharacterPictures.Length) return;

        CharacterPicture currentCharacterPicture = CharacterPictures[indexValue];

        if (indexValue == 0)
        {
            currentCharacterPicture.Picture.gameObject.SetActive(true);
            return;
        }

        CharacterPicture previousCharacterPicture = CharacterPictures[indexValue - 1];

        foreach (CharacterPicture characterPicture in CharacterPictures)
        {
            if (characterPicture.Type == currentCharacterPicture.Type)
            {
                if (characterPicture != currentCharacterPicture)
                {
                    characterPicture.Picture.gameObject.SetActive(false);
                }
                else
                {
                    characterPicture.Picture.gameObject.SetActive(true);
                    characterPicture.Picture.color = Color.white;
                }
            }
            else
            {
                characterPicture.Picture.color = new Color(75f / 255f, 75f / 255f, 75f / 255f);
            }
        }

        if (currentCharacterPicture.Type == Speaker.Dog)
        {
            DogBox.gameObject.SetActive(true);
            ManBox.gameObject.SetActive(false);
        }
        else if (currentCharacterPicture.Type == Speaker.OldMan)
        {
            ManBox.gameObject.SetActive(true);
            DogBox.gameObject.SetActive(false);
        }
    }
}
