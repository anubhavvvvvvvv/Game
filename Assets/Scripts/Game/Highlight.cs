using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("update");
        transform.Rotate(new Vector3(0f, 0f, 400.0f * Time.deltaTime));
    }
}
