using Photon;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NetworkManager : Photon.PunBehaviour 
{
	void Start()
	{
		PhotonNetwork.ConnectUsingSettings("0.1");
		PhotonNetwork.logLevel = PhotonLogLevel.Full;

	}

	void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}

	public override void OnReceivedRoomListUpdate ()
	{
		base.OnReceivedRoomListUpdate ();
	}

	public void CreatePublicRoom()
    {
        PhotonNetwork.autoCleanUpPlayerObjects = false;
        PhotonNetwork.CreateRoom (null);
	}

	public void JoinRandomPublicRoom()
    {
        PhotonNetwork.autoCleanUpPlayerObjects = false;
        PhotonNetwork.JoinRandomRoom();
	}

	public void CreatePrivateRoom()
	{
		Debug.Log ("Create room called!");
		GameObject nameInputFieldGO = GameObject.Find ("Name");
		InputField nameInputField = nameInputFieldGO.GetComponent<InputField> ();
		Debug.Log ("Name is :" + nameInputField.text);

		GameObject passwordInputFieldGO = GameObject.Find ("Name");
		InputField passwordInputField = nameInputFieldGO.GetComponent<InputField> ();
		Debug.Log ("Password is: " + passwordInputField.text);

		if (nameInputField.text != "" && passwordInputField.text != "")
		{
			Debug.Log ("Created room with name and password!");
			RoomOptions ro = new RoomOptions ();
			ro.isVisible = false;

            PhotonNetwork.autoCleanUpPlayerObjects = false;
            PhotonNetwork.CreateRoom (nameInputField.text + passwordInputField.text, ro, null);
		} 
		else 
			Debug.Log ("Enter a room name and password.");		
	}

	public void JoinPrivateRoom()
	{
		Debug.Log ("Join room called!");
		GameObject nameInputFieldGO = GameObject.Find ("Name");
		InputField nameInputField = nameInputFieldGO.GetComponent<InputField> ();
		Debug.Log ("Name is :" + nameInputField.text);

		GameObject passwordInputFieldGO = GameObject.Find ("Name");
		InputField passwordInputField = nameInputFieldGO.GetComponent<InputField> ();
		Debug.Log ("Password is: " + passwordInputField.text);

		if (nameInputField.text != "" && passwordInputField.text != "")
		{
			Debug.Log ("Joined room with name and password!");
            PhotonNetwork.autoCleanUpPlayerObjects = false;
            PhotonNetwork.JoinRoom (nameInputField.text + passwordInputField.text);
		} 
		else
			Debug.Log ("Room name and password incorrect.");
	}

	public override void OnJoinedRoom()
	{
        PhotonNetwork.LoadLevel ("Editor");
		Debug.Log ("Test called!");
	}
}
