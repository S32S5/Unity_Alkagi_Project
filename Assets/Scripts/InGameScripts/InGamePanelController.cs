/**
 * Control esc listener and network connecting
 * 
 * @version 1.1.0
 * - Another pc mode's encapsulation
 * - Add AiUpdate
 * - Code optimization
 * @author S3
 * @date 2024/03/10
*/

using Unity.Netcode;
using UnityEngine;

public class InGamePanelController : NetworkBehaviour, Panel
{
    float thinkingDelta = 0;

    InGameCanvasController canvas;
    EndGameController end;
    PauseGameController pause;
    DisconnectedController disconnected;
    GameResultController result;

    EggController egg;
    TurnController turn;
    CameraController cam;

    GameObject netManager;
    NetworkManager net;

    private void Awake()
    {
        canvas = GameObject.Find("InGameCanvas").GetComponent<InGameCanvasController>();
        end = GameObject.Find("EndGamePanel").GetComponent<EndGameController>();
        pause = GameObject.Find("PauseGamePanel").GetComponent<PauseGameController>();
        disconnected = GameObject.Find("DisconnectedPanel").GetComponent<DisconnectedController>();
        result = GameObject.Find("GameResultPanel").GetComponent<GameResultController>();

        egg = GetComponent<EggController>();
        turn = GetComponent<TurnController>();
        cam = GetComponent<CameraController>();

        netManager = GameObject.Find("NetworkManager");
        net = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }

    private void Update()
    {
        EscListener();
        GameModeUpdate();
    }

    public void Init() { SetGame();}

    public void SetPanel(bool OnOff) 
    { 
        gameObject.SetActive(OnOff);

        if (OnOff)
            Init();
    }

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

    // Set new game
    private void SetGame()
    {
        egg.Init();
        turn.Init();
        cam.Init();
    }

    // 0 - Ai mode
    // 1 - Play this pc mode
    // 2 - Play with another pc mode
    private void GameModeUpdate()
    {
        if (!result.GetPanelIsOn())
        {
            if (canvas.GetGameMode() == 0)
                AiUpdate();
            else if (canvas.GetGameMode() == 2)
                AnotherPcUpdate();
        }
    }

    // Control ai mode's method
    private void AiUpdate()
    {
        if (turn.GetTurn() && !turn.GetTurnEnd())
        {
            thinkingDelta += Time.deltaTime;
            if(thinkingDelta > 0.25f)
            {
                int playAttack = Random.Range(0, 10);
                if (playAttack == 0)
                {
                    int randInt = Random.Range(0, egg.eggs[1].Count);
                    GameObject attackEgg = egg.eggs[1][randInt];

                    if (canvas.GetAiLevel() == 0)
                        Level0Ai(attackEgg);
                    else if (canvas.GetAiLevel() == 1)
                        Level1Ai(attackEgg);
                    else if (canvas.GetAiLevel() == 2)
                        Level2Ai(attackEgg);
                    else if (canvas.GetAiLevel() == 3)
                        Level3Ai(attackEgg);

                    turn.SetTurnEnd(true);
                }

                thinkingDelta = Time.deltaTime;
            }
        }
    }

    // Control another pc mode's method
    private void AnotherPcUpdate()
    {
        if (net.IsHost && net.ConnectedClients.Count == 2 || net.IsConnectedClient)
        {
            GetPauseMessage();
            GetEggVel();
        }
        else
            GameDisconnected();
    }

    // Attack random dir, power
    private void Level0Ai(GameObject attackEgg)
    {
        float rotationFloat = Random.Range(0f, 360f);
        float powerFloat = Random.Range(0.25f, 1f);
        attackEgg.transform.rotation = Quaternion.Euler(0, 0, rotationFloat);
        attackEgg.GetComponent<Egg>().SetEggVelocity(-powerFloat);
    }

    // Attack straight at random power
    private void Level1Ai(GameObject attackEgg)
    {
        float powerFloat = Random.Range(0.5f, 1f);
        attackEgg.GetComponent<Egg>().SetEggVelocity(-powerFloat);
    }

    // Attack random target dir, power
    private void Level2Ai(GameObject attackEgg)
    {
        int targetEgg = Random.Range(0, egg.eggs[0].Count);
        float powerFloat = Random.Range(0.5f, 1f);
        float angle = Mathf.Atan2(egg.eggs[0][targetEgg].transform.position.y - attackEgg.transform.position.y, egg.eggs[0][targetEgg].transform.position.x - attackEgg.transform.position.x) * Mathf.Rad2Deg;
        attackEgg.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        attackEgg.GetComponent<Egg>().SetEggVelocity(powerFloat);
    }

    // Attack egg at edge, maximum power
    private void Level3Ai(GameObject attackEgg)
    {
        GameObject mostEdge = null;
        float edgePos = 0;
        for (int i = 0; i < egg.eggs[0].Count; i++)
        {
            if (Mathf.Abs(egg.eggs[0][i].transform.position.x) > edgePos)
            {
                mostEdge = egg.eggs[0][i];
                edgePos = Mathf.Abs(egg.eggs[0][i].transform.position.x);
            }

            if (Mathf.Abs(egg.eggs[0][i].transform.position.y) > edgePos)
            {
                mostEdge = egg.eggs[0][i];
                edgePos = Mathf.Abs(egg.eggs[0][i].transform.position.y);
            }
        }

        float angle = Mathf.Atan2(mostEdge.transform.position.y - attackEgg.transform.position.y, mostEdge.transform.position.x - attackEgg.transform.position.x) * Mathf.Rad2Deg;
        attackEgg.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        attackEgg.GetComponent<Egg>().SetEggVelocity(1);
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

            if (!turn.GetTurn())
                synVel(0);
            else
                synVel(1);
            void synVel(int color)
            {
                egg.eggs[color][int.Parse(messageSplit[0])].GetComponent<Rigidbody2D>().velocity = new Vector2(float.Parse(messageSplit[1]), float.Parse(messageSplit[2]));
                turn.SetTurnEnd(true);
            }
        });
    }

    private void GameDisconnected()
    {
        net.Shutdown();
        netManager.SetActive(false);

        disconnected.SetPanel(true);
    }
}