/**
 * Manage egg's physics
 * 
 * @version 1.0.0
 * - Change class name EggPhysics_Script to EggPhysicsController
 * - Code optimization
 * @author S3
 * @date 2024/03/07
*/

using System;
using System.IO;
using UnityEngine;

public class EggPhysicsController : MonoBehaviour
{
    private EggPhysics data;
    private string path;

    private void Awake()
    {
        path = Path.Combine(Application.dataPath + "/EggPhysics.json");
        LoadData();
    }

    private void LoadData()
    {
        string jsonStr = File.ReadAllText(path);
        data = JsonUtility.FromJson<EggPhysics>(jsonStr);
    }

    // Return mass
    //
    // @return float
    public float GetMass() { return data.mass; }

    // Return linearDrag
    // 
    // @return float
    public float GetLinearDrag() { return data.linearDrag; }

    // Return maxPower
    //
    // @return float
    public float GetMaxPower() { return data.maxPower; }
}

[Serializable]
class EggPhysics
{
    public float mass;
    public float linearDrag;
    public float maxPower;
}