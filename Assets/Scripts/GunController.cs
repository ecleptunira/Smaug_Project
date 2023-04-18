using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{   
    
    public GameObject bullet;
    public float firerate;
    float nextfire;
    [SerializeField]
    public Animator anim;
    public bool whip;


    [SerializeField]
    SpriteRenderer sprite;
    

   
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
       
     
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 1){
            Aim();
            Shoot();
            
        }
    }

    void Shoot() 
    {
        if(Time.time > nextfire)
        {
            if (Input.GetButtonDown("Fire1"))
            {   if(whip){
                 anim.SetTrigger("Whip");
            }
               
                nextfire = Time.time + firerate;
                Instantiate(bullet, transform.position,transform.rotation);
                Sound.instance.soundAttack.Play();
            }
        }
        
    }

    void Aim() {
        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);

        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);

        float angle = Mathf.Atan2(offset.y,offset.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);

        sprite.flipY = (mousePos.x < screenPoint.x);
    }
}
