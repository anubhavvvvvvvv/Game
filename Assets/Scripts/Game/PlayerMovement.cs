using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;
public class PlayerMovement : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    [SerializeField] public RollingDice rollingDice; 
    private bool OnHome = true;
    private int currentPosition=0;
    [SerializeField] private GameObject[] Path;

    private float singlePathSpeed = 0.3f;
    private float MoveToStartPositionSpeed = 0.25f;

    private GameObject HomePosition;
    private bool Home = false;

    private int val = 0;
    private int sixCount = 0;
    private int botstep;
    public TMP_Text pointDelete;
    private bool Afterhome = true;

    
   
    private void Start()
    {
        Debug.Log("Inside player1 start" +timetoShrink);
        HomePosition = transform.parent.gameObject;

        //transform.gameObject.tag = "player1";
    }



     public void MovePlayer()
    {
        Debug.Log("////dice");
        Home = true;
        Debug.Log("MovePlayer"+Home);
        int step = rollingDice.GetStep();
        //if (PlayerPrefs.GetString("gamemode") == "rush") step = botstep;
        //rollingDice.SetStep();
        if (step == 6)
        {
           // rollingDice.SetCountSix(true);
        }
        else rollingDice.SetCountSix(false);
        Debug.Log("sixcount " + rollingDice.GetCountSix());
        if (rollingDice.GetCountSix() > 2)
        {
            Debug.Log("////++++++++dice");
            Debug.Log("sixcount more" + rollingDice.GetCountSix());
            rollingDice.HighlightPlayerGoti(rollingDice.GetGoti(rollingDice.GetTurn(), true), false);
            
            rollingDice.SetTurn();
            if (rollingDice.GetTurn() != 0) rollingDice.Setflag(rollingDice.GetTurn());
            rollingDice.SetCountSix(false);
            return;
        }
        Debug.Log("kk" + rollingDice.GetActvePlayer() + " " + currentPosition + step);
        if ( rollingDice.GetActvePlayer() && (currentPosition+step)<=56)
        {
            Debug.Log("////+++++++--------dice");
            Debug.Log("Time to shrink" + timetoShrink);
            if (rollingDice.GetTurn() == 0 ) photonView.RPC("PhotonPlayer", RpcTarget.Others, timetoShrink);
            Debug.Log("active plater pass " + Home + step);
            if (!Home && (step == 6 || step ==1))
            {
               
                onClick = false;
                GoToStartPosition();
                Home = true;
                rollingDice.GotiManupulation(transform.gameObject, true, rollingDice.GetTurn()+1);
                if(rollingDice.GetTurn()==0) rollingDice.setActiveGoti(1);
            }
            else if (Home)
            {
                onClick = false;
                MoveBySteps(step);

            }
        }
      //  else if (Afterhome && (currentPosition + step) > 56)
       // {
           // Afterhome = false;
           // rollingDice.RollAfter(step - 1);
        //}

    }


    public void GoHome()
    {
        Debug.Log("gohome");
        //for(int j = currentPosition ; j > 1; j --)
        //{
           // transform.SetParent(Path[j].transform);
            //transform.localPosition = Vector2.zero;
            StartCoroutine(MoveDelayed(0, MoveToStartPositionSpeed, false, false, Path[currentPosition]));
       // }
       // StartCoroutine(MoveDelayed(0, MoveToStartPositionSpeed, true, true, Path[currentPosition]));
        Home = false;
        int n = int.Parse(gameObject.tag[gameObject.tag.Length - 1].ToString());
        Debug.Log("int " + n);
        rollingDice.GotiManupulation(transform.gameObject, false, n);
        rollingDice.RemoveGotiToHome(n - 1, gameObject.GetComponent<PlayerMovement>());
    }




    
    public void GoToStartPosition()
    {
        Debug.Log("Go to start Position");
        currentPosition = 0;
      
       // StartCoroutine(MoveDelayed(0, HomePosition.anchoredPosition, Path[currentPosition+1].anchoredPosition,
          //  MoveToStartPositionSpeed, true, true);

        StartCoroutine(MoveDelayed(0,  MoveToStartPositionSpeed, true, true, Path[currentPosition]));
        
    }



    public void MoveBySteps(int steps)
    {
        
        Debug.Log("Steps : " + steps);
        bool check = true;
        //if(currentPosition>=50) CheckHomeFunction(steps);

        for (int i = 0; i < steps; i++)
        {
            bool last = false;
            if (i == steps - 1) last = true;

            currentPosition++;

            //StartCoroutine(
                //MoveDelayed
                 //   (i, Path[currentPosition - 1].anchoredPosition, Path[currentPosition].anchoredPosition, singlePathSpeed, last, true));

          StartCoroutine(
                MoveDelayed
                    (i,  singlePathSpeed, last, true, Path[currentPosition]));
        }
        
        //rollingDice.SetStep();
    }







    public int timetoShrink;
    //private iTween.EaseType _easeType = iTween.EaseType.Spring;

    private IEnumerator MoveDelayed(int delay,  float time, bool last, bool playSound,GameObject parent)
    {
        //timetoShrink = time / 2;
        //highlight.SetActive(true);
        rollingDice.audioSource.clip = rollingDice.audioClips[1];
        StartCoroutine(rollingDice.PlayAudioMultipleTimes(rollingDice.audioSource, rollingDice.step + 1, 1.0f));
        if (!playSound && currentPosition>2)
        {
            yield return new WaitForSeconds(delay * 1f);
            Debug.Log("before last go home");
            transform.SetParent(parent.transform);
            transform.localPosition = Vector2.zero;
            StartCoroutine(MoveDelayed(0, MoveToStartPositionSpeed, false, false, Path[currentPosition-=2]));
        }
        else if (!playSound && currentPosition <= 2)
        {
            Debug.Log("before last");
            StartCoroutine(MoveDelayed(0, MoveToStartPositionSpeed, true, true, HomePosition));
        }

        if (playSound )
        {
            yield return new WaitForSeconds(delay * 1f);
            transform.SetParent(parent.transform);
            transform.localPosition = Vector2.zero;
        }
        

        if (last)
        {

            Debug.Log("move delay " + rollingDice.GetStep());
            /*if (rollingDice.GetStep() == 6) rollingDice.SetRolled();*/

            onClick = true;
            for (int j = 0; j < parent.transform.childCount; j++)
            {
                if (parent.transform.GetChild(j).gameObject.tag != transform.gameObject.tag && parent.tag!="stamp")
                {
                    Debug.Log("parent.transform.GetChild(j).gameObject.tag"+parent.transform.GetChild(j).gameObject.tag);
                    parent.transform.GetChild(j).gameObject.GetComponent<PlayerMovement>().GoHome();
                    int delete = int.Parse(pointDelete.text);
                    delete = delete - 10;
                    pointDelete.text = delete.ToString();
                }
            }
            rollingDice.HighlightPlayerGoti(rollingDice.GetGoti(rollingDice.GetTurn(), true),false);


            if (currentPosition ==56)
            {
                rollingDice.IncreaseWinGoti(rollingDice.GetTurn());
                int n = int.Parse(gameObject.tag[gameObject.tag.Length - 1].ToString());
                Debug.Log("int " + n);
                rollingDice.GotiManupulation(transform.gameObject, false, n);
                rollingDice.RemoveGotiToHome(n - 1, gameObject.GetComponent<PlayerMovement>());

                if (rollingDice.GetTurn() == 0) rollingDice.setActiveGoti(-1);
            }


            Debug.Log("set turn"+rollingDice.GetCountSix());
                rollingDice.SetTurn();
            /*if (rollingDice.GetCountSix() == 0)
            {
                
                //photonView.RPC("PhotonPlayerPass", RpcTarget.Others, PhotonNetwork.LocalPlayer.ActorNumber);

            }*/
            if (rollingDice.GetTurn() != 0) rollingDice.Setflag(rollingDice.GetTurn());
            rollingDice.SetFillerFlag();

        }
        Debug.Log("after move delay");

       /* if (last)
        {
            Debug.Log("if///////////move delayed");
            iTween.ValueTo(
                gameObject,
                iTween.Hash(
                    "from", transform.position, "to", Vector2.zero, "time", time,
                    "easetype", iTween.EaseType.spring, "onupdate", "UpdatePosition", "oncomplete", "MoveFinished"));
        }
        else
        {
            Debug.Log("else///////////move delayed");
            iTween.ValueTo(
                gameObject,
                iTween.Hash(
                    "from", transform.position, "to", Vector2.zero, "time", time,
                    "easetype", iTween.EaseType.spring, "onupdate", "UpdatePosition"));
        
         iTween.ScaleTo(
             gameObject,
             iTween.Hash(
                 "x", 0.8, "y", 0.8, "time", timetoShrink,
                 "easetype", iTween.EaseType.spring, "oncomplete", "ScaleDown"));

          }*/

    }

    bool onClick = true;
    private void Update()
    {
        if(transform.GetComponent<Button>())
        {

            transform.gameObject.GetComponent<Button>().onClick.AddListener(() => {
               /* rollingDice.audioSource.clip = rollingDice.audioClips[1];
                StartCoroutine(rollingDice.PlayAudioMultipleTimes(rollingDice.audioSource, rollingDice.step + 1, 1.0f));*/
                if (onClick && rollingDice.GetTurn() == 0) { MovePlayer(); Afterhome = true; }
            });
        }
    }
    public void Bot(int step)
    {
        botstep = step;
       MovePlayer();
        Afterhome = true;
    }


   public int GetCurrentPosition()
    {
       return currentPosition;
    }

    public bool GetHomestatus()
    {
        return Home;
    }
}

