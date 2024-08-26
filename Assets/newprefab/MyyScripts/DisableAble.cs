using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisableAble : MonoBehaviour
{

    public GameObject obj;
 
    public void EnableDisable()
    {
        if (obj.activeInHierarchy)
        {
            obj.SetActive(false);
        }
        else
        {
            obj.SetActive(true);
        }
    }

    public GameObject button,button2;
    public void OnClickedButton()
    {
        button.SetActive(false);
        button2.SetActive(true);
    }
}
