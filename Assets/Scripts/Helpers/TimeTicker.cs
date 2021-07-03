using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTicker : MonoBehaviour
{
    List<Action<float>> updateActions = new List<Action<float>>();

    private void Update()
    {
        for (int i = 0; i < updateActions.Count; i++)
        {
            updateActions[i](Time.deltaTime);
        }
    }

    public void RegisterUpdate(Action<float> action)
    {
        updateActions.Add(action);
    }
}