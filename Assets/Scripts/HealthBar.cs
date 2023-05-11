using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public int maxLife;
    public static int currentLife;

    // Start is called before the first frame update
    void Start()
    {
        currentLife = 10;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(currentLife * 100/maxLife, 23, 1);

        
    }

    
}
