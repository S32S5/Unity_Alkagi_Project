/**
 * Control main canvas
 * 
 * @version 1.1.0
 * - Add play with ai set false
 * - Init optimization
 * @author S3
 * @date 2024/03/09
*/

using UnityEngine;

public class MainCanvasController : MonoBehaviour, Canvas
{
    MainPanelController main;

    private void Awake() { main = GameObject.Find("MainPanel").GetComponent<MainPanelController>(); }

    public void Init() { main.SetPanel(true); }

    public void SetCanvas(bool OnOff) { gameObject.SetActive(OnOff); }
}