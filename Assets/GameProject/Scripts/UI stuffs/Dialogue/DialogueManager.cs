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
    [Header("Boxes:")]
    public TextMeshProUGUI TextHolder;
    public string[] Dialogues;

    [Space(20)]
    public Speaker[] Speakers;

    [Space(10)]
    [Header("Boxes:")]
    public Image DogBox;
    public Image ManBox;
    public GameObject ScreenFilter;

    [Space(20)]
    [Header("Extras:")]
    public float delay = 2f; // Time delay before allowing the next dialogue index
    public PlayerController playerController;
    public GameObject NavButtons;
    public GameObject HelpButtons;
    [SerializeField] private AudioSource buttonPress;


    [HideInInspector]public int indexValue = 0;
    private bool canProgress = false; // Flag to control the ability to move to the next dialogue index
    private bool canSound = true;

    [SerializeField] private float writeSpeed = 0.1f;

    [HideInInspector]public int refLength;

    private bool dialogueDone = false;

    void Start()
    {
        indexValue = 0;

        StartCoroutine(InitializeDialogues());
        UpdateCharacterPictures();
        NavButtons.SetActive(false);
        HelpButtons.SetActive(false);
        canSound = true;

        refLength = CharacterPictures.Length;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canProgress && dialogueDone && indexValue != refLength)
        {
            if (canSound)
            {
                buttonPress.Play();
            }
            StartCoroutine(NextDialogue());
        }
    }

    IEnumerator NextDialogue()
    {
        canProgress = false; // Disable progression while coroutine is active
       
        indexValue++;

        if (indexValue < Dialogues.Length)
        {
            UpdateDialogues();
        }
        else
        {
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


        if (Dialogues.Length > 0)
        {
            NextSentence();
        }
            

        if (CharacterPictures.Length > 0)
            CharacterPictures[0].Picture.gameObject.SetActive(true);

        StartCoroutine(stopTime());

        yield return new WaitForSecondsRealtime(delay); // Use WaitForSecondsRealtime for unscaled delay
        canProgress = true; // Re-enable progression after delay
    }

    void UpdateDialogues()
    {
        NextSentence();
        UpdateCharacterPictures();
    }

    public void EndDialogues()
    {
        indexValue = refLength;

        Time.timeScale = 1f;
        canSound = false;
        playerController.enabled = true;

        foreach (CharacterPicture characterPicture in CharacterPictures)
        {
            characterPicture.Picture.gameObject.SetActive(false);
        }


        DogBox.enabled = false;
        ManBox.enabled = false;
        ScreenFilter.SetActive(false);
        NavButtons.SetActive(true);
        HelpButtons.SetActive(true);
        TextHolder.enabled = false;
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

    private IEnumerator stopTime()
    {
        playerController.enabled = false;
        yield return new WaitForSeconds(1f);
        //Time.timeScale = 0f;
    }

    private IEnumerator WriteSentences()
    {
        dialogueDone = false;
            foreach (char Character in Dialogues[indexValue].ToCharArray())
            {
            TextHolder.text += Character;
            yield return new WaitForSeconds(writeSpeed);
            }
        dialogueDone = true;
    }

    private void NextSentence()
    {
        if(indexValue <= Dialogues.Length - 1)
        {
            TextHolder.text = "";
            StartCoroutine(WriteSentences());
        }
        
    }
}
