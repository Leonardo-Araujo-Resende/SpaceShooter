using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Configurar atributos")]
    [SerializeField] private float _speed;
    [SerializeField] private int vidas = 2;

    [Header("Outros Objetps")]
    [SerializeField] private GameObject thrustes;
    [SerializeField] private GameObject hurt;
    [SerializeField] private GameObject explosionAnim;

    //Privates
    private LivesManager scoreManager;
    private GameManeger gameManeger;

    void Start(){
        scoreManager = GameObject.Find("GameManager").GetComponent<LivesManager>();
        gameManeger = GameObject.Find("GameManager").GetComponent<GameManeger>();
        hurt.transform.localPosition = new Vector2(Random.Range(-0.25f,0.25f) ,Random.Range(0.25f,1.5f));
    }

    void Update()
    {   
        Damage();
        if(GameManeger.gameOver || scoreManager.isBossTime()){
            Instantiate(explosionAnim, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        float randomX = Random.Range(-7.1f, 7.1f);
        if(transform.position.y < -6.5) transform.position = new Vector3(randomX, 6, 0);
    }


    private void OnTriggerEnter2D(Collider2D other){


        if(other.tag == "Player"){

            Player player = other.GetComponent<Player>();
            player.Damage();
            Death();
        }

        if(other.tag == "Laser"){
            hurt.SetActive(true);
            thrustes.SetActive(false);
            Laser laser = other.GetComponent<Laser>();
            laser.Death();
            
            vidas --;
        }
    }

    private void Damage(){
        if(vidas <= 0){
            Death();
        }
    }

    private void Death(){
        Instantiate(explosionAnim, transform.position, Quaternion.identity);
        scoreManager.UpadeScore();
        Destroy(this.gameObject);
    }
}
