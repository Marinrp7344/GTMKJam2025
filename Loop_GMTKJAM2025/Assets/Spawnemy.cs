using UnityEngine;

public class Spawnemy : MonoBehaviour
{
    public GameObject enemy;


    public void SpawnBeat()
    {
        SpawningManager.Instance.SpawnemySpawner(enemy, transform);
    }
}
