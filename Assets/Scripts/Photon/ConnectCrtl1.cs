using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine.UI;
using TMPro;
using Photon.Pun.Demo.SlotRacer;
using System.Drawing;

public enum RegionCode
{
    AUTO,
    CAE,
    EU,
    US,
    USW,
    SA
}

public class ConnectCrtl1 : MonoBehaviourPunCallbacks
{
    [SerializeField] string gameVersion = "1";
    [SerializeField] string regionCode = null;
    [SerializeField] Button button;
    [SerializeField] Button readyButton;
    [SerializeField] private GameObject panelConnect;
    [SerializeField] private GameObject panelRoom;
    [SerializeField] TMP_Dropdown dropdownColors;

    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void SetRegion(int index)
    {
        RegionCode region = (RegionCode)index;

        if (region == RegionCode.AUTO)
        {
            regionCode = null;
        }
        else
        {
            regionCode = region.ToString();
        }

        Debug.Log("Region seleccionada: " + regionCode);
        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = regionCode;

    }

    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }

    void ShowRoomPanel()
    {
        panelConnect.SetActive(false);
        panelRoom.SetActive(true);
    }

    void SetButton(bool state, string msg)
    {
        button.GetComponentInChildren<TMP_Text>().text = msg;
        button.enabled = state;
    }

    public void SetColor(int index)
    {
        string color = dropdownColors.options[index].text;

        if (color == "Colors")
        {
            readyButton.interactable = false;
            Debug.Log("Choose a color please.");
        }
        else
        {
            Debug.Log("Color: " + color);
            var propsToSet = new ExitGames.Client.Photon.Hashtable() { { "color", color } };
            PhotonNetwork.LocalPlayer.SetCustomProperties(propsToSet);
            readyButton.interactable = true;
        }
    }

    #region monobehaviour callbacks

    public override void OnConnectedToMaster()
    {
        Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedMaster() was called by PUN");
        ShowRoomPanel();
        SetButton(true, "LETS BATTLE!!");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called  by PUN with reason {0}", cause);

    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        PhotonNetwork.CreateRoom(null, new RoomOptions());
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
        SetButton(false, "Waiting Players");

        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            print("Room ready");
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {

        Debug.Log(newPlayer.NickName + " Se ha unido al cuarto, Players: " + PhotonNetwork.CurrentRoom.PlayerCount);

        if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Game");
        }
    }
    #endregion
}
