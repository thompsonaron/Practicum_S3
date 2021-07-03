using System.Collections;
using UnityEngine;

public class AssetProvider : AssetBaseProvider
{
    [Header("Ranged")]
    public int bulletPoolSize;
    public GameObject bullet;
    public GameObject fireBullet;
    public GameObject frozenOrb;

    [Header("Enemies")]
    public int defaultPoolSize = 10;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;
    public GameObject enemy5;

    [Header("LoadSave test")]
    public GameObject box;

    private static AssetProvider _instance;
    public static AssetProvider Instance
    {
        get {
            if (_instance == null) {
                _instance = Resources.Load<AssetProvider>("AssetProvider");
            }

            return _instance;
        }
    }

    protected override AssetBaseProvider GetInstance()
    {
        return Instance;
    }

    public static GameObject GetAsset(GameAsset asset)
    {
        return Instance.GetObjectFromPool(GameObjectForType(asset));
    }

    public static void Prewarm()
    {
        if (_instance == null)
        {
            _instance = Resources.Load<AssetProvider>("AssetProvider");
            //prefill pool (this could be done through reflection)

            _instance.poolObject = new GameObject();
            _instance.poolObject.name = "Pool";
            GameObject.DontDestroyOnLoad(_instance.poolObject);

            //bullet pool
            Instance.InstatiatePool(_instance.bullet, _instance.bulletPoolSize);
            Instance.InstatiatePool(_instance.fireBullet, _instance.bulletPoolSize);
            Instance.InstatiatePool(_instance.frozenOrb, _instance.bulletPoolSize);

            Instance.InstatiatePool(_instance.enemy1, _instance.defaultPoolSize);
            Instance.InstatiatePool(_instance.enemy2, _instance.defaultPoolSize);
            Instance.InstatiatePool(_instance.enemy3, _instance.defaultPoolSize);
            Instance.InstatiatePool(_instance.enemy4, _instance.defaultPoolSize);
            Instance.InstatiatePool(_instance.enemy5, _instance.defaultPoolSize);

            //load save
        }
    }

    public PoolableObject RegisterObjectAsPoolable(GameObject gameObject, GameAsset type) 
    {
        var poolable = gameObject.GetComponent<PoolableObject>();
        if (poolable == null)
        {
            poolable = gameObject.AddComponent<PoolableObject>();
        }

        poolable.SetPool(GameObjectForType(type), Instance);

        return poolable;
    }

    public static GameObject GameObjectForType(GameAsset type)
    {
        switch (type)
        {
            case GameAsset.Bullet:
                return Instance.bullet;
            case GameAsset.FireBullet:
                return Instance.fireBullet;
            case GameAsset.FrozenOrb:
                return Instance.frozenOrb;
            case GameAsset.MeleeWeak:
                return Instance.enemy1;
            case GameAsset.MeleeFast:
                return Instance.enemy2;
            case GameAsset.MeleeStrong:
                return Instance.enemy3;
            case GameAsset.Ranged:
                return Instance.enemy4;
            case GameAsset.Mage:
                return Instance.enemy5;
            default:
                return null;
        }
    }

    public static void ReturnAllToPool()
    {
        foreach (var item in GameObject.FindObjectsOfType<PoolableObject>())
        {
            item.ReturnToPool();
        }
    }
}

public enum GameAsset
{
    Bullet, 
    FireBullet, 
    FrozenOrb, 
    MeleeWeak, 
    MeleeFast, 
    MeleeStrong, 
    Ranged, 
    Mage, 
    Box
}

