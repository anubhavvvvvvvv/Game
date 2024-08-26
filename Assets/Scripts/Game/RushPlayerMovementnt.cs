using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RushPlayerMovementnt : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private RushDice rollingDice; 
    private bool OnHome = true;
    private int currentPosition=-1;
    [SerializeField] private GameObject[] Path;

    private float singlePathSpeed = 0.3f;
    private float MoveToStartPositionSpeed = 0.25f;

    private GameObject HomePosition;
    private bool Home = false;

    private int val = 0;
    private int sixCount = 0;

    

   
    private void Start()
    {
        Debug.Log("Inside player1 start");
        HomePosition = transform.parent.gameObject;

        //transform.gameObject.tag = "player1";
    }



     public void MovePlayer()
    {
        
        Debug.Log("MovePlayer"+Home);
        int step = rollingDice.GetStep();
        //rollingDice.SetStep();
        /*   if (step == 6)
           {
               rollingDice.SetCountSix(true);
           }
           else rollingDice.SetCountSix(false);
           Debug.Log("sixcount " + rollingDice.GetCountSix());
           if (rollingDice.GetCountSix() > 2)
           {
               Debug.Log("sixcount more" + rollingDice.GetCountSix());
               rollingDice.HighlightPlayerGoti(rollingDice.GetGoti(0, true), false);

               rollingDice.SetTurn();
              // if (rollingDice.GetTurn() != 0) rollingDice.Setflag(rollingDice.GetTurn());
               rollingDice.SetCountSix(false);
               return;
           }*/
        Debug.Log("Moveplayer step " + step);
        if (rollingDice.GetActvePlayer())
        {
            Debug.Log("Moveplayer step1 " + step);
            if (!Home && (step == 6 || step==1))
            {
                Debug.Log("Moveplayer step2 " + step);
                onClick = false;
                GoToStartPosition();
                Home = true;
                rollingDice.GotiManupulation(transform.gameObject, true, 1);
                if(rollingDice.GetUser()) rollingDice.setActiveGoti(1);
            }
            else if (Home)
            {
                Debug.Log("Moveplayer step3 " + step);
                onClick = false;
                MoveBySteps(step);
            }
        }
        
    }


    public void GoHome()
    {
        Debug.Log("gohome");
        StartCoroutine(MoveDelayed(0, MoveToStartPositionSpeed, true, true, HomePosition));
        Home = false;
        int n = int.Parse(gameObject.tag[gameObject.tag.Length - 1].ToString());
        Debug.Log("int " + n);
        rollingDice.GotiManupulation(transform.gameObject, false, n);
        rollingDice.RemoveGotiToHome(n - 1, gameObject.GetComponent<RushPlayerMovementnt>());
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







    public float timetoShrink=1f;
    //private iTween.EaseType _easeType = iTween.EaseType.Spring;

    private IEnumerator MoveDelayed(int delay,  float time, bool last, bool playSound,GameObject parent)
    {
        //timetoShrink = time / 2;
        //highlight.SetActive(true);

        yield return new WaitForSeconds(delay * 1f);
        transform.SetParent(parent.transform);
        transform.localPosition = Vector2.zero;
        

        if (last)
        {
            Debug.Log("move delay " + rollingDice.GetStep());
            if (rollingDice.GetStep() == 6) rollingDice.SetRolled();

            onClick = true;
            for (int j = 0; j < parent.transform.childCount; j++)
            {
                if (parent.transform.GetChild(j).gameObject.tag != transform.gameObject.tag && parent.tag!="stamp")
                {
                    Debug.Log("remove");
                    Debug.Log("parent.transform.GetChild(j).gameObject.tag"+parent.transform.GetChild(j).gameObject.tag);
                    parent.transform.GetChild(j).gameObject.GetComponent<PlayerMovement>().GoHome();
                }
            }

            if (currentPosition == 56)
            {
                rollingDice.IncreaseWinGoti();
            }
            rollingDice.HighlightPlayerGoti(rollingDice.GetGoti(0, true),false);

            Debug.Log("set turn"+rollingDice.GetCountSix());
            
                rollingDice.SetTurn();
         

        }

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
       // Debug.Log(!rollingDice.GetTurn() +" "+ onClick + rollingDice.GetUser());
        transform.gameObject.GetComponent<Button>().onClick.AddListener(() => 
        { if(!rollingDice.GetTurn() && onClick && rollingDice.GetUser()) MovePlayer(); });
    }
    public void Bot(int step)
    {
       MovePlayer();
    }


   public int GetCurrentPosition()
    {
       return currentPosition;
    }


}

