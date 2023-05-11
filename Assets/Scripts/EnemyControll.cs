using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class EnemyControll : MonoBehaviour
{   
    public static EnemyControll  instance; 
    public float speed;
    public float checkRadius;
    public float attackRadius;
    private Vector3 dir;
    public bool scene2;
   
 
        


    public bool shouldRotate;

    public LayerMask WhatIsPlayer;

    private Transform target;
    private Rigidbody2D rb;
    private BoxCollider2D col;
    public Animator anim;
    private Vector2 movement;
    private bool isInChaseRange;
    private bool isInAttackRange;
    public static bool isAlive = true;
    




    [SerializeField] ParticleSystem effectDestroy;
    [SerializeField] ParticleSystem effectHurt;
    public  int lifeEnemy = 3;
    public  static int lifeEnemyCurrent = 3;
    public int doDamage = 1;
   


  
    private void Start()
    {
        rb  = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
        target = GameObject.FindWithTag("Player").transform;
        
    }

    private void Update()
    {
        //anim.SetBool("isRunning", isInChaseRange);
        if (target != null && isAlive)
        {   
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, WhatIsPlayer);
            isInAttackRange = Physics2D.OverlapCircle(transform.position, checkRadius, WhatIsPlayer);

            dir = target.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            dir.Normalize();
            movement = dir;
            if(shouldRotate)
            {
                anim.SetFloat("X", dir.x);
                anim.SetFloat("Y", dir.y);
            }
        }

      

        
    }

    private void FixedUpdate()
    {
        if(isInChaseRange && !isInAttackRange)
        {
            MoveCharacter(movement);
        }
        if (isInAttackRange)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void MoveCharacter(Vector2 dir)
    {
        rb.MovePosition((Vector2)transform.position + (dir * speed * Time.deltaTime));
    }

   

    public void doEffectDestroy(){
        Instantiate(effectDestroy,transform.position, transform.rotation);
    }

    public void doEffectHurt(){
        Instantiate(effectHurt,transform.position, transform.rotation);
    }

    void LoadScene(){
        SceneManager.LoadScene("Scene1.1");
    }

    

    public void DamageEnemy(int damaged){
        lifeEnemyCurrent -= damaged;
        doEffectHurt();
        Player.stockBulletBoss ++;
        

        if(lifeEnemyCurrent <= 0){
            
            doEffectDestroy();
                isAlive = false;
                Sound.instance.soundEnemy.Play();
                col.enabled = false;
                anim.SetTrigger("Dead");
                Invoke("LoadScene", 1.5f);
            }
    }
}
