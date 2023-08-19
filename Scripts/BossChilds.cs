using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChilds : MonoBehaviour
{

    [Header("Configurar acoes")]

    public float maxLife;
    public float speed;
    public bool movingRight = true;
    

    [Header("Outros objetos")]
    [SerializeField] private AudioSource reloadingLaser;
    [SerializeField] private AudioSource laserSound;
    [SerializeField] private GameObject bossGrandSons;
    [SerializeField] private GameObject laser;

    [Header("Outros objetos")]   
    private LineRenderer lr;

    //Privates
    private float lifes;
    private bool isLaserBeam;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    void Start()
    {
        Shooting();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        lifes= maxLife;
        lr = GetComponent<LineRenderer>();
        lr.SetPosition(0, new Vector3(0,0,0));
        lr.SetPosition(1, new Vector3(0,-5,0));
        LaserBeam();
    }

    // Update is called once per frame
    void Update()
    {
        Moviment();
        if(GameManeger.gameOver) Destroy(this.gameObject);
        LinhaBeam();
    }



    private void Moviment(){
        if(movingRight){
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else{
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if(transform.position.x >= 7.2f) {
            movingRight = false;
        }
        if(transform.position.x <= -7.2f){
            movingRight = true;
        }


        if(transform.position.y < 3.2) transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void Shooting(){
        StartCoroutine(ShootRoutine());
    }

    private IEnumerator ShootRoutine(){
        while(true){
            yield return new WaitForSeconds(1);
            Instantiate(laser, transform.position + new Vector3(0,-1,0), Quaternion.identity);
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
        
        spriteRenderer.color = Color.Lerp(Color.red, Color.green, lifes / maxLife);
        if(lifes <= 0){
            //Instantiate(explosion, transform.position, Quaternion.identity);
            Instantiate(bossGrandSons, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    private void DamageColorChange(){
        StartCoroutine(DamageRoutine());
    }

    private IEnumerator DamageRoutine(){
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = originalColor;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = originalColor;
    }

    

    private void LaserBeam(){
        StartCoroutine(LaserBeamRoutine());
    }

    private IEnumerator LaserBeamRoutine(){
        yield return new WaitForSeconds(2);
        while(true){
            reloadingLaser.Play();
            yield return new WaitForSeconds(1);
            isLaserBeam = true;
            laserSound.Play();
            yield return new WaitForSeconds(4);
            isLaserBeam = false;
            yield return new WaitForSeconds(3);
        }
    }



    private void LinhaBeam()
    {
        if(isLaserBeam){

            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position + new Vector3(0,-2,0) , Vector3.down);

            if (raycastHit2D && raycastHit2D.collider.gameObject.CompareTag("Player")){


                Player player = raycastHit2D.collider.gameObject.GetComponent<Player>();
                player.Damage();
                float temp = -(transform.position.y - raycastHit2D.collider.gameObject.transform.position.y);

                if(temp < -12) temp = -12;
                lr.SetPosition(1, new Vector3(0, temp, 0));


            }
            else{
                lr.SetPosition(1, new Vector3(0,-12,0));
            }


        }
        else{
            lr.SetPosition(0, new Vector3(0, 0, 0));
            lr.SetPosition(1, new Vector3(0, 0, 0));
        }


    }





}
