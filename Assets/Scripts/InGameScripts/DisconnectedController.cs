/**
 * Control disconnected panel
 * 
 * @version 1.0.0, new class
 * @author S3
 * @date 2024/03/09
*/

using UnityEngine;

public class DisconnectedController : MonoBehaviour, Panel
{
    EndGameController end;

    private void Awake()
    {
        end = GameObject.Find("EndGamePanel").GetComponent<EndGameController>();
    }

    public void Init() { }

    public void SetPanel(bool OnOff) { gameObject.SetActive(OnOff); }

    public bool GetPanelIsOn() { return gameObject.activeSelf; }

    public void EscListener() { EndGame(); }

    public void EndGame() { end.EndGame(); }
}