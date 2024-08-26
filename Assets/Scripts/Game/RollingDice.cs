
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class RollingDice : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    [SerializeField] private GameObject _dice;
    private Image diceValue;
    [SerializeField] private Sprite[] dice_sprites;
    [SerializeField] private Sprite _sprites;
    public float wait = 0.25f;
    public float _rotate = 5f;

    private bool Roll_Flag = false;

    public int step = 0;

    private bool Player1 = false, Player2 = false;
    private int myTurn = 0;

    //public BotMovement bot1,bot2,bot3,bot4;
    public List<PlayerMovement> bot1InHome = new List<PlayerMovement>();
    public List<PlayerMovement> bot2InHome = new List<PlayerMovement>();
    public List<PlayerMovement> bot3InHome = new List<PlayerMovement>();
    public List<PlayerMovement> bot4InHome = new List<PlayerMovement>();

    public List<PlayerMovement> bot1OutHome = new List<PlayerMovement>();
    public List<PlayerMovement> bot2OutHome = new List<PlayerMovement>();
    public List<PlayerMovement> bot3OutHome = new List<PlayerMovement>();
    public List<PlayerMovement> bot4OutHome = new List<PlayerMovement>();

    public List<PlayerMovement> yhome = new List<PlayerMovement>();
    public List<PlayerMovement> rhome = new List<PlayerMovement>();
    public List<PlayerMovement> bhome = new List<PlayerMovement>();
    public List<PlayerMovement> ghome = new List<PlayerMovement>();

    public List<GameObject> player0Goti = new List<GameObject>();
    public List<GameObject> player1Goti = new List<GameObject>();
    public List<GameObject> player2Goti = new List<GameObject>();
    public List<GameObject> player3Goti = new List<GameObject>();

    public List<GameObject> player0GotiOutHome = new List<GameObject>();
    public List<GameObject> player1GotiOutHome = new List<GameObject>();
    public List<GameObject> player2GotiOutHome = new List<GameObject>();
    public List<GameObject> player3GotiOutHome = new List<GameObject>();

    public List<PlayerMovement> player0InWinHome = new List<PlayerMovement>();
    public List<PlayerMovement> player1InWinHome = new List<PlayerMovement>();
    public List<PlayerMovement> player2InWinHome = new List<PlayerMovement>();
    public List<PlayerMovement> player3InWinHome = new List<PlayerMovement>();

    public List<GameObject> profiles = new List<GameObject>();

    public List<TMP_Text> pointTextfield = new List<TMP_Text>();

    public GameObject result0, result1, result2, result3;

    [SerializeField] private GameObject[] DisableHome;

    private int activeGoti = 2;/// 0
    public GameObject arrow;

    public int totalPlayer = 2;

    private int[] diceVal = { 5, 0, 1, 2, 5, 3, 4, 5, 5, 5, 5 };

    public bool rolled = true;

    public int GameType = 2;
    public int n;
    private bool TwoPlayer = false;

    public int winner = 0;
    public int player0gotiwin = 0, player1gotiwin = 0, player2gotiwin = 0, player3gotiwin = 0;

    public bool OnlineGame = false;
    public List<PlayerMovement> playerInWinHome = new List<PlayerMovement>();
    public List<GameObject> hilightGoti = new List<GameObject>();
    public GameObject[] OppDice = new GameObject[4];

    public int gotiCount;
    public int gotiCount1;
    public int gotiCount2;
    public int gotiCount3;


    bool condition = false;
    private void Start()
    {

        //GameType = PlayerPrefs.GetInt("gametype");
        if (PlayerPrefs.HasKey("PlayerCount"))
            totalPlayer = PlayerPrefs.GetInt("PlayerCount");
        if (DataSaver.Instance.GetMode() == 1) OnlineGame = false;
        else OnlineGame = true;

        totalPlayer = 2;
        totalPlayer = DataSaver.Instance.GetPlayerCount();
        diceValue = _dice.GetComponent<Image>();
        // OnlineGame = false;totalPlayer = 2;
        RenderProfiles();

        if (totalPlayer <= 2) { SetActivePlayers(player1Goti); totalPlayer = 3; TwoPlayer = true; }
        if (totalPlayer <= 3) SetActivePlayers(player3Goti);
        // bot1InHome.Add(bot1);bot1InHome.Add(bot2);bot1InHome.Add(bot3);bot1InHome.Add(bot4);
        //OnlineGame = false;
        if (OnlineGame)
            if (PhotonNetwork.IsMasterClient)
            {
                Debug.Log("master client");
                myTurn = 0;
                fillerFlag = true;
            }
            else
            {
                Debug.Log("is not master client");
                SetTurn();
                Setflag(totalPlayer - 1);

            }
    }

    bool[] flag = { true, true, true, true };
    bool[] Profileflag = { false, false, false, false };

    public AudioSource audioSource;
    public AudioClip[] audioClips;

    private void Update()
    {
        if (Roll_Flag) _dice.transform.Rotate(new Vector3(0f, 0f,- _rotate * Time.deltaTime));
        if (Profileflag[myTurn]) OppDice[myTurn].transform.Rotate(new Vector3(0f, 0f,- _rotate * Time.deltaTime));
        if (sixCount > 2)
        {

            SetTurn();
            if (GetTurn() != 0) Setflag(GetTurn());
            SetCountSix(false);
        }


        if (GameType == 2)
        {
            n = int.Parse(gameObject.tag[gameObject.tag.Length - 1].ToString());
            if (myTurn != n) Setflag(n);
            myTurn = n;
        }

        //Debug.Log(myTurn + " " + flag[myTurn]);
        if (myTurn != 0 && flag[myTurn])
        {
            if (!OnlineGame) flag[myTurn] = false;
            // BotRolling();
            if (!OnlineGame) Invoke("BotRolling", 2f);
            arrow.SetActive(false);
            if (myTurn == 1)
            {
                if (fillerFlag) { DisableFiller(); profiles[1].transform.GetChild(0).gameObject.SetActive(true); fillerFlag = false; }
                //if (fillerFlag && !OnlineGame){ DisableFiller(); profiles[1].transform.GetChild(0).gameObject.SetActive(true); fillerFlag = false; }
                CallOpponentGoti(1);
                if (player1gotiwin == 4)
                {
                    SetTurn(); flag[myTurn] = true;
                }
                DisableHomeColor();

                DisableHome[1].SetActive(false);
            }
            else if (myTurn == 2)
            {
                if (fillerFlag) { DisableFiller(); profiles[2].transform.GetChild(0).gameObject.SetActive(true); fillerFlag = false; }
                //if (fillerFlag && !OnlineGame) { DisableFiller(); profiles[2].transform.GetChild(0).gameObject.SetActive(true); fillerFlag = false; }
                CallOpponentGoti(2);
                if (player2gotiwin == 4)
                {
                    SetTurn(); flag[myTurn] = true;
                }
                DisableHomeColor();

                DisableHome[2].SetActive(false);
            }
            else if (myTurn == 3)
            {
                if (fillerFlag) { DisableFiller(); profiles[3].transform.GetChild(0).gameObject.SetActive(true); fillerFlag = false; }
                //if (fillerFlag && !OnlineGame) { DisableFiller(); profiles[3].transform.GetChild(0).gameObject.SetActive(true); fillerFlag = false; }
                CallOpponentGoti(3);
                if (player3gotiwin == 4)
                {
                    SetTurn(); flag[myTurn] = true;
                }
                DisableHomeColor();
                Debug.Log("myturn disable color3");
                DisableHome[3].SetActive(false);
            }

        }
        else if (myTurn == 0)
        {
            //DisableHome[0].SetActive(false);

            if (fillerFlag) { DisableFiller(); fillerFlag = false; profiles[0].transform.GetChild(0).gameObject.SetActive(true); }
            if (myTurn == 0)
            {
                if (player0gotiwin == 4)
                {
                    SetTurn(); flag[myTurn] = true;
                }
                //DisableHomeColor();


            }
            arrow.SetActive(true);
            //DisableHomeColor();
            // Debug.Log("myturn disable color" + myTurn);
            DisableHome[0].SetActive(false);
        }




    }

    public void DisableFiller()
    {
        // Debug.Log("disable filler " + myTurn);
        for (int j = 0; j < 4; j++)
            profiles[j].transform.GetChild(0).gameObject.SetActive(false);
    }

    public void DisableHomeColor()
    {
        // for (int j = 0; j < 4; j++) { DisableHome[j].SetActive(true); }
    }


    public void RollDice()
    {
        if (myTurn != 0 || !rolled) return;

        rolled = false;
        step = diceVal[Random.Range(0, 10)];//step = 0;
        if (sixCount >= 2) step = Random.Range(0, 5);
        Debug.Log("step" + step);
        if (OnlineGame) photonView.RPC("PhotonStep", RpcTarget.Others, step);
        StartCoroutine(Rolling(step, true));
    }

    public void BotRolling()
    {
        step = diceVal[Random.Range(0, 10)];
        Debug.Log("step" + step);

        StartCoroutine(Rolling(step, true));
        // RollAfter(step);
    }
    public void RollAfter(int step)
    {
        StartCoroutine(Rolling(step, false));
    }



    IEnumerator Rolling(int step, bool fl)
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
        diceValue = OppDice[myTurn].GetComponent<Image>();
        if (fl)
        {
            diceValue.sprite = _sprites;
            //Roll_Flag = true;
            Profileflag[myTurn] = true;
            yield return new WaitForSeconds(2.0f);
            //Roll_Flag = false;
            Profileflag[myTurn] = false;
            OppDice[myTurn].transform.rotation = Quaternion.identity;
            diceValue.sprite = dice_sprites[step];

        }
        DisableFiller();
        //else yield return null;
        Player1 = true;
        if (!OnlineGame && myTurn != 0) Invoke("roolingggg", Random.Range(0f, 2f));
        else roolingggg();

    }
    public void roolingggg()
    {
        if (myTurn == 1)
        {
            //int botNo = Random.Range(0, 2);
            if (player1gotiwin == 1)
            {
                gotiCount1 = gotiCount1 + 1;

            }
            int botNo = gotiCount1;
            if (step == 5 || step == 0)
            {
                if (bot1OutHome[botNo].GetCurrentPosition() + step + 1 >= 56)
                {
                    Debug.Log("else step==5");
                    SetTurn();
                    Setflag(0);
                    fillerFlag = true;
                }
                else
                {

                HighlightPlayerGoti(player1Goti, true);
                int dicevalue = int.Parse(pointTextfield[1].text) + step + 1;
                pointTextfield[1].text = dicevalue.ToString();
                Debug.Log("step==5");
                playerInWinHome.Clear();
                //List<PlayerMovement> playerInWinHome = new List<PlayerMovement>();
                for (int j = 0; j < bot1InHome.Count; j++)
                {
                    if ((bot1InHome[j].GetCurrentPosition() + step + 1) <= 56)
                    {
                        playerInWinHome.Add(bot1InHome[j]);

                    }
                }

                // botNo = Random.Range(0, playerInWinHome.Count);
                botNo = gotiCount1;
                // bot1InHome[botNo].Bot(step+1);
                if (playerInWinHome.Count == 0) { SetTurn(); Setflag(0); }
                playerInWinHome[botNo].Bot(step + 1);
                // StartCoroutine(AddOutHome(playerInWinHome[botNo], bot1OutHome));
                if (!bot1OutHome.Contains(playerInWinHome[botNo]) && sixCount < 2) bot1OutHome.Add(playerInWinHome[botNo]);
                //bot1OutHome.Add(bot1InHome[botNo]);

                /* bot1InHome[botNo].Bot(step+1);
                if (!bot1OutHome.Contains(bot1InHome[botNo]) && sixCount<=2)
                {
                    Debug.Log("Add 1out bot");
                    bot1OutHome.Add(bot1InHome[botNo]);
                }*/
                }
            }
            else if (bot1OutHome.Count > 0)
            {
                if (bot1OutHome[botNo].GetCurrentPosition() + step  >= 56)
                {
                    Debug.Log("else step==5");
                    SetTurn();
                    Setflag(0);
                    fillerFlag = true;
                }
                else
                {

                HighlightPlayerGoti(player1GotiOutHome, true);
                int dicevalue = int.Parse(pointTextfield[1].text) + step + 1;
                pointTextfield[1].text = dicevalue.ToString();
                Debug.Log("else if step==5");
                playerInWinHome.Clear();
                // List<PlayerMovement> playerInWinHome = new List<PlayerMovement>();
                for (int j = 0; j < bot1OutHome.Count; j++)
                {
                    if ((bot1OutHome[j].GetCurrentPosition() + step ) <= 56)
                    {
                        playerInWinHome.Add(bot1OutHome[j]);
                    }
                }
                int n = Random.Range(0, playerInWinHome.Count);
                if (playerInWinHome.Count == 0) { SetTurn(); Setflag(0); }
                playerInWinHome[n].Bot(step + 1);


                /* int n=Random.Range(0,bot1OutHome.Count);
                bot1OutHome[n].Bot(step+1);*/
                }
            }
            else
            {
                Debug.Log("else step==5");
                SetTurn();
                Setflag(0);
                fillerFlag = true;
            }


        }
        else if (myTurn == 2)
        {

            /*int botNo = Random.Range(0, 2);*/
            if (player2gotiwin==1)
            {
               /* gotiCount2 = gotiCount2+1;*/
                
            }
            int botNo = gotiCount2;
            
            

            if (step == 5 || step == 0)
            {
                Debug.Log(bot2OutHome[botNo].GetCurrentPosition() + step + 1 );
                if(bot2OutHome[botNo].GetCurrentPosition()+step>=56)
                {
                    Debug.Log("else step==5");
                    SetTurn();
                    Setflag(1);
                    fillerFlag = true;
                }
                else
                {

                HighlightPlayerGoti(player2Goti, true);
                int dicevalue = int.Parse(pointTextfield[2].text) + step + 1;
                pointTextfield[2].text = dicevalue.ToString();
                Debug.Log("step==5");
                playerInWinHome.Clear();
                //List<PlayerMovement> playerInWinHome = new List<PlayerMovement>();
                for (int j = 0; j < bot2InHome.Count; j++)
                {
                    if ((bot2InHome[j].GetCurrentPosition() + step + 1) <= 56)
                    {
                        playerInWinHome.Add(bot2InHome[j]);

                    }
                }
                /* botNo = Random.Range(0, playerInWinHome.Count);*/
                botNo = gotiCount2;
                if (playerInWinHome.Count == 0) { SetTurn(); Setflag(1); }
                playerInWinHome[botNo].Bot(step + 1);
                Debug.Log("+++//" + playerInWinHome[n].GetCurrentPosition());
                // StartCoroutine(AddOutHome(playerInWinHome[botNo], bot2OutHome));
                if (!bot2OutHome.Contains(playerInWinHome[botNo]) && sixCount < 2) bot2OutHome.Add(playerInWinHome[botNo]);

                /*  bot2InHome[botNo].Bot(step+1);
                 if (!bot2OutHome.Contains(bot2InHome[botNo]) && sixCount <= 2)
                 {
                     Debug.Log("Add 2out bot");
                     bot2OutHome.Add(bot2InHome[botNo]);
                 }*/
                }
            }
            else if (bot2OutHome.Count > 0)
            {
                Debug.Log(bot2OutHome[botNo].GetCurrentPosition() + step + 1);
                if (bot2OutHome[botNo].GetCurrentPosition() + step >= 56)
                {
                    Debug.Log("else step==5");
                    SetTurn();
                    Setflag(1);
                    fillerFlag = true;
                }
                else
                {
                    HighlightPlayerGoti(player2GotiOutHome, true);
                    int dicevalue = int.Parse(pointTextfield[2].text) + step + 1;
                    pointTextfield[2].text = dicevalue.ToString();
                    Debug.Log("else if step==5");
                    playerInWinHome.Clear();
                    //List<PlayerMovement> playerInWinHome = new List<PlayerMovement>();
                    for (int j = 0; j < bot2OutHome.Count; j++)
                    {
                        if ((bot2OutHome[j].GetCurrentPosition() + step + 1) <= 56)
                        {
                            playerInWinHome.Add(bot2OutHome[j]);

                        }
                    }
                    //int n = Random.Range(0, playerInWinHome.Count);
                    int n = gotiCount2;
                    if (playerInWinHome.Count == 0) { SetTurn(); Setflag(1); }
                    Debug.Log("+++//" + playerInWinHome[n].GetCurrentPosition());


                    playerInWinHome[n].Bot(step + 1);

                    /*int n = Random.Range(0, bot2OutHome.Count);
                    bot2OutHome[n].Bot(step+1);*/
                }

            }
            else
            {
                Debug.Log("else step==5");
                SetTurn();
                Setflag(1);
                fillerFlag = true;
            }


        }
        else if (myTurn == 3)
        {

            /*int botNo = Random.Range(0, 2);*/
            if (player3gotiwin == 1)
            {
                gotiCount3 = gotiCount3 + 1;

            }
            int botNo = gotiCount3;
            if (step == 5 || step == 0)
            {
                if (bot3OutHome[botNo].GetCurrentPosition() + step  >= 56)
                {
                    Debug.Log("else step==5");
                    SetTurn();
                    Setflag(2);
                    fillerFlag = true;
                }
                else
                {

                HighlightPlayerGoti(player3Goti, true);
                int dicevalue = int.Parse(pointTextfield[3].text) + step + 1;
                pointTextfield[3].text = dicevalue.ToString();
                Debug.Log("step==5");
                playerInWinHome.Clear();
                //List<PlayerMovement> playerInWinHome = new List<PlayerMovement>();
                for (int j = 0; j < bot3InHome.Count; j++)
                {
                    // Debug.Log(myTurn +" "+j+ " " + bot2InHome[j].GetCurrentPosition() + " " + step);
                    if ((bot3InHome[j].GetCurrentPosition() + step + 1) <= 56)
                    {
                        playerInWinHome.Add(bot3InHome[j]);

                    }
                }
                /*botNo = Random.Range(0, playerInWinHome.Count);*/
                 botNo = gotiCount3;
                if (playerInWinHome.Count == 0) { SetTurn(); Setflag(2); }
                playerInWinHome[botNo].Bot(step + 1);
                //StartCoroutine(AddOutHome(playerInWinHome[botNo], bot3OutHome));
                if (!bot3OutHome.Contains(playerInWinHome[botNo]) && sixCount < 2) bot3OutHome.Add(playerInWinHome[botNo]);

                /* bot3InHome[botNo].Bot(step+1);
                 if (!bot3OutHome.Contains(bot3InHome[botNo]) && sixCount <= 2)
                 {
                     Debug.Log("Add3 out bot");
                     bot3OutHome.Add(bot3InHome[botNo]);
                 }*/
                }
            }
            else if (bot3OutHome.Count > 0)
            {
                if (bot3OutHome[botNo].GetCurrentPosition() + step  >= 56)
                {
                    Debug.Log("else step==5");
                    SetTurn();
                    Setflag(1);
                    fillerFlag = true;
                }
                else
                {

                HighlightPlayerGoti(player3GotiOutHome, true);
                int dicevalue = int.Parse(pointTextfield[3].text) + step + 1;
                pointTextfield[3].text = dicevalue.ToString();
                Debug.Log("else if step==5");
                playerInWinHome.Clear();
                // List<PlayerMovement> playerInWinHome = new List<PlayerMovement>();
                for (int j = 0; j < bot3OutHome.Count; j++)
                {
                    // Debug.Log(myTurn + " " + j + " " + bot3InHome[j].GetCurrentPosition() + " " + step);
                    if ((bot3OutHome[j].GetCurrentPosition() + step + 1) <= 56)
                    {
                        playerInWinHome.Add(bot3OutHome[j]);

                    }
                }
                if (playerInWinHome.Count == 0) { SetTurn(); Setflag(2); }
                int n = Random.Range(0, playerInWinHome.Count);
                playerInWinHome[n].Bot(step + 1);

                /* int n = Random.Range(0, bot3OutHome.Count);
                 bot3OutHome[n].Bot(step+1);*/
                }
            }
            else
            {
                Debug.Log("else step==5");
                SetTurn();
                Setflag(2);
                fillerFlag = true;
            }


        }
        else if (myTurn == 0)
        {
            
            if (player0gotiwin == 1&&condition==false)
            {
                gotiCount = gotiCount + 1;
                bot4OutHome[gotiCount].GetComponent<Button>().enabled = true;
                condition = true;
            }
            int botNo = gotiCount;
            int v = bot4OutHome[botNo].GetCurrentPosition() ;
            Debug.Log("ppp" + v);
            if (bot4OutHome[botNo].GetCurrentPosition() + step  >= 56)
            {
                Debug.Log("else step==5");
                SetTurn();
                Setflag(totalPlayer - 1);
                fillerFlag = true;
            }
            else
            {

            bool flagg = false;
            flagg = true;
            HighlightPlayerGoti(player0Goti, true);
            int dicevalue = int.Parse(pointTextfield[0].text) + step + 1;
            pointTextfield[0].text = dicevalue.ToString();
            Debug.Log("step==5");
            for (int j = 0; j < 2; j++)
            {

                PlayerMovement pm = player0Goti[j].GetComponent<PlayerMovement>();

                /*if (pm.GetHomestatus() && (pm.GetCurrentPosition() + step + 1) <= 56) flagg = true;
                else if ((step == 5 || step == 0) && !pm.GetHomestatus()) flagg = true;*/
            }
            if ((step != 5 && step != 0 && activeGoti < 1) || sixCount > 2 || !flagg)
            {
                Debug.Log("hh");
                if (OnlineGame) photonView.RPC("PhotonPlayerPass", RpcTarget.Others, PhotonNetwork.LocalPlayer.ActorNumber);
                SetTurn();
                Setflag(totalPlayer - 1);
                fillerFlag = true;
                sixCount = 0;

            }
               

                /*if (step == 5 || step == 0) HighlightPlayerGoti(player0Goti, true);
                else*/
                HighlightPlayerGoti(player0GotiOutHome, true);
            }
            //if(OnlineGame) photonView.RPC("PhotonStep", RpcTarget.Others, step);
            /*   int botNo = Random.Range(0, 2);
               if (step == 5 || step == 0)
               {
                   HighlightPlayerGoti(player0Goti, true);
                   int dicevalue = int.Parse(pointTextfield[0].text) + step + 1;
                   pointTextfield[0].text = dicevalue.ToString();
                   Debug.Log("step==5");
                   playerInWinHome.Clear();
                   //List<PlayerMovement> playerInWinHome = new List<PlayerMovement>();
                   for (int j = 0; j < bot4InHome.Count; j++)
                   {
                       // Debug.Log(myTurn +" "+j+ " " + bot2InHome[j].GetCurrentPosition() + " " + step);
                       if ((bot4InHome[j].GetCurrentPosition() + step + 1) <= 56)
                       {
                           playerInWinHome.Add(bot4InHome[j]);

                       }
                   }
                   botNo = Random.Range(0, playerInWinHome.Count);
                   if (playerInWinHome.Count == 0) { SetTurn(); Setflag(2); }
                   playerInWinHome[botNo].Bot(step + 1);
                   //StartCoroutine(AddOutHome(playerInWinHome[botNo], bot3OutHome));
                   if (!bot4OutHome.Contains(playerInWinHome[botNo]) && sixCount < 2) bot4OutHome.Add(playerInWinHome[botNo]);
               }

               else if (bot4OutHome.Count > 0)
               {
                   HighlightPlayerGoti(player0GotiOutHome, true);
                   int dicevalue = int.Parse(pointTextfield[0].text) + step + 1;
                   pointTextfield[0].text = dicevalue.ToString();
                   Debug.Log("else if step==5");
                   playerInWinHome.Clear();
                   // List<PlayerMovement> playerInWinHome = new List<PlayerMovement>();
                   for (int j = 0; j < bot4OutHome.Count; j++)
                   {
                       // Debug.Log(myTurn + " " + j + " " + bot3InHome[j].GetCurrentPosition() + " " + step);
                       if ((bot4OutHome[j].GetCurrentPosition() + step + 1) <= 56)
                       {
                           playerInWinHome.Add(bot4OutHome[j]);

                       }
                   }
                   int n = Random.Range(0, playerInWinHome.Count);
                   if (playerInWinHome.Count == 0) { SetTurn(); Setflag(2); }
                   playerInWinHome[n].Bot(step + 1);

                   *//* int n = Random.Range(0, bot3OutHome.Count);
                    bot3OutHome[n].Bot(step+1);*//*
               }
               else
               {
                   Debug.Log("else step==5");
                   SetTurn();
                   Setflag(2);
                   fillerFlag = true;
               }*/
        }

    }
   public IEnumerator PlayAudioMultipleTimes(AudioSource source, int times, float delay)
    {
        for (int i = 0; i < times; i++)
        {
            source.Play();
            yield return new WaitForSeconds(delay); // Delay between plays
        }
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
        return true;
    }
    public void setActiveGoti(int a)
    {
        activeGoti += a;
    }
    public int GetTurn()
    {
        return myTurn;
    }
    
    public List<GameObject> GetGoti(int bot, bool fl)
    {
        if (bot == 0)
        {
            if (fl) return player0Goti;
            else return player0GotiOutHome;
        }
        else if (bot == 1)
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
        }
        return null;
    }



    public void SetTurn()
    {
        fillerFlag = true;
        Debug.Log("set turn" + myTurn);
        myTurn -= 1;
        if (myTurn >= totalPlayer)
        {
            rolled = true;
            myTurn = 0;
        }
        if (myTurn < 0)
        {
            myTurn = totalPlayer - 1;
        }
        if (TwoPlayer && myTurn == 1)
        {
            myTurn = 0;
        }
        if (myTurn == 0) rolled = true;
        Debug.Log("set turn" + myTurn);

    }
    public void Setflag(int i)
    {
        flag[0] = false; flag[1] = false; flag[2] = false; flag[3] = false;
        flag[i] = true;
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
        if (bot == 1)
        {
            if (fl) player0GotiOutHome.Add(goti);
            else player0GotiOutHome.Remove(goti);
        }
        else if (bot == 2)
        {
            if (fl) player1GotiOutHome.Add(goti);
            else player1GotiOutHome.Remove(goti);
        }
        else if (bot == 3)
        {
            if (fl) player2GotiOutHome.Add(goti);
            else player2GotiOutHome.Remove(goti);
        }
        else if (bot == 4)
        {
            if (fl) player3GotiOutHome.Add(goti);
            else player3GotiOutHome.Remove(goti);
        }
    }

    public void RemoveGotiToHome(int val, PlayerMovement goti)
    {
        Debug.Log(val);
        if (val == 1) bot1OutHome.Remove(goti);
        else if (val == 2) bot2OutHome.Remove(goti);
        else if (val == 3) bot3OutHome.Remove(goti);
    }




    public void SetActivePlayers(List<GameObject> player)
    {
        for (int j = 0; j < player.Count; j++)
        {
            player[j].gameObject.SetActive(false);
        }

    }




    public void audioToplay(int num)
    {
        for (int i=0;i<num;i++)
        {
            audioSource.Play();
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


    public void AddwinHomeGoti(GameObject goti, int fl)
    {
        if (fl == 0) player0InWinHome.Add(goti.GetComponent<PlayerMovement>());
        else if (fl == 1) player1InWinHome.Add(goti.GetComponent<PlayerMovement>());
        else if (fl == 2) player2InWinHome.Add(goti.GetComponent<PlayerMovement>());
        else if (fl == 3) player3InWinHome.Add(goti.GetComponent<PlayerMovement>());

    }
    public void RemovewinHomeGoti(GameObject goti, int fl)
    {
        if (fl == 0) player0InWinHome.Remove(goti.GetComponent<PlayerMovement>());
        else if (fl == 1) player1InWinHome.Remove(goti.GetComponent<PlayerMovement>());
        else if (fl == 2) player2InWinHome.Remove(goti.GetComponent<PlayerMovement>());
        else if (fl == 3) player3InWinHome.Remove(goti.GetComponent<PlayerMovement>());
    }



    public void IncreaseWinGoti(int va)
    {
        if (va == 0)
        {
            player0gotiwin++;
            IncreaseWinner(player0gotiwin, va);
        }
        else if (va == 1)
        {
            player1gotiwin++;
            IncreaseWinner(player1gotiwin, va);
        }
        else if (va == 2)
        {
            player2gotiwin++;
            IncreaseWinner(player2gotiwin, va);
        }
        else if (va == 3)
        {
            player3gotiwin++;
            IncreaseWinner(player3gotiwin, va);
        }
    }
    [SerializeField] private WinnerApi Winnerapi;
    public void IncreaseWinner(int player, int va)
    {
        if (player == 2)
        {
            winner++;
            if (va == 0)
            {
                result0.SetActive(true);
                result0.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = GetWinnerText(winner);
            }
            else if (va == 1)
            {
                result1.SetActive(true);
                result1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = GetWinnerText(winner);
            }
            else if (va == 2)
            {
                result2.SetActive(true);
                result2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = GetWinnerText(winner);
            }
            else if (va == 3)
            {
                result3.SetActive(true);
                result3.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = GetWinnerText(winner);
            }
            Debug.Log("myturn //////// " + myTurn);
            if (myTurn == 0 && winner == 1) { Winnerapi.winner("https://ludo-project-backend.vercel.app/api/v1/user/winner/Contest"); win.SetActive(true); }
            else if (winner == 2 && myTurn == 0) { Winnerapi.Secondwinner("https://ludo-project-backend.vercel.app/api/v1/user/secondPrize/Contest"); win.SetActive(true); }
            else if (winner == 3 && myTurn == 0) { Winnerapi.Thirdwinner("https://ludo-project-backend.vercel.app/api/v1/user/thirdPrize/Contest"); win.SetActive(true); }
        }
        if (winner == totalPlayer - 1 || (totalPlayer == 1 && TwoPlayer)) Loose.SetActive(true);
    }

    public GameObject win;
    public GameObject Loose;
    public string GetWinnerText(int winner)
    {
        if (winner == 1) return "1st";
        else if (winner == 2) return "2nd";
        else if (winner == 3) return "3rd";
        else return "4th";
    }

    //if (!bot2OutHome.Contains(playerInWinHome[botNo]) && sixCount <= 2) bot2OutHome.Add(playerInWinHome[botNo]);
    IEnumerator AddOutHome(PlayerMovement home, List<PlayerMovement> outhome)
    {
        yield return new WaitForSeconds(2.0f);
        if (!outhome.Contains(home) && sixCount > 2) outhome.Add(home);
    }


    public int Pstep = 10;

    public void SetStep(int ste)
    {
        Debug.Log("setstep Pstep" + ste);
        Pstep = ste;
        step = ste;
        if (Pstep != 10) { StartCoroutine(DemoRolling()); }

    }
    public int PhotonPlayer = 10;
    public void SetPhotonPLayer(int player)
    {
        Debug.Log("setplayer Pstep" + player);
        PhotonPlayer = player;
    }

    public bool pass = false;
    public void SetPass(bool p)
    {
        Debug.Log("pass");
        // if(myTurn!=0)
        pass = p;
    }



    public void CallOpponentGoti(int val)
    {
        if (OnlineGame)
        {
            if (pass)
            {
                Pstep = 10;
                pass = false;
                flag[myTurn] = false;
                SetTurn();
                flag[myTurn] = true;
                // return;
            }
            else if (Pstep != 10 && PhotonPlayer != 10)
            {
                //if (fillerFlag) { DisableFiller(); profiles[val].transform.GetChild(0).gameObject.SetActive(true); fillerFlag = false; }
                //if (fillerFlag && !OnlineGame) { DisableFiller(); profiles[3].transform.GetChild(0).gameObject.SetActive(true); fillerFlag = false; }
                Debug.Log(myTurn + " " + Pstep + "v  v" + PhotonPlayer);
                flag[myTurn] = false;
                if (val == 1) bot1InHome[PhotonPlayer].Bot(Pstep + 1);
                else if (val == 2) bot2InHome[PhotonPlayer].Bot(Pstep + 1);
                else if (val == 3) bot3InHome[PhotonPlayer].Bot(Pstep + 1);
                PhotonPlayer = 10;
                Pstep = 10;
            }

        }
    }

    public bool GetOnline()
    {
        return OnlineGame;
    }
    IEnumerator DemoRolling()
    {
        diceValue = OppDice[myTurn].GetComponent<Image>();
        diceValue.sprite = _sprites;
        //OppDice[myTurn - 1].SetActive(true);
        Profileflag[myTurn] = true;
        //Roll_Flag = true;
        yield return new WaitForSeconds(2.0f);
        //Roll_Flag = false;
        Profileflag[myTurn] = false;
        OppDice[myTurn].transform.rotation = Quaternion.identity;
        diceValue.sprite = dice_sprites[step];
    }





    public void RenderProfiles()
    {
        Debug.Log("renderprofiles" + totalPlayer);
        if (totalPlayer <= 2)
        {
            profiles[1].SetActive(false);
            profiles[3].SetActive(false);
        }
        else if (totalPlayer == 3)
        {
            profiles[3].SetActive(false);
        }

    }

    public void ExaustedTime()
    {
        if (OnlineGame && myTurn == 0)
        {
            SetTurn(); photonView.RPC("PhotonPlayerPass", RpcTarget.Others, PhotonNetwork.LocalPlayer.ActorNumber);
            Setflag(myTurn);
            fillerFlag = true;
        }
        if (!OnlineGame)
        {
            SetTurn();
            Setflag(myTurn);
            fillerFlag = true;
        }
    }

    public bool fillerFlag = true;
    public void SetFillerFlag()
    {
        fillerFlag = true;
        profiles[myTurn].transform.GetChild(0).gameObject.GetComponent<Image>().fillAmount = 1;
        Debug.Log("SetFillerFlag");
    }


}




































































