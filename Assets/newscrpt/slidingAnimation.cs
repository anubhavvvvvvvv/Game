using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slidingAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject []panel=new GameObject[3];
    public GameObject[] text = new GameObject[3];
    public GameObject []Slider=new GameObject[3];
    public float waitValue = 3.0f;

    private void Start()
    {
        StartCoroutine(SlidingAnimation(0));
    }

    IEnumerator SlidingAnimation(int val)
    {
        HideAll();
        panel[val].SetActive(true);
        text[val].SetActive(true);
        Slider[val].SetActive(true);

        yield return new WaitForSeconds(waitValue);
        int k = val;
        if (val >= 2) k = -1;
        StartCoroutine(SlidingAnimation(k+1));
    }

    public void HideAll()
    {
        for (int j = 0; j < 3; j++)
        {
            panel[j].SetActive(false);
            text[j].SetActive(false);
            Slider[j].SetActive(false);
        }
    }
}
