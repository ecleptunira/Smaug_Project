using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    public Animator anim;
    [SerializeField]
    public float speed;
    public GameObject lastDialogue;
    [SerializeField] ParticleSystem hurt;

    public int life;
    public int lifeCurrent;

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
        if(collision.gameObject.CompareTag("Enemy")){
            lifeCurrent -= 1;
            Destroy(collision.gameObject);
            doHurt();
            Sound.instance.soundPlayer.Play();

            if(lifeCurrent <= 0){
                Destroy(gameObject);
                lastDialogue.SetActive(true);
                Time.timeScale = 0f;
            }
           
        }

        
    }
}
