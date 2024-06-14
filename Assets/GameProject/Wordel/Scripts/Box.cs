using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Box : MonoBehaviour
{
    private TextMeshProUGUI text;
    public char letter {  get; private set; }

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetLetter(char letter)
    {
        this.letter = letter;
        text.text = letter.ToString();
    }
}