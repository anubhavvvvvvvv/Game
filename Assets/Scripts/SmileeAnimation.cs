using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class SmileeAnimation : MonoBehaviour
{
    private float popupElapsetime=0;
    private float popdownElapsetime=0;

    public float popuptime=3f; 
    public float popdowntime=3f;

    private Vector3 initialScale;
    public float finalscale;
    private Vector3 FfinalScale;

    public float holdTime = 5.0f;

    public GameObject ob;
    public bool loop = false;
    public TextMeshProUGUI text;
    public bool Istext = false;
    public int time = 10;
    public GameObject NotFound;
    public GameObject current;
    // Start is called before the first frame update
    private void OnEnable()
    {   
        time=10;text.text = time.ToString();
        eenable();
    }
    public void eenable()
    {
         popupElapsetime = 0; popdownElapsetime = 0;
        initialScale = ob.transform.localScale;
        FfinalScale = ob.transform.localScale;
        Debug.Log("Ffinal " + FfinalScale+"  "+ initialScale);
        StartCoroutine(popUp());
    }

    IEnumerator popUp()
    {
        
        Debug.Log("popup"+popupElapsetime+" "+popuptime);
        while (popupElapsetime < popuptime)
        {
            ob.transform.localScale = Vector3.Lerp(initialScale, Vector3.one * finalscale, popupElapsetime / popuptime);
            popupElapsetime += Time.deltaTime;
            yield return null;
        }
        initialScale = ob.transform.localScale;
        Debug.Log("init "+initialScale);
        StartCoroutine(popDown());
        
    }
    IEnumerator popDown()
    {
        //yield return new WaitForSeconds(holdTime);
        Debug.Log("popdown");
        while (popdownElapsetime < popdowntime)
        {
            ob.transform.localScale = Vector3.Lerp(initialScale, FfinalScale, popdownElapsetime / popdowntime);
            popdownElapsetime += Time.deltaTime;
            yield return null;
        }
        time--;
        if (Istext && time>=0) text.text = time.ToString();
        
        if (loop) eenable();
        //Invoke("SetDeActive", 5.0f);
        if (time == 0) PlayerNotFound();

    }
    void LateUpdate()
    {
        //if (time == 0) PlayerNotFound();

    }
    public void SetDeActive()
    {
        transform.gameObject.SetActive(false);
    }
    public void PlayerNotFound()
    {
        NotFound.SetActive(true);
        current.SetActive(false);
    }
    public int GetTimer(){return time;}
}
