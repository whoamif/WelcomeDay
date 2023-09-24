using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public Question[] Questions;
    public TextMeshProUGUI QuestionFeild;
    public TextMeshProUGUI ChoiceFeild_1, ChoiceFeild_2, ChoiceFeild_3, ChoiceFeild_4;
    public KeyCode NextQuestionKey,LastQuestionKey,ShowAnswerKey;
    private int CurrentQuestion;
    private Color DefaultColor;

    private void Start()
    {
        CurrentQuestion = 0;
        DefaultColor = ChoiceFeild_1.color;
    }

    private void Update()
    {
        QuestionFeild.text = Questions[CurrentQuestion].theQuestion;
        ChoiceFeild_1.text = Questions[CurrentQuestion].Choice_1;
        ChoiceFeild_2.text = Questions[CurrentQuestion].Choice_2;
        ChoiceFeild_3.text = Questions[CurrentQuestion].Choice_3;
        ChoiceFeild_4.text = Questions[CurrentQuestion].Choice_4;


        if (Input.GetKeyDown(NextQuestionKey))
        {
            if (CurrentQuestion < Questions.Length - 1)
            {

                CurrentQuestion++;
                ChoiceFeild_1.color = DefaultColor;
                ChoiceFeild_2.color = DefaultColor;
                ChoiceFeild_3.color = DefaultColor;
                ChoiceFeild_4.color = DefaultColor;
            }
            else
                EndQuiz();

           

        }
        if (Input.GetKeyDown(LastQuestionKey)&& CurrentQuestion > 0)
        {

                CurrentQuestion--;
                ChoiceFeild_1.color = DefaultColor;
                ChoiceFeild_2.color = DefaultColor;
                ChoiceFeild_3.color = DefaultColor;
                ChoiceFeild_4.color = DefaultColor;
    
        }

        if (Input.GetKeyDown(ShowAnswerKey))
            ShowAnswer();
    }

    private void EndQuiz()
    {
        Debug.Log("End");
    }

    private void ShowAnswer()
    {
        if(Questions[CurrentQuestion].AnswerNumber == 1)
        ChoiceFeild_1.color = Color.green;
        else
        ChoiceFeild_1.color = Color.red;


        if (Questions[CurrentQuestion].AnswerNumber == 2)
        ChoiceFeild_2.color = Color.green;
        else
        ChoiceFeild_2.color = Color.red;


        if (Questions[CurrentQuestion].AnswerNumber == 3)
        ChoiceFeild_3.color = Color.green;
        else
        ChoiceFeild_3.color = Color.red;


        if (Questions[CurrentQuestion].AnswerNumber == 4)
        ChoiceFeild_4.color = Color.green;
        else
        ChoiceFeild_4.color = Color.red;

    }
}
