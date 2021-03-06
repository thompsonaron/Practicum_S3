using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuController : USceneController
{
    public StartMenuController() : base(SceneNames.StartMenu) { }
    private Text levelSelectorLabel;
    private int value = 0;

    public override void SceneDidLoad()
    {
        HandleButtons();
        SetupLabels();
    }

    public override void SceneWillAppear()
    {
        var obj = GameObject.Find("StartMenu");
    }

    private void SetupLabels()
    {
        levelSelectorLabel = GameObject.Find("LevelSelectorLabel").GetComponent<Text>();

        //guard
        if (levelSelectorLabel == null) return;
        //levelSelectorLabel.text = value.ToString();
        levelSelectorLabel.text = "Level Selector";
    }



    private void HandleButtons()
    {
        var levelSelectorButton = GameObject.Find("LevelSelectorButton").GetComponent<Button>();
        if (levelSelectorButton == null) return;

        levelSelectorButton.onClick.AddListener(() =>
        {
            var levelSelector = new LevelSelectorController();
            AddChildSceneController(levelSelector);
        });

        var settingsButton = GameObject.Find("SettingsButton").GetComponent<Button>();
        if (settingsButton == null) return;

        settingsButton.onClick.AddListener(() =>
        {
            var settings = new SettingsMenuController();
            AddChildSceneController(settings);
        });

        var exitButton = GameObject.Find("ExitButton").GetComponent<Button>();
        if (exitButton == null) return;

        exitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        //guard
        //if (newGameButton == null) return;

        //newGameButton.onClick.AddListener(() =>
        //{
        //    //Debug.Log("Click on a button");
        //    var townVC = new TownController();

        //    townVC.SetValue(value);

        //    PushSceneController(townVC);
        //});

        //var addButton = GameObject.Find("AddButton").GetComponent<Button>();

        ////guard
        //if (addButton == null) return;

        //addButton.onClick.AddListener(() =>
        //{
        //    value++;
        //    valueLabel.text = value.ToString();
        //});

    }
}
