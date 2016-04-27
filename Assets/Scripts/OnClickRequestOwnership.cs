using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PhotonView))]
public class OnClickRequestOwnership : Photon.MonoBehaviour
{
    public void Take()
    {
        Debug.Log("Take called!");
        if (this.photonView.ownerId == PhotonNetwork.player.ID)
        {
            Debug.Log("Not requesting ownership. Already mine.");
            return;
        }

        this.photonView.RequestOwnership();
    }
}