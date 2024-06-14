using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.XR.Interaction;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{

    private static readonly KeyCode[] SUPPORTED_KEYS = new KeyCode[]
    {
        KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F, 
        KeyCode.G, KeyCode.H, KeyCode.I, KeyCode.J, KeyCode.K, KeyCode.L,
        KeyCode.M, KeyCode.N, KeyCode.O, KeyCode.P, KeyCode.Q, KeyCode.R,
        KeyCode.S, KeyCode.T, KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X,
        KeyCode.Y, KeyCode.Z
    };

    private Row[] rows;
    private int rowIndex;
    private int columnIndex;
    private Row currentRow;
    public string Answer;
    public float checkDelay = 0.5f;

    [Header("States")]
    public Box.State emptyState;
    public Box.State OccupiedState;

    public Box.State correctState;
    public Box.State wrongSpotState;
    public Box.State incorrectState;

    private bool canModify = true;
    private bool canSubmit = true;
    private bool isChecked = false;

    private void Awake()
    {
        rows = GetComponentsInChildren<Row>();
        FixFormat();
    }

    private void Update()
    {
      currentRow = rows[rowIndex];

        if (canModify)
        {
            if (Input.GetKeyDown(KeyCode.Backspace))    //Change to GetKey is u want insta-delete
            {
                columnIndex = Mathf.Max(columnIndex - 1, 0);
                currentRow.tiles[columnIndex].SetLetter('\0');
                currentRow.tiles[columnIndex].SetState(emptyState);

            }

            else if (columnIndex >= rows[rowIndex].tiles.Length)
            {
                //submit for evaluation
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    StartCoroutine(SubmitRow(currentRow));

                }


            }
            else
            {
                for (int i = 0; i < SUPPORTED_KEYS.Length; i++)
                {
                    if (Input.GetKeyDown(SUPPORTED_KEYS[i]))
                    {
                        currentRow.tiles[columnIndex].SetLetter((char)SUPPORTED_KEYS[i]);
                        columnIndex++;
                        break;
                    }
                }
            }
        }
    }

    private IEnumerator SubmitRow(Row row)
    {
        
        string Remaining = Answer;

        for (int i = 0; i < row.tiles.Length; i++)
        {
            canModify = false;
            canSubmit = false;

            Box tile = row.tiles[i];

            if (tile.letter == Answer[i])
            {
                tile.SetState(correctState);

                Remaining = Remaining.Remove(i, 1);
                Remaining = Remaining.Insert(i, " ");
            }

            else if (!Answer.Contains(tile.letter))
            {
                tile.SetState(incorrectState);
            }

           

            if (tile.state != correctState && tile.state != incorrectState)
            {
                if (Remaining.Contains(tile.letter))
                {
                    tile.SetState(wrongSpotState);

                    int index = Remaining.IndexOf(tile.letter);
                    Remaining = Remaining.Remove(index, 1);
                    Remaining = Remaining.Insert(index, " ");
                }
                else
                {
                    tile.SetState(incorrectState);
                }
            }
            yield return new WaitForSeconds(checkDelay);
        }

        canModify = true;
        canSubmit = true;

        if (HasWon(row))
        {
            enabled = false;
        }

        if (rowIndex >= rows.Length)
        {
           Debug.Log("Finished");
           enabled = false;
        }
    }

    private void FixFormat()
    {
        Answer = Answer.ToLower().Trim();
    }

    private bool HasWon(Row row)
    {
        for(int i = 0; i < row.tiles.Length; i++)
        {
            if(row.tiles[i].state == correctState)
            {
                return false;
            }
        }
        return true;
    }

    public void Restart()
    {
        if (canModify)
        {
            enabled = true;
            ClearBoard();
        }
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void ClearBoard()
    {
        for (int row = 0; row < rows.Length; row++) 
        { 
            for(int col = 0; col < rows[row].tiles.Length; col++)
            {
                rows[row].tiles[col].SetLetter('\0');
                rows[row].tiles[col].SetState(emptyState);
            }    
        }
        isChecked = false;
        rowIndex = 0;
        columnIndex = 0;
    }

    public void Submit()
    {
        if (columnIndex >= rows[rowIndex].tiles.Length && canSubmit && !isChecked)
        {
            StartCoroutine(SubmitRow(currentRow));
            isChecked = true;

        }
        EventSystem.current.SetSelectedGameObject(null);
    }
}
