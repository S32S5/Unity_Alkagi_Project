/**
 * Control is right player answer panel
 * 
 * @version 1.0.0
 * - Change class name IsRightPlayerAnswer_Script to IsRightPlayerAnswerController
 * - Code optimization
 * @author S3
 * @date 2024/03/08
*/

using Unity.Netcode;
using UnityEngine;

public class IsRightPlayerAnswerController : MonoBehaviour, Panel
{
    PlayerWaitingController player;
    NewGameSettingController newGameSetting;

    NetworkManager net;

    private void Awake()
    {
        player = GameObject.Find("PlayerWaitingPanel").GetComponent<PlayerWaitingController>();
        newGameSetting = GameObject.Find("NewGameSettingPanel").GetComponent<NewGameSettingController>();

        net = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }

    private void Update()
    {
        EscListener();
        CheckDisConnected();
    }

    public void Init() { }

    public void SetPanel(bool OnOff){ gameObject.SetActive(OnOff); }

    public bool GetPanelIsOn() { return gameObject.activeSelf; }

    public void EscListener()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            NotButtonOnClick();
    }

    private void CheckDisConnected()
    {
        if (net.ConnectedClients.Count != 2)
            player.SetPanel(true);
    }

    public void IsButtonOnClick()
    {
        gameObject.SetActive(false);
        player.UpdateStatusText("");
        newGameSetting.SetPanel(true);

        SendPermitMessage();
    }

    private void SendPermitMessage()
    {
        using FastBufferWriter writer = new FastBufferWriter(256, Unity.Collections.Allocator.Temp);
        writer.WriteValueSafe(1);
        NetworkManager.Singleton.CustomMessagingManager.SendNamedMessage("permitMessage", net.ConnectedClientsIds[1], writer, NetworkDelivery.Reliable);
    }

    public void NotButtonOnClick()
    {
        net.DisconnectClient(net.ConnectedClientsIds[1]);
        player.SetPanel(true);
    }
}