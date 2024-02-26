/**
 * Manages join connecting status
 * 
 * @version 0.0.4, New script
 * @author S3
 * @date 2024/02/25
*/

using System.Net.Sockets;
using System.Net;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using Unity.Netcode.Transports.UTP;

public class JoinConnectingStatus_Script : NetworkBehaviour
{
    private Text statusText;
    private Text cancelOkButtonText;

    private HostIpInput_Script ipInput;
    private Scene_Director_Script director;

    private GameObject netManager;
    private NetworkManager net;
    private UnityTransport trans;

    // Specifies
    private void Awake()
    {
        statusText = GameObject.Find("JoinConnectStatus_Text").GetComponent<Text>();
        cancelOkButtonText = GameObject.Find("JoinConnectingCancelOk_Button_Text").GetComponent<Text>();

        ipInput = GameObject.Find("HostIpInput_Panel").GetComponent<HostIpInput_Script>();
        director = GameObject.Find("Scene_Director").GetComponent<Scene_Director_Script>();

        netManager = GameObject.Find("NetworkManager");
        net = netManager.GetComponent<NetworkManager>();
        trans = netManager.GetComponent<UnityTransport>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            CancelOkButtonOnClick();
    }

    // Set JoinConnectingStatus_Panel on off
    //
    // @param bool
    public void SetPanel(bool OnOff) {
        if (OnOff)
        {
            statusText.text = "";
            cancelOkButtonText.text = "취소";
        }

        gameObject.SetActive(OnOff);
    }

    // Update status text
    //
    // @param string
    public void UpdateStatusText(string message) { statusText.text = message; }

    // CancelOk button onclick listener
    public void CancelOkButtonOnClick()
    {
        gameObject.SetActive(false);
        ipInput.SetPanel();
        net.Shutdown();
        netManager.SetActive(false);
    }

    // Procedure of connecting to host
    public IEnumerator ConnectingToHost(string hostIp)
    {
        trans.ConnectionData.Address = hostIp;
        net.StartClient();

        string joinIp = "";
        var join = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in join.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
                joinIp = ip.ToString();
        }
        statusText.text = hostIp + "에 연결 중입니다..." + "\n<size=40>내 아이피 주소: " + joinIp + "</size>";

        while (net.IsClient && !net.IsConnectedClient)
            yield return null;

        if (!net.IsClient)
            yield break;

        using FastBufferWriter writer = new FastBufferWriter(256, Unity.Collections.Allocator.Temp);
        writer.WriteValueSafe(joinIp);
        NetworkManager.Singleton.CustomMessagingManager.SendNamedMessage("joinIp", OwnerClientId, writer, NetworkDelivery.Reliable);

        int answer = 0;
        while (answer == 0 && net.IsConnectedClient)
        {
            NetworkManager.Singleton.CustomMessagingManager.RegisterNamedMessageHandler("permitAnswer", (senderClientId, reader) =>
            {
                reader.ReadValueSafe(out int answerIndex);
                answer = answerIndex;
            });
            yield return null;
        }

        if (!net.IsConnectedClient)
        {
            statusText.text = "대국 상대가 연결을 거부했습니다...";
            cancelOkButtonText.text = "확인";

            yield break;
        }

        statusText.text = "대국 상대와의 연결에 성공했습니다!\n대국 규칙을 설정 중입니다.";

        string gameSetting = "";
        while (gameSetting.Equals("") && net.IsConnectedClient)
        {
            NetworkManager.Singleton.CustomMessagingManager.RegisterNamedMessageHandler("gameSetting", (senderClientId, reader) =>
            {
                reader.ReadValueSafe(out string message);
                gameSetting = message;
            });
            yield return null;
        }

        statusText.text = "대국 상대와의 연결이 끊겼습니다...";
        cancelOkButtonText.text = "확인";

        if (!net.IsConnectedClient)
            yield break;

        string[] gameSettingSplit = gameSetting.Split(" ");
        int eggNum = int.Parse(gameSettingSplit[0]);
        bool firstTurn = Convert.ToBoolean(gameSettingSplit[1]);
        int turnTimeLimit = int.Parse(gameSettingSplit[2]);

        director.InGame(1, eggNum, firstTurn, turnTimeLimit);
    }
}