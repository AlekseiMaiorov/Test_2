using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Enemies Spawn Data", menuName = "Configs/Data/Enemies Spawn Data")]
    public class EnemiesSpawnData : ScriptableObject
    {
        public int MaxEnemies;
        public int SpawnCountdown;
        public Transform[] SpawnPoints;
    }
}