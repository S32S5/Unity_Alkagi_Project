/**
 * In Game
 * 
 * @version 0.0.4
 * - Add gameMode
 * - Code optimization
 * @author S3
 * @date 2024/02/24
*/

using Unity.Netcode;
using UnityEngine;

public class InGame_Script : NetworkBehaviour
{
    private int gameMode;
    public bool playGame;

    private GameObject pauseGamePanel;

    private EggControl_Script egg;
    private TurnControl_Script turn;
    private CameraControl_Script cameraControl;
    private EndCurrentGameControl_Script endGame;
    private GameResultControl_Script result;

    private Scene_Director_Script director;

    private GameObject netManager;
    private NetworkManager net;

    // Specifies
    private void Awake()
    {
        pauseGamePanel = GameObject.Find("PauseGame_Panel");
        pauseGamePanel.SetActive(false);

        egg = GetComponent<EggControl_Script>();
        turn = GetComponent<TurnControl_Script>();
        cameraControl = GetComponent<CameraControl_Script>();
        endGame = GameObject.Find("EndCurrentGame_Panel").GetComponent<EndCurrentGameControl_Script>();
        result = GameObject.Find("GameResult_Panel").GetComponent<GameResultControl_Script>();

        director = GameObject.Find("Scene_Director").GetComponent<Scene_Director_Script>();

        netManager = GameObject.Find("NetworkManager");
        net = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!endGame.GetPanelIsOn() && !pauseGamePanel.activeSelf && !result.GetPanelIsOn())
            {
                if(gameMode == 1)
                {
                    using FastBufferWriter writer = new FastBufferWriter(256, Unity.Collections.Allocator.Temp);
                    writer.WriteValueSafe(1);
                    if(net.IsHost)
                        NetworkManager.Singleton.CustomMessagingManager.SendNamedMessage("pauseGame", net.ConnectedClientsIds[1], writer, NetworkDelivery.Reliable);
                    else
                        NetworkManager.Singleton.CustomMessagingManager.SendNamedMessage("pauseGame", OwnerClientId, writer, NetworkDelivery.Reliable);
                }

                endGame.SetPanel(true);
                playGame = false;
            }
        }

        if(gameMode == 1 && !result.GetPanelIsOn())
        {
            if (net.IsHost && net.ConnectedClients.Count != 2)
            {
                net.Shutdown();
                netManager.SetActive(false);

                director.MainOn();
            }
            else if(!net.IsConnectedClient)
            {
                director.MainOn();
            }
            else
            {
                NetworkManager.Singleton.CustomMessagingManager.RegisterNamedMessageHandler("pauseGame", (senderClientId, reader) =>
                {
                    reader.ReadValueSafe(out int pause);

                    if(pause == 0)
                    {
                        playGame = true;
                        pauseGamePanel.SetActive(false);
                    }
                    else
                    {
                        playGame = false;
                        pauseGamePanel.SetActive(true);
                    }
                });

                NetworkManager.Singleton.CustomMessagingManager.RegisterNamedMessageHandler("eggVel", (senderClientId, reader) =>
                {
                    reader.ReadValueSafe(out string message);
                    string[] messageSplit = message.Split(" ");

                    void synVel(int color)
                    {
                        egg.eggs[color][int.Parse(messageSplit[0])].GetComponent<Rigidbody2D>().velocity = new Vector2(float.Parse(messageSplit[1]), float.Parse(messageSplit[2]));
                        egg.StartCoroutine(egg.eggs[color][int.Parse(messageSplit[0])].GetComponent<Egg_Script>().EggIsMoving());
                        turn.SetTurnEnd(true);
                    }

                    if (!turn.GetTurn())
                        synVel(0);
                    else
                        synVel(1);
                });
            }
        }
    }

    // Init
    public void Init(int gameMode, int initialEggNumber, bool firstTurn, int turnTimeLimit)
    {
        this.gameMode = gameMode;
        egg.InitEggs(initialEggNumber);
        turn.InitTurn(firstTurn, turnTimeLimit);
        if(gameMode == 0)
            cameraControl.SetCam(firstTurn);
        else if(gameMode == 1)
        {
            if (net.IsHost)
                cameraControl.SetCam(false);
            else
                cameraControl.SetCam(true);
        }

        endGame.SetPanel(false);
        pauseGamePanel.SetActive(false);

        playGame = true;
    }

    // Return gameMode
    //
    // @return int
    public int GetGameMode() { return gameMode; }
}