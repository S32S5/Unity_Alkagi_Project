/**
 * Manages Eggs
 * 
 * Script Explanation
 * - Init eggs
 * - Return all egg is stop or not
 * - Destroy egg
 * - Clear before game eggs
 * 
 * @version 0.0.2, Code optimization
 * @author S3
 * @date 2024/01/28
*/

using System.Collections.Generic;
using UnityEngine;

public class EggControl_Script : MonoBehaviour
{
    private int initialEggNumber;
    public List<GameObject>[] eggs = new List<GameObject>[2];

    private GameObject BlackEgg_Prefab, WhiteEgg_Prefab;

    public InGame_Script ig_Script;
    public GameResultControl_Script grc_Script;

    // Specifies
    private void Awake()
    {
        for(int i = 0; i < 2; i++)
            eggs[i] = new List<GameObject> ();

        BlackEgg_Prefab = Resources.Load<GameObject>("Prefabs/BlackEgg_Prefab");
        WhiteEgg_Prefab = Resources.Load<GameObject>("Prefabs/WhiteEgg_Prefab");
    }

    // Specifies when game start
    private void Start()
    {
        ig_Script = GetComponent<InGame_Script>();
        grc_Script = GetComponent<GameResultControl_Script>();
    }

    // Check a list where the number of eggs is 0
    private void Update()
    {
        if (ig_Script.playGame)
        {
            if (eggs[0].Count == 0 || eggs[1].Count == 0)
            {
                ig_Script.playGame = false;
                grc_Script.ShowGameResult();
            }
        }
    }

    /*
     * Init eggs
     * 
     * @param int initialEggNumber
     */
    public void InitEggs(int initialEggNumber)
    {
        this.initialEggNumber = initialEggNumber;

        void ClearBeforeGameEggs()
        { 
            for(int i = 0; i < 2; i++)
            {
                foreach (GameObject egg in eggs[i])
                    Destroy(egg);

                eggs[i].Clear();
            }
        }
        ClearBeforeGameEggs();

        void SpawnEggs()
        {
            List<Vector2> blackSpawnLocations = SetEggsSpawnLocation(1);
            List<Vector2> whiteSpawnLocations = SetEggsSpawnLocation(-1);

            // Spawns Eggs
            for (int i = 0; i < initialEggNumber; i++)
            {
                GameObject newBlackEgg = Instantiate(BlackEgg_Prefab, blackSpawnLocations[i], Quaternion.identity);
                newBlackEgg.GetComponent<Egg_Script>().SetEgg(false);
                eggs[0].Add(newBlackEgg);

                GameObject newWhiteEgg = Instantiate(WhiteEgg_Prefab, whiteSpawnLocations[i], Quaternion.identity);
                newWhiteEgg.GetComponent<Egg_Script>().SetEgg(true);
                eggs[1].Add(newWhiteEgg);
            }
        }
        SpawnEggs();
    }

    /*
     * Set the spawn location of eggs
     * 
     * @param int color-1 if black, -1 if white-
     * @return spawn location's list
     */
    private List<Vector2> SetEggsSpawnLocation(int color)
    {
        List<Vector2> spawnLocations = new List<Vector2>();

        if (initialEggNumber % 2 == 1)
        {
            spawnLocations.Add(new Vector3(0, -2.25f * color, 0));

            spawnLocations.Add(new Vector3(-2.25f, -2.25f * color, 0));
            spawnLocations.Add(new Vector3(2.25f, -2.25f * color, 0));

            spawnLocations.Add(new Vector3(-3.375f, -3.375f * color, 0));
            spawnLocations.Add(new Vector3(3.375f, -3.375f * color, 0));

            spawnLocations.Add(new Vector3(-1.125f, -3.375f * color, 0));
            spawnLocations.Add(new Vector3(1.125f, -3.375f * color, 0));
        }
        else
        {
            spawnLocations.Add(new Vector3(-1.125f, -3.375f * color, 0));
            spawnLocations.Add(new Vector3(1.125f, -3.375f * color, 0));

            spawnLocations.Add(new Vector3(-3.375f, -3.375f * color, 0));
            spawnLocations.Add(new Vector3(3.375f, -3.375f * color, 0));

            spawnLocations.Add(new Vector3(-2.25f, -2.25f * color, 0));
            spawnLocations.Add(new Vector3(2.25f, -2.25f * color, 0));
        }

        return spawnLocations;
    }

    /*
     * Return All eggs is stop
     * 
     * @return bool true or false
     */
    public bool GetAllEggsStop()
    {
        bool allStop = true;

        List<GameObject> integratedEggs = new List<GameObject>();
        integratedEggs.AddRange(eggs[0]);
        integratedEggs.AddRange(eggs[1]);
        foreach (GameObject egg in integratedEggs)
            if (egg.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
                allStop = false;

        return allStop;
    }

    /*
     * Destroy egg
     * 
     * @param bool color, GameObject egg
     */
    public void DestroyEgg(bool color, GameObject egg)
    {
        if (color)
            eggs[1].Remove(egg);
        else
            eggs[0].Remove(egg);

        Destroy(egg);
    }

    /*
     * Return egg number in list
     * 
     * @param bool color
     * @return int egg number
     */
    public int GetEggsCount(bool color)
    {
        if(color)
            return eggs[1].Count;
        else
            return eggs[0].Count;
    }
}