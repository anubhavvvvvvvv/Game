using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject Classic;
    [SerializeField] private GameObject Rush;

    void Start()
    {
       /* Debug.Log("ll"+ PlayerPrefs.GetString("gamemode"));
        if (PlayerPrefs.GetString("gamemode") == "classic")
        {
            Rush.SetActive(false);
            Classic.SetActive(true);
        }
        else */if (PlayerPrefs.HasKey("gamemode") )
        {
            Classic.SetActive(false);
            Rush.SetActive(true);
        }
        PlayerPrefs.DeleteKey("gamemode");
    }

 
}
