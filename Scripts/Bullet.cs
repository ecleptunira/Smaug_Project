using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] ParticleSystem effect;

    

    // Update is called once per frame
    void Update()
    {
       transform.Translate(Vector3.right * Time.deltaTime * speed); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        Instantiate(effect,transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
