using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrandSonUnidade : MonoBehaviour
{   
    [Header("Configurar ações")]
    public float maxLife;
    public float timeBetweenLaser;
    public float speed;
    public GameObject laser;

    private float rotation;
    private GameObject player;

    [Header("Outros objetos")]
    [SerializeField] private LivesManager livesManager;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    //Privates
    private float lifes;
    private Vector3 originalPosition;
    private BossGrandSons parent;


    void Start()
    {
        player = GameObject.Find("Player (Clone)");
        Shooting();
        livesManager = GameObject.Find("GameManager").GetComponent<LivesManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        lifes = maxLife;
        parent = GetComponentInParent<BossGrandSons>();
        originalPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Dash();
    }

    private void Dash(){

        if(player == null) Destroy(this.gameObject);

        if(parent.IsDash()){
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }else{
            transform.localPosition = originalPosition;
            Vector3 direcao = -transform.position + player.transform.position;
            rotation = Mathf.Atan2(direcao.y , direcao.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0,0,rotation+90);
        }



    }

    private void Shooting(){
        StartCoroutine(ShootRoutine());
    }

    private IEnumerator ShootRoutine(){
        while(true){
            yield return new WaitForSeconds(timeBetweenLaser);
            Instantiate(laser, transform.position + new Vector3(0,-1,0), Quaternion.Euler(0,0,rotation+90));
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
        
        spriteRenderer.color = Color.Lerp(Color.red, Color.white, lifes / maxLife);
        if(lifes <= 0){
            //Instantiate(explosion, transform.position, Quaternion.identity);
            livesManager.finishBossTime();
            Destroy(this.gameObject);
        }
    }
}
