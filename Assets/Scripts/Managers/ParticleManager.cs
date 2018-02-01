using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : SingletonMonoBehaviour<ParticleManager>
{
    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }

        //状況に応じて
        //DontDestroyOnLoad(this.gameObject);
    }

    public void Create(string name, Vector2 pos)
    {
        string effect_name = "Prefabs/Particle/" + name;

        GameObject prefab = (GameObject)Resources.Load(effect_name);

        if (prefab != null)
        {
            Instantiate(prefab, pos, Quaternion.identity);
        }
        else
        {
            Debug.Log("Missing Particle");
        }
    }
}
