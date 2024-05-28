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

    public CharacterPicture[] CharacterPictures;

    public TMP_Text[] Dialogues;

    public Speaker[] Speakers;

    public Image DogBox;
    public Image ManBox;

    private int indexValue = 0;

    void Start()
    {
        Time.timeScale = 0f;
        
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

        UpdateCharacterPictures();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            indexValue++;

            if (indexValue < Dialogues.Length)
            {
                Dialogues[indexValue - 1].gameObject.SetActive(false);
                Dialogues[indexValue].gameObject.SetActive(true);

                UpdateCharacterPictures();
            }
            else
            {
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

                Time.timeScale = 1f;
            }
        }
    }

    void UpdateCharacterPictures()
    {
        if (indexValue < CharacterPictures.Length)
        {
            CharacterPicture currentCharacterPicture = CharacterPictures[indexValue];

            foreach (CharacterPicture characterPicture in CharacterPictures)
            {
                characterPicture.Picture.gameObject.SetActive(true);

                if (characterPicture.Type != currentCharacterPicture.Type)
                {
                    SetSpriteColor(characterPicture.Picture, new Color(75f / 255f, 75f / 255f, 75f / 255f));
                }
                else
                {
                    ResetSpriteColor(characterPicture.Picture);
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
        else
        {
            Debug.Log("End of character pictures reached.");
        }
    }

    void SetSpriteColor(Image image, Color color)
    {
        image.color = color;
    }

    void ResetSpriteColor(Image image)
    {
        image.color = Color.white;
    }
}
