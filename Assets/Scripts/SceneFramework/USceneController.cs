using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public interface ISceneController
{
    string SceneName { get; set; }

    bool IsLoaded { get;  }
    bool IsRegisteredLoad { get; }

     USceneController ParentSceneController { get; }
}

public class USceneController: ISceneController
{
    public string SceneName { get; set; }

    public bool IsLoaded { get; private set; }
    public bool IsRegisteredLoad { get; private set; }

    public USceneController ParentSceneController { get; set; }

    private List<USceneController> ChildControllers = new List<USceneController>();

    public USceneController(string sceneName)
    {
        SceneName = sceneName; 
    }

    private void RegisterLoadCompleted(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == SceneName)
        {
            IsLoaded = true;
            SceneDidLoad();
            UnregisterLoad();
        }
    }

    //scene stack management
    public void PushSceneController(USceneController controller)
    {
        controller.RegisterLoad();
        controller.SceneWillAppear();

        UnregisterLoad();
        SceneWillDisappear();

        controller.ParentSceneController = this;

        UNavigationController.PresentViewController(controller);
    }

    public void AddChildSceneController(USceneController controller)
    {
        controller.RegisterLoad();
        controller.SceneWillAppear();
        controller.ParentSceneController = this;
        ChildControllers.Add(controller);
        SceneManager.LoadSceneAsync(controller.SceneName, LoadSceneMode.Additive);
    }

    public void RemoveFromParentSceneController()
    {
        ParentSceneController.RemoveChildSceneController(this);
    }

    public void RemoveChildSceneController(USceneController controller)
    {
        ChildControllers.Remove(controller);
        controller.ParentSceneController = null;
        controller.SceneWillDisappear();
        SceneManager.UnloadSceneAsync(controller.SceneName);
    }

    public void PopToParentSceneController()
    {
        if (UNavigationController.ActiveController == this)
        {
            UNavigationController.RemoveViewController();
        }

    }

    //Life cycle
    public virtual void SceneDidLoad()
    {
        //Debug.Log("ViewDidLoad: " + SceneName);
    }

    public virtual void SceneWillDisappear()
    {
        //Debug.Log("ViewWillDisappear:" + SceneName);
    }

    public virtual void SceneWillAppear()
    {
        //Debug.Log("ViewWillAppear:" + SceneName);
    }

    public void RegisterLoad()
    {
        if (IsRegisteredLoad) return;

        IsRegisteredLoad = true;
        SceneManager.sceneLoaded += RegisterLoadCompleted;
    }

    public void UnregisterLoad()
    {
        if (!IsRegisteredLoad) return;

        IsRegisteredLoad = false;
        SceneManager.sceneLoaded -= RegisterLoadCompleted;
    }

    //unregister
    ~USceneController()
    {
        UnregisterLoad();
    }
}
