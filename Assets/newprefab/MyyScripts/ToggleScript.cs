using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleScript : MonoBehaviour
{
    public GameObject toggleButon1, toggleButton2;
    public void ToggleButton(bool val)
    {
        toggleButon1.SetActive(false);
        toggleButton2.SetActive(false);
        if (val) toggleButon1.SetActive(true);
        else toggleButton2.SetActive(true);

    }


    
}
