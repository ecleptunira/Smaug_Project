using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    Vector2 moveInput;
    Animator anim;

    public int lifeEnemy;
    public int lifeEnemyCurrent;

    //bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        transform.Translate(moveInput * Time.deltaTime * moveSpeed);

        //anim.SetBool("isMoving", (Mathf.Abs(moveInput.x) > 0 || Mathf.Abs(moveInput.y) > 0));
    }

    public void DamagePlayer(int damaged){
        lifeEnemyCurrent -= damaged;

        if(lifeEnemyCurrent <= 0){
            Destroy(gameObject);
        }
    }
}
