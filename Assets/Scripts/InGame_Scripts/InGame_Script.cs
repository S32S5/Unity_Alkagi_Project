/**
 * In Game
 * 
 * Script Explanation
 * - Init Ingame
 * - Set InGame_Panel active or not
 * 
 * @version 0.1, First version
 * @author S3
 * @date 2023/12/27
*/

using UnityEngine;

public class InGame_Script : MonoBehaviour
{
    public bool playGame;

    private GameObject InGame_Panel;

    private EggControl_Script ec_Script;
    private TurnControl_Script tc_Script;
    public EndCurrentGameControl_Script ecgc_Script;
    public GameResultControl_Script grc_Script;

    /*
     * Specifies
     */
    private void Awake()
    {
        InGame_Panel = GameObject.Find("InGame_Canvas").transform.Find("InGame_Panel").gameObject;

        gameObject.AddComponent<EggControl_Script>();
        gameObject.AddComponent<TurnControl_Script>();
        gameObject.AddComponent<Attack_Script>();
        gameObject.AddComponent<CameraControl_Script>();
        gameObject.AddComponent<EndCurrentGameControl_Script>();
        gameObject.AddComponent<GameResultControl_Script>();

        ec_Script = gameObject.GetComponent<EggControl_Script>();
        tc_Script = gameObject.GetComponent<TurnControl_Script>();
        ecgc_Script = gameObject.GetComponent<EndCurrentGameControl_Script>();
        grc_Script = gameObject.GetComponent<GameResultControl_Script>();

        gameObject.GetComponent<InGame_Script>().enabled = false;
    }

    /*
     * Init and start game when OnEnable
     */
    private void OnEnable()
    {
        int initialEggNumber = GetComponent<InitialEggNumberControl_Script>().GetInitialEggNumber();
        bool firstTurn = GetComponent<FirstTurnControl_Script>().GetFirstTurn();
        int turnTimeLimit = GetComponent<TurnTimeLimitControl_Script>().GetTurnTimeLimit();

        ec_Script.InitEggs(initialEggNumber);
        tc_Script.InitTurn(firstTurn, turnTimeLimit);

        InGame_Panel.SetActive(true);
        playGame = true;
    }

    /*
     * Set InGame_Panel active or not
     * 
     * @param bool true or false
     */
    public void SetActiveInGame_Panel(bool onOff)
    {
        InGame_Panel.SetActive(onOff);
    }
}