/**
 * Manages join connecting status
 * 
 * @version 1.0.0
 * - Change class name JoinConnectingStatus_Script to JoinConnectingStatusController
 * - Code optimization
 * @author S3
 * @date 2024/03/08
*/

using System.Net.Sockets;
using System.Net;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using Unity.Netcode.Transports.UTP;

public class JoinConnectingStatusController : NetworkBehaviour, Panel
{
    private string hostIp, myIp;
    private string gameSetting;

    Text statusTxt, cancelOkBtnTxt;

    SceneDirector director;
    HostIpInputController ipInput;

    InGameCanvasController inGame;

    InitialSettingDataController data;

    GameObject netManager;
    NetworkManager net;
    UnityTransport trans;

    private void Awake()
    {
        statusTxt = GameObject.Find("JoinConnectStatusText").GetComponent<Text>();
        cancelOkBtnTxt = GameObject.Find("JoinConnectingCancelOkButtonText").GetComponent<Text>();

        ipInput = GameObject.Find("HostIpInputPanel").GetComponent<HostIpInputController>();
        director = GameObject.Find("SceneDirector").GetComponent<SceneDirector>();

        inGame = GameObject.Find("InGameCanvas").GetComponent<InGameCanvasController>();

        data = GameObject.Find("NewGameSettingPanel").GetComponent<InitialSettingDataController>();

        netManager = GameObject.Find("NetworkManager");
        net = netManager.GetComponent<NetworkManager>();
        trans = netManager.GetComponent<UnityTransport>();
    }

    private void Update() { EscListener(); }

    public void Init() 
    {
        statusTxt.text = "";
        cancelOkBtnTxt.text = "���";

        ConnectNet();
        CallMyIp();
        statusTxt.text = hostIp + "�� ���� ���Դϴ�..." + "\n<size=40>�� ������ �ּ�: " + myIp + "</size>";
        StartCoroutine(ConnectingforHost());
    }

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
            CancelOkButtonOnClick();
    }

    // Procedure of connecting to host
    private IEnumerator ConnectingforHost()
    {
        while (net.IsClient && !net.IsConnectedClient)
            yield return null;

        if (!net.IsClient)
            yield break;

        SendMyIp();

        StartCoroutine(WaitingPermit());
    }

    private IEnumerator WaitingPermit()
    {
        int answer = 0;
        while (answer == 0 && net.IsConnectedClient)
        {
            NetworkManager.Singleton.CustomMessagingManager.RegisterNamedMessageHandler("permitMessage", (senderClientId, reader) =>
            {
                reader.ReadValueSafe(out int answerIndex);
                answer = answerIndex;
            });
            yield return null;
        }

        if (!net.IsConnectedClient)
        {
            statusTxt.text = "�뱹 ��밡 ������ �ź��߽��ϴ�...";
            cancelOkBtnTxt.text = "Ȯ��";

            yield break;
        }

        StartCoroutine(WaitingGameSetting());
    }

    private IEnumerator WaitingGameSetting()
    {
        statusTxt.text = "�뱹 ������ ���ῡ �����߽��ϴ�!\n�뱹 ��Ģ�� ���� ���Դϴ�.";

        gameSetting = "";
        while (gameSetting.Equals("") && net.IsConnectedClient)
        {
            GetGameSetting();
            yield return null;
        }

        if (!net.IsConnectedClient)
        {
            statusTxt.text = "�뱹 ������ ������ ������ϴ�...";
            cancelOkBtnTxt.text = "Ȯ��";
            yield break;
        }

        GameStart();
    }

    private void ConnectNet()
    {
        netManager.SetActive(true);
        trans.ConnectionData.Address = hostIp;
        trans.ConnectionData.ServerListenAddress = hostIp;
        net.StartClient();
    }

    // Call my ip
    private void CallMyIp()
    {
        myIp = "";
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
                myIp = ip.ToString();
        }
    }

    private void SendMyIp()
    {
        using FastBufferWriter writer = new FastBufferWriter(256, Unity.Collections.Allocator.Temp);
        writer.WriteValueSafe(myIp);
        NetworkManager.Singleton.CustomMessagingManager.SendNamedMessage("joinIp", OwnerClientId, writer, NetworkDelivery.Reliable);
    }

    private void GetGameSetting()
    {
        NetworkManager.Singleton.CustomMessagingManager.RegisterNamedMessageHandler("gameSetting", (senderClientId, reader) =>
        {
            reader.ReadValueSafe(out string message);
            gameSetting = message;
        });
    }

    private void GameStart()
    {
        string[] gameSettingSplit = gameSetting.Split(" ");
        inGame.SetGameMode(2);
        data.SetEggNum(int.Parse(gameSettingSplit[0]));
        data.SetFirstTurn(Convert.ToBoolean(gameSettingSplit[1]));
        data.SetTimeLimit(gameSettingSplit[2]);

        director.InGame();
    }

    // Set hostIp
    //
    // @param string
    public void SetHostIp(string hostIp) { this.hostIp = hostIp; }

    public void CancelOkButtonOnClick()
    {
        net.Shutdown();
        netManager.SetActive(false);

        gameObject.SetActive(false);
        ipInput.SetPanel(true);
    }
}