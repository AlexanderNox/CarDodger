using UnityEngine.SceneManagement;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component 
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var objs = FindObjectOfType(typeof(T)) as T[];
                    if ( objs?.Length > 0)
                    {
                        _instance = objs[0];
                    }

                    if (objs?.Length > 1)
                    {
                        Debug.LogError("There is more than one " + typeof(T).Name + " in the scene");
                    }

                    if (_instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.hideFlags = HideFlags.HideAndDontSave;
                        _instance = obj.AddComponent<T>();
                    }
                }
                return _instance;
            }
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
    }

    // public class SinglettonPersistent<T> : MonoBehaviour where T : Component
    // {
    //     private static T _instance;
    //
    //     public static T Instance
    //     {
    //         get
    //         {
    //             if (_instance == null)
    //             {
    //                 Scene activeScene = SceneManager.GetActiveScene();
    //                 SceneManager.SetActiveScene(SceneManager.GetSceneByName("Managers"));
    //                 GameObject obj = new GameObject();
    //                 obj.name = typeof(T).Name;
    //                 obj.hideFlags = HideFlags.HideAndDontSave;
    //                 _instance = obj.AddComponent<T>();
    //                 SceneManager.SetActiveScene(activeScene);
    //             }
    //
    //             return _instance;
    //         }
    //     }
    //
    //     private void OnDestroy()
    //     {
    //         if (_instance == this)
    //         {
    //             _instance = null;
    //         }
    //     }
    // }