using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Load : MonoBehaviour
{
    public string[] level;

    [Header("leguage")]
    public int idLevel;

    public bool reLoad;

    public string currentScene;

    //GameManager gameManager;

    [SerializeField] string[] inglishData;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        
        //gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;


    }
    private void Update()
    {
        if(currentScene == "Credits")
        {
        if (Input.GetButtonDown("Cancel") || Input.GetButtonDown("Back"))
        {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            ChangeScene("Title");
        }    
        }
    }
    public void ChangeScene(string newScene)
    {
        currentScene = newScene;
        SceneManager.LoadScene(newScene);
    }
    public void NewGameButton()
    {
        ChangeScene(level[idLevel]);
    }
    public void OptionsButton()
    {
        print("Options");
    }
    public void QuitButton()
    {
        Application.Quit();
    }
   
}
