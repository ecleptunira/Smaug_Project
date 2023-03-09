using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    // [SerializeField] ParticleSystem effect;
    public int doDamage;

    

    // Update is called once per frame
    void Update()
    {
       transform.Translate(Vector3.right * Time.deltaTime * speed); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if(collision.gameObject.CompareTag("Enemy")){
            collision.gameObject.GetComponent<Movement_enemyAnim>().DamageEnemy(doDamage);
            // Instantiate(effect,transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
        
        
    }
}
