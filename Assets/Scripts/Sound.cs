using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    public static Sound  instance; 
    private bool audioOn = true;
    public AudioSource soundPlayer, soundEnemy, soundAttack;
    public Sprite[] clickedSprite;
    private Image buttonImage;
    // Start is called before the first frame update
    void Awake(){
        instance = this;
    }
    void Start()
    {
        buttonImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void VolumeGame(){
        audioOn = !audioOn;
        if(audioOn){
            AudioListener.volume = 1;
            buttonImage.sprite = clickedSprite[0];
        } else{
            AudioListener.volume = 0;
            buttonImage.sprite = clickedSprite[1];
        }
    }
}
