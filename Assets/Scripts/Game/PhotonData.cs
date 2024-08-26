using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class PhotonData : MonoBehaviourPunCallbacks
{
    public RollingDice rolling;
    // Start is called before the first frame update
    [PunRPC]
    void PhotonStep(int step)
    {
        rolling.SetStep(step);
        Debug.Log(")))))))))))))Pstep" + step); 
    }
    [PunRPC]
    void PhotonPlayer(int step)
    {
        rolling.SetPhotonPLayer(step);
        Debug.Log(")))))))))Pplayer" + step);
    }
    [PunRPC]
    void PhotonPlayerPass(int step)
    {
        if (PhotonNetwork.LocalPlayer.ActorNumber != step)
        {
            rolling.SetPass(true);
            Debug.Log(")))))))))Pplayerpass" + PhotonNetwork.LocalPlayer.ActorNumber + " " + step);
        }
    }
}
