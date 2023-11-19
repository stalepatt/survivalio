using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerDef
{
    private Define.Scene _currentSceneType = Define.Scene.Default;

    public Define.Scene CurrentSceneType
    {
        get
        {
            if (_currentSceneType != Define.Scene.Default)
            {
                return _currentSceneType;
            }

            return CurrentScene.SceneType;
        }
        set { _currentSceneType = value; }
    }

    public BaseScene CurrentScene { get { return GameObject.Find("@Scene").GetComponent<BaseScene>(); } }

    public void Init()
    {

    }

    public void ChangeScene(Define.Scene type)
    {
        CurrentScene.Clear();

        _currentSceneType = type;
        SceneManager.LoadScene(GetSceneName(type));
    }

    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        char[] letters = name.ToLower().ToCharArray();
        letters[0] = char.ToUpper(letters[0]);
        return new string(letters);
    }
}
