using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelsTab : EditorWindow
{
    [SerializeField] private Inspector inspector;
    [SerializeField] private LevelListManager levelListManager;

    //Level Values
    private int prevLvlCount = 0;

    //UI Elements
    private VisualElement levelListView;

    void Start()
    {
        levelListView = levelListTab.Q<VisualElement>("level-box-list");
    }

    void Update()
    {
        if (prevLvlCount != levelListManager.levelsCount)
        {
            UpdateLevelListView();
            prevLvlCount = levelListManager.levelsCount;
        }
    }

    private void UpdateLevelListView()
    {
        levelListView.Clear();

        List<string> levelNames = new List<string>();

        for (int i = 0; i < levelListManager.levelsCount; i++)
        {
            levelNames.Add($"{i + 1}");
        }

        List<GameObject> lvlGameObjs = GetSortedGameObjectsByLevelIndex(levelListManager.lvlObjectList);

        foreach (GameObject lvlGameObj in lvlGameObjs)
        {
            int levelIndex = lvlGameObj.GetComponent<Level>().levelIndex;
            levelListView.Add(CreateLevelElement(levelIndex, levelNames, (levelIndex + 1).ToString(), lvlGameObj));
        }
    }

    private List<GameObject> GetSortedGameObjectsByLevelIndex(List<GameObject> list)
    {
        List<GameObject> sortedGameObjects = list.OrderBy(go => go.GetComponent<Level>().levelIndex).ToList();
        return sortedGameObjects;
    }

    private VisualElement CreateLevelElement(int lvlIndex, List<string> levelNames, string selectedName, GameObject currentObj)
    {
        //CREATING ELEMENTS

        //Parent
        VisualElement levelBox = new VisualElement();

        //-Label
        VisualElement levelLabel = new VisualElement();
        //--Label
        Label lvlLabelTxt = new Label();
        //--Options-Box
        VisualElement lvlOptionsBox = new VisualElement();
        //---
        DropdownField lvlDropdown = new DropdownField();
        //---Remove-Button
        Button removeLvlBtn = new Button();


        //-Body
        VisualElement levelActionList = new VisualElement();
        //--Condition View
        VisualElement lvlConditionView = new VisualElement();
        //---Condition List
        ScrollView lvlConditionsList = new ScrollView();


        //NAMING ELEMENTS

        levelBox.name = "level-box-" + lvlIndex;
        levelLabel.name = "level-label";
        lvlOptionsBox.name = "level-options-box";
        lvlDropdown.name = "level-dropdown";
        removeLvlBtn.name = "remove-level-btn";
        levelActionList.name = "level-action-list";
        lvlConditionView.name = "level-condition-view";
        lvlConditionsList.name = "conditions-list";

        //ADD CLASSES
        levelBox.AddToClassList("level-box");
        levelLabel.AddToClassList("level-box-label");
        lvlLabelTxt.AddToClassList("level-box-label-text");
        lvlOptionsBox.AddToClassList("action-options-box");
        lvlDropdown.AddToClassList("level-dropdown");
        removeLvlBtn.AddToClassList("remove-level-btn");
        levelActionList.AddToClassList("level-action-list");
        lvlConditionView.AddToClassList("level-condition-view");
        lvlConditionsList.AddToClassList("level-condition-list");

        //ADDITIONAL VALUES

        //ADD TEXT
        lvlLabelTxt.text = "Level " + (lvlIndex + 1);
        removeLvlBtn.text = "x";

        //Add to DropdownField
        lvlDropdown.choices = levelNames;
        lvlDropdown.value = selectedName;

        //REGISTER EVENTS
        lvlDropdown.RegisterValueChangedCallback(evt =>
        {
            string selectedValue = evt.newValue;
            levelListManager.ChangeLevel(currentObj, currentObj.GetComponent<Level>().levelIndex, int.Parse(selectedValue));
            UpdateLevelListView();
        });

        removeLvlBtn.RegisterCallback<MouseUpEvent>(evt =>
        {
            levelListManager.DeleteLevel(currentObj);
            UpdateLevelListView();
        });
        
        levelLabel.RegisterCallback<MouseUpEvent>(evt =>
        {
            inspector.SelectLevel(currentObj);
        });

        //ADD ELEMENTS

        //Add to levelBox
        levelBox.Add(levelLabel);
        levelBox.Add(levelActionList);

        //-Add to levelLabel
        levelLabel.Add(lvlLabelTxt);
        levelLabel.Add(lvlOptionsBox);

        //--Add to lvlOptionsBox
        lvlOptionsBox.Add(lvlDropdown);
        lvlOptionsBox.Add(removeLvlBtn);

        //-Add to levelActionList
        levelActionList.Add(lvlConditionView);

        //--Add to lvlConditionView
        lvlConditionView.Add(lvlConditionsList);

        return levelBox;
    }
}
