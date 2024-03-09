/**
 * Control end game
 * 
 * @version 1.0.0
 * - Change class name EndCurrentGameControl_Script EndGameController
 * - Code optimization
 * @author S3
 * @date 2024/03/07
*/

using Unity.Netcode;
using UnityEngine;

public class EndGameController : NetworkBehaviour, Panel
{
    SceneDirector director;
    InGameCanvasController inGame;
    PlayWithAnotherPcController anotherPc;

    GameObject netManager;
    NetworkManager net;

    private void Awake()
    {
        director = GameObject.Find("SceneDirector").GetComponent<SceneDirector>();
        inGame = GameObject.Find("InGameCanvas").GetComponent<InGameCanvasController>();
        anotherPc = GameObject.Find("PlayWithAnotherPcPanel").GetComponent<PlayWithAnotherPcController>();

        netManager = GameObject.Find("NetworkManager");
        net = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }

    private void Update() 
    {
        EscListener();

        if(inGame.GetGameMode() == 2)
            if (net.IsHost && net.ConnectedClients.Count != 2 || !net.IsConnectedClient)
                CancelEndGame();
    }

    public void Init() { }

    public void SetPanel(bool OnOff) {
        gameObject.SetActive(OnOff);
    }

    public bool GetPanelIsOn() { return gameObject.activeSelf; }

    public void EscListener()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            CancelEndGame();
    }

    // End game
    public void EndGame()
    {
        director.Main();

        if (inGame.GetGameMode() == 2)
            ShutDownNet();
    }

    // End game cancel
    public void CancelEndGame()
    {
        inGame.SetPlayGame(true);
        gameObject.SetActive(false);

        if (inGame.GetGameMode() == 2)
            SendCancelMessage();
    }

    private void ShutDownNet()
    {
        net.Shutdown();
        netManager.SetActive(false);

        anotherPc.SetPanel(true);
    }

    // Send cancel end game message for opponent
    private void SendCancelMessage()
    {
        using FastBufferWriter writer = new FastBufferWriter(256, Unity.Collections.Allocator.Temp);
        writer.WriteValueSafe(0);
        if (net.IsHost)
            NetworkManager.Singleton.CustomMessagingManager.SendNamedMessage("pauseGame", net.ConnectedClientsIds[1], writer, NetworkDelivery.Reliable);
        else
            NetworkManager.Singleton.CustomMessagingManager.SendNamedMessage("pauseGame", OwnerClientId, writer, NetworkDelivery.Reliable);
    }
}