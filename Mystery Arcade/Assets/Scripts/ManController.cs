using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManController : MonoBehaviour
{
    public float topSpeed = 10f;
    bool facingRight = true;

    Animator anim;

    bool grounded = false;

    public Transform groundCheck;

    float groundRadius = 0.1f;

    public float jumpForce = 50f;

    public LayerMask whatIsGround;

    private Rigidbody2D _rig;

    // Status
    public int curHealth;
    public int maxHealth = 6;

    bool damaged = false;
    // float timeToRun = 5f;

    //Timed Immortality
    /*
    private float attackTimer = 0;
    private float attackCd = 0.7f;
    bool damagedd = true;
    */
    bool damag = false;

    // for jump
    float jumpValue = 0f;
    float maxJumpValue = 5f;

    bool item = false;

    // Чтобы остановить движение
    // public RigidbodyConstraints2D rigc;

    private Enemy1Controller enemy;
    bool ignore = true;

    void Start()
    {
        _rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        curHealth = maxHealth;

        //Игнорирование коллайдера противника
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy1Controller>();
        Collider2D[] c2dP = this.GetComponents<Collider2D>();
        //Collider2D[] c2dE = enemy.GetComponents<Collider2D>();
        Physics2D.IgnoreCollision(c2dP[0], enemy.GetComponent<CapsuleCollider2D>(), ignore);
        Physics2D.IgnoreCollision(c2dP[1], enemy.GetComponent<CapsuleCollider2D>(), ignore);
    }

    private void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        anim.SetBool("Ground", grounded);

        anim.SetBool("Damage", damaged);

        anim.SetFloat("vSpeed", _rig.velocity.y);

        float move = Input.GetAxis("Horizontal");

        _rig.velocity = new Vector2(move * topSpeed, _rig.velocity.y);

        anim.SetFloat("Speed", Mathf.Abs(move));

        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();

        // Handle Jump
        /*
        if (grounded)
        {
            grounded = false;
            Jump();
        }
        */

        if(grounded && Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }
        if(item && Input.GetKeyDown(KeyCode.UpArrow))
        {
            _rig.velocity = Vector2.zero;
            dJump();
        }

        /*if(grounded && Input.GetKey(KeyCode.UpArrow))
        {
            JumpL();
        }*/

        if (curHealth >= maxHealth)
        {
            curHealth = maxHealth;
        }
        if (curHealth <= 0)
        {
            Die();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;

        theScale.x *= -1;

        transform.localScale = theScale;
    }
    void Jump()
    {
        if (grounded)
        {
            item = false;
            grounded = false;
            _rig.AddForce(new Vector2(_rig.velocity.x, jumpForce));
            /*jumpForce -= Time.deltaTime;
            if (jumpForce < 0f)
                jumpForce = 0f;*/
            anim.SetBool("Ground", false);
        }
        /*
        if (item)
        {
            item = false;
            grounded = false;
            _rig.AddForce(new Vector2(_rig.velocity.x, jumpForce));
            // jumpForce -= Time.deltaTime;
            // if (jumpForce < 0f)
                // jumpForce = 0f;
            anim.SetBool("Ground", false);
        }*/
    }
    void dJump()
    {
        item = false;
        grounded = false;
        _rig.AddForce(new Vector2(_rig.velocity.x, jumpForce));
        // jumpForce -= Time.deltaTime;
        // if (jumpForce < 0f)
        // jumpForce = 0f;
        anim.SetBool("Ground", false);
    }


    // Handle Jump
    // Set jumpForce to 20
    /*
    void JumpD()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // grounded = false;
            _rig.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
            anim.SetBool("Ground", false);
            // Jump();
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            // grounded = false;
            if (jumpValue < maxJumpValue)
            {
                jumpValue = 0f;
                // jumpValue += Time.time;
                _rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
                anim.SetBool("Ground", false);
            }
        }
    }*/


    /*void JumpL()
    {
        if (grounded)
        {
            grounded = false;
            _rig.AddForce(new Vector2(_rig.velocity.x, jumpForce * 2));
            anim.SetBool("Ground", false);
        }
    }*/

    void Die()
    {
        // gameObject.GetComponent<ManController>().enabled = false;
        // anim.Play("Die");
        StartCoroutine(Died());
        // SceneManager.LoadScene("ma1");
    }
    public void Damage(int dmg)
    {
        if (!damag)
        {
            curHealth -= dmg;
            StartCoroutine(Damaged());
            anim.SetBool("Damage", damaged);
            damag = true;
            Invoke("immort", 1.5f);
        }
    }
    void immort()
    {
        damag = false;
    }


    public IEnumerator Knockback(float knockDur, float knockbackPwr, Vector3 knockbackDir)
    {
        float timer = 0;

        while (knockDur > timer)
        {
            timer += Time.deltaTime;
            _rig.velocity = new Vector2(0, 0);

            if (!facingRight)
            {
                // knocbackDir * -350
                _rig.AddForce(new Vector3(-250, knockbackPwr, transform.position.z));
            }
            if (facingRight)
            {
                _rig.AddForce(new Vector3(250, knockbackPwr, transform.position.z));
            }
        }
        yield return 0;
    }

    public IEnumerator Damaged()
    {
        damaged = true;
        yield return new WaitForSeconds(0.25f);
        damaged = false;
    }

    /*public IEnumerator Damagedd()
    {
        damagedd = false;
        yield return new WaitForSeconds(1f);
        damagedd = true;
    }*/


    public IEnumerator Died()
    {
        anim.Play("Die");
        // anim.SetBool("Die", true);
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<ManController>().enabled = false;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("ma1");
    }

    public IEnumerator Boost()
    {
        item = true;
        yield return new WaitForSeconds(0.5f);
        item = false;
    }
    void event123()
    {

    }
}
