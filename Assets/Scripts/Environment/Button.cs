using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    Animator anim;

    [SerializeField]
    GameObject bridge;
    [SerializeField] bool openMode;
    string idAnimation;

    bool open = false;

    float blend;

    void Start()
    {
        if(openMode)
        {
            idAnimation = "Open";
            blend = 0;
        }
        else
        {
            idAnimation = "Open";
            blend = 1;
        }
        anim = GetComponent<Animator>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!open)
        {
            open = true;
            bridge.GetComponent<Animator>().SetFloat("Blend", blend);
            bridge.GetComponent<Animator>().SetBool(idAnimation, true);


            anim.SetTrigger("Active");
        }
    }

}
