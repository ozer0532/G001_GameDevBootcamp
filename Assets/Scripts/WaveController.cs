using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    public WaveData[] waves;
    public int currentWave = 0;

    private float waveDuration;
    private float waveTimer;

    public SpawnArea[] spawnArea;

    private void Start()
    {
        // Calculates the total wave duration for each waves
        foreach (WaveData wave in waves) {
            float max = 0f;
            foreach (WaveData.Subwave subwave in wave.subwaves) {
                if (subwave.duration + subwave.delay > max)
                    max = subwave.duration + subwave.delay;
            }
            wave.totalWaveDuration = max + wave.breatheTime;
        }

        waveDuration = waves[currentWave].totalWaveDuration;
        StartWave();
    }

    private void FixedUpdate()
    {
        // Starts next wave if the timer is over and there is a next wave
        while ((waveTimer > waveDuration) && (currentWave < waves.Length - 1)) {
            currentWave++;
            waveTimer -= waveDuration;
            waveDuration = waves[currentWave].totalWaveDuration;

            StartWave();
        }

        waveTimer += Time.deltaTime;
    }

    private void StartWave () {
        // Ready the enemies to spawn
        foreach (WaveData.Subwave subwave in waves[currentWave].subwaves) {
            for (int i = 0; i < subwave.amount; i++) {
                if (subwave.amount > 1) {
                    StartCoroutine(Summon(subwave.prefab, subwave.delay + (subwave.duration / (subwave.amount - 1)) * i));
                } else {
                    print("Summon");
                    StartCoroutine(Summon(subwave.prefab, subwave.delay));
                }
            }
        }
    }

    private IEnumerator Summon (GameObject prefab, float delay) {
        yield return new WaitForSeconds(delay);

        SpawnArea area = spawnArea[Random.Range(0, spawnArea.Length)];
        Bounds bounds = area.area.bounds;
        Vector2 position = new Vector2(Random.Range(bounds.min.x, bounds.max.x),
                                       Random.Range(bounds.min.y, bounds.max.y));

        EnemyController enemy = Instantiate(prefab, position, Quaternion.identity).GetComponent<EnemyController>();
        Transform enemyTransform = enemy.transform;
        enemyTransform.localScale = new Vector3((int)area.dir * enemyTransform.localScale.x, enemyTransform.localScale.y, enemyTransform.localScale.z);
    }
    
    [System.Serializable]
    public struct SpawnArea {
        public Collider2D area;
        public Direction dir;

        public enum Direction
        {
            Left = -1,
            Right = 1
        }
    }
}
