using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossEnemy : MonoBehaviour
{

    [Header("Configurar acoes")]
    public float lifes;
    public float maxLifes;
    public int numberBullets;
    public bool movingRight;
    public float speed;
    public float timeBetweenFire;
    public float timeBetweenAlly;
    public float dashSpeed;


    private bool dashing = false; 
    private bool goingDown = true; 

    //Privates primitivos
    private float timerShoot;  
    private float timerAlly;
    private bool canBulletAtack = false;

    [Header("Outros objetos")]
    [SerializeField] private GameObject laser;
    [SerializeField] private GameObject bossChild;
    [SerializeField] private GameObject allys;
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject[] boost;
    [SerializeField] private GameObject bullet;
    
    //Privates
    private Player player;
    private SpriteRenderer spriteRenderer;
    private LivesManager livesManager;
    private GameManeger gameManeger;
    
    void Update()
    {
        Moviment();
        Shooting();
        if(GameManeger.gameOver) Destroy(this.gameObject);
        Allys();
        BulletAtack();
    }

    void Start(){
        livesManager = GameObject.Find("Canvas").GetComponent<LivesManager>();
        gameManeger = GameObject.Find("GameManager").GetComponent<GameManeger>();
        player = GameObject.Find("Player (Clone)").GetComponent<Player>();
        dashing = false;
        goingDown = true;
        lifes = maxLifes;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Moviment(){

        if(dashing){
            if(goingDown){
                transform.Translate(Vector3.down * (speed+dashSpeed) * Time.deltaTime);
                boost[0].SetActive(true);
                boost[1].SetActive(true);
            }
            if(!goingDown){
                transform.Translate(Vector3.up * (speed+dashSpeed) * Time.deltaTime);
                boost[0].SetActive(false);
                boost[1].SetActive(false);
            }
            if(transform.position.y < -4) goingDown = false;
            if(transform.position.y > 3.2f && goingDown == false){
                dashing = false;
                goingDown = true;
                speed /= 1.85f;
            } 
        }

        else{

            if(movingRight){
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
            else{
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }

            if(transform.position.x >= 5.5f) {
                movingRight = false;
                canBulletAtack = true;
            }
            if(transform.position.x <= -5.5f){
                movingRight = true;
                canBulletAtack = true;
            }
        }

    }

    private void Shooting(){

        timerShoot += Time.deltaTime;

        if(timerShoot >= timeBetweenFire){
            Instantiate(laser, transform.position + new Vector3(-0.6f,-1.7f,0), Quaternion.identity);
            Instantiate(laser, transform.position + new Vector3(0.6f,-1.7f,0), Quaternion.identity); 
            timerShoot = 0;
        }

    }

    private void OnTriggerEnter2D(Collider2D other){


        if(other.tag == "Player"){
            Player player = other.GetComponent<Player>();
            player.killPLayer();
            Destroy(this.gameObject);
        }

        if(other.tag == "Laser"){
            Damage();
            Laser laser = other.GetComponent<Laser>();
            laser.Death();
        }
    }

    private void Damage(){
        lifes --;
        DamageColorChange();
        if(lifes <= 0){


            GameObject aux;

            Instantiate(bossChild, transform.position, Quaternion.identity);
            aux = Instantiate(bossChild, transform.position, Quaternion.identity);
            
            BossChilds aux2 = aux.GetComponent<BossChilds>();
            aux2.movingRight = true;


            Instantiate(explosion, transform.position, Quaternion.identity);

            Destroy(this.gameObject);
        }
    }

    private void Allys(){
        timerAlly += Time.deltaTime;

        if(timerAlly > timeBetweenAlly){
            timerAlly = 0;
            Instantiate(allys, new Vector3(7.1f, 6, 0), Quaternion.identity);
            Instantiate(allys, new Vector3(-7.1f, 6, 0), Quaternion.identity);
        }


    }   

    public void DashUp(){
        dashing = true;
        speed *= 1.85f;
    }

    public void BulletAtack(){ 

        int aux = -90;
        if(transform.position.x < 0.2f && transform.position.x > -0.2f && canBulletAtack){

            canBulletAtack = false;

            for(int i = 0; i < numberBullets; i++){

                Instantiate(bullet, transform.position + new Vector3(0,-2,0), Quaternion.Euler(0,0,aux));
                aux += 180/(numberBullets-1);

            }
            if(movingRight)numberBullets ++;
        }
    }

    private void DamageColorChange(){
        StartCoroutine(DamageRoutine());
    }

    private IEnumerator DamageRoutine(){
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = Color.yellow;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = Color.yellow;
    }
}
