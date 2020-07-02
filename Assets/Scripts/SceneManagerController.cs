using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerController : MonoBehaviour
{
    // Singleton instance
    public static SceneManagerController Instance { get; private set; }

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

    void Start() {
        DontDestroyOnLoad(this.gameObject);
    }

    public void ToGamplayScene() {
        SceneManager.LoadScene(1);
    }

    public void ToMainScene() {
        SceneManager.LoadScene(0);
    }
}
