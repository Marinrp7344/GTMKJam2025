
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
struct ParticleSpawnData
{
    public GameObject particlePrefab;
    public int spawnCountMin;
    public int spawnCountMax;
}

public class ParticleSpawner : MonoBehaviour
{
    [SerializeField] List<ParticleSpawnData> particles = new List<ParticleSpawnData>();
    [SerializeField] Color color = Color.white;

    [ContextMenu("Spawn Particles")]
    public void SpawnParticles()
    {
        foreach (ParticleSpawnData particleSpawnData in particles)
        {
            int particlesToSpawn = Random.Range(particleSpawnData.spawnCountMin, particleSpawnData.spawnCountMax);
            for (int i = 0; i < particlesToSpawn; i++)
            {
                GameObject particle = Instantiate(particleSpawnData.particlePrefab, transform.position, Quaternion.identity);
                particle.GetComponent<Particle>().SetColor(color);
            }
        }

    }
}
