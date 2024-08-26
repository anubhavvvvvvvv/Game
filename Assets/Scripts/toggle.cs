using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggle : MonoBehaviour
{
   public bool tog=false;
   public GameObject Onn,Off;

   public void OnClick()
   {
      if(tog)
      {
        Onn.gameObject.SetActive(true);
        Off.gameObject.SetActive(false);
      }
      else
      {
          Onn.gameObject.SetActive(false);
          Off.gameObject.SetActive(true);
      }
      tog=!tog;
   }


    public GameObject current;
    public GameObject next;

    public GameObject game;
    public GameObject menu;

    public void EnterGame()
    {
        Debug.Log(DataSaver.Instance.GetMode());
        if (DataSaver.Instance.GetMode() == 1)
        {
            menu.SetActive(false);
            game.SetActive(true);
        }
        else
        {
            current.SetActive(false);
            next.SetActive(true);
        }

    }
}
