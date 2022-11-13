using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Pun.Demo.SlotRacer.Utils;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSystem : MonoBehaviour, IOnEventCallback
{
    private const byte CureEventCode = 1;
    [SerializeField] GameObject cure;
    [SerializeField] Transform spawn;
    
    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            GenerateCure();
        }
    }
    private void GenerateCure()
    {
        RaiseEventOptions eventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.All,
            CachingOption = EventCaching.AddToRoomCache,
        };

        PhotonNetwork.RaiseEvent(CureEventCode, null, eventOptions, SendOptions.SendReliable);
    }

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == CureEventCode)
        {
            Debug.Log("Generar CURA");
            Instantiate(cure, spawn.position, Quaternion.identity);
        }
    }

    
}
