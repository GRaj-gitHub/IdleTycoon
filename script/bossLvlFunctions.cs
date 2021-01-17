using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class bossLvlFunctions : MonoBehaviour
{

    public GameObject thief;

    
    public Text CreditCounter;  //to display at the top of the screen
    public Text coinBossLvl;  //to display the coins at the top of the boss level

    double totalCoinCollected = 20.0f; //to calculate the total money collected
    double incrementValue =  20; 
    bool bossManagerSet = false ; 
    private mainGame _mainGame;
    private double coinInBossLvl = 0.0; //to calculate the value in coinBossLvl
    
    // Start is called before the first frame update
    void Start()
    {
        thief.GetComponent<Animator>().enabled = false;
         _mainGame = GameObject.Find("world1").GetComponent<mainGame>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void theifBossLvl()
    {

        thief.GetComponent<Animator>().enabled = true;
        thief.transform.localScale = new Vector3(50, 50, 50);
        thief.transform.DOLocalMoveX(361, 5f).SetEase(Ease.Linear).OnComplete(turnBossLvl);
    }

     void turnBossLvl()
    {
        coinInBossLvl -= 20;
        coinBossLvl.text = coinInBossLvl + "" + ".0";
        coinUpdate();
        thief.transform.localScale = new Vector3(-50, 50,50);
        thief.transform.DOLocalMoveX(-147, 5f).SetEase(Ease.Linear).OnComplete(restartAnim);
    }

    void coinUpdate()
    {
        totalCoinCollected += incrementValue;
        CreditCounter.text = totalCoinCollected + "" ;
        Debug.Log("total coin = " + totalCoinCollected);
        _mainGame.unlockLevel(totalCoinCollected);

    }

    void restartAnim()
    {
        if(bossManagerSet == true)
        {
            theifBossLvl();
        }

        else
        {
            thief.GetComponent<Animator>().enabled = false;
            thief.GetComponent<Button>().enabled = true;
        }
    }

    public void coinUpdateBossLevel()
    {
        coinInBossLvl += 20;
        coinBossLvl.text = coinInBossLvl + "" + ".0";
    }

    public void totalCreditUpdate(double decrementValue)
    {
        totalCoinCollected -= decrementValue;
        CreditCounter.text = totalCoinCollected + "" ;
    }

    
     
}
