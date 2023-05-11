using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public Animator anim;
    [SerializeField]
    public float speed;
    public GameObject lastDialogue;
    public GameObject Dialogue;
    public GameObject periquito;
    public GameObject father;
    
    public static int stockBulletBoss = 0;

    [SerializeField] ParticleSystem hurt;

    public static Player instance; 
    
    
    
    
    public int life;
    public int lifeCurrent;

    void Start(){
        
    }

    void Update()
    {   
        
       
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.magnitude);
        transform.position = transform.position + movement * speed * Time.deltaTime;

       
      
    }
    
     public void doHurt(){
        Instantiate(hurt,transform.position, transform.rotation);
    }

   
   
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Periquito") || collision.gameObject.CompareTag("Father")){
            Dialogue.SetActive(true);
            Time.timeScale = 0f;
        }
        if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Nut")){
            lifeCurrent -= 1;
            HealthBar.currentLife --;
            Destroy(collision.gameObject);
            doHurt();
            Sound.instance.soundPlayer.Play();

           
           
        }else if(collision.gameObject.CompareTag("Boss")){
            
            
            Debug.Log("oiii");
            Destroy(gameObject);
            // lastDialogue.SetActive(true);
            Time.timeScale = 0f;
        }

        
    }
}

