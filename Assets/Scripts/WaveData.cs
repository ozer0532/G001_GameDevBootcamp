using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "GDBC/WaveData")]
public class WaveData : ScriptableObject
{
    public Subwave[] subwaves;
    public float breatheTime;

    [HideInInspector]
    public float totalWaveDuration;

    [System.Serializable]
    public struct Subwave
    {
        public GameObject prefab;
        public int amount;
        public int duration;
        public int delay;
    }
}
