/**
 * Manage is right player answer
 * 
 * @version 0.0.4, New script
 * @author S3
 * @date 2024/02/24
*/

using Unity.Netcode;
using UnityEngine;

public class IsRightPlayerAnswer_Script : MonoBehaviour
{
    private PlayerWaiting_Script player;
    private NewGameSetting_Script newGameSetting;

    private NetworkManager net;

    // Specifies
    private void Awake()
    {
        player = GameObject.Find("PlayerWaiting_Panel").GetComponent<PlayerWaiting_Script>();
        newGameSetting = GameObject.Find("NewGameSetting_Panel").GetComponent<NewGameSetting_Script>();

        net = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }

    // Set panel active false when game start
    private void Start() { gameObject.SetActive(false); }

    // Esc
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            NotButtonOnClick();
        else if (net.ConnectedClients.Count != 2)
        {
            gameObject.SetActive(false);
            player.SetCancelOkButton(true, "취소");
            player.StartCoroutine(player.ConnectingToJoin());
        }
    }

    // Return panel is on
    //
    // @return bool
    public bool GetPanelIsOn() { return gameObject.activeSelf; }

    // IsRightPlayerAnswer_Panel set on off
    public void SetPanel() { gameObject.SetActive(true); }

    // IsButton onclick listener
    public void IsButtonOnClick()
    {
        using FastBufferWriter writer = new FastBufferWriter(256, Unity.Collections.Allocator.Temp);
        writer.WriteValueSafe(1);
        NetworkManager.Singleton.CustomMessagingManager.SendNamedMessage("permitAnswer", net.ConnectedClientsIds[1], writer, NetworkDelivery.Reliable);

        gameObject.SetActive(false);
        player.UpdateStatusText("");
        newGameSetting.SetPanel(true);
        newGameSetting.SetGameMode(1);
    }

    // NotButton onclick listener
    public void NotButtonOnClick()
    {
        gameObject.SetActive(false);
        net.DisconnectClient(net.ConnectedClientsIds[1]);
        player.SetCancelOkButton(true, "취소");
        player.StartCoroutine(player.ConnectingToJoin());
    }
}