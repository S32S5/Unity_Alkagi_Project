/**
 * Control pause game panel
 * 
 * @version 1.0.0, new class
 * @author S3
 * @date 2024/03/09
*/

using Unity.Netcode;
using UnityEngine;

public class PauseGameController : MonoBehaviour, Panel
{
    EndGameController end;

    NetworkManager net;

    private void Awake()
    {
        end = GameObject.Find("EndGamePanel").GetComponent<EndGameController>();

        net = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }

    private void Update()
    {
        if (net.IsHost && net.ConnectedClients.Count != 2 || !net.IsConnectedClient)
            SetPanel(false);
    }

    public void Init() { }

    public void SetPanel(bool OnOff) { gameObject.SetActive(OnOff); }

    public bool GetPanelIsOn() { return gameObject.activeSelf; }

    public void EscListener() { EndGame(); }

    public void EndGame() { end.EndGame(); }
}