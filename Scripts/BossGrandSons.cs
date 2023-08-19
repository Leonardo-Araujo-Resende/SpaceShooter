using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGrandSons : MonoBehaviour
{   
    private bool movingRight;
    private bool movingUp;

    [Header("Configurar acoes")]
    [SerializeField] private float speed;
    
    private GameManeger gameManeger;
    private float time;

    private bool isDashing;
    



    void Start()
    {
        gameManeger = GameObject.Find("GameManager").GetComponent<GameManeger>();
        isDashing = false;
        Dash();
    }

    void Update()
    {
        Moviment();
        if(GameManeger.gameOver) Destroy(this.gameObject);
    }

    private void Moviment(){

        time += Time.deltaTime;

        if(!isDashing){
            if(movingRight)transform.Translate(Vector3.right * speed * Time.deltaTime);
            else transform.Translate(Vector3.left * speed * Time.deltaTime);

            if(transform.position.x >= 6.7f) movingRight = false;
            if(transform.position.x <= -6.7f) movingRight = true;
            
            if(movingUp) transform.Translate(Vector3.up * speed * Time.deltaTime);
            else transform.Translate(Vector3.down * speed * Time.deltaTime);

            if(transform.position.y >= 4.3) movingUp = false;
            if(transform.position.y <= 1.5) movingUp = true;
        }

    }

    private void Dash(){
        StartCoroutine(DashRoutine());
    }

    private IEnumerator DashRoutine(){
        while(true){
            yield return new WaitForSeconds(5);
            isDashing = true;
            yield return new WaitForSeconds(3f);
            isDashing = false;
        }
    }

    public bool IsDash(){
        return isDashing;
    }


} 
