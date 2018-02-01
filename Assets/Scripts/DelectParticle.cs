using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelectParticle : MonoBehaviour {

    public float life_time = 0;

    private float timer;

    // Use this for initialization
    void Start () {
        timer = 0;
    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;

        if(timer >= life_time)
        {
            Destroy(this.gameObject);
        }
    }
}
