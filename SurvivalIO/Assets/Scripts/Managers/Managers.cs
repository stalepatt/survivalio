using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers s_instance = null;
    public static Managers Instance { get { Init(); return s_instance; } }

    private static DataManager s_dataManager = new DataManager();
    private static GameManager s_gameManager = new GameManager();
    private static PoolManager s_poolManager = new PoolManager();
    private static ResourceManager s_resourceManager = new ResourceManager();
    private static SceneManagerDef s_sceneManagerDef = new SceneManagerDef();
    private static UIManager s_uiManager = new UIManager();

    public static DataManager DataManager { get { Init(); return s_dataManager; } }
    public static GameManager GameManager { get { Init(); return s_gameManager; } }
    public static PoolManager PoolManager { get { Init(); return s_poolManager; } }
    public static ResourceManager ResourceManager { get { Init(); return s_resourceManager; } }
    public static SceneManagerDef SceneManager { get { Init(); return s_sceneManagerDef; } }
    public static UIManager UIManager { get { Init(); return s_uiManager; } }


    private void Start()
    {
        Init();
    }

    private static void Init()
    {
        if (s_instance == null)
        {
            GameObject gameObject = GameObject.Find(@"Managers");

            if (gameObject == null)
            {
                gameObject = new GameObject { name = "@Managers" };
            }

            s_instance = Utils.GetOrAddComponent<Managers>(gameObject);
            DontDestroyOnLoad(gameObject);

            s_dataManager.Init();
            s_gameManager.Init();
            s_poolManager.Init();
            s_resourceManager.Init();
            s_sceneManagerDef.Init();
            s_uiManager.Init();
        }
    }
}
