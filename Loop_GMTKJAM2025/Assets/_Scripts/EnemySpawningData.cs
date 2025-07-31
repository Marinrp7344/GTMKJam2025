using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Data")]
public class EnemySpawningData : ScriptableObject
{
    public int weight;
    public int cost;
    public GameObject EnemyPrefab;
}
