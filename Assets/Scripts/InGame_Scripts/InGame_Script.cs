/**
 * In Game
 * 
 * Script Explanation
 * - Init Ingame
 * - Set InGame_Panel active or not
 * 
 * @version 0.0.3
 * - Add EggPhysics_Script
 * @author S3
 * @date 2024/01/30
*/

using UnityEngine;

public class InGame_Script : MonoBehaviour
{
    public bool playGame;

    private EggControl_Script ec_Script;
    private TurnControl_Script tc_Script;
    private CameraControl_Script cc_Script;
    public EndCurrentGameControl_Script ecgc_Script;
    public GameResultControl_Script grc_Script;

    // Specifies
    private void Awake()
    {
        gameObject.AddComponent<EggControl_Script>();
        gameObject.AddComponent<TurnControl_Script>();
        gameObject.AddComponent<Attack_Script>();
        gameObject.AddComponent<CameraControl_Script>();
        gameObject.AddComponent<EndCurrentGameControl_Script>();
        gameObject.AddComponent<GameResultControl_Script>();

        ec_Script = GetComponent<EggControl_Script>();
        tc_Script = GetComponent<TurnControl_Script>();
        cc_Script = GetComponent<CameraControl_Script>();
        ecgc_Script = GetComponent<EndCurrentGameControl_Script>();
        grc_Script = GetComponent<GameResultControl_Script>();
    }

    // Esc
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ecgc_Script.EndCurrentGame_Panel_IsOn())
                ecgc_Script.EndCurrentGameCancel();
            else if (grc_Script.GetGameResult_Panel_IsOn())
                grc_Script.CheckGameResult();
            else
                ecgc_Script.ShowEndCurrentGame_Panel();
        }
    }

    // Init
    public void Init(int initialEggNumber, bool firstTurn, int turnTimeLimit)
    {
        ec_Script.InitEggs(initialEggNumber);
        tc_Script.InitTurn(firstTurn, turnTimeLimit);
        if (firstTurn)
            cc_Script.InitCamera(180);
        else
            cc_Script.InitCamera(0);
        ecgc_Script.Init();
        grc_Script.Init();

        playGame = true;
    }
}