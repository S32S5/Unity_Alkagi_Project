/**
 * Control game resolution button
 * 
 * @version 1.0.0
 * - Change class name GameResolution_Script to GameResolutionController
 * - Code optimization
 * @author S3
 * @date 2024/03/08
*/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameResolutionController : MonoBehaviour, OptionContent
{
    Text GameResolutionTxt;

    GameObject GrChoicesPanel;
    GameObject GrBtn;
    private Button[] grBtns = new Button[3];

    private static List<int[]> gameResolutions = new List<int[]> { new int[2] { 1280, 720 }, new int[2] { 1600, 900 }, new int[2] { 1920, 1080 } };

    private void Awake()
    {
        GameResolutionTxt = GameObject.Find("GameResolutionText").GetComponent<Text>();

        GrChoicesPanel = GameObject.Find("GrChoicesPanel");
        GrBtn = Resources.Load<GameObject>("Prefabs/GrButton");
        for (int i = 0; i < gameResolutions.Count; i++)
            AddGrBtn(i);
    }

    // Init when game start
    public void Start() { Init(); }

    public void Init()
    {
        GameResolutionTxt.text = Screen.width + "x" + Screen.height;
        GrChoicesPanel.SetActive(false);
    }

    public void GameResolutionBtnOnClick() { GrChoicesPanel.SetActive(!GrChoicesPanel.activeSelf); }

    private void AddGrBtn(int index)
    {
        int tempI = index; // Duplicated and used due to closure issues
        string grText = gameResolutions[tempI][0].ToString() + "x" + gameResolutions[tempI][1].ToString();

        grBtns[tempI] = Instantiate(GrBtn, GrChoicesPanel.transform).GetComponent<Button>();
        grBtns[tempI].transform.Find("GrButtonText").GetComponent<Text>().text = grText;
        grBtns[tempI].GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 52.5f - 35 * tempI);
        grBtns[tempI].onClick.AddListener(() => { GrBtnOnClick(tempI); });
    }

    private void GrBtnOnClick(int tempIndex)
    {
        Screen.SetResolution(gameResolutions[tempIndex][0], gameResolutions[tempIndex][1], Screen.fullScreen);
        GameResolutionTxt.text = gameResolutions[tempIndex][0] + "x" + gameResolutions[tempIndex][1];
        GrChoicesPanel.SetActive(false);
    }
}