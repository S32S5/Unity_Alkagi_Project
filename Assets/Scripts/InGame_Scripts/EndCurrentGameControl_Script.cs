/**
 * Manages related to EndCurrentGame
 * 
 * @version 0.0.4
 * - Code optimization
 * @author S3
 * @date 2024/02/26
*/

using Unity.Netcode;
using UnityEngine;

public class EndCurrentGameControl_Script : NetworkBehaviour
{
    private Scene_Director_Script director;
    private InGame_Script inGame;
    private HostJoin_Script hostJoin;
    private PlayerWaiting_Script player;
    private JoinWaiting_Script join;

    private GameObject netManager;
    private NetworkManager net;

    // Specifies
    private void Awake()
    {
        director = GameObject.Find("Scene_Director").GetComponent<Scene_Director_Script>();
        inGame = GameObject.Find("InGame_Panel").GetComponent<InGame_Script>();
        hostJoin = GameObject.Find("HostJoin_Panel").GetComponent<HostJoin_Script>();
        player = GameObject.Find("PlayerWaiting_Panel").GetComponent<PlayerWaiting_Script>();
        join = GameObject.Find("JoinWaiting_Panel").GetComponent<JoinWaiting_Script>();

        netManager = GameObject.Find("NetworkManager");
        net = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }

    // Esc
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            CancelEndGame();
    }

    // Panel set on off
    //
    // @param bool
    public void SetPanel(bool OnOff) { gameObject.SetActive(OnOff); }

    // Return EndCurrentGame_Panel is active
    // 
    // @return bool
    public bool GetPanelIsOn() { return gameObject.activeSelf; }

    // End current game
    public void EndGame()
    {
        if (inGame.GetGameMode() == 1)
        {
            net.Shutdown();
            netManager.SetActive(false);

            if (net.IsHost)
                player.SetPanel(false);
            else
                join.SetPanel(false);

            hostJoin.SetPanel();
        }

        gameObject.SetActive(false);
        director.MainOn();
    }

    // End current game cancel
    public void CancelEndGame()
    {
        if (inGame.GetGameMode() == 1)
        {
            using FastBufferWriter writer = new FastBufferWriter(256, Unity.Collections.Allocator.Temp);
            writer.WriteValueSafe(0);
            if (net.IsHost)
                NetworkManager.Singleton.CustomMessagingManager.SendNamedMessage("pauseGame", net.ConnectedClientsIds[1], writer, NetworkDelivery.Reliable);
            else
                NetworkManager.Singleton.CustomMessagingManager.SendNamedMessage("pauseGame", OwnerClientId, writer, NetworkDelivery.Reliable);
        }

        inGame.playGame = true;
        gameObject.SetActive(false);
    }
}