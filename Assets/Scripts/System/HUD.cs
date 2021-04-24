using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField]
    Text appleAmountTxt;
    [SerializeField]
    Image[] heartImage;
    [SerializeField]
    Sprite[] heartSprite;
    [SerializeField]
    int apple;
    [SerializeField]
    int life;
    [SerializeField]
     int maxLife;


    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
        foreach (Image o in heartImage)
        {
            o.sprite = heartSprite[0];
            o.enabled = false;
        }
        
        life = 0;
        maxLife = 0;

    }

    // Update is called once per frame
    void Update()
    {
        ItemAndLife();
    }
    void ItemAndLife()
    {
        if(gameManager.apple != apple)
        {
            apple = gameManager.apple;
            appleAmountTxt.text = apple.ToString();
        }

        maxLife = gameManager.maxLife;
        life = gameManager.life;

        switch (maxLife)
        {
            case 0:
                heartImage[0].enabled = false;
                heartImage[1].enabled = false;
                heartImage[2].enabled = false;
                heartImage[3].enabled = false;
                heartImage[4].enabled = false;
                break;
            case 1:
                heartImage[0].enabled = true;
                heartImage[1].enabled = false;
                heartImage[2].enabled = false;
                heartImage[3].enabled = false;
                heartImage[4].enabled = false;
                break;
            case 2:
                heartImage[0].enabled = true;
                heartImage[1].enabled = true;
                heartImage[2].enabled = false;
                heartImage[3].enabled = false;
                heartImage[4].enabled = false;
                break;
            case 3:
                heartImage[0].enabled = true;
                heartImage[1].enabled = true;
                heartImage[2].enabled = true;
                heartImage[3].enabled = false;
                heartImage[4].enabled = false;
                break;
            case 4:
                heartImage[0].enabled = true;
                heartImage[1].enabled = true;
                heartImage[2].enabled = true;
                heartImage[3].enabled = true;
                heartImage[4].enabled = false;
                break;
            case 5:
                heartImage[0].enabled = true;
                heartImage[1].enabled = true;
                heartImage[2].enabled = true;
                heartImage[3].enabled = true;
                heartImage[4].enabled = true;
                break;
        }
        switch (life)
        {
            case 0:
                heartImage[0].sprite = heartSprite[0];
                heartImage[1].sprite = heartSprite[0];
                heartImage[2].sprite = heartSprite[0];
                heartImage[3].sprite = heartSprite[0];
                heartImage[4].sprite = heartSprite[0];
                break;
            case 1:
                heartImage[0].sprite = heartSprite[1];
                heartImage[1].sprite = heartSprite[0];
                heartImage[2].sprite = heartSprite[0];
                heartImage[3].sprite = heartSprite[0];
                heartImage[4].sprite = heartSprite[0];
                break;
            case 2:
                heartImage[0].sprite = heartSprite[1];
                heartImage[1].sprite = heartSprite[1];
                heartImage[2].sprite = heartSprite[0];
                heartImage[3].sprite = heartSprite[0];
                heartImage[4].sprite = heartSprite[0];
                break;
            case 3:
                heartImage[0].sprite = heartSprite[1];
                heartImage[1].sprite = heartSprite[1];
                heartImage[2].sprite = heartSprite[1];
                heartImage[3].sprite = heartSprite[0];
                heartImage[4].sprite = heartSprite[0];
                break;
            case 4:
                heartImage[0].sprite = heartSprite[1];
                heartImage[1].sprite = heartSprite[1];
                heartImage[2].sprite = heartSprite[1];
                heartImage[3].sprite = heartSprite[1];
                heartImage[4].sprite = heartSprite[0];
                break;
            case 5:
                heartImage[0].sprite = heartSprite[1];
                heartImage[1].sprite = heartSprite[1];
                heartImage[2].sprite = heartSprite[1];
                heartImage[3].sprite = heartSprite[1];
                heartImage[4].sprite = heartSprite[1];
                break;
        }
        
    }
}
