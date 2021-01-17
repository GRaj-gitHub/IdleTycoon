using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class mainGame : MonoBehaviour
{
    private bossLvlFunctions _bossLvlFunctions; 

    public List<GameObject> world1Levels = new List<GameObject>();
    public GameObject ladderGuy;
    public GameObject goldBar;
    public GameObject popUp; // for al the popups
    private List<float> checkPoints = new List<float>();
    public List<bool> isUnlocked = new List<bool>();
    private int worldOpen = 0;
    private int totalLvls = 10;
    int currentPick = 0;
    public double ladderCredit = 20.0; //money ladderguy has in his bag at a given time before relesing it to boss level;
    double unlockValue = 60000;
    int j = 0;
    bool lvl2 = false, lvl3 = false; // for first two levels 
    bool ladderGuyManagerSet = false, animationLadderGuy = false;  
    string ladderGuyDirection = "down";
    //public GameObject levelContainer;
    
    
    // Start is called before the first frame update
    void Start()
    {
         _bossLvlFunctions = GameObject.Find("bosslvl").GetComponent<bossLvlFunctions>();


        ladderGuy.GetComponent<Animator>().enabled = false;
        
        for (int i = 0; i < totalLvls; i++)
        {
            checkPoints.Add(ladderGuy.transform.localPosition.y - (330f*(i + 0)));
            GameObject temp =  GameObject.Find("lockLvl" + (i + 1) + "");
            world1Levels.Add(temp);
            Debug.Log("worldlevel" + i + "" + " = " + world1Levels[i].name);
            isUnlocked.Add(false); 
        }
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void moveLadderGuy()
    {
        Debug.Log("moveLadderGUy()");
        Debug.Log("currentPick =  " + currentPick);
        
        //StartCoroutine(pauseClimb());
       if (animationLadderGuy == true)
        {
             if(ladderGuyDirection == "down")
            {
                currentPick++;
            }
            else
            {
                currentPick--;
            }
        
            ladderGuy.GetComponent<Animator>().enabled = true;
            ladderGuy.transform.DOLocalMoveY(checkPoints[currentPick], 2f).SetEase(Ease.Linear).OnComplete(()=>pickMoney());
        }
       
        
    }
    IEnumerator pauseClimb()
    {
        yield return new WaitForSeconds(0.3f);
        ladderGuy.transform.DOPause();
        yield return new WaitForSeconds(0.3f);
        ladderGuy.transform.DOPlay();
        StartCoroutine(pauseClimb());
        
    }
    void pickMoney()
    {
        Debug.Log("pickMoney()");
        if(ladderGuyDirection == "down")
        {
            world1_level _world = GameObject.Find(world1Levels[currentPick - 1].name).GetComponent<world1_level>();
            _world.upgradeText();
            ladderGuy.GetComponent<Animator>().SetBool("takeCash", true);
            StartCoroutine(stopPicking());
            
        }
        else
        {
            ladderGuy.GetComponent<Animator>().SetBool("takeCash", false);
            returnLadderGuy(); 
        }
       
       
        

    }
    IEnumerator stopPicking()
    {
        Debug.Log("StopPicking()");
        yield return new WaitForSeconds(2.3f);
        ladderGuy.GetComponent<Animator>().SetBool("takeCash", false);
        coinCollect();
        returnLadderGuy();

    }

    void returnLadderGuy()
    {
        if(currentPick > worldOpen-1)
        {
            ladderGuyDirection = "up";
            moveLadderGuy();
        }
        else if(currentPick <= 0)
        {
            bossLvl();
        }
        else
        {
            
            moveLadderGuy();
        }

    }



    public void unlockLevel(double coinCollected) // total coins passed from bosslvl() after it is being updated in text
    {
        Debug.Log("unlockLevel()");

        
        if (coinCollected >= unlockValue)
        {
            
            GameObject temp =  GameObject.Find(world1Levels[j].name).transform.Find("unlockLvl").gameObject;
            temp.SetActive(true);
            unlockValue *= 20;
            Debug.Log("unlock value = " + unlockValue);
            _bossLvlFunctions.totalCreditUpdate(unlockValue);
        
        }
        
        else if (coinCollected >= 3000 && lvl3 == false)
        {
            
            GameObject temp =  GameObject.Find(world1Levels[j].name).transform.Find("unlockLvl").gameObject;
            temp.SetActive(true);
            lvl3 = true;
            _bossLvlFunctions.totalCreditUpdate(3000.0f);
    
        }
       
        else if (coinCollected >= 1000 && lvl2 == false)
        {
    
            GameObject temp =  GameObject.Find(world1Levels[j].name).transform.Find("unlockLvl").gameObject;
            temp.SetActive(true);
            lvl2 = true;
            _bossLvlFunctions.totalCreditUpdate(1000.0f);
    
        }
       
    }

    void coinCollect() //caluculating the coin collected by ladderaguy from each rotation
    {
        ladderCredit += 20;
        if (ladderCredit >= 100)  // checking the limit that the ladder guy can hold at a time.
        {
           ladderGuyDirection = "up";
            moveLadderGuy(); 
        }
    }


    public void bossLvl()
    {
        _bossLvlFunctions.coinUpdateBossLevel();
        goldBar.SetActive(true);
        ladderGuyDirection = "down";
        ladderGuy.GetComponent<Animator>().enabled = false;
        ladderCredit = 0;
        if (ladderGuyManagerSet == true)
        {
            moveLadderGuy();
        }
        
    }

    public void popUpManager()
    {
        GameObject tempButton = GameObject.Find(world1Levels[j].name).transform.Find("unlockLvl").gameObject;
        tempButton.SetActive(false);
        GameObject temp =  GameObject.Find(world1Levels[j].name).transform.Find("unlock").gameObject;
        temp.SetActive(true);
        isUnlocked[j] = true;
        _bossLvlFunctions.totalCreditUpdate(20.0f);
        j++;
        worldOpen++;
        animationLadderGuy = true;
    }
    public void ladderManagerSet()
    {
        ladderGuyManagerSet = true;
    }
}
