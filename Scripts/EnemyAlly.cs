using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAlly : MonoBehaviour
{
    
    [SerializeField] private  float speed;
    [SerializeField] private GameObject explosionAnim;
    private GameManeger gameManeger;

    // Update is called once per frame

    void Start(){
        gameManeger = GameObject.Find("GameManager").GetComponent<GameManeger>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        float randomX = Random.Range(-7.1f, 7.1f);
        if(transform.position.y < -6.5) transform.position = new Vector3(randomX, 6, 0);
        if(GameManeger.gameOver) Destroy(this.gameObject);

    }
    private void OnTriggerEnter2D(Collider2D other){


        if(other.tag == "Player"){

            Player player = other.GetComponent<Player>();
            player.Damage();
            Instantiate(explosionAnim, transform.position, Quaternion.identity);
            Destroy(this.gameObject);

        }

        if(other.tag == "Laser"){
            GameObject nave = Instantiate(explosionAnim, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        
    }
}
