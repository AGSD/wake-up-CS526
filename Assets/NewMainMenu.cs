using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMainMenu : MonoBehaviour {

    public Animator animator;
    private int levelToLoad;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void BeginGame()
    {
        FadeToLevel(1);
    }

    public void quitGame()
    {
        Debug.Log("Quit");
        Application.Quit();

    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
        animator.SetTrigger("FadeIn");
    }

    public void OnFadeInComplete()
    {

    }
}
