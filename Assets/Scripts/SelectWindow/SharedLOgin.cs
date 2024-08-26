using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SharedLOgin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString("Login", "yes");
    }
    public void LogOut()
    {
        Debug.Log("logout");
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("SampleScene");
    }
    
}
