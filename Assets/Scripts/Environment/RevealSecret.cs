using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RevealSecret : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        GetComponent<TilemapRenderer>().enabled = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
                anim.SetTrigger("Fade");
                Destroy(gameObject, 0.8f);
                break;
        }
    }
    
}
