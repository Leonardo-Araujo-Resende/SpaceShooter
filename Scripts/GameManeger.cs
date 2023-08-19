using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManeger : MonoBehaviour
{
    [Header("Configuracoes")]
    [SerializeField] private LivesManager livesManager;
    [SerializeField] private GameObject imagemMenu;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject escolheControleMenu;
    [SerializeField] private GameObject tutorialTeclado;
    [SerializeField] private GameObject tutorialMouse;
    [SerializeField] private float tempoTutorial;
    

    //Estaticos
    public static bool gameOver = true;
    public static bool capitaoFalou = false;

    //Variaveis de controle
    private int formaControlarNave; // 1 teclado // 2 mouse
    private bool escolheuControle = false;
    private bool tutorialFinished = false;
    



    void Update(){
        
        if(gameOver && Input.anyKeyDown && capitaoFalou && escolheuControle && tutorialFinished){
            StartGame();
        }

    }
    public void MenuUp(){

        imagemMenu.SetActive(true);
        livesManager.finishBossTime();
        gameOver = true;
        
    }

    public void StartGame(){
        if(gameOver){
            InstanciaPlayer();
            livesManager.resetBossTime();
            gameOver = false;
            livesManager.ResetScore();
            imagemMenu.SetActive(false);
        }
    }

    public void ControlaTeclado(){
        formaControlarNave = 1;
        escolheuControle = true;
        escolheControleMenu.SetActive(false);
        tutorialTeclado.SetActive(true);
        StartCoroutine(Tutorial());
    }

    public void ControlaMouse(){
        formaControlarNave = 2;
        escolheuControle = true;
        escolheControleMenu.SetActive(false);
        tutorialMouse.SetActive(true);
        StartCoroutine(Tutorial());
    }

    public void TerminouFalaCapitao(){
        capitaoFalou = true;
        escolheControleMenu.SetActive(true);
        imagemMenu.SetActive(false);
    }

    public int GetFormaControlarNave(){
        return formaControlarNave;
    }

    public IEnumerator Tutorial(){
        InstanciaPlayer();
        yield return new WaitForSeconds(tempoTutorial);
        tutorialTeclado.SetActive(false);
        tutorialMouse.SetActive(false);
        livesManager.resetBossTime();
        gameOver = false;
        livesManager.ResetScore();
        imagemMenu.SetActive(false);
        tutorialFinished = true;
    }

    private void InstanciaPlayer(){
        Player aux = Instantiate(player, Vector3.zero, Quaternion.identity).GetComponent<Player>();
        aux.SetControleNave(formaControlarNave);
        
    }

}
