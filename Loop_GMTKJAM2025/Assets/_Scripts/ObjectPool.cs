using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Singleton;
    [SerializeField] List<GameObject> particles = new List<GameObject>();
    public List<GameObject> particlePool;
    List<GameObject> activeParticles = new List<GameObject>();
    public int amountToPool = 1000; // amount to spawn of EACH particle

    void Awake()
    {
        // singleton code
        if (Singleton == null) { Singleton = this; }
        else if (Singleton != this) { Destroy(this); }
    }

    void Start()
    {
        particlePool = new List<GameObject>();

        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            foreach( GameObject particle in particles)
            {
                tmp = Instantiate(particle, transform);
                tmp.SetActive(false);
                particlePool.Add(tmp);
            }
        }
    }


    public GameObject GetPooledParticle(GameObject particlePrefab, Vector3 position)
    {
        string targetName = particlePrefab.name + "(Clone)";

        for (int i = 0; i < particlePool.Count; i++)
        {
            // if particle is active and has the right name
            if (!particlePool[i].activeInHierarchy &&
                particlePool[i].name == targetName)
            {
                particlePool[i].SetActive(true);
                particlePool[i].transform.position = position;

                // move it to the active particles list so searching goes faster
                particlePool.Remove(particlePrefab);
                activeParticles.Add(particlePrefab);

                return particlePool[i];
            }
        }
        return null;
    }

    public void DeactivatePooledParticle(GameObject particle)
    {
        // deactivated particle so it can be used again by the pool
        // and moves it back to the pool list
        particle.SetActive(false);
        activeParticles.Remove(particle);
        particlePool.Add(particle);
    }

}
