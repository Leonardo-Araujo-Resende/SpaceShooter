using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    private float timer;



    void Update()
    {
        timer+= Time.deltaTime;
        spriteRenderer.color = Color.Lerp(Color.white, Color.red, timer/3);
        if(timer > 4) timer = 0;
    }
    

    private void OnTriggerStay2D(Collider2D other){

        if(other.tag == "Player"){
            Player player = other.GetComponent<Player>();
            player.Damage();
        }


    }

}
