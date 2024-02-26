/**
 * Manages Eggs
 * 
 * @version 0.0.4 
 * - Code optimization
 * @author S3
 * @date 2024/02/18
*/

using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EggControl_Script : NetworkBehaviour
{
    private int eggNumber;

    private GameObject black, white;
    public List<GameObject>[] eggs = new List<GameObject>[2];

    private InGame_Script inGame;
    private GameResultControl_Script result;

    // Specifies
    private void Awake()
    {
        for(int i = 0; i < 2; i++)
            eggs[i] = new List<GameObject> ();

        black = Resources.Load<GameObject>("Prefabs/BlackEgg_Prefab");
        white = Resources.Load<GameObject>("Prefabs/WhiteEgg_Prefab");

        inGame = GetComponent<InGame_Script>();
        result = GameObject.Find("GameResult_Panel").GetComponent<GameResultControl_Script>();
    }

    // Check a list where the number of eggs is 0
    private void Update()
    {
        if (inGame.playGame)
        {
            if (eggs[0].Count == 0 || eggs[1].Count == 0)
            {
                inGame.playGame = false;
                result.ShowGameResult();
            }
        }
    }

    // Init eggs
    // 
    // @param int
    public void InitEggs(int eggNumber)
    {
        this.eggNumber = eggNumber;

        for (int i = 0; i < 2; i++)
        {
            foreach (GameObject egg in eggs[i])
                Destroy(egg);

            eggs[i].Clear();
        }

        List<Vector2> blackLocations = SetEggLocations(1);
        List<Vector2> whiteLocations = SetEggLocations(-1);
        for (int i = 0; i < eggNumber; i++)
        {
            GameObject newBlack = Instantiate(black, blackLocations[i], Quaternion.identity);
            newBlack.GetComponent<Egg_Script>().SetEgg(false);
            eggs[0].Add(newBlack);

            GameObject newWhite = Instantiate(white, whiteLocations[i], Quaternion.identity);
            newWhite.GetComponent<Egg_Script>().SetEgg(true);
            eggs[1].Add(newWhite);
        }
    }

    // Set the spawn location of eggs
    // 
    // @param int color-1 if black, -1 if white-
    // @return spawn location's list
    private List<Vector2> SetEggLocations(int color)
    {
        List<Vector2> eggLocations = new List<Vector2>();
        if (eggNumber % 2 == 1)
        {
            eggLocations.Add(new Vector3(0, -2.25f * color, 0));

            eggLocations.Add(new Vector3(-2.25f, -2.25f * color, 0));
            eggLocations.Add(new Vector3(2.25f, -2.25f * color, 0));

            eggLocations.Add(new Vector3(-3.375f, -3.375f * color, 0));
            eggLocations.Add(new Vector3(3.375f, -3.375f * color, 0));

            eggLocations.Add(new Vector3(-1.125f, -3.375f * color, 0));
            eggLocations.Add(new Vector3(1.125f, -3.375f * color, 0));
        }
        else
        {
            eggLocations.Add(new Vector3(-1.125f, -3.375f * color, 0));
            eggLocations.Add(new Vector3(1.125f, -3.375f * color, 0));

            eggLocations.Add(new Vector3(-3.375f, -3.375f * color, 0));
            eggLocations.Add(new Vector3(3.375f, -3.375f * color, 0));

            eggLocations.Add(new Vector3(-2.25f, -2.25f * color, 0));
            eggLocations.Add(new Vector3(2.25f, -2.25f * color, 0));
        }

        return eggLocations;
    }

    // Return all eggs are stop
    // 
    // @return bool
    public bool GetAllStop()
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

    // Destroy egg
    // 
    // @param bool, GameObject
    public void DestroyEgg(bool color, GameObject egg)
    {
        if (color)
            eggs[1].Remove(egg);
        else
            eggs[0].Remove(egg);

        Destroy(egg);
    }

    // Return egg number in list
    //
    // @param bool color
    // @return int egg number
    public int GetEggsCount(bool color)
    {
        if(color)
            return eggs[1].Count;
        else
            return eggs[0].Count;
    }
}