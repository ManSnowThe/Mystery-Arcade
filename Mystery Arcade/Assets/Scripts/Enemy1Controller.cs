using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controller : MonoBehaviour {

    // public float health = 100f;

    // For AI
    public float engageDistance = 10f;
    public float attackDistance = 2.7f;
    public Transform target;

    // Move
    public float moveSpeed = 5f;
    private bool facingLeft = true;

    // Components
    private Animator anim;
    private Rigidbody2D _rig;

    // Attack
    //public ManController manController;

    //private float attackDamage = 2f; // нужно доделать

    // Status
    public int curHealth = 6;

    // Attack
    public Collider2D attackTrigger;
    private bool attacking = false;
    private float attackTimer = 0;
    private float attackCd = 0.4f;

    bool damag = false;

    public void Awake()
    {
        attackTrigger.enabled = false;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        _rig = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        anim.SetBool("IsIdle", true);
        anim.SetBool("IsAttacking", false);
        anim.SetBool("IsRunning", false);


        if(Vector3.Distance(target.position, this.transform.position) < engageDistance)
        {
            anim.SetBool("IsIdle", false);

            Vector3 direction = target.position - this.transform.position;

            if (Mathf.Sign(direction.x) == 1 && facingLeft)
            {
                Flip();
            }
            else if(Mathf.Sign(direction.x) == -1 && !facingLeft)
            {
                Flip();
            }

            if (direction.magnitude >= attackDistance)
            {
                anim.SetBool("IsRunning", true);

                Debug.DrawLine(target.transform.position, this.transform.position, Color.yellow);

                if (facingLeft)
                {
                    this.transform.Translate(Vector3.left * (Time.deltaTime * moveSpeed));
                }
                else if (!facingLeft)
                {
                    this.transform.Translate(Vector3.right * (Time.deltaTime * moveSpeed));
                }

                attackTrigger.enabled = false;
            }

            if(direction.magnitude < attackDistance)
            {
                if (!attacking)
                {
                    Debug.DrawLine(target.transform.position, this.transform.position, Color.red);

                    attacking = true;
                    attackTimer = attackCd;

                    attackTrigger.enabled = true;
                    // anim.SetBool("IsAttacking", true);
                }
                if (attacking)
                {
                    if (attackTimer > 0)
                    {
                        attackTimer -= Time.deltaTime;
                    }
                    else
                    {
                        attacking = false;
                        attackTrigger.enabled = false;
                    }
                }
                anim.SetBool("IsAttacking", attacking);
                // Будет наносить урон игроку
                // manController.GetComponentInChildren<PlayerHealth>().curHealth -= attackDamage;
            }
        }


        else if(Vector3.Distance(target.position, this.transform.position) > engageDistance)
        {
            // nothing, just check result
            Debug.DrawLine(target.position, this.transform.position, Color.green);
        }

        if(curHealth <= 0)
        {
            StartCoroutine(Died());
        }
    }
    private void Flip()
    {
        facingLeft = !facingLeft;

        Vector3 theScale = transform.localScale;

        theScale.x *= -1;

        transform.localScale = theScale;
    }

    public void Damage(int damage)
    {
        if (!damag)
        {
            curHealth -= damage;
            gameObject.GetComponent<Animation>().Play("RedFlash");
            damag = true;
            Invoke("immort", 0.2f);
            // gameObject.GetComponent<Animation>.Play("Damage");
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

            if (!facingLeft)
            {
                // knocbackDir * -350
                _rig.AddForce(new Vector3(-150, knockbackPwr, transform.position.z));
            }
            if (facingLeft)
            {
                _rig.AddForce(new Vector3(150, knockbackPwr, transform.position.z));
            }
        }
        yield return 0;
    }
    public IEnumerator Died()
    {
        anim.Play("Dead");
        // anim.SetBool("Die", true);
        // yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<Enemy1Controller>().enabled = false;
        gameObject.GetComponentInChildren<EnemyTrigger>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
    void event123()
    {

    }
}
