using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateProfiles : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI id;
    [SerializeField] private TextMeshProUGUI wallet;

    public int v;
    // Start is called before the first frame update

    private void Start()
    {
        SetProfile(v);
    }
    private void Update()
    {
        SetProfile(v);
    }
    public void SetProfile(int val)
    {
        if (val == 1) name.text = DataSaver.Instance.firstName + " " + DataSaver.Instance.lastName;
        else if (val == 2) id.text ="ID: "+ DataSaver.Instance._id;
        else if (val == 3) wallet.text = "wallet:\n$" + DataSaver.Instance.deposite;
    }
}
