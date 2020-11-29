using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class winScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Cooldown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("WinScene");
    }
}
