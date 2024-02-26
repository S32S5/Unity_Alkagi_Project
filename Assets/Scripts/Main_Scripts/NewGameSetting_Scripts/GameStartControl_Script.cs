/**
 * Manage related to game start
 * 
 * @version 0.0.4
 * - Set the action of the game start button differently for each mode
 * - Code optimization
 * @author S3
 * @date 2024/02/25
*/

using Unity.Netcode;
using UnityEngine;

public class GameStartControl_Script : MonoBehaviour
{
    private NewGameSetting_Script newGameSetting;
    private PlayerWaiting_Script player;
    private InitialEggNumberControl_Script initialEggNum;
    private FirstTurnControl_Script firstTurn;
    private TurnTimeLimitControl_Script turnTimeLimit;

    private Scene_Director_Script director;
    private NetworkManager net;

    // Specifies
    private void Awake()
    {
        newGameSetting = GetComponent<NewGameSetting_Script>();
        player = GameObject.Find("PlayerWaiting_Panel").GetComponent<PlayerWaiting_Script>();
        initialEggNum = GetComponent<InitialEggNumberControl_Script>();
        firstTurn = GetComponent<FirstTurnControl_Script>();
        turnTimeLimit = GetComponent<TurnTimeLimitControl_Script>();

        director = GameObject.Find("Scene_Director").GetComponent<Scene_Director_Script>();
        net = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }

    // Game start button onclick listener
    public void GameStartButtonOnClick()
    {
        int mode = newGameSetting.GetGameMode();
        int eggNum = initialEggNum.GetEggNumber();
        bool first = firstTurn.GetFirstTurn();
        int time = turnTimeLimit.GetTurnTimeLimit();

        if (newGameSetting.GetGameMode() == 1)
        {
            newGameSetting.SetPanel(false);
            player.UpdateStatusText("대국 상대와 연결이 끊어졌습니다.");
            player.SetCancelOkButton(true, "확인");
            string settingStr = eggNum.ToString() + " " + first.ToString() + " " + time.ToString();
            using FastBufferWriter writer = new FastBufferWriter(256, Unity.Collections.Allocator.Temp);
            writer.WriteValueSafe(settingStr);
            NetworkManager.Singleton.CustomMessagingManager.SendNamedMessage("gameSetting", net.ConnectedClientsIds[1], writer, NetworkDelivery.Reliable);
        }

        director.InGame(mode, eggNum, first, time);
    }
}