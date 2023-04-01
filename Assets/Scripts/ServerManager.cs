using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Servere bağlanıldı");
        Debug.Log("Lobiye bağlanılıyor");
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("Lobiye bağlanıldı");
        Debug.Log("Odaya bağlanılıyor");
        PhotonNetwork.JoinOrCreateRoom("Odaismi", new RoomOptions {MaxPlayers=2, IsOpen=true, IsVisible=true}, TypedLobby.Default); ;
    }
   
    public override void OnJoinedRoom() 
    {
        base .OnJoinedRoom();
        Debug.Log("Odaya bağlanıldı");
        Debug.Log("Karakter oluşturuluyor...");
        PhotonNetwork.Instantiate("Kemal", new Vector3(-10, 2.54f, 7.75f), Quaternion.identity, 0, null);
    }

}
