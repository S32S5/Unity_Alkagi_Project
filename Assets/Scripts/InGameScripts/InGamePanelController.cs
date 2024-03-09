/**
 * Control esc listener and network connecting
 * 
 * @version 1.0.0
 * - Change class name InGame_Script to InGamePanelController
 * - Code optimization
 * @author S3
 * @date 2024/03/09
*/

using Unity.Netcode;
using UnityEngine;

public class InGamePanelController : NetworkBehaviour, Panel
{
    InGameCanvasController canvas;
    EndGameController end;
    PauseGameController pause;
    DisconnectedController disconnected;
    GameResultController result;

    SceneDirector director;
    EggController egg;
    TurnController turn;

    GameObject netManager;
    NetworkManager net;

    private void Awake()
    {
        canvas = GameObject.Find("InGameCanvas").GetComponent<InGameCanvasController>();

        end = GameObject.Find("EndGamePanel").GetComponent<EndGameController>();
        pause = GameObject.Find("PauseGamePanel").GetComponent<PauseGameController>();
        disconnected = GameObject.Find("DisconnectedPanel").GetComponent<DisconnectedController>();
        result = GameObject.Find("GameResultPanel").GetComponent<GameResultController>();

        egg = GameObject.Find("InGameCanvas").GetComponent<EggController>();
        turn = GameObject.Find("InGameCanvas").GetComponent<TurnController>();

        director = GameObject.Find("SceneDirector").GetComponent<SceneDirector>();

        netManager = GameObject.Find("NetworkManager");
        net = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }

    private void Update()
    {
        EscListener();

        if (canvas.GetGameMode() == 2 && !result.GetPanelIsOn())
        {
            if (net.IsHost && net.ConnectedClients.Count != 2 || !net.IsConnectedClient)
                GameDisconnected();
            else
            {
                GetPauseMessage();
                GetEggVel();
            }
        }
    }

    public void Init() { }

    public void SetPanel(bool OnOff) { gameObject.SetActive(OnOff); }

    public bool GetPanelIsOn() { return gameObject.activeSelf; }

    public void EscListener()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!end.GetPanelIsOn() && !pause.GetPanelIsOn() && !disconnected.GetPanelIsOn() && !result.GetPanelIsOn())
            {
                end.SetPanel(true);

                if (canvas.GetGameMode() == 2)
                    SendPauseMessage();
            }
        }
    }

    private void GameDisconnected()
    {
        net.Shutdown();
        netManager.SetActive(false);

        disconnected.SetPanel(true);
    }

    // Send pause message to opponent
    private void SendPauseMessage()
    {
        using FastBufferWriter writer = new FastBufferWriter(256, Unity.Collections.Allocator.Temp);
        writer.WriteValueSafe(1);
        if (net.IsHost)
            NetworkManager.Singleton.CustomMessagingManager.SendNamedMessage("pauseGame", net.ConnectedClientsIds[1], writer, NetworkDelivery.Reliable);
        else
            NetworkManager.Singleton.CustomMessagingManager.SendNamedMessage("pauseGame", OwnerClientId, writer, NetworkDelivery.Reliable);
    }

    // Get pause message to opponent
    private void GetPauseMessage()
    {
        NetworkManager.Singleton.CustomMessagingManager.RegisterNamedMessageHandler("pauseGame", (senderClientId, reader) =>
        {
            reader.ReadValueSafe(out int reply);

            if (reply == 0)
                pause.SetPanel(false);
            else
                pause.SetPanel(true);
        });
    }

    private void GetEggVel()
    {
        NetworkManager.Singleton.CustomMessagingManager.RegisterNamedMessageHandler("eggVel", (senderClientId, reader) =>
        {
            reader.ReadValueSafe(out string message);
            string[] messageSplit = message.Split(" ");

            void synVel(int color)
            {
                egg.eggs[color][int.Parse(messageSplit[0])].GetComponent<Rigidbody2D>().velocity = new Vector2(float.Parse(messageSplit[1]), float.Parse(messageSplit[2]));
                egg.StartCoroutine(egg.eggs[color][int.Parse(messageSplit[0])].GetComponent<Egg>().EggIsMoving());
                turn.SetTurnEnd(true);
            }

            if (!turn.GetTurn())
                synVel(0);
            else
                synVel(1);
        });
    }
}