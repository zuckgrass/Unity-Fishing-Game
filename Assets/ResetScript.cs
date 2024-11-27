using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ResetManager : MonoBehaviour
{
    public static ResetManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetGame()
    {
        ClearAllSpawnedObjects();
        ReloadScene();
    }

    private void ClearAllSpawnedObjects()
    {
        var fish = GameObject.FindGameObjectsWithTag("fish");
        foreach (var f in fish)
        {
            Destroy(f);
        }

        var rocks = GameObject.FindGameObjectsWithTag("rock");
        foreach (var r in rocks)
        {
            Destroy(r);
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
