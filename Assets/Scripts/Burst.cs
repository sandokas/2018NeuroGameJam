using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burst : MonoBehaviour {

    // Use this for initialization
    float timeElapsed = 0;
    bool stilldown = false;
    bool secondshot = false;
    public GameObject circle;
    
    void Start () {
    }
	
	void Update () {
        timeElapsed = timeElapsed + Time.deltaTime;

        if (!stilldown)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!secondshot)
                {
                    secondshot = true;
                    timeElapsed = 0;
                }
                else { 

                Debug.Log(string.Format("Time between clicks: {0}.", timeElapsed));
                float bpm = (1 / timeElapsed) * 60; //bpm / 60 = 1 / timeElapsed
                Debug.Log(string.Format("That's {0} bpm!", bpm));
                if (bpm < 20 || bpm > 500)
                {
                    Debug.Log("too slow or too fast");
                }
                else
                {
                    PlaySound(timeElapsed);
                    CreateCircle(timeElapsed);
                }
                timeElapsed = 0;
                stilldown = true;
                secondshot = false;
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                stilldown = false;
            }
        }
    }
    public void PlaySound(float speed)
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.pitch = (1/speed)/3;
        audio.Play();
        //StartCoroutine(StopPlayingAfterXSeconds(audio, 6));
    }
    public void CreateCircle(float speed)
    {
        //GameObject newInstance = Instantiate(circle);
        var x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        var y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        var z = Input.mousePosition.z;
        var mousepos = new Vector3(x, y, 0);
        var newInstance = Instantiate(circle, mousepos, Quaternion.identity) as GameObject;

        newInstance.GetComponent<BurstFrequency>().bpm = (1 / timeElapsed) * 60;

        PaintCircle(newInstance, speed);

        StartCoroutine(DestroyObjectAfterXSeconds(newInstance, speed));
        StartCoroutine(ScaleAndDestroyObjectAtSpeed(newInstance, speed));
    }

    private void PaintCircle(GameObject newInstance, float speed)
    {
        Renderer renderer = newInstance.GetComponent<Renderer>();

        float bpm = (1 / speed) * 60;

        renderer.material.color = ColorBpmMapper.GetColor(bpm);

    }

    private IEnumerator ScaleAndDestroyObjectAtSpeed(GameObject instanced, float speed)
    {
        while(instanced != null)
        {
            float ratio = 0;
            ratio += Time.deltaTime * (1/speed);
            var x = Mathf.Lerp(instanced.transform.localScale.x, 10, ratio);
            var y = Mathf.Lerp(instanced.transform.localScale.y, 10, ratio);
            instanced.transform.localScale = new Vector3(x, y, 0);
            Renderer renderer = instanced.GetComponent<Renderer>();
            Color currentColor = renderer.material.color;
            currentColor.a -= .3f * ratio;
            renderer.material.color = currentColor;
            // -= Mathf.Lerp(instanced.GetComponent<Renderer>().material.color.a, 0.5f, ratio);

            yield return null; 
        }
    }

    public IEnumerator DestroyObjectAfterXSeconds(GameObject instanced, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(instanced);
    }
    public IEnumerator StopPlayingAfterXSeconds(AudioSource audioSource, float seconds)
    {
        yield return new WaitForSeconds(seconds-1);
        audioSource.dopplerLevel = audioSource.dopplerLevel*.2f;
        yield return new WaitForSeconds(1);
        audioSource.Stop();
    }
}
