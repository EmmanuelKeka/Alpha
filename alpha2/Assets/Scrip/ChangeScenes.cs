using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GotoGameOver()
    {
        SceneManager.LoadScene("Gameover");
    }
    public void GotoMainMenu()
    {
        SceneManager.LoadScene("Mainmenu");
    }
    public void GotoHowtoPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }
    public void GoToLevel1()
    {
        SceneManager.LoadScene("level1");
    }
    public void GoToEnd()
    {
        SceneManager.LoadScene("End");
    }
    public void GoToSouceCode()
    {
        Application.OpenURL("https://github.com/EmmanuelKeka/Alpha.git");
    }

}
