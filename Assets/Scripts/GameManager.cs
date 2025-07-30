using UnityEngine;
using UnityEngine.SceneManagement;


/*
For advice on delegate and event usage:
https://www.youtube.com/watch?v=J01z1F-du-E

For gamemanager tutorial:
https://www.youtube.com/watch?v=j_eQGp-IbCE
*/
public class GameManager : MonoBehaviour
{
    //Declaration of a singleton.
    public static GameManager instance;

    [Header("Persistent Objects")]
    public GameObject[] persistentObjects;

    private void Awake()
    {
        //If there is a GameManager already in the scene (global "instance" has a value attached) it destroys itself.
        if (instance != null)
        {
            CleanUpAndDestroy();
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            MarkPersistentObjects();

            //If sceneLoaded triggers and this object is in the scene (theorhetically always possible after the first game scene), 
            // run OnSceneLoaded
            SceneManager.activeSceneChanged += OnSceneActivated;
        }
    }

    private void MarkPersistentObjects()
    {
        foreach (GameObject obj in persistentObjects)
        {
            if (obj != null)
            {
                DontDestroyOnLoad(obj);
            }
        }
    }

    private void OnSceneActivated(Scene lastScene, Scene nextScene)
    {
        //Determines if the scene is the main menu, and destroys all of this game manager's persistent objects if so.
        if (nextScene.name == "MainMenu")
        {
            CleanUpAndDestroy();
        }
    }

    private void CleanUpAndDestroy()
    {
        foreach (GameObject obj in persistentObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        Destroy(gameObject);
        SceneManager.activeSceneChanged -= OnSceneActivated;
    }
}
