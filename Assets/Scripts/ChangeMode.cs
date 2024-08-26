using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMode : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject classicObject;
    [SerializeField] private GameObject rushObject;

    [SerializeField] private Sprite check;
    [SerializeField] private Sprite uncheck;

    private void Start()
    {
        PlayerPrefs.SetInt("PlayerCount", 2);
        PlayerPrefs.SetInt("BoardColor",1);
        DataSaver.Instance.SetPlayerCount(2);
        //PlayerPrefs.SetString("gamemode", "classic");
    }
    

    public void ChangeGameMode(bool mode)
    {
        if (mode)
        {
            Debug.Log("classic");
           // PlayerPrefs.SetString("gamemode", "classic");
            classicObject.GetComponent<Image>().sprite = check;
            rushObject.GetComponent<Image>().sprite = uncheck;
        }
        else
        {

            PlayerPrefs.SetString("gamemode", "rush");
            Debug.Log("rush"+ PlayerPrefs.GetInt("gamemode"));
            classicObject.GetComponent<Image>().sprite = uncheck;
            rushObject.GetComponent<Image>().sprite = check;
        }
    }



    [SerializeField] private GameObject player1Object;
    [SerializeField] private GameObject player2Object;
    [SerializeField] private GameObject player3Object;

    [SerializeField] private Sprite checksign;
    [SerializeField] private Sprite unchecksign;
    public void SelectPlayer(int val)
    {
        if (val == 1)
        {
            PlayerPrefs.SetInt("PlayerCount", 2);
            player1Object.GetComponent<Image>().sprite = checksign;
            player2Object.GetComponent<Image>().sprite = unchecksign;
            player3Object.GetComponent<Image>().sprite = unchecksign;
        }
        else if (val == 2)
        {
            PlayerPrefs.SetInt("PlayerCount", 3);
            player2Object.GetComponent<Image>().sprite = checksign;
            player1Object.GetComponent<Image>().sprite = unchecksign;
            player3Object.GetComponent<Image>().sprite = unchecksign;
        }
        else if (val == 3)
        {
            PlayerPrefs.SetInt("PlayerCount", 4);
            player3Object.GetComponent<Image>().sprite = checksign;
            player2Object.GetComponent<Image>().sprite = unchecksign;
            player1Object.GetComponent<Image>().sprite = unchecksign;
        }
    }


    [SerializeField] private GameObject color1;
    [SerializeField] private GameObject color2;
    [SerializeField] private GameObject color3;
    [SerializeField] private GameObject color4;
    public void SelectColor(int val)
    {
        color1.transform.GetChild(0).gameObject.SetActive(false);
        color1.transform.GetChild(1).gameObject.SetActive(false);

        color2.transform.GetChild(0).gameObject.SetActive(false);
        color2.transform.GetChild(1).gameObject.SetActive(false);

        color3.transform.GetChild(0).gameObject.SetActive(false);
        color3.transform.GetChild(1).gameObject.SetActive(false);

        color4.transform.GetChild(0).gameObject.SetActive(false);
        color4.transform.GetChild(1).gameObject.SetActive(false);

        if (val == 1)
        {
            PlayerPrefs.SetInt("BoardColor",3);
            color1.transform.GetChild(0).gameObject.SetActive(true);
            color1.transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (val == 2)
        {
            PlayerPrefs.SetInt("BoardColor", 2);
            color2.transform.GetChild(0).gameObject.SetActive(true);
            color2.transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (val == 3)
        {
            PlayerPrefs.SetInt("BoardColor", 4);
            color3.transform.GetChild(0).gameObject.SetActive(true);
            color3.transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (val == 4)
        {
            PlayerPrefs.SetInt("BoardColor", 1);
            color4.transform.GetChild(0).gameObject.SetActive(true);
            color4.transform.GetChild(1).gameObject.SetActive(true);
        }
        
    }

    [SerializeField] private GameObject daily;
    [SerializeField] private GameObject monthly;
    [SerializeField] private GameObject weekly;

    [SerializeField] private GameObject dailyPanel;
    [SerializeField] private GameObject monthlyPanel;
    [SerializeField] private GameObject weeklyPanel;

    public void SelectDay(int val)
    {
        daily.SetActive(true);
        monthly.SetActive(true);
        weekly.SetActive(true);
        dailyPanel.SetActive(false);
        monthlyPanel.SetActive(false);
        weeklyPanel.SetActive(false);


        if (val == 1)
        {
            DataSaver.Instance.SetPlayerCount(2);
            //daily.SetActive(false);
            dailyPanel.SetActive(true);
        }
        else if (val == 2)
        {
            DataSaver.Instance.SetPlayerCount(3);
            // monthly.SetActive(false);
            monthlyPanel.SetActive(true);
        }
        else if (val == 3)
        {
            DataSaver.Instance.SetPlayerCount(4);
            // weekly.SetActive(false);
            weeklyPanel.SetActive(true);
        }
      

    }

    [SerializeField] private GameObject soundObj;
    [SerializeField] private GameObject musicObj;
    bool sound = false;
    bool music = false;
    public void SoundMusic(int val)
    {
        if (val == 1)
        {
            if (sound) soundObj.transform.GetChild(0).gameObject.SetActive(true);
            else soundObj.transform.GetChild(0).gameObject.SetActive(false);
            sound = !sound;
        }
        else
        {
            if (music) musicObj.transform.GetChild(0).gameObject.SetActive(true);
            else musicObj.transform.GetChild(0).gameObject.SetActive(false);
            music = !music;
        }
    }


}
