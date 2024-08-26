
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;


public class RushDice : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject _dice;
    private Image diceValue;
    [SerializeField] private Sprite[] dice_sprites;
    [SerializeField] private Sprite _sprites;
    public float wait = 0.25f;
    public float _rotate = 5f;

    private bool Roll_Flag = false;

    private int step = 0;

    private bool Player1 = false, Player2 = false;
    private bool myTurn = true;

    //public BotMovement bot1,bot2,bot3,bot4;
    public List<RushPlayerMovementnt> botInHome = new List<RushPlayerMovementnt>();
    public List<RushPlayerMovementnt> botOutHome = new List<RushPlayerMovementnt>();


    public List<RushPlayerMovementnt> yhome = new List<RushPlayerMovementnt>();
    public List<RushPlayerMovementnt> rhome = new List<RushPlayerMovementnt>();
    public List<RushPlayerMovementnt> bhome = new List<RushPlayerMovementnt>();
    public List<RushPlayerMovementnt> ghome = new List<RushPlayerMovementnt>();

    public List<GameObject> playerGoti = new List<GameObject>();
   
    public List<GameObject> playerGotiOutHome = new List<GameObject>();
   

    private int activeGoti = 0;
    public GameObject arrow;

    public int totalPlayer = 2;

    private int[] diceVal = { 5, 0, 1, 2, 5, 3, 4, 5, 5, 5, 5 };

    public bool rolled = true;

    public bool user = false;

    public int winner = 0;
    public int player0gotiwin = 0, player1gotiwin = 0, player2gotiwin = 0, player3gotiwin = 0;
    public GameObject result0, result1, result2, result3;


    private void Start()
    {
        //GameType = PlayerPrefs.GetInt("gametype");

        totalPlayer = PlayerPrefs.GetInt("PlayerCount");
        int n = int.Parse(gameObject.tag[gameObject.tag.Length - 1].ToString());
        if (totalPlayer <= 2 && (n == 2 || n == 3)) { SetActivePlayers(playerGoti); gameObject.SetActive(false); }
        if (totalPlayer <= 3 && (n == 3)) { SetActivePlayers(playerGoti); gameObject.SetActive(false); }


        diceValue = _dice.GetComponent<Image>();
     
    }

    
    private void Update()
    {
        if (Roll_Flag) _dice.transform.Rotate(new Vector3(0f, 0f, _rotate * Time.deltaTime));
        if (myTurn && !user )
        {
            myTurn = false;
            Invoke("BotRolling", 2f);
            arrow.SetActive(false);
            
        }
      

    }




    public void RollDice()
    {
        if (!myTurn  ) return;
        myTurn = false;
        step = diceVal[Random.Range(0, 10)];
        Debug.Log("step" + step);
        StartCoroutine(Rolling(step));
    }

    public void BotRolling()
    {
        step = diceVal[Random.Range(0, 10)];
        Debug.Log("step" + step);
        StartCoroutine(Rolling(step));
    }



    IEnumerator Rolling(int step)
    {
        diceValue.sprite = _sprites;
        Roll_Flag = true;
        yield return new WaitForSeconds(2.0f);
        Roll_Flag = false;
        _dice.transform.rotation = Quaternion.identity;
        diceValue.sprite = dice_sprites[step];
        Player1 = true;
        if (!myTurn && !user)
        {
            int botNo = Random.Range(0, 4);
            if (step == 5 || step==0)
            {
                HighlightPlayerGoti(playerGoti, true);
                Debug.Log("step==5");
                // if(!CheckHomeFunction(step, bot1InHome[botNo].GetCurrentPosition())) { }
                botInHome[botNo].Bot(step+1);
                if (!botOutHome.Contains(botInHome[botNo]))
                {
                    botOutHome.Add(botInHome[botNo]);
                }
            }
            else if (botOutHome.Count > 0)
            {
                HighlightPlayerGoti(playerGotiOutHome, true);
                Debug.Log("else if step==5");
                int n = Random.Range(0, botOutHome.Count);
                botOutHome[n].Bot(step+1);
            }
            else
            {
                Debug.Log("else step==5");
                SetTurn();
               
            }


        }
        else if (!myTurn && user)
        {
            if ((step != 5 && step!=0) && activeGoti < 1)
            {
                SetTurn();
                
            }
            if (step == 5 || step==0) HighlightPlayerGoti(playerGoti, true);
            else HighlightPlayerGoti(playerGotiOutHome, true);
        }
      /*  else if (myTurn == 0)
        {
            if (step != 5 && activeGoti < 1)
            {
                SetTurn();
                Setflag(totalPlayer - 1);
            }

            if (step == 5) HighlightPlayerGoti(player0Goti, true);
            else HighlightPlayerGoti(player0GotiOutHome, true);
        }*/

    }

    public int GetStep()
    {
        return step + 1;
    }
    public void SetStep()
    {
        step = -1;
    }
    public bool GetActvePlayer()
    {
        return Player1;
    }
    public void setActiveGoti(int a)
    {
        activeGoti += a;
    }
    public bool GetTurn()
    {
        return myTurn;
    }
    public List<GameObject> GetGoti(int bot, bool fl)
    {
        if (bot == 0)
        {
            if (fl) return playerGoti;
            else return playerGotiOutHome;
        }
      /*  else if (bot == 1)
        {
            if (fl) return player1Goti;
            else return player1GotiOutHome;
        }
        else if (bot == 2)
        {
            if (fl) return player2Goti;
            else return player2GotiOutHome;
        }
        else if (bot == 3)
        {
            if (fl) return player3Goti;
            else return player3GotiOutHome;
        }*/
        return null;
    }



    public void SetTurn()
    {
        myTurn = !myTurn ;
       /* if (myTurn >= totalPlayer)
        {
            rolled = true;
            myTurn = 0;
        }
        if (myTurn < 0)
        {
            myTurn = totalPlayer - 1;
        }
        if (myTurn == 0) rolled = true;
        Debug.Log("set turn" + myTurn);*/
    }




    public void HighlightPlayerGoti(List<GameObject> gotis, bool fl)
    {
        if (fl)
        {
            for (int i = 0; i < gotis.Count; i++)
            {
                gotis[i].transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < gotis.Count; i++)
            {
                gotis[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    public void GotiManupulation(GameObject goti, bool fl, int bot)
    {

        if (fl) playerGotiOutHome.Add(goti);
        else playerGotiOutHome.Remove(goti);


    }

    public void RemoveGotiToHome(int val, RushPlayerMovementnt goti)
    {
         botOutHome.Remove(goti);
        
    }




    public void SetActivePlayers(List<GameObject> player)
    {
        for (int j = 0; j < player.Count; j++)
        {
            player[j].gameObject.SetActive(false);
        }

    }





    public int sixCount = 0;
    public void SetCountSix(bool val)
    {
        if (val) sixCount += 1;
        else sixCount = 0;
    }
    public int GetCountSix()
    {
        return sixCount;
    }

    public void SetRolled()
    {
        rolled = true;
    }

    public bool GetUser()
    {
        return user;
    }





    public bool CheckHomeFunction(int steps, int currentPosition)
    {
        if ((currentPosition + steps) > 56)
        {
            return false;
        }
        return true;
    }

    public void AddHomeGoti(List<PlayerMovement> homeGoti, GameObject goti)
    {
        homeGoti.Add(goti.GetComponent<PlayerMovement>());
    }

    public void IncreaseWinGoti()
    {
            player0gotiwin++;
            IncreaseWinner(player0gotiwin);
        
    }
    public void IncreaseWinner(int player)
    {
        if (player == 4)
        {
            winner++;

            result0.SetActive(true);
            result0.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = GetWinnerText(winner);

        }
        if (winner == totalPlayer) SceneManager.LoadScene("LostPage");
    }

    public string GetWinnerText(int winner)
    {
        if (winner == 1) return "1st";
        else if (winner == 2) return "2nd";
        else if (winner == 3) return "3rd";
        else return "4th";
    }



}




































































