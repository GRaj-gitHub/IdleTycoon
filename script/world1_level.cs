using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class world1_level : MonoBehaviour
{
    public bool managerSet = false;
    public GameObject manager;
    public GameObject goldBar;
    public GameObject thief;
    public GameObject managerAdd;
    public GameObject moneyCollected; // coin collected on each theif_walk animation
    double money = 0.0f;
    double upgradeValue = 50.0f; // limit for the next upgrade; activates upgrade
    int lvlVal = 20;    // level value is added to coin collected at animation ; increases when level is upgraded
    public GameObject upgradeButton;
    public Text coinCollected;      // text on top left
    
    
    // Start is called before the first frame update
    void Start()
    {
        thief.GetComponent<Animator>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void addManager()
    {
        
        managerSet = true;

       if (managerSet == true)
        {  
            managerAdd.SetActive(false);  
            manager.SetActive(true);
            //startThiefWalk();
            
        }
        
    }
    
    public void startThiefWalk()
    {
        //calls from addManger() and Theif button onCLick
         
        thief.GetComponent<Animator>().enabled = true;
        thief.GetComponent<Animator>().SetBool("turn", false);
        thief.transform.localScale = new Vector3(50, 50, 50);
        thief.transform.DOLocalMoveX(361, 5f).SetEase(Ease.Linear).OnComplete(turn);
    }
    void turn()
    {
        thief.GetComponent<Animator>().SetBool("turn", true);
        thief.transform.localScale = new Vector3(-50, 50,50);
        thief.transform.DOLocalMoveX(-147, 5f).SetEase(Ease.Linear).OnComplete(restartAnim);

        money += lvlVal;
        coinCollected.text = money + "" + ".0";
        
        if(money >= upgradeValue)
        {
            upgradeButton.SetActive(true);
            upgradeValue += (10 + upgradeValue);
        }
    }
   void restartAnim()
    {
        goldBar.SetActive(true);
        if(managerSet == false)
        {
            thief.transform.localScale = new Vector3(50, 50, 50);
            thief.GetComponent<Animator>().enabled = false;
            thief.GetComponent<Button>().enabled = true;
        }
        
        if(managerSet == true)
        {
            
            startThiefWalk();

        }
        
    }
    public void levelUpgrade()
    {
        lvlVal *= 2;
    }

    public void upgradeText()
    {
        money -= 20;
        coinCollected.text = money + "" + ".0";
    }

}

