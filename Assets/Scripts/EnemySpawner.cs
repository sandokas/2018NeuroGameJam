using Assets.Scripts;
using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemyTemplate;
    private int gameDifficulty = 0;
    public int increaseDifficultySeconds = 30;
    public int changeBpmSeconds = 5;
    private int changeBpm = 0;
    private int velocity;
    private Vector3 direction;
    private Vector3 positionMin;
    private Vector3 positionMMax;
    public int timeToLiveSeconds = 1;
    private float currentBpm;

    // Use this for initialization
    void Start () {
        timeToLiveSeconds = 30;
        gameDifficulty = 0;
        changeBpm = 0;
    }
	
	// Update is called once per frame
	void Update () {
		if (Time.time / increaseDifficultySeconds > gameDifficulty)
        {
            gameDifficulty++;
        }
        if (UnityEngine.Random.value < .3f * Time.deltaTime + gameDifficulty * Time.deltaTime * .01f)
        {
            float bpm = currentBpm;
            SpawnEnemy(bpm);
        }
        if (Time.time / changeBpmSeconds > changeBpm)
        {
            currentBpm = UnityEngine.Random.Range(30, 250);
            changeBpm++;
        }
    }

    private void SpawnEnemy(float bpm)
    {
        Vector3 pos = new Vector3(UnityEngine.Random.value, UnityEngine.Random.value, 10);
        pos = Camera.main.ViewportToWorldPoint(pos);
        GameObject newEnemy = Instantiate(enemyTemplate, pos, Quaternion.identity);
        newEnemy.GetComponent<EnemyFrequency>().bpm = bpm;
        Renderer renderer = newEnemy.GetComponent<Renderer>();
        renderer.material.color = ColorBpmMapper.GetColor(bpm);
        PlaySound(bpm);
        StartCoroutine(DestroyEnemy(newEnemy, timeToLiveSeconds));
    }

    private IEnumerator DestroyEnemy(GameObject enemy, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(enemy);

    }
    public void PlaySound(float bpm)
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.pitch = (bpm/60)/ 3;
        audio.Play();
        //StartCoroutine(StopPlayingAfterXSeconds(audio, 6));
    }
}
