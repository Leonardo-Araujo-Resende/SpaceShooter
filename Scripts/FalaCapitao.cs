using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FalaCapitao : MonoBehaviour
{

    
    public Image fotoCapitao;
    public Text nomeCapitao;
    public Text falaCapitao;
    public string[] todasFalasCapitao;
    private int contFala;
    public Image seta;
    private bool pdProxFala;
    public  GameManeger gameManeger;
    public GameObject panel;
    public float delayFala;
    private GameManeger gm;

    void Start(){
        pdProxFala = true;
        gm = GameObject.Find("GameManager").GetComponent<GameManeger>();
    }

    void Update(){

        if (Input.anyKeyDown) ProximaFala();
        
    }
    private void AtivarDesativar(bool a){
        panel.SetActive(a);
    }

    private IEnumerator AtivarFala(){


        pdProxFala = false;
        seta.gameObject.SetActive(false);

        for (int i = 0; i <= todasFalasCapitao[contFala].Length; i++){
            falaCapitao.text = todasFalasCapitao[contFala].Substring(0, i);
            yield return new WaitForSeconds(delayFala);

        }

        contFala ++;
        pdProxFala = true;
        seta.gameObject.SetActive(true);
    }

    private void ProximaFala(){
        if(contFala == 0) AtivarDesativar(true);
        if(contFala < todasFalasCapitao.Length && pdProxFala == true)StartCoroutine(AtivarFala());
        if(contFala == todasFalasCapitao.Length) {
            AtivarDesativar(false);
            gm.TerminouFalaCapitao();
        }
    }






}
