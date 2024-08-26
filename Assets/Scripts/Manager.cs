using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Manager : MonoBehaviour
{
    public static Manager Instance { get; private set; }
    public GameObject contestPage1, contestPage2;
    public TMP_Text price_txt,price_pool_txt,firstPrice,secondPrice,thirdPrice;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void BackButton_Click()
    {
        NavigationManager.Instance.NavigateBack();
    }
    public void ContestNextPage(string price_txt_value,string pricepool_txt_value, string firstPrice_txt_value, string secondPrice_txt_value, string thirdPrice_txt_value)
    {
        NavigationManager.Instance.NavigateTo(contestPage2);
        contestPage1.SetActive(false);
        contestPage2.SetActive(true);
        price_txt.text = price_txt_value;
        price_pool_txt.text = pricepool_txt_value;
        firstPrice.text = firstPrice_txt_value;
        secondPrice.text = secondPrice_txt_value;
        thirdPrice.text = thirdPrice_txt_value;
    }
}
