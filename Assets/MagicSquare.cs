using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using System;

public class MagicSquare : MonoBehaviour
{
    public TextMeshProUGUI[]  ButtonText = new TextMeshProUGUI[9];
    public TextMeshProUGUI outputText;
    
    int[] ButtonNumber = new int[9];
    int a = 1, b = 1, c = 1, d = 1, e = 1, f = 1, g = 1, h = 1, i = 1;
    int[] square = new int[9];
    bool isMagic;
    bool isDiscrete;

    public void ButtonClick(int whatButton)
    {
        if (whatButton == 0)
            Increment(0, ref a);
        else if (whatButton == 1)
            Increment(1, ref b);
        else if (whatButton == 2)
            Increment(2, ref c);
        else if (whatButton == 3)
            Increment(3, ref d);
        else if (whatButton == 4)
            Increment(4, ref e);
        else if (whatButton == 5)
            Increment(5, ref f);
        else if (whatButton == 6)
            Increment(6, ref g);
        else if (whatButton == 7)
            Increment(7, ref h);
        else if (whatButton == 8)
            Increment(8, ref i);
    }

    void Increment(int index, ref int counter)
    {
        ButtonText[index].text = counter.ToString();
        ButtonNumber[index] = counter;
        ++counter;
        if (counter>9)
            counter = 1;
    }

     public void CheckMagicSquare()
    {
        string element = ButtonText[i].text.ToString();

        for (int i = 0; i<9; i++)
        {
            try 
            {
                square[i] = Int32.Parse(ButtonText[i].text.ToString());
            }
            catch 
            {
                square[i] = 0;
            }
        }

        if (square.Distinct().Count() == 1 && square[0] == 0)
        {
            return;
        }

        isMagic = CheckRows() && CheckColumns() && CheckDiagonals();

        UpdateOutputText();
    }

     private void UpdateOutputText()
    {
        int discrete = square.Distinct().Count();

        if (!isMagic && discrete > 1)
        {
            outputText.text = "No magic here. Guess again.";
            Debug.Log("No magic here. Guess again");
        }
        else if (isMagic && discrete == 9)
        {
            outputText.text = "Congratulations! You are magical!";
            Debug.Log("Congratulations! You are magical");
        }
        else
        {
            outputText.text = "No discrete magic here. Guess again.";
            Debug.Log("No discrete magic here. Guess again");
        }
    }
    

    bool CheckRows()
    {
        int expectedSum = square.Take(3).Sum();
        for (int i = 0; i < 9; i += 3)
        {
            if (square.Skip(i).Take(3).Sum() != expectedSum)
            {
                return false;
            }
        }
        return true;
    }

    bool CheckColumns()
    {
        for (int i = 0; i < 3; i++)
        {
            int sum = 0;
            for (int j = 0; j < 3; j++)
            {
                sum += square[j * 3 + i];
            }
            if (sum != 15)
            {
                return false;
            }
        }
        return true;
    }

    bool CheckDiagonals()
    {
        int diagonal1Sum = square[0] + square[4] + square[8];
        int diagonal2Sum = square[2] + square[4] + square[6];
        return diagonal1Sum == diagonal2Sum;
    }

}