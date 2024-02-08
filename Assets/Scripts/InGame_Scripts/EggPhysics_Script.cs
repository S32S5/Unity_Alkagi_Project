/**
 * Manage egg's physics
 * 
 * @version 0.0.3
 * - New script
 * @author S3
 * @date 2024/02/08
*/

using System;
using System.IO;
using UnityEngine;

public class EggPhysics_Script : MonoBehaviour
{
    private string epPath;
    private EggPhysics ep;

    // Load json file
    private void Awake()
    {
        epPath = Path.Combine(Application.persistentDataPath + "/EggPhysics.json");
        if (!File.Exists(epPath))
        {
            ep = new EggPhysics();
            ep.maxPower = 22.5f;
            ep.mass = 0.25f;
            ep.linearDrag = 2.25f;

            string json = JsonUtility.ToJson(ep, true);
            File.WriteAllText(epPath, json);
        }
        else
        {
            string json = File.ReadAllText(epPath);
            ep = JsonUtility.FromJson<EggPhysics>(json);
        }
    }

    /*
     * Get maxPower
     * 
     * @return float ep.maxPower
     */
    public float GetMaxPower()
    {
        return ep.maxPower;
    }

    /*
     * Get mass
     * 
     * @return float ep.mass
     */
    public float GetMass()
    {
        return ep.mass;
    }

    /*
     * Get linearDrag
     * 
     * @return float ep.linearDrag
     */
    public float GetLinearDrag()
    {
        return ep.linearDrag;
    }
}

[Serializable]
class EggPhysics
{
    public float maxPower;
    public float mass;
    public float linearDrag;
}