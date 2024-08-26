using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FindingTimer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI timer;
    public int _time { get; set; }
    public float waitTime = 1.0f;

    void OnEnable()
    {
        _time = 15;
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        
        timer.text = _time.ToString();
        _time--;
        yield return new WaitForSeconds(waitTime);
        if(_time>=0) StartCoroutine(Timer());
    }

    
}
