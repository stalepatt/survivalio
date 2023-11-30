using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Extensions
{
    public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
    {
        return Utils.GetOrAddComponent<T>(go);
    }

    public static void BindEvent(this GameObject go, Action action, Define.UIEvent type = Define.UIEvent.Click)
    {
        UIBase.BindEvent(go, action, type);
    }

    public static T GetRandomElement<T>(this T[] array) => array[Random.Range(0, array.Length)];

    public static T GetRandomElement<T>(this T[] array, int start, int end) => array[Random.Range(start, end)];
}