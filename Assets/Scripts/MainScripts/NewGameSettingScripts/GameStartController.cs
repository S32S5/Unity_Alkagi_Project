/**
 * Control game start button
 * 
 * @version 1.0.0
 * - Change class name GameStartControl_Script to GameStartController
 * - Code optimization
 * @author S3
 * @date 2024/03/08
*/

using Unity.Netcode;
using UnityEngine;

public class GameStartController : MonoBehaviour
{
    SceneDirector director;
    NewGameSettingController newGame;
    InGameCanvasController inGame;

    InitialSettingDataController data;

    NetworkManager net;

    private void Awake()
    {
        director = GameObject.Find("SceneDirector").GetComponent<SceneDirector>();
        newGame = GameObject.Find("NewGameSettingPanel").GetComponent<NewGameSettingController>();
        inGame = GameObject.Find("InGameCanvas").GetComponent <InGameCanvasController>();

        data = GameObject.Find("NewGameSettingPanel").GetComponent<InitialSettingDataController>();

        net = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }

    public void GameStartButtonOnClick()
    {
        director.InGame();

        if (inGame.GetGameMode() == 2)
        {
            newGame.SetPanel(false);
            SendGameSettingData();
        }
    }

    // Send game setting data for join
    private void SendGameSettingData()
    {
        int eggNum = data.GetEggNum();
        bool first = data.GetFirstTurn();
        string time = data.GetTimeLimit();
        string settingStr = eggNum.ToString() + " " + first.ToString() + " " + time.ToString();
        using FastBufferWriter writer = new FastBufferWriter(256, Unity.Collections.Allocator.Temp);
        writer.WriteValueSafe(settingStr);
        NetworkManager.Singleton.CustomMessagingManager.SendNamedMessage("gameSetting", net.ConnectedClientsIds[1], writer, NetworkDelivery.Reliable);
    }
}