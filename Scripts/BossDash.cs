using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDash : MonoBehaviour
{

    public BossEnemy boss;
    public float timeBetweenDash;
    private float timer;

    void Update(){
        timer += Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D other){

        if(other.tag == "Player" ){
            if(Random.Range(1,5) == 2 && timer > timeBetweenDash){
                 boss.DashUp();
                 timer = 0;
            }
        }
    }
}
