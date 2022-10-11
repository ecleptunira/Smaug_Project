using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    SpriteRenderer sprite;
    AudioSource shootFx;

    public GameObject bullet;
    public Transform spawnBullet;


    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        shootFx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
        Shoot();
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
