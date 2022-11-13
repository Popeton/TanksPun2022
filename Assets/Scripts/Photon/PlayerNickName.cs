using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlayerNickName : MonoBehaviour
{
    const string playerNamePrefKey = "PlayerName";
    [SerializeField] TMP_InputField inputField;

    void Start()
    {
        string defauldName = string.Empty;

        if (PlayerPrefs.HasKey(playerNamePrefKey))
        {
            defauldName = PlayerPrefs.GetString(playerNamePrefKey);
            inputField.text = defauldName;
        }

        PhotonNetwork.NickName = defauldName;
    }

    public void SetPlayerName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("player name is empty");
            return;
        }

        PhotonNetwork.NickName = value;
        PlayerPrefs.SetString(playerNamePrefKey, value);
    }
}
