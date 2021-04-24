using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rig;
    Animator anim;

    [SerializeField] GameObject appleDrop;
    public bool lookLeft;
    [SerializeField]    bool idle;
    [SerializeField]    float baseSpeed;
    float speed;
    
    public float life;
    float lifeCheck;
    bool dead = false;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if (!idle)
        {
        StartCoroutine(nameof(RoutineA));
        }
        lifeCheck = life;
    }

    void Update()
    {
        Movement();
        if(life <= 0 && !dead)
        {
            dead = true;
            rig.simulated = false;
            anim.SetTrigger("Die");
            Instantiate(appleDrop, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
            Destroy(gameObject, 2);

        }
    }
    private void FixedUpdate()
    {
        rig.velocity = new Vector2(speed, rig.velocity.y);
    }
    //Action
   
    void Movement()
    {
        if (idle)
        {
            anim.SetBool("Move", false);
            speed = 0;
        }
        else
        {
            anim.SetBool("Move", true);
            speed = baseSpeed;

        }
    }
    void Flip()
    {
        float x;
        lookLeft = !lookLeft;
        if (lookLeft)
        {
            x = -1;
        }
        else
        {
            x = 1;
        }
        gameObject.transform.localScale = new Vector3(x, 1, 1);
    }
    //Collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {
            case "Wall":
                baseSpeed *= -1;
                Flip();
                break;
            case "Plataform Limit":
                baseSpeed *= -1;
                Flip();
                break;
            case "AppleProjectiles":
                float x;
                if(transform.position.x > collision.transform.position.x)
                {
                    x = 1;
                }
                else
                {
                    x = -1;
                }
                life--;
                rig.AddForce(new Vector2(x * 250, 400));
                anim.SetTrigger("Hit");
                break;


        }
    }
    //Ienumerator

    IEnumerator RoutineA()
    {
        while (true)
        {
        yield return new WaitForSeconds(Random.Range(3,5));
        int random = Random.Range(1, 100);
        if(random <= 50)
        {
            idle = false;
        }
        else
        {
            idle = true;
        }
        }
    }

}
