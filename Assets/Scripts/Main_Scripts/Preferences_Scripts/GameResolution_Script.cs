/**
 * Manages related to game resolution
 * 
 * @version 0.0.3
 * - Delete 1024, 768 scale
 * @author S3
 * @date 2024/02/22
*/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameResolution_Script : MonoBehaviour
{
    private Text GameResolution_Text;
    private Button GameResolution_Button;
    private GameObject GrChoice_Panel;
    private GameObject Gr_Button_Prefab;
    private Button[] grButtons = new Button[3];

    private static List<int[]> gameResolutions = new List<int[]> { new int[2] { 1280, 720 }, new int[2] { 1600, 900 }, new int[2] { 1920, 1080 } };

    private PreferencesData_Script pd_Script;

    // Specifies
    private void Awake()
    {
        pd_Script = GameObject.Find("Scene_Director").GetComponent<PreferencesData_Script>();

        GameResolution_Text = GameObject.Find("GameResolution_Text").GetComponent<Text>();

        GameResolution_Button = GameObject.Find("GameResolution_Button").GetComponent<Button>();
        GrChoice_Panel = GameObject.Find("GrChoice_Panel");
        Gr_Button_Prefab = Resources.Load<GameObject>("Prefabs/Gr_Button_Prefab");
        for (int i = 0; i < 3; i++)
        {
            int temp = i; // Duplicated and used due to closure issues
            string grText = gameResolutions[temp][0].ToString() + "x" + gameResolutions[temp][1].ToString();

            grButtons[temp] = Instantiate(Gr_Button_Prefab, GrChoice_Panel.transform).GetComponent<Button>();
            grButtons[temp].GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 52.5f -35 * temp);
            grButtons[temp].transform.Find("Gr_Button_Prefab_Text").GetComponent<Text>().text = grText;
            grButtons[temp].onClick.AddListener(() => {
                Screen.SetResolution(gameResolutions[temp][0], gameResolutions[temp][1], Screen.fullScreen);
                pd_Script.SetGameResolution(temp);
                GameResolution_Text.text = grText;
                GrChoice_Panel.SetActive(false);
            });
        }
    }

    // Specifies when game start
    private void Start()
    {
        GameResolution_Text.text = gameResolutions[pd_Script.GetGameResolution()][0].ToString() + "x" + gameResolutions[pd_Script.GetGameResolution()][1].ToString();

        GameResolution_Button.onClick.AddListener(() => {
            GrChoice_Panel.SetActive(!GrChoice_Panel.activeSelf);
        });

        GrChoice_Panel.SetActive(false);
    }
}