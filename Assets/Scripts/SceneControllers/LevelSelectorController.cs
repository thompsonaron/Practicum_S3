using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectorController : USceneController
{
    public LevelSelectorController() : base(SceneNames.LevelSelector) { }
    public UnlockedLevelsScriptableObject lockedLevels;

    private int value = 0;

    public override void SceneDidLoad()
    {
        lockedLevels = Resources.Load<UnlockedLevelsScriptableObject>("UnlockedLevels");
        HandleButtons();
        SetupLabels();
    }

    public override void SceneWillAppear()
    {
        var obj = GameObject.Find("StartMenu");
    }

    private void SetupLabels()
    {
       
    }

    private void HandleButtons()
    {
        var level1Button = GameObject.Find("Level1Button").GetComponent<Button>();
        var level2Button = GameObject.Find("Level2Button").GetComponent<Button>();
        var level3Button = GameObject.Find("Level3Button").GetComponent<Button>();
        var level4Button = GameObject.Find("Level4Button").GetComponent<Button>();
        var level5Button = GameObject.Find("Level5Button").GetComponent<Button>();
        var level6Button = GameObject.Find("Level6Button").GetComponent<Button>();
        var backButton = GameObject.Find("BackButton").GetComponent<Button>();

        level1Button.onClick.AddListener(() =>
        {
            var level1Controller = new Level1Controller();
            level1Controller.SceneName = SceneNames.Level1;
            UNavigationController.SetRootViewController(level1Controller);
        });
        level2Button.onClick.AddListener(() =>
        {
            var level1Controller = new Level1Controller();
            level1Controller.SceneName = SceneNames.Level2;
            UNavigationController.SetRootViewController(level1Controller);
        });
        level3Button.onClick.AddListener(() =>
        {
            var level1Controller = new Level1Controller();
            level1Controller.SceneName = SceneNames.Level3;
            UNavigationController.SetRootViewController(level1Controller);
        });
        level4Button.onClick.AddListener(() =>
        {
            var level1Controller = new Level1Controller();
            level1Controller.SceneName = SceneNames.Level4;
            UNavigationController.SetRootViewController(level1Controller);
        });
        level5Button.onClick.AddListener(() =>
        {
            var level1Controller = new Level1Controller();
            level1Controller.SceneName = SceneNames.Level5;
            UNavigationController.SetRootViewController(level1Controller);
        });
        level6Button.onClick.AddListener(() =>
        {
            var level1Controller = new Level1Controller();
            level1Controller.SceneName = SceneNames.Level6;
            UNavigationController.SetRootViewController(level1Controller);
        });

        backButton.onClick.AddListener(() =>
        {
            RemoveFromParentSceneController();
        });

        ActivateButtons();

        void ActivateButtons()
        {
            if (lockedLevels.level1Locked)
            {
                level1Button.gameObject.SetActive(false);
            }
            if (lockedLevels.level2Locked)
            {
                level2Button.gameObject.SetActive(false);
            }
            if (lockedLevels.level3Locked)
            {
                level3Button.gameObject.SetActive(false);
            }
            if (lockedLevels.level4Locked)
            {
                level4Button.gameObject.SetActive(false);
            }
            if (lockedLevels.level5Locked)
            {
                level5Button.gameObject.SetActive(false);
            }
            if (lockedLevels.level6Locked)
            {
                level6Button.gameObject.SetActive(false);
            }
        }
    }    
}
