/**
 * New Game Setting.
 * 
 * @version 0.0.4
 *  - Code optimization
 *  - Add gameMode
 * @author S3
 * @date 2024/02/24
*/

using Unity.Netcode;
using UnityEngine;

public class NewGameSetting_Script : MonoBehaviour
{
    private int gameMode;

    private Main_Script main;
    private PlayerWaiting_Script player;
    private HostJoin_Script hostJoin;

    private NetworkManager net;

    // Specifies
    private void Awake()
    {
        main = GameObject.Find("Main_Panel").GetComponent<Main_Script>();
        player = GameObject.Find("PlayerWaiting_Panel").GetComponent<PlayerWaiting_Script>();
        hostJoin = GameObject.Find("HostJoin_Panel").GetComponent<HostJoin_Script>();

        net = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }

    // NewGameSetting_Panel set false when game start
    private void Start() { gameObject.SetActive(false); }

    // Esc
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);

            if (gameMode == 0)
                main.SetPanel();
            else if(gameMode == 1)
            {
                net.Shutdown();
                player.SetPanel(false);
                hostJoin.SetPanel();
            }
        }
        
        if(gameMode == 1 && net.ConnectedClientsIds.Count != 2)
        {
            gameObject.SetActive(false);

            player.UpdateStatusText("대국 상대와 연결이 끊겼습니다...");
            player.SetCancelOkButton(true, "확인");
        }
    }

    // Return NewGameSetting_Panel is on
    //
    // @return bool
    public bool GetPanelIsOn() { return gameObject.activeSelf; }

    // Set NewGameSetting_Panel
    //
    // @param bool
    public void SetPanel(bool OnOff) { gameObject.SetActive(OnOff); }

    // Return game mode
    //
    // @return int gameMode
    // - 0: Play on this pc, 1: Play with another pc
    public int GetGameMode() { return gameMode; }

    // Set game mode
    //
    // @param int modeNum
    // - 0: Play on this pc, 1: Play with another pc
    public void SetGameMode(int modeNum) { gameMode = modeNum; }
}