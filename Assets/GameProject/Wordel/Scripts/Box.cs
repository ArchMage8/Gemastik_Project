using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Box : MonoBehaviour
{
    private TextMeshProUGUI text;
    
    public char letter {  get; private set; }
    public State state { get; private set; }

    private Image fill;
    private Outline outline;

    [System.Serializable]
    public class State
    {
        public Color fillColor;
        public Color outlineColor;
    }

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        fill = GetComponent<Image>();
        outline = GetComponent<Outline>();
    }

    public void SetLetter(char letter)
    {
        this.letter = letter;
        text.text = letter.ToString();
    }

    public void SetState(State state)
    {
        this.state = state;
        fill.color = state.fillColor;
        outline.effectColor = state.outlineColor;
    }
}
