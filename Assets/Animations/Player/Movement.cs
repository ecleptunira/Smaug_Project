using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    public Animator anim;
    [SerializeField]
    public float speed;

    public int lifeEnemy;
    public int lifeEnemyCurrent;

    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.magnitude);
        transform.position = transform.position + movement * speed * Time.deltaTime;
    }

    public void DamagePlayer(int damaged){
        lifeEnemyCurrent -= damaged;

        if(lifeEnemyCurrent <= 0){
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Enemy")){
            Destroy(gameObject);
        }
    }
}
