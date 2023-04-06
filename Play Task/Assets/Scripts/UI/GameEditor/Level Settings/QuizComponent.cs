using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class QuizComponent : MonoBehaviour
{
    //UI Elements
    private DropdownField answerCountDropdowwn;
    private DropdownField quizTypeDropdown;
    private DropdownField answersDropdown;
    private TextField answerTextField;
    private DropdownField answersSelectDropdown;
    private DropdownField answersTypeDropdown;

    //Value Lists
    [SerializeField] private List<string> answerCountList = new List<string>();
    [SerializeField] private List<string> quizTypeList = new List<string>();
    [SerializeField] private List<string> answerTypeList = new List<string>();

    private List<string> currentAnswers = new List<string>();

    //Values
    public int answerCount;
    public string quizType;
    private string selectedAnswerForTxt;
    private string selectedAnswerForType;
    public List<AnswerData> answerData = new List<AnswerData>();
    public List<AnswerData> answerValues = new List<AnswerData>();

    public void Setup(VisualElement parent)
    {
        //Get Elements
        answerCountDropdowwn = parent.Q<VisualElement>("answer-count").Q<DropdownField>();
        quizTypeDropdown = parent.Q<VisualElement>("quiz-type").Q<DropdownField>();
        answersDropdown = parent.Q<VisualElement>("answers").Q<DropdownField>();
        answerTextField = parent.Q<VisualElement>("answers").Q<TextField>();
        answersSelectDropdown = parent.Q<VisualElement>("answers-type").Q<DropdownField>("answer");
        answersTypeDropdown = parent.Q<VisualElement>("answers-type").Q<DropdownField>("answer-type");

        //Set Choices
        answerCountDropdowwn.choices = answerCountList;
        quizTypeDropdown.choices = quizTypeList;
        answersDropdown.choices = currentAnswers;
        answersSelectDropdown.choices = currentAnswers;
        answersTypeDropdown.choices = answerTypeList;

        //Initial Values
        UpdateAnswerCount(answerCountList[0]);

        //Events
        RegisterEvents();
    }

    private void RegisterEvents()
    {
        answerCountDropdowwn.RegisterValueChangedCallback(evt =>
        {
            string selectedValue = evt.newValue;
            UpdateAnswerCount(selectedValue);
        });
        
        answersDropdown.RegisterValueChangedCallback(evt =>
        {
            string selectedValue = evt.newValue;
            selectedAnswerForTxt = selectedValue;
            answerTextField.value = answerData[int.Parse(selectedValue) - 1].AnswerTxt;
        });

        answerTextField.RegisterCallback<BlurEvent>(evt =>
        {
            string typedValue = answerTextField.value;
            UpdateAnswerText(typedValue);
        });

        answersSelectDropdown.RegisterValueChangedCallback(evt =>
        {
            string selectedValue = evt.newValue;
            selectedAnswerForType = selectedValue;
            answersTypeDropdown.value = answerValues[int.Parse(selectedValue) - 1].AnswerTxt;
        });

        answersTypeDropdown.RegisterValueChangedCallback(evt =>
        {
            string selectedValue = evt.newValue;
            UpdateAnswerType(selectedValue);
        });
    }

    private void UpdateAnswerCount(string acCount)
    {
        answerCount = int.Parse(acCount);
        quizType = quizTypeList[0];

        currentAnswers.Clear();
        answerData.Clear();
        answerValues.Clear();

        for (int i = 0; i < answerCount; i++)
        {
            currentAnswers.Add((i + 1).ToString());
        }

        selectedAnswerForTxt = currentAnswers[0];
        selectedAnswerForType = currentAnswers[0];

        foreach (string count in currentAnswers)
        {
            AnswerData aData = new AnswerData();
            aData.AnswerIndex = int.Parse(count);
            aData.AnswerTxt = "";
            answerData.Add(aData);
        }

        foreach (string count in currentAnswers)
        {
            AnswerData aValue = new AnswerData();
            aValue.AnswerIndex = int.Parse(count);
            aValue.AnswerTxt = answerTypeList[0];
            answerValues.Add(aValue);
        }

        //Set Dropdown Values
        answerCountDropdowwn.value = answerCount.ToString();
        quizTypeDropdown.value = quizType;
        answersDropdown.value = selectedAnswerForTxt;
        answerTextField.value = answerData[0].AnswerTxt;
        answersSelectDropdown.value = selectedAnswerForType;
        answersTypeDropdown.value = answerValues[0].AnswerTxt;
    }

    private void UpdateAnswerText(string textValue)
    {
        foreach (AnswerData aData in answerData)
        {
            if (aData.AnswerIndex == int.Parse(selectedAnswerForTxt))
            {
                aData.AnswerTxt = textValue;
            }
        }
    }
    
    private void UpdateAnswerType(string type)
    {
        foreach (AnswerData ValueData in answerValues)
        {
            if (ValueData.AnswerIndex == int.Parse(selectedAnswerForType))
            {
                ValueData.AnswerTxt = type;
            }
        }
    }
}
