using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI Occupancy_RateText_ForSchool;
    public TextMeshProUGUI Occupancy_RateText_ForOutdoor;
    private string maptype;
       public string linkToOpen = "https://www.example.com";

    private void Start() {
        PhotonNetwork.AutomaticallySyncScene = true;
        if (!PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        else{
            PhotonNetwork.JoinLobby();
        }
    }
    public void JoinRandomRoom(){
        PhotonNetwork.JoinRandomRoom();
    }

    public void OnEnterButtonClicked_Outdoor(){
        maptype = MultiplayerConstants.MAP_TYPE_KEY_OUTDOOR;
    ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties = new ExitGames.Client.Photon.Hashtable() {{MultiplayerConstants.MAP_TYPE_KEY, maptype}};
    PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties,0);
    }

   public void OnEnterButtonClicked_School() {
    maptype = MultiplayerConstants.MAP_TYPE_KEY_SCHOOL;
    ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties = new ExitGames.Client.Photon.Hashtable() {{MultiplayerConstants.MAP_TYPE_KEY, maptype}};
    PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties,0);
    }
    public void OpenLink()
    {
        // Open the link in a web browser
        Application.OpenURL(linkToOpen);
    }

    public override void OnJoinRandomFailed(short returncode, string message){
        Debug.Log(message);
        CreateandJoinRoom();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connecte to Server again");
        PhotonNetwork.JoinLobby();
    }

    public override void OnCreatedRoom(){
        Debug.Log("A room is created with the name: "+ PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("The local player "+ PhotonNetwork.NickName);

        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(MultiplayerConstants.MAP_TYPE_KEY)){
            object mapType;
            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(MultiplayerConstants.MAP_TYPE_KEY,out mapType))
            {
                Debug.Log("Joined room with the map: "+(string)mapType );

                if ((string)mapType == MultiplayerConstants.MAP_TYPE_KEY_SCHOOL)
                {
                    PhotonNetwork.LoadLevel("World_School");
                }
                else if((string)mapType == MultiplayerConstants.MAP_TYPE_KEY_OUTDOOR)
                {
                    PhotonNetwork.LoadLevel("World_Outdoor");
                }
            }
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer){
        Debug.Log(newPlayer.NickName + "Joined to" + "Player Count: "+ PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnRoomListUpdate(List< RoomInfo> roomList)
    {
        if (roomList.Count == 0)
        {
            Occupancy_RateText_ForSchool.text= 0 + "/" + 20;
            Occupancy_RateText_ForOutdoor.text = 0+ "/" + 20;
        }

        foreach (RoomInfo room in roomList)
        {
            Debug.Log(room.Name);
            if (room.Name.Contains(MultiplayerConstants.MAP_TYPE_KEY_OUTDOOR))
            {
                Debug.Log("Room is a outdoor map. Player count is: "+ room.PlayerCount);
                Occupancy_RateText_ForOutdoor.text = room.PlayerCount + "/" + 20;
            }else if (room.Name.Contains(MultiplayerConstants.MAP_TYPE_KEY_SCHOOL))
            {
                Debug.Log("Room is a School map. Player count is: "+ room.PlayerCount);
                Occupancy_RateText_ForSchool.text = room.PlayerCount + "/" + 20;
            }
        }
    }

    public override void OnJoinedLobby(){
        Debug.Log("Joined the lobby");
    }

    private void CreateandJoinRoom()
    {
        string randomRoomName = "Room_"+ maptype + Random.Range(0,10000);
        RoomOptions roomoptions = new RoomOptions();
        roomoptions.MaxPlayers = 20;

        string[] roompropsInLobby = {MultiplayerConstants.MAP_TYPE_KEY};

        ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable() {{MultiplayerConstants.MAP_TYPE_KEY,maptype}};

        roomoptions.CustomRoomPropertiesForLobby = roompropsInLobby;
        roomoptions.CustomRoomProperties = customRoomProperties;

        PhotonNetwork.CreateRoom(randomRoomName,roomoptions);
    }

}
