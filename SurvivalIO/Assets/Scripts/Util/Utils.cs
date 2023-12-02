using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
        {
            component = go.AddComponent<T>();
        }
        return component;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
        {
            return null;
        }

        if (recursive == false)
        {
            Transform transform = go.transform.Find(name);

            return transform.GetComponent<T>();
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                {
                    return component;
                }
            }
        }

        return null;
    }

    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform targetTransform = FindChild<Transform>(go, name, recursive);
        if (targetTransform == null)
        {
            return null;
        }
        return targetTransform.gameObject;
    }

    public static float GetAngleFromCircleDivide(int count, out float[] array)
    {        
       array = new float[count]; 
        
        float angle = 360 / count;
        for (int i = 0; i < count; ++i)
        {
            array[i] = i * angle;
        }

        return angle;
    }
}
