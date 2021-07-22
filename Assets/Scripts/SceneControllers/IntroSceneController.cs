using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSceneController : USceneController
{
    public IntroSceneController() : base(SceneNames.IntroController) { }


    public override void SceneDidLoad()
    {
        ChangeSceneAnim.IntroScenEnded += ChangeSceneAnim_IntroScenEnded;
    }

    private void ChangeSceneAnim_IntroScenEnded()
    {
        var level1Controller = new Level1Controller();
        level1Controller.SceneName = SceneNames.Level1;
        UNavigationController.SetRootViewController(level1Controller);
    }
}