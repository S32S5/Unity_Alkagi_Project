/**
 * Control game mode and progress
 * 
 * @version 1.0.0
 *  - Change class name NewGameSetting_Script to NewGameSettingController
 *  - Code optimization
 * @author S3
 * @date 2024/03/08
*/

using Unity.Netcode;
using UnityEngine;

public class NewGameSettingController : MonoBehaviour, Panel
{
    MainPanelController main;
    PlayWithAiController ai;
    PlayerWaitingController player;

    InGameCanvasController inGame;

    NetworkManager net;

    private void Awake()
    {
        main = GameObject.Find("MainPanel").GetComponent<MainPanelController>();
        ai = GameObject.Find("PlayWithAiPanel").GetComponent<PlayWithAiController>();
        player = GameObject.Find("PlayerWaitingPanel").GetComponent<PlayerWaitingController>();

        inGame = GameObject.Find("InGameCanvas").GetComponent <InGameCanvasController>();

        net = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }

    private void Update()
    {
        EscListener();

        if (inGame.GetGameMode() == 2 && net.ConnectedClientsIds.Count != 2)
            JoinDisconnected();
    }

    public void Init() {}

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
            gameObject.SetActive(false);

            if (inGame.GetGameMode() == 0)
                ai.SetPanel(true);
            else if (inGame.GetGameMode() == 1)
                main.SetPanel(true);
            else if (inGame.GetGameMode() == 2)
                player.SetPanel(false);
        }
    }

    private void JoinDisconnected()
    {
        gameObject.SetActive(false);

        player.UpdateStatusText("대국 상대와 연결이 끊겼습니다...");
        player.SetCancelOkButton(true, "확인");
    }
}