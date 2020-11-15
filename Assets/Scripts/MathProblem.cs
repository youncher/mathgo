using System;
using UnityEngine;

[Serializable]
public class MathProblem
{
    [SerializeField] private string question;
    [SerializeField] private string[] potentialAnswers;
    [SerializeField] private int correctIndex;
    
    public string Question
    {
        get => question;
        set => question = value;
    }

    public string[] PotentialAnswers
    {
        get => potentialAnswers;
        set => potentialAnswers = value;
    }

    public int CorrectIndex
    {
        get => correctIndex;
        set => correctIndex = value;
    }
}
