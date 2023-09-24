using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Question : ScriptableObject
{
    public string theQuestion;
    public string Choice_1, Choice_2, Choice_3, Choice_4;
    public int AnswerNumber;

}
