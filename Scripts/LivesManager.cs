using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour
{
    [SerializeField]
    private Sprite[] lifesSprites;
    [SerializeField]
    private Image lifeImage;
    [SerializeField]
    private Text textScore;
    private int score = 00;
    private bool bossTime = false;
    public GameObject boss;
    private Text textDinherus;
    public Image lifeBar;
    private BossEnemy bossEnemy;
    public Text porcentageLife;
    public float finishBoss;
    public int qntInimigosAteBoss;

    void Start(){
        finishBoss = 0.0f;
    }
    void Update(){
        AtualizaScore();
        if(bossTime){
            lifeBar.gameObject.SetActive(true);
            porcentageLife.gameObject.SetActive(true);
            lifeBar.fillAmount = bossEnemy.lifes / bossEnemy.maxLifes;
            lifeBar.color = Color.Lerp(Color.red, Color.green, bossEnemy.lifes / bossEnemy.maxLifes);
            porcentageLife.text = (bossEnemy.lifes / bossEnemy.maxLifes) * 100 + "%";

        }
        else{
            lifeBar.gameObject.SetActive(false);
            porcentageLife.gameObject.SetActive(false);
        }


    }

    public void UpdateLife(int currentLife)
    {
        lifeImage.sprite = lifesSprites[currentLife];
    }

    public void UpadeScore(){

        score += 10;  
        
        if(score % qntInimigosAteBoss == 0){
            bossTime = true;
            Instantiate(boss, new Vector3(0,3.2f,0), Quaternion.identity); 
            bossEnemy = GameObject.Find("Boss(Clone)").GetComponent<BossEnemy>();
        }
        
    }

    public void ResetScore(){
        score = 0;
        textScore.text = "Score " + score;
    }
    public void AtualizaScore(){
        textScore.text = "Score " + score;
    }

    public bool isBossTime(){
        return bossTime;
    }    

    public void finishBossTime(){
        finishBoss += 1;

        if(finishBoss == 6){
            resetBossTime();
        }
    }

    public void subtraiScore(int subtrai){
        if(score - subtrai >= 0) score -= subtrai;
    }
    public int getScore(){
        return score;
    }

    public void resetBossTime(){
        bossTime = false;
        finishBoss = 0;
    }
}   
