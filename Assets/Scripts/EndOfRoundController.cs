using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndOfRoundController : USceneController
{
    public EndOfRoundController() : base(SceneNames.EndOfRoundController) { }

    public bool playerWon = false;
    private string retryScene;
    private string nextScene;
    // TODO add retry scene somehow and add next scene

    public void SetCurrentAndNextScene(string currentScene, string nextScene)
    {
        this.retryScene = currentScene;
        this.nextScene = nextScene;
    }

    public override void SceneDidLoad()
    {
        var nextLevelButton = GameObject.Find("NextLevelButton").GetComponent<Button>();
        var mainMenuButton = GameObject.Find("MainMenuButton").GetComponent<Button>();
        var retryButton = GameObject.Find("RetryButton").GetComponent<Button>();
        var winLoseText = GameObject.Find("WinLoseText").GetComponent<Text>();



        //should guard
        // TODO Set all proper controllers
        mainMenuButton.onClick.AddListener(() =>
        {
            AssetProvider.ReturnAllToPool();
            Time.timeScale = 1;
            var startMenu = new StartMenuController();
            UNavigationController.SetRootViewController(startMenu);
        });

        if (playerWon)
        {
            nextLevelButton.onClick.AddListener(() =>
            {
                var level1Controller = new Level1Controller();
                level1Controller.SceneName = nextScene;
                UNavigationController.SetRootViewController(level1Controller);
            });
            retryButton.gameObject.SetActive(false);
            winLoseText.text = "You won!";
        }
        else
        {
            retryButton.onClick.AddListener(() =>
            {
                var level1Controller = new Level1Controller();
                level1Controller.SceneName = retryScene;
                UNavigationController.SetRootViewController(level1Controller);
            });
            nextLevelButton.gameObject.SetActive(false);
            winLoseText.text = "Try again?";
        }
        if (nextScene.Equals(""))
        {
            nextLevelButton.gameObject.SetActive(false);
            winLoseText.text = "Game Complete!";
        }
    }

    // TODO set as a global variable.. b uttons and winlosetext
    public void EnableNextLvlButton() {  }
    public void EnableRetryButton() { }

    public override void SceneWillAppear()
    {
    }

    public override void SceneWillDisappear()
    {
    }
}
