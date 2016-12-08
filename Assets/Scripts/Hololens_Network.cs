using UnityEngine;
using System.Collections;

public class Hololens_Network : Photon.PunBehaviour
{
    public Player player;

    string roomName;
    float[] accelArray = new float[1];

    // LOOK-1.b: creating a room on PC
    void Start()
    {
        // Make sure "Auto-Join Lobby" was checked at 
        //   Assets-> Photon Unity Networking-> Resources-> PhotonServerSettings
        //   so the application will automatically connect to Lobby
        //   and call OnJoinedLobby()
        PhotonNetwork.ConnectUsingSettings("0.1");
        roomName = GenerateRoomName();
    }

    static string GenerateRoomName()
    {
        const string characters = "abcdefghijklmnopqrstuvwxyz0123456789"; //add the characters you want

        string result = "";

        int charAmount = Random.Range(4, 6); //set those to the minimum and maximum length of your string
        for (int i = 0; i < charAmount; i++)
        {
            result += characters[Random.Range(0, characters.Length)];
        }

        return result;
    }

    public override void OnJoinedLobby()
    {
        //PhotonNetwork.CreateRoom(null);
        PhotonNetwork.CreateRoom(roomName);
    }

    // Look-1.b: We are not doing anything in the functions below
    // , but you may want to do something at the corresponding mobile function
    // On mobile client, use OnJoinedRoom() instead of OnCreatedRoom()
    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
    {
        base.OnPhotonJoinRoomFailed(codeAndMsg);
    }
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
    }

    // setup our OnEvent as callback:
    void Awake()
    {
        PhotonNetwork.OnEventCall += this.OnEvent;
    }
 
    // handle events:
    private void OnEvent(byte eventcode, object content, int senderid)
    {
        if (eventcode == 0) // cast event. should have gotten back a single float.
        {
            PhotonPlayer sender = PhotonPlayer.Find(senderid);  // who sent this?
            float[] result = (float[])content;
            //print(result[0]);
            player.Cast(result[0] * 3.0f);
        }
        if (eventcode == 1) // reel event. should have gotten 1 byte, no need to do anything to it.
        {
            //print("reeling in!");
            player.Reel();
        }
    }
}
