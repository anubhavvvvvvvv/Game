using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private RollingDice rollingDice;
    private bool OnHome = true;
    private int currentPosition = -1;
    [SerializeField] private GameObject[] Path;


    private float singlePathSpeed = 0.3f;
    private float MoveToStartPositionSpeed = 0.25f;

    private GameObject HomePosition;
    private bool Home = false;

    private int val = 0;
    private int sixCount = 0;


    private void Start()
    {
        Debug.Log("Inside player2 start");
        HomePosition = transform.parent.gameObject;

        transform.gameObject.tag = "player2";
    }



    public void MovePlayer()
    {
        onClick = false;
        Debug.Log("MovePlayer" + Home);
        int step = rollingDice.GetStep();
        //rollingDice.SetStep();
        if (step == 6)
        {
            sixCount++;
        }
        else sixCount = 0;
        /*if (sixCount > 2)
        {
            rollingDice.SetTurn();
            rollingDice.Setflag();
            sixCount = 0;
            return;
        }*/
        if (rollingDice.GetActvePlayer())
        {
            if (!Home && step == 6)
            {
                
                GoToStartPosition();
                Home = true;
                rollingDice.GotiManupulation(transform.gameObject, true, 2);
            }
            else if (Home)
            {
                
                MoveBySteps(step);
            }
        }

    }

    public void GoHome()
    {
        StartCoroutine(MoveDelayed(0, MoveToStartPositionSpeed, true, true, HomePosition));
    }



    public void GoToStartPosition()
    {
        Debug.Log("Go to start Position");
        currentPosition = 0;


        StartCoroutine(MoveDelayed(0, MoveToStartPositionSpeed, true, true, Path[currentPosition]));

    }



    public void MoveBySteps(int steps)
    {

        Debug.Log("Steps : " + steps);


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
                        (i, singlePathSpeed, last, true, Path[currentPosition]));

           
        }
        
    }







    public float timetoShrink = 1f;
    //private iTween.EaseType _easeType = iTween.EaseType.Spring;

    private IEnumerator MoveDelayed(int delay, float time, bool last, bool playSound, GameObject parent)
    {
        //timetoShrink = time / 2;
        //highlight.SetActive(true);

        yield return new WaitForSeconds(delay * 1f);
        transform.SetParent(parent.transform);
        transform.localPosition = Vector2.zero;
        onClick = true;




        if (last)
        {
            if (sixCount ==0 )
            {
                //rollingDice.SetTurn();
              //  rollingDice.Setflag();
               // sixCount = 0;
            }
            else
            {
                //rollingDice.Setflag();
                //rollingDice.SetTurn();
            }
            rollingDice.SetTurn();
            rollingDice.Setflag(0);
            sixCount = 0;


            for (int j = 0; j < parent.transform.childCount; j++)
            {
                if (parent.transform.GetChild(j).gameObject.tag != transform.gameObject.tag)
                {
                    parent.transform.GetChild(j).gameObject.GetComponent<PlayerMovement>().GoHome();
                }
            }
            rollingDice.HighlightPlayerGoti(rollingDice.GetGoti(1, true),false);
        }


      /*  if (last)
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
   /* private void Update()
    {
        if (Home) gameObject.transform.GetChild(0).gameObject.SetActive(true);
        else if (!Home && rollingDice.GetStep() == 6) gameObject.transform.GetChild(1).gameObject.SetActive(true);
        transform.gameObject.GetComponent<Button>().onClick.AddListener(() => { if (onClick) MovePlayer(); });

    }*/
  public void Bot()
    {
        MovePlayer();
    }
}
