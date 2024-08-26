using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TMP_InputField InputCode;
    [SerializeField] GameObject next;
    [SerializeField] GameObject present;

    public void GetCode()
    {
        Debug.Log("code : " + InputCode.text);
        if (InputCode.text == "" || InputCode.text == null)
        {
           
        }
        else
        {
            DataSaver.Instance.SetRoomId(InputCode.text);
            next.SetActive(true);
            present.SetActive(false);
        }
    }

    public void GameMode(int val)
    {
        DataSaver.Instance.SetMode(val);
    }
}
