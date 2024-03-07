using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PadNumbers : MonoBehaviour
{
    private int _nextNum = 1;

    [SerializeField]
    private List<GameObject> _textNum;

    public void GoodNumber(TextMeshProUGUI textMeshPro)
    {
        int num;
        if (int.TryParse(textMeshPro.text, out num))
        {
            if (num == _nextNum)
            {
                if (_nextNum == 6)
                {
                    Debug.Log("victory");
                    _nextNum = 1;
                    return;
                }
                _nextNum++;
                Debug.Log("oui");
                ChangeNumbers();
            }
            else
            {
                _nextNum = 1;
                Debug.Log("t'es nul");
                ChangeNumbers();
            }
        }
        else
        {
            Debug.LogError("Le texte dans TextMeshPro n'est pas un nombre valide.");
        }
    }


    private void ChangeNumbers()
    {
        List<int> uniqueNumbers = GenerateUniqueNumbers(1, 6);

        if (uniqueNumbers.Count < _textNum.Count)
        {
            Debug.LogError("Pas assez de nombres uniques disponibles pour les TextMeshPro.");
            return;
        }

        uniqueNumbers.Shuffle();

        for (int i = 0; i < _textNum.Count; i++)
        {
            GameObject text = _textNum[i];
            TextMeshProUGUI textMeshPro = text.GetComponent<TextMeshProUGUI>();

            if (textMeshPro != null)
            {
                textMeshPro.text = uniqueNumbers[i].ToString();
            }
            else
            {
                Debug.LogError("Le GameObject " + text.name + " ne contient pas de composant TextMeshPro.");
            }
        }
    }

    private List<int> GenerateUniqueNumbers(int min, int max)
    {
        List<int> numbers = new List<int>();
        for (int i = min; i <= max; i++)
        {
            numbers.Add(i);
        }
        return numbers;
    }
}

public static class ListExtensions
{
    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}