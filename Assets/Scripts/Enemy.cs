using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_enemyAnim : MonoBehaviour
{
    public float speed;
    public float checkRadius;
    public float attackRadius;
    private Vector3 dir;
  


    public bool shouldRotate;

    public LayerMask WhatIsPlayer;

    private Transform target;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;
    private bool isInChaseRange;
    private bool isInAttackRange;
    public static bool isAlive = true;






    [SerializeField] ParticleSystem effectDestroy;
    [SerializeField] ParticleSystem effectHurt;
    public int lifeEnemy = 2;
    public int lifeEnemyCurrent;
    public bool squirrel;


    private void Start()
    {
        rb  = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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

    public void DamageEnemy(int damaged){
        lifeEnemyCurrent -= damaged;
        doEffectHurt();
        
       if(lifeEnemyCurrent <= 0){
            doEffectDestroy();
            isAlive = false;
            Sound.instance.soundEnemy.Play();
            Destroy(this.gameObject,0.1f);
            
           
        }
        
    }
}
