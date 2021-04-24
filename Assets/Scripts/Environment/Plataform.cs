using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataform : MonoBehaviour
{
    Rigidbody2D rig;
    [SerializeField]
    float speed;
    [SerializeField]
    bool leftToRight;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (leftToRight)
        {
        rig.velocity = new Vector2(speed , 0);
        }
        else
        {
            rig.velocity = new Vector2(0, speed);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Plataform Limit"))
        {
            speed *= -1;
        }
    }

}
