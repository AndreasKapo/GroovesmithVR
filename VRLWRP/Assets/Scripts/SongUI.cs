using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongUI : MonoBehaviour
{

    public GameObject[] uiObjects;
    public static SongUI instance;
    public bool objectsHidden;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        HideUIObjects();

    }

    private void Update()
    {
        
    }


    public void ShowUIObjects()
    {
        foreach(GameObject uiObject in uiObjects)
        {
            uiObject.SetActive(true);
        }
        objectsHidden = false;
    }

    public void HideUIObjects()
    {
        foreach (GameObject uiObject in uiObjects)
        {
            uiObject.SetActive(false);
        }
        objectsHidden = true;
    }
}
