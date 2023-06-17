using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelloader : MonoBehaviour
{
    public Animator anim;
    public float transitionTime = 1f;

    public void LoadNextLevel(int i)
    {
        StartCoroutine(LoadLevel(i));
    }
    IEnumerator LoadLevel(int index)
    {
        anim.SetTrigger("NextScene");
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(index);
    }
}
