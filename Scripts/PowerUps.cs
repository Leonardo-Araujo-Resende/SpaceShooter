using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField]
    private int _Id; // 0 TripleShot // 1 Speed // 2 Shiled
    [SerializeField]
    private float _speed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y < -6) Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other){

        if(other.tag == "Player"){

            Player player = other.GetComponent<Player>();

            if(_Id == 0) player.TripleShotOn();   
            if(_Id == 1) player.SpeedOn();
            if(_Id == 2) player.ShieldUp();

            Destroy(this.gameObject);

        }

        
    }

    
}
