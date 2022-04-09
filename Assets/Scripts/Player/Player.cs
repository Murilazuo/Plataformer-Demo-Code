using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("movevemnt")]
    [SerializeField]
    float speed;
    [SerializeField]
    private float jumpForce;
    float h;
    float run;
    float speedY;
    [SerializeField]
    bool inWall;
    float direction;

    [Header("Wall")]
    [SerializeField]
    float extraHeight;
    [SerializeField]
    LayerMask ground;
    LayerMask plataform;


    [Header("prefabs")]
    [SerializeField]
    GameObject collected;
    [SerializeField]
    GameObject appleProjectile;




    public GameObject oPlayer;

    public bool lookLeft;
    [SerializeField]
    bool hited = false;

    Rigidbody2D rig;
    Animator anim;
    CapsuleCollider2D capsCollider;
    GameManager gameManager;
    HUD hud;

    bool inWallJump;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        capsCollider = GetComponent<CapsuleCollider2D>();
        gameManager = FindObjectOfType(typeof(GameManager)) as GameManager;
        hud = FindObjectOfType(typeof(HUD)) as HUD;

        
        if (lookLeft)
        {
            Flip(-1);
        }
    }

    void Update()
    {
        if (gameManager.currentGameState == GameState.Gameplay)
        {

            Jump();
            Movement();
            PlayerAnimatior();
            Attack();
            Dead();
        }
        if (!inWall)
        {
            speedY = rig.velocity.y;
        }
        else
        {
            speedY = 0;
        }

    }
    private void FixedUpdate()
    {
        rig.velocity = new Vector2(run * h * speed * Time.deltaTime * 15, speedY);
        Physics2D.OverlapCircle(new Vector2(0, -1), 0.2f);
    }
    //Actions
    void Attack()
    {
        if(Input.GetButtonDown("Throw"))
        {
            if (gameManager.apple >= 1)
            {
            gameManager.apple--;
            Instantiate(appleProjectile, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>().AddForce(new Vector2(600 * direction, 50),0);
            }
        }
    }
    void Jump()
    {
        if (IsGround() && Input.GetButtonDown("Jump"))
        {

            rig.simulated = true;
            rig.AddForce(new Vector2(rig.velocity.x, jumpForce * 10));
        }
        if (gameManager.wallJump)
        {
            if (inWall && Input.GetButtonDown("Jump"))
            {
                StartCoroutine(nameof(JumpWall));
                h = transform.localScale.x * -1;
                Flip((int)h);
                inWall = false;
                rig.AddForce(new Vector2(jumpForce * h * 4, jumpForce * 12));
            }
        }
    }
    void Movement()
    {
        if(!inWallJump)
        {
            h = Input.GetAxis("Horizontal");
        }

        run = 1;
        if (gameManager.speedUpgrade && (Input.GetButton("Run")))
        {

            run = 1.5f;
        }
        else
        {
            run = 1;
        }
        if (!inWall)
        {
            if (h > 0)
            {
                rig.simulated = true;
                Flip(1);
            }
            else if (h < 0)
            {
                rig.simulated = true;

                Flip(-1);
            }
        }


    }
    private bool IsGround()
    {
        RaycastHit2D rayCastHit = Physics2D.Raycast(transform.position, Vector2.down, 1 + extraHeight, ground);

        Color rayColor;
        if (rayCastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(transform.position, Vector2.down * 1, rayColor);
        return rayCastHit.collider != null;
    }
    void Flip(int x)
    {
        lookLeft = !lookLeft;
        gameObject.transform.localScale = new Vector3(x, 1, 1);
        direction = x;
        
    }
    void Dead()
    {
        if (gameManager.playerDead)
        {
        anim.SetTrigger("Die");
            rig.simulated = false;
        }
    }
    //Animation
    void PlayerAnimatior()
    {
        if(rig.simulated == false)
        {
            anim.SetBool("Move", false);
        }

        if (rig.velocity.x == 0)
        {
            anim.SetBool("Move", false);
        }
        else
        {
            anim.SetBool("Move", true);
        }

        anim.SetFloat("SpeedY", speedY);
        anim.SetBool("Ground", IsGround());
        anim.SetBool("InWall", inWall);
    }
    void AnimationHit(float x, float y)
    {
        anim.SetTrigger("Hit");
        rig.AddForce(new Vector2(x, y * 4));
    }
    //Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Upgrade":
                switch (collision.GetComponent<Upgrade>().idUpgrade)
                {
                    case 0:
                         gameManager.wallJump = true; 
                        break;
                    case 1:
                        gameManager.speedUpgrade = true;
                        break;
                }
                GameObject effect = Instantiate(collected, collision.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
                Destroy(effect, 1);
                break;
            case "Tutorial0":
                gameManager.ActiveTutorial(0);
                Destroy(collision.gameObject);
                break;
            case "Tutorial1":
                gameManager.ActiveTutorial(1);
                Destroy(collision.gameObject);

                break;
            case "Tutorial2":
                gameManager.ActiveTutorial(2);
                Destroy(collision.gameObject);
                break;
            case "Tutorial3":
                gameManager.ActiveTutorial(3);
                Destroy(collision.gameObject);
                break;
            case "Tutorial4":
                gameManager.ActiveTutorial(4);
                Destroy(collision.gameObject);
                break;
            case "Objective":
                gameManager.win = true;
                break;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Spike":
                if (!hited)
                {
                    h = 0;
                    AnimationHit(rig.velocity.x, 250);
                    StartCoroutine(nameof(HitTime));
                }
                break;
            case "Apple":
                gameManager.apple++;
                GameObject effect = Instantiate(collected, collision.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
                Destroy(effect, 1);
                break;

            case "Destroy Player":
                gameManager.life = 0;
                break;



        }

    }
    //Collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {

            case "Wall":
                if (!IsGround())
                {
                    StartCoroutine(nameof(HoldWall));
                }
                break;
            
            case "Enemy":
                if (!hited)
                {
                    float directionY = 150;
                    float directionX = directionY * rig.velocity.x * -1;
                    h = 0;
                    AnimationHit(directionX, directionY);
                    StartCoroutine(nameof(HitTime));
                }
                break;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {
            case "Wall":
                StopCoroutine(nameof(HoldWall));
                inWall = false;
                break;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {
         case "Plataform":
                rig.simulated = false;
                transform.parent = collision.transform;
                anim.SetBool("Move", false);
                break;
        }
    }
    //IEnumerator
    IEnumerator HoldWall()
    {
        inWall = true;
        yield return new WaitForSeconds(2);
        inWall = false;
    }
    IEnumerator HitTime()
    {
        hited = true;
        gameManager.life--;
        yield return new WaitForSeconds(1);
        hited = false;
        
    }
    IEnumerator JumpWall()
    {
        inWallJump = true;
        yield return new WaitForSeconds(0.5f);
        inWallJump = false;
    }
}

