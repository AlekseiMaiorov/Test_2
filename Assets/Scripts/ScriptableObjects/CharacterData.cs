using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Character Data", menuName = "Configs/Data/Character Data")]
    public class CharacterData : ScriptableObject
    {
        public GameObject Prefab;
        public int MaximumHealth;
        public float AttackDamage;
        public float MoveSpeed;
    }
}