using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{   
    // public BoxCollider2D box;
    public GameObject bullet;
    public float firerate;
    float nextfire;
    // public GameObject swordAttack;

    [SerializeField]
    SpriteRenderer sprite;
    

    // int cooldown = 1;


    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
       
        // box = GetComponent<BoxCollider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 1){
            Aim();
            Shoot();
            // StartCoroutine("Sword");
        }
    }

    void Shoot() 
    {
        if(Time.time > nextfire)
        {
            if (Input.GetButtonDown("Fire1"))
            {   
                nextfire = Time.time + firerate;
                Instantiate(bullet, transform.position,transform.rotation);
                Sound.instance.soundAttack.Play();
            }
        }
        
    }

    // IEnumerator Sword() {
       
    //     if (Input.GetButtonDown("Fire1")){
    //         if (cooldown == 1){
    //             StartCoroutine("SwordAttack");
    //             cooldown = 0;
    //         }else{
    //             yield return new WaitForSeconds(0.1f);
    //             StartCoroutine("SwordAttack");
    //         }
    //     }
    // }

    // IEnumerator SwordAttack() {

    //     swordAttack.SetActive(true);
    //     yield return new WaitForSeconds(0.2f);
    //     swordAttack.SetActive(false);
        
    // }

    void Aim() {
        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);

        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);

        float angle = Mathf.Atan2(offset.y,offset.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);

        sprite.flipY = (mousePos.x < screenPoint.x);
    }
}
