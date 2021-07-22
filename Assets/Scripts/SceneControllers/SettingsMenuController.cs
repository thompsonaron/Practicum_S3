using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuController : USceneController
{
    public SettingsMenuController() : base(SceneNames.SettingsMenuController) { }

    private int value = 0;

    private bool enableMainMenuButton = false;

    public override void SceneDidLoad()
    {
        HandleButtons();
        SetupLabels();
    }

    public override void SceneWillAppear()
    {
        var obj = GameObject.Find("SettingsMenuController");
    }

    internal void EnableMainMenuButton()
    {
        enableMainMenuButton = true;
    }

    private void SetupLabels()
    {

    }

    private void HandleButtons()
    {
        

        var mainMenuButton = GameObject.Find("MainMenuButton").GetComponent<Button>();
        if (mainMenuButton == null) return;
        if (enableMainMenuButton)
        {
            mainMenuButton.onClick.AddListener(() =>
            {
                AssetProvider.ReturnAllToPool();
                Time.timeScale = 1;
                var startMenu = new StartMenuController();
                UNavigationController.SetRootViewController(startMenu);
            });
        }
        else
        {
            mainMenuButton.gameObject.SetActive(true);//--------
        }
       
    }
}
