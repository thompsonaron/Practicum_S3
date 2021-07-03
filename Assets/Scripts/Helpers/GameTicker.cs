using System.Collections;
using UnityEngine;
using System;

public class GameTicker
{
    public static float TimeScale = 1f;
    public static float DeltaTime;
    public TimeTicker Ticker;
    public event Action Update;

    public static GameTicker SharedInstance
    {
        get
        {
            if (_sharedInstance == null)
            {
                var tickerObject = new GameObject();
                tickerObject.name = "TimeTicker";
                GameObject.DontDestroyOnLoad(tickerObject);

                _sharedInstance = new GameTicker();
                _sharedInstance.Ticker = tickerObject.AddComponent<TimeTicker>();
                _sharedInstance.Ticker.RegisterUpdate(_sharedInstance.update);
            }

            return _sharedInstance;
        }
    }

    private static GameTicker _sharedInstance;

    private void update(float deltaTime)
    {
        DeltaTime = deltaTime * TimeScale;

        Update?.Invoke();
    }
}