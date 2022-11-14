using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    public Animator anim;
    [SerializeField]
    public float speed;
    public GameObject gameOver;

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

   
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.CompareTag("Enemy")){
            lifeCurrent -= 1;
            Destroy(collision.gameObject);

            if(lifeCurrent <= 0){
                Destroy(gameObject);
                gameOver.SetActive(true);
                Time.timeScale = 0f;
            }
           
        }

        
    }
}
