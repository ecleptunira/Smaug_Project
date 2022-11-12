using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] AudioClip deadFx;
    [SerializeField] ParticleSystem effect;

    GameObject player;
    Animator anim;
    AudioSource enemyFx;

    public int lifeEnemy;
    public int lifeEnemyCurrent;
    public int doDamage;
    

    bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponentInChildren<Animator>();
        enemyFx = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && isAlive)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

   private void OnCollisionEnter2D(Collision2D collision)
    {   
        if(collision.gameObject.CompareTag("Player")){
            collision.gameObject.GetComponent<PlayerController>().DamagePlayer(doDamage);
        } 
    }

    public void doEffect(){
        Instantiate(effect,transform.position, transform.rotation);
    }

    public void DamageEnemy(int damaged){
        lifeEnemyCurrent -= damaged;

        if(lifeEnemyCurrent <= 0){
            anim.SetTrigger("Dead");
            isAlive = false;
            enemyFx.PlayOneShot(deadFx);
            Destroy(this.gameObject);
        }
    }
}
