using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameState currentGameState;

    [SerializeField] GameObject panelPause;
    
    public string level;
    
    public int life;
    public int maxLife;
    public bool playerDead;

    public int apple;

    public bool wallJump;
    public bool speedUpgrade;

    public int idCheckPoint;

    public bool win = false;

    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform[] checkPoint;
    
    [SerializeField]GameObject player;


    [Header("Tutorial")]
    [SerializeField] GameObject[] tutorial;

    public Load load;

    private void Start()
    {
        ChangeState(GameState.Gameplay);
        SpawnPlayer();
        load = FindObjectOfType(typeof(Load)) as Load;
        panelPause.SetActive(false);

        foreach(GameObject o in tutorial)
        {
            o.SetActive(false);
        }
        
    }
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if(currentGameState == GameState.Gameplay)
            {
            ChangeState(GameState.Pause);
            }else if( currentGameState == GameState.Pause)
            {
                ChangeState(GameState.Gameplay);

            }
        }
        if (Input.GetButtonDown("Back") && currentGameState == GameState.Pause)
        {
              ChangeState(GameState.Gameplay);
        }
        if (apple >= 1 && !playerDead)
        {
        if (Input.GetButtonDown("Eat"))
        {
            life++;
            apple--;
        }
        }
      if (life <= 0 && !playerDead)
        {
            
            playerDead = true;
            StartCoroutine(nameof(PlayerDie));
            Destroy(player, 1);
            SpawnPlayer();
        }
      if (win)
        {
            ChangeState(GameState.Menu);
            load.ChangeScene("Credits");
        }
    }
    IEnumerator PlayerDie()
    {   
        yield return new WaitForSeconds(2);
        load.ChangeScene(load.level[load.idLevel]);
        
       
    }
    void SpawnPlayer()
    {

        if (player == null)
        {
            player = Instantiate(playerPrefab, checkPoint[idCheckPoint].position, Quaternion.identity);
            playerDead = false;
        }
    }
    public void ContinueButton()
    {
        ChangeState(GameState.Gameplay);
    }
    void ChangeState(GameState newState)
    {
        currentGameState = newState;
        switch (newState)
        {
            case GameState.Gameplay:
                Time.timeScale = 1;
                panelPause.SetActive(false);
                LockMouse(true);
                break;
            case GameState.Pause:
                Time.timeScale = 0;
                panelPause.SetActive(true);
                LockMouse(false);
                break;
            case GameState.Menu:
                Time.timeScale = 1;
                LockMouse(true);
                break;
        }
    }
    public void MainMenu()
    {
        load.ChangeScene("Title");
    }
    public void ActiveTutorial(int idTutorial)
    {
        tutorial[idTutorial].SetActive(true);
        tutorial[idTutorial].GetComponent<Animator>().SetTrigger("Appear");
        Destroy(tutorial[idTutorial], 3);
    }
    void LockMouse(bool x)
    {
        Cursor.visible = !x;
        if (x)
        {
        Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
        Cursor.lockState = CursorLockMode.None;
        }
    }
}
