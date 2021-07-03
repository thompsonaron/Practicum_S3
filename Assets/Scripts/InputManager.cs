using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    public static event Action OnPressedFire;
    public static event Action OnPressedEsc;
    public static event Action<Vector3> OnMovement;
    public static event Action OnPressedSpace;
    public static event Action<int> OnPressedNumber;

    private void Update()
    {
        //fire
        if (Input.GetMouseButton(0))
        {
            OnPressedFire?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPressedEsc?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnPressedSpace?.Invoke();
        }
        ScanKeyboard();

        //move
        var inputX = Input.GetAxis(InputStrings.axisX);
        var inputY = Input.GetAxis(InputStrings.axixY);

        OnMovement?.Invoke(new Vector3(inputX, 0, inputY).normalized);
    }

    private static void ScanKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            OnPressedNumber?.Invoke(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            OnPressedNumber?.Invoke(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            OnPressedNumber?.Invoke(3);
        }
    }

    public static void Activate()
    {
        var manager = GameObject.Instantiate(new GameObject());

        manager.AddComponent<InputManager>();
        manager.name = "InputManager";

        GameObject.DontDestroyOnLoad(manager);
    }
}

public struct InputStrings
{
    public static string axisX = "Horizontal";
    public static string axixY = "Vertical";
}
