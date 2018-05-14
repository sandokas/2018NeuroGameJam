using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFrequency : MonoBehaviour {
    public float bpm;
    private float difference;
	// Use this for initialization
	void Start () {
        difference = 10;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D coll)
    {
        //Debug.Log("BURST AGAINST ");
        if (coll.gameObject.tag == "burst")
        {
            float burstBpm = coll.gameObject.GetComponent<BurstFrequency>().bpm;
            Debug.Log("DELETED BALL " + bpm.ToString() + " burst:" + burstBpm.ToString());
            if (burstBpm < bpm + bpm*.15f && burstBpm > bpm - bpm * .15f)
            {
                Destroy(this.gameObject);
            }
        }

    }
}
