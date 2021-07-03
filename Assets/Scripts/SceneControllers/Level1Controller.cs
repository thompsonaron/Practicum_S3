using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Controller : USceneController
{
    public Level1Controller() : base(SceneNames.Level1) { }
    public UnlockedLevelsScriptableObject lockedLevels;
    private int totalEnemiesCount;
    private int enemiesLeft;
    SettingsMenuController settingsMenu;
    bool levelComplete = false;
    bool settingsOpen = false;

    public override void SceneDidLoad()
    {
        HandleButtons();
        SetupLabels();
        lockedLevels = Resources.Load<UnlockedLevelsScriptableObject>("UnlockedLevels");
        InputManager.OnPressedEsc += InputManager_OnPressedEsc;
        SpawnerController.OnEnemiesSet += SpawnerController_OnEnemiesSet;
        EnemyHealth.OnEnemyDeath += EnemyHealth_OnDeath;
        PlayerHealth.OnPlayerDeath += PlayerHealth_OnPlayerDeath;
        Time.timeScale = 1;
    }

    private void PlayerHealth_OnPlayerDeath()
    {
        var endOfRoundController = new EndOfRoundController();
        endOfRoundController.playerWon = false;
        endOfRoundController.SetCurrentAndNextScene(SceneName, GetNextGameScene(SceneName));
        AddChildSceneController(endOfRoundController);
        Time.timeScale = 0;
        AssetProvider.ReturnAllToPool();
    }

    private void ActivateInGameLevelSelector()
    {

    }

    private void EnemyHealth_OnDeath()
    {
        enemiesLeft--;
        if (enemiesLeft <= 0)
        {
            // TODO load end level scene and unlock the next scene if there is such
            UnlockLevel(GetNextGameScene(SceneName));
            var endOfRoundController = new EndOfRoundController();
            endOfRoundController.playerWon = true;
            endOfRoundController.SetCurrentAndNextScene(SceneName, GetNextGameScene(SceneName));
            AddChildSceneController(endOfRoundController);
            Time.timeScale = 0;
        }
    }

    private void SpawnerController_OnEnemiesSet(int enemiesCount)
    {
        totalEnemiesCount = enemiesCount;
        enemiesLeft = totalEnemiesCount;
    }

    private void InputManager_OnPressedEsc()
    {
        if (settingsMenu == null)
        {
            settingsMenu = new SettingsMenuController();
            settingsOpen = false;
        }

        //if (!settingsOpen && !levelComplete)
        if (!levelComplete && !settingsOpen)
        {
            settingsMenu.EnableMainMenuButton();
            AddChildSceneController(settingsMenu);
            Time.timeScale = 0;
            settingsOpen = true;
        }
        else if (settingsOpen)
        {
            RemoveChildSceneController(settingsMenu);
            settingsMenu = null;
            Time.timeScale = 1;
            settingsOpen = false;
        }
    }

    public override void SceneWillAppear()
    {
        var obj = GameObject.Find("Level1");
    }

    private void SetupLabels()
    {

    }

    private void HandleButtons()
    {


    }

    public string GetCurrentGameScene(GameScene gameScene)
    {
        switch (gameScene)
        {
            case GameScene.Level1:
                return SceneNames.Level1;
            case GameScene.Level2:
                return SceneNames.Level2;
            case GameScene.Level3:
                return SceneNames.Level3;
            default:
                return "";
        }
    }

    public string GetNextGameScene(GameScene gameScene)
    {
        switch (gameScene)
        {
            case GameScene.Level1:
                return SceneNames.Level2;
            case GameScene.Level2:
                return SceneNames.Level3;
            case GameScene.Level3:
                return "";
            default:
                return "";
        }
    }

    // TODO - needs automatization?
    // WARNING - final level should somehow block the next button too
    public string GetNextGameScene(string gameSceneName)
    {
        switch (gameSceneName)
        {
            case "Level1":
                return "Level2";
            case "Level2":
                return "Level3";
            case "Level3":
                return "";
            default:
                return "";
        }
    }

    private void UnlockLevel(string levelToUnlock)
    {
        switch (levelToUnlock)
        {
            case "Level1":
                lockedLevels.level1Locked = false;
                break;
            case "Level2":
                lockedLevels.level2Locked = false;
                break;
            case "Level3":
                lockedLevels.level3Locked = false;
                break;
            case "Level4":
                lockedLevels.level4Locked = false;
                break;
            case "Level5":
                lockedLevels.level5Locked = false;
                break;
            case "Level6":
                lockedLevels.level6Locked = false;
                break;
            default:
                break;
        }
    }
}