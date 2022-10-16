using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{   

    [SerializeField]
    private Sprite[] images;

    [SerializeField]
    private int x = 1;
    [SerializeField]
    SpriteRenderer sprite;
    AudioSource shootFx;

    public GameObject bullet;
    public Transform spawnBullet;


    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = images[1];
        shootFx = GetComponent<AudioSource>();
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
        }
    }

    void Shoot() {
        if (Input.GetButtonDown("Fire1")){
            Instantiate(bullet, spawnBullet.position, transform.rotation);
            shootFx.Play();
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
