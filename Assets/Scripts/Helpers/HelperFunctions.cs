using System;
using UnityEngine;

public static class HelperFunctions
{
    public static System.Random randomizer = new System.Random();

    public static T RandomEnumElement<T>()
    {
        var values = Enum.GetValues(typeof(T));
        var randomIndex = randomizer.Next(values.Length);

        T randomEnumValue = (T)values.GetValue(randomIndex);

        return randomEnumValue;
    }

    public static Vector3 RandomVector(int a, int b, int c)
    {
        return new Vector3(randomizer.Next(a), randomizer.Next(b), randomizer.Next(c));
    }
}