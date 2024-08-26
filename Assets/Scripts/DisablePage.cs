using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisablePage : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject nextPage;
    [SerializeField] private GameObject disablePage;
    [SerializeField] private GameObject yesNnoPage;
    

    public void PageDisable()
    {
        disablePage.SetActive(false);
    }
    public void YesnNo()
    {
        yesNnoPage.SetActive(true);
    }
    public void No()
    {
        yesNnoPage.SetActive(false);
    }
    public void MoveNext(bool mode)
    {
        nextPage.SetActive(true);
        if (mode)
        {
            disablePage.SetActive(false);
            yesNnoPage.SetActive(false);
            Manager.Instance.contestPage1.SetActive(true);
            Manager.Instance.contestPage2.SetActive(false);
            
        }
    }
     public GameObject panel;
    public void LoadGame()
    {
        if(PlayerPrefs.HasKey("mode")){panel.SetActive(true);PlayerPrefs.DeleteKey("mode");}
        else SceneManager.LoadScene("Game");
    }
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }


    public void GameMode(int va)
    {
         if(va==3)
         {
            PlayerPrefs.SetInt("mode", 3);
         }
    }


}
