using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserBoss : MonoBehaviour
{
    [SerializeField] private float _speed;

    void Start(){
        Destroy(this.gameObject,4f);
    }
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
       
    }

    private void OnTriggerEnter2D(Collider2D other){


        if(other.tag == "Player"){
            Player player = other.GetComponent<Player>();
            player.Damage();
            Destroy(this.gameObject);
        }
    }
}   

