/**
 * Control eggs and egg lists
 * 
 * @version 1.0.0
 * - Change class name EggControl_Script to EggController
 * - Code optimization
 * @author S3
 * @date 2024/03/07
*/

using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class EggController : NetworkBehaviour
{
    GameObject black, white;
    public List<GameObject>[] eggs = new List<GameObject>[2];
    private int eggNum;

    InGameCanvasController inGame;
    GameResultController result;

    GameSettingDataController data;

    private void Awake()
    {
        black = Resources.Load<GameObject>("Prefabs/BlackEgg");
        white = Resources.Load<GameObject>("Prefabs/WhiteEgg");

        for (int i = 0; i < 2; i++)
            eggs[i] = new List<GameObject> ();

        inGame = GameObject.Find("InGameCanvas").GetComponent<InGameCanvasController>();
        result = GameObject.Find("GameResultPanel").GetComponent<GameResultController>();

        data = GameObject.Find("SceneDirector").GetComponent<GameSettingDataController>();
    }

    // Check a list where the number of eggs is 0
    private void Update() { BeCompetitive(); }

    private void BeCompetitive()
    {
        if (inGame.GetPlayGame())
        {
            if (eggs[0].Count == 0 || eggs[1].Count == 0)
                result.SetPanel(true);
        }
    }

    public void Init()
    {
        this.eggNum = data.GetEggNum();

        ClearEggs();
        SpawnEggs();
    }

    private void ClearEggs()
    {
        for (int i = 0; i < 2; i++)
        {
            foreach (GameObject egg in eggs[i])
                Destroy(egg);

            eggs[i].Clear();
        }
    }

    private void SpawnEggs()
    {
        List<Vector2> blackLocations = SetEggLocations(1);
        List<Vector2> whiteLocations = SetEggLocations(-1);
        for (int i = 0; i < eggNum; i++)
        {
            GameObject newBlack = Instantiate(black, blackLocations[i], Quaternion.identity);
            newBlack.GetComponent<Egg>().SetEgg(false);
            eggs[0].Add(newBlack);

            GameObject newWhite = Instantiate(white, whiteLocations[i], Quaternion.identity);
            newWhite.GetComponent<Egg>().SetEgg(true);
            eggs[1].Add(newWhite);
        }
    }

    // Set the spawn location of eggs
    // 
    // @param int
    // @return List<Vector2>
    private List<Vector2> SetEggLocations(int color)
    {
        List<Vector2> eggLocations = new List<Vector2>();
        if (eggNum % 2 == 1)
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

    // Return egg number in list
    //
    // @param bool
    // @return int
    public int GetEggsCount(bool color)
    {
        if (color)
            return eggs[1].Count;
        else
            return eggs[0].Count;
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
            if (egg.GetComponent<Rigidbody2D>().velocity != Vector2.zero || (egg.GetComponent<Egg>().GetSavedRb() != Vector2.zero))
                allStop = false;

        return allStop;
    }
}