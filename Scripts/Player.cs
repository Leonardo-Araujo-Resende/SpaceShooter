using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour

{
    [Header("Configurar atributos")]
    [SerializeField] private float speedDash;
    [SerializeField] private float _speed;
    [SerializeField] private float _timeBetweenFire;
    [SerializeField] private float speedMouseControl;
    
    
    //Privates
    private float _timer;
    private bool _canSpeedUp;
    private bool _canTripleShot;
    private bool isShield;
    
    

    [Header("Audios")]
    [SerializeField] private AudioSource laserAudio;
    [SerializeField] private AudioSource powerUpAudio;

    [Header("Outros Objetps")]
    [SerializeField] private GameObject[] damages;
    [SerializeField] private PlayerAnimator playerAnim;
    [SerializeField] private GameObject _laser;
    [SerializeField] private GameObject laserTripleShot;
    [SerializeField] private GameObject shield;
    [SerializeField]private GameObject playerExplosion;

    [Header("Variaveis teste")]
    [SerializeField] private bool canTakeDamage;

    //Privates
    private int life = 3;
    private float horizontal;
    private float vertical;
    private LivesManager lifeSprite;
    private GameManeger gameManeger;
    private float timerDash = 1;
    private SpriteRenderer spriteRenderer;
    private int formaControlarNave; //1 teclado // 2 mouse


//-----------------------------------------------------------------------------------------------------------------------------------

    void Start(){

        lifeSprite = GameObject.Find("GameManager").GetComponent<LivesManager>();
        lifeSprite.UpdateLife(life);
        gameManeger = GameObject.Find("GameManager").GetComponent<GameManeger>();
        laserAudio = GetComponent<AudioSource>();
        // dataUpdatesSpeed = GameObject.Find("BotaoSpeed").GetComponent<Button>();
        // dataUpdatesTiro = GameObject.Find("BotaoTiro").GetComponent<Button>();
        
        // _timeBetweenFire -= dataUpdatesTiro.GetSubtraiCadencia();
        // _speed += dataUpdatesSpeed.GetAumentaSpeed();
        // dataUpdatesTiro.gameObject.SetActive(false);
        // dataUpdatesSpeed.gameObject.SetActive(false);
        spriteRenderer = GetComponent<SpriteRenderer>();
        canTakeDamage = true;
        
    }

//-----------------------------------------------------------------------------------------------------------------------------------
    void Update()
    {

        if(formaControlarNave == 1) MovimentTeclado();
        if(formaControlarNave == 2) MovimentMouse();
        Shooting();
        PlayerAnimation();
        Dash();

        
    }

//-----------------------------------------------------------------------------------------------------------------------------------

    private void MovimentTeclado(){

        
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        
        if(!_canSpeedUp){
            transform.Translate(Vector3.up * _speed * vertical * Time.deltaTime);
            transform.Translate(Vector3.right * _speed * horizontal * Time.deltaTime);
        }else{
            transform.Translate(Vector3.up * _speed * 2 * vertical * Time.deltaTime);
            transform.Translate(Vector3.right * _speed * 2 *horizontal * Time.deltaTime);
        }

        if(transform.position.x > 9) transform.position = new Vector3(9, transform.position.y,0);
        if(transform.position.x < -9) transform.position = new Vector3(-9, transform.position.y,0);

        if(transform.position.y > 5) transform.position = new Vector3(transform.position.x, 5,0);
        if(transform.position.y < -5) transform.position = new Vector3(transform.position.x, -5,0);

        

    }

    private void MovimentMouse(){

        Camera mainCamera = Camera.main;
        Vector3 mousePos = Input.mousePosition;
        mousePos = mainCamera.ScreenToWorldPoint(mousePos);

        Vector3  targetPosition = new Vector3(mousePos.x, mousePos.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, speedMouseControl * Time.deltaTime);
    }

    public void SetControleNave(int a){
        formaControlarNave = a;
    }
    
//-----------------------------------------------------------------------------------------------------------------------------------
    private void Shooting(){

        _timer += Time.deltaTime;

        if((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0) )&& _timer > _timeBetweenFire){

            laserAudio.Play();
            
            if( _canTripleShot){
                Instantiate(_laser, transform.position + new Vector3(0.64f,0,0), Quaternion.Euler(0,0,90));
                Instantiate(_laser, transform.position + new Vector3(-0.64f,0,0), Quaternion.Euler(0,0,90)); 
            }
            else{
                Instantiate(_laser, transform.position + new Vector3(0,0.5f,0), Quaternion.Euler(0,0,90));
            }

            _timer = 0;
        }
               
        
    }


//-----------------------------------------------------------------------------------------------------------------------------------
    public void TripleShotOn(){
        StartCoroutine(TripleShotRoutine());
    }
    private IEnumerator TripleShotRoutine(){
        powerUpAudio.Play();
        _canTripleShot = true;
        yield return new WaitForSeconds(5);
        _canTripleShot = false;
    }

//-----------------------------------------------------------------------------------------------------------------------------------

    public void SpeedOn(){
        StartCoroutine(SpeedRoutine());
    }
    private IEnumerator SpeedRoutine(){
        powerUpAudio.Play();
        _canSpeedUp = true;
        yield return new WaitForSeconds(5);
        _canSpeedUp = false;
    }

//-----------------------------------------------------------------------------------------------------------------------------------

    public void Damage(){

        if(isShield){
            isShield = false;
            shield.gameObject.SetActive(false);
            DamageColorChange();
            return;
        }

        if(canTakeDamage){
            life --;
            DamageColorChange();
            if(life==2) damages[Random.Range(0, 2)].SetActive(true);
            if(life==1){
                damages[1].SetActive(true);
                damages[0].SetActive(true);
            } 

            lifeSprite.UpdateLife(life);

            if(life <= 0){
                Instantiate(playerExplosion, transform.position, Quaternion.identity);
                gameManeger.MenuUp();
                Destroy(this.gameObject);
            }
        }
        
    }

//-----------------------------------------------------------------------------------------------------------------------------------

    public void ShieldUp(){
        StartCoroutine(ShieldRoutine());
    }

    private IEnumerator ShieldRoutine(){
        shield.gameObject.SetActive(true);
        powerUpAudio.Play();
        isShield = true;
        yield return new WaitForSeconds(10);
        shield.gameObject.SetActive(false);
        isShield = false;
    }
//-----------------------------------------------------------------------------------------------------------------------------------

    public void PlayerAnimation(){
        if(horizontal > 0) playerAnim.PlayAnimation("PlayerMoveRight");
        if(horizontal < 0) playerAnim.PlayAnimation("PlayerMoveLeft");
        if(horizontal == 0) playerAnim.PlayAnimation("PlayerIdle");
    }
//-----------------------------------------------------------------------------------------------------------------------------------

    public void killPLayer(){
        Instantiate(playerExplosion, transform.position, Quaternion.identity);
        gameManeger.MenuUp();
        life = 0;
        lifeSprite.UpdateLife(life);
        Destroy(this.gameObject);
    }
//-----------------------------------------------------------------------------------------------------------------------------------

    public void Dash(){
        timerDash += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.LeftShift) && !_canSpeedUp && timerDash >= 1){
            timerDash = 0;
            _speed *= speedDash;
            StartCoroutine(DashRoutine());
        }
    }
    private IEnumerator DashRoutine(){
        yield return new WaitForSeconds(0.3f);
        _speed /= speedDash;
    }
//-----------------------------------------------------------------------------------------------------------------------------------

    private void DamageColorChange(){
        StartCoroutine(DamageRoutine());
    }

    private IEnumerator DamageRoutine(){
        canTakeDamage = false;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
        canTakeDamage = true;

    }
}
