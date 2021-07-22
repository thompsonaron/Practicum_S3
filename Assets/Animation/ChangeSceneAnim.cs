using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChangeSceneAnim : MonoBehaviour
{
    public static event Action IntroScenEnded;

    public void LoadNewScene()
    {
        IntroScenEnded?.Invoke();
    }
}
