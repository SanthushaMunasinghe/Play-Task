using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameInfoTab : GamePlayer
{
    //UI Elements
    private Label questionLabel;
    public VisualElement answerElement;

    public void GetElements()
    {
        questionLabel = infoTab.Q<VisualElement>("question-text").Q<Label>();
        answerElement = infoTab.Q<VisualElement>("answer-element");
    }

    public void UpdateQuestion(string qTxt)
    {
        questionLabel.text = qTxt;
    }

    public void UpdateAnswerList(int aIndex, string text, string value)
    {
        //CREATE
        VisualElement answerItem = new VisualElement();
        Label answerTxt = new Label();

        //ADD CLASSES
        answerItem.AddToClassList("answer-element-label");
        answerTxt.AddToClassList("answer-element-label-text");

        //ADD VALUES
        answerTxt.text = text;

        //ADD EVENTS
        answerItem.RegisterCallback<MouseUpEvent>(evt => {
            //Return Value
        });

        //ADD ELEMENTs
        answerItem.Add(answerTxt);

        answerElement.Add(answerItem);
    }
}
