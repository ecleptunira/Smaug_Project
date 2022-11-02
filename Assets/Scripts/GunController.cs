using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{   
   
    public BoxCollider2D box;
    
    public GameObject swordAttack;
    [SerializeField]
    private Sprite[] images;

    [SerializeField]
    private int x = 1;
    [SerializeField]
    SpriteRenderer sprite;
    AudioSource shootFx;

    public GameObject bullet;
    public Transform spawnBullet;
    int cooldown = 1;


    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = images[1];
        shootFx = GetComponent<AudioSource>();
        box = GetComponent<BoxCollider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
        GunShoot();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(x == 0)
            {
                x = 1;
                
            }
            else if (x == 1)
            {
                x = 2;
                
            }
            else {
                x = 0;
                
            }
            sprite.sprite = images[x];
        }
    }

    void GunShoot() {
        if (x == 0) {
            Shoot();
            box.enabled = false;
        }

        if (x == 1 || x == 2) {
            StartCoroutine("Sword");
            box.enabled = true;
        }
    }

    void Shoot() {
        if (Input.GetButtonDown("Fire1")){
            Instantiate(bullet, spawnBullet.position, transform.rotation);
            shootFx.Play();
        }
    }

    IEnumerator Sword() {
       
        if (Input.GetButtonDown("Fire1")){
            if (cooldown == 1){
                StartCoroutine("SwordAttack");
                cooldown = 0;
            }else{
                yield return new WaitForSeconds(2f);
                StartCoroutine("SwordAttack");
            }
        }
    }

    IEnumerator SwordAttack() {

        swordAttack.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        swordAttack.SetActive(false);
        yield return new WaitForSeconds(0.5F);
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
