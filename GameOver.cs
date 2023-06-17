using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public void Replay()
    {
        FindObjectOfType<levelloader>().LoadNextLevel(1);
        Debug.Log("Yosh");
    }

    public void LoadMainMenu()
    {
        FindObjectOfType<levelloader>().LoadNextLevel(0);
    }
}
