using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    [Header("Configurar atributos")]
    [SerializeField] private float timeBetweenEnemy;
    [SerializeField] private float timeBetweenPowerUp;

    //Privates
    private float timerPowerUp;
    private float timerEnemy;
    private int lastPowerUp = -1;
    private int currentPowerup;
    

    [Header("Outros Objetps")]
    [SerializeField] private PowerUps[] powerUps;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameManeger gameManeger;
    [SerializeField] private LivesManager livesManager;

    // Update is called once per frame
    void Update()
    {
        timerEnemy += Time.deltaTime;
        timerPowerUp += Time.deltaTime;

        if(timerEnemy > timeBetweenEnemy && !GameManeger.gameOver && !livesManager.isBossTime()){
            Instantiate(enemy, new Vector3(Random.Range(-8f, 8f), 6, 0),Quaternion.identity);
            timerEnemy = 0;
        }
        if(timerPowerUp > timeBetweenPowerUp && !GameManeger.gameOver && gameManeger.GetFormaControlarNave() == 1){
            
            do{
                currentPowerup = Random.Range(0,3);

            }while(currentPowerup == lastPowerUp);
            lastPowerUp = currentPowerup;
            Instantiate(powerUps[currentPowerup], new Vector3(Random.Range(-7.1f, 7.1f), 6, 0),Quaternion.identity);
            timerPowerUp = 0;
        }

        if(timerPowerUp > timeBetweenPowerUp && !GameManeger.gameOver && gameManeger.GetFormaControlarNave() == 2){
            
            do{
                currentPowerup = Random.Range(0,2);

            }while(currentPowerup == lastPowerUp);
            lastPowerUp = currentPowerup;
            Instantiate(powerUps[currentPowerup], new Vector3(Random.Range(-7.1f, 7.1f), 6, 0),Quaternion.identity);
            timerPowerUp = 0;
        }

    }
}
