using UnityEngine;
using System.Collections;

public class General : MonoBehaviour
{

    public void OnOwnershipRequest(object[] viewAndPlayer)
    {
        PhotonView view = viewAndPlayer[0] as PhotonView;
        PhotonPlayer requestingPlayer = viewAndPlayer[1] as PhotonPlayer;
        view.TransferOwnership(requestingPlayer.ID);
    }
}
