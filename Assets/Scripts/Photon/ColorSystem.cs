using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class ColorSystem : MonoBehaviourPun, IOnEventCallback
{
    public static ColorSystem Instance;

    public void Awake()
    {
        Instance = this;
    }

    private const byte ColorEventCode = 2;
    private Renderer playerRenderer;

    private void Start()
    {
        playerRenderer = GetComponentInChildren<Renderer>();
    }

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void SetCustomColor(int id, string color)
    {
        object[] data = new object[2];
        data[0] = id;
        data[1] = color;
        RaiseEventOptions eventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.All,
            CachingOption = EventCaching.AddToRoomCache,
        };

        PhotonNetwork.RaiseEvent(ColorEventCode, data, eventOptions, SendOptions.SendReliable);
    }

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == ColorEventCode)
        {
            object[] data = (object[])photonEvent.CustomData;
            ChangeColor(data);
        }
    }

    private void ChangeColor(object[] data)
    {
        int objId = (int)data[0];
        Color color = ToColor((string)data[1]);
        if (photonView.ViewID == objId)
        {
            playerRenderer.material.color = color;
        }
    }

    public Color ToColor(string color)
    {
        return (Color)typeof(Color).GetProperty(color.ToLowerInvariant()).GetValue(null, null);
    }
}
