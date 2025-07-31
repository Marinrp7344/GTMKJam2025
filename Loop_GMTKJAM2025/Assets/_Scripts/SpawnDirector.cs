using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SpawnDirector : MonoBehaviour
{

    public uint credits = 100;

    [SerializeField] uint wavesPerStage = 4;
    uint groupsPerWave;

    [Space]
    [SerializeField] List<EnemySpawningData> allEnemies = new List<EnemySpawningData>();
    [SerializeField] List<float> waveSizeMultipliers = new List<float>();

    [Space]
    [SerializeField] Composer composer;
    [SerializeField] BeatAction spawnBeatAction;
    Metronome metronome;

    uint leftoverCredits = 0;

    List<EnemySpawningData> enemySpawnOrder = new List<EnemySpawningData>();
    List<uint> groupSizes = new List<uint>();


    private void Start()
    {
        metronome = Metronome.Singleton;

        groupsPerWave = metronome.quartersPerMeasure;
    }

    /// <summary>
    /// splits credits between the waves. uses a random multiplier to adjust the size of each wave budget, 
    /// and gives all remaining credits to the final wave
    /// </summary>
    /// <returns></returns>
    List<uint> AllocateCreditsForWaves()
    {
        List<uint> waveBudgets = new List<uint>();

        uint creditsRemaining = credits;

        for (uint i = 0; i < wavesPerStage; i++)
        {
            uint waveBudget = 0;

            if (i < wavesPerStage - 1)
            {
                int multiplierIndex = Random.Range(0, waveSizeMultipliers.Count);
                waveBudget = (uint)((creditsRemaining / (wavesPerStage - i)) * waveSizeMultipliers[multiplierIndex]);
            }
            else { waveBudget = creditsRemaining; }

            creditsRemaining -= waveBudget;

            waveBudgets.Add(waveBudget);
        }

        return waveBudgets;
    }

    /// <summary>
    /// searches enemy list for enemies that could be spawned at least once every wave.
    /// returns a lit of those enemies
    /// </summary>
    /// <param name="waveBudget"></param>
    /// <returns></returns>
    List<EnemySpawningData> CreateWaveEnemySpawnPool(uint waveBudget)
    {
        List<EnemySpawningData> spawnPool = new List<EnemySpawningData>();

        foreach (EnemySpawningData enemy in allEnemies)
        {
            // enemy goes in the pool if they can be spawned at least once per group
            // prevents an enemy leaving no resources for the other groups to spawn enemies
            if (waveBudget >= enemy.cost * groupsPerWave)
            {
                spawnPool.Add(enemy);
            }    
        }
        return spawnPool;
    }

    /// <summary>
    /// randomly selects an enemy from a spawn pool while considering the weighted spawn chances.
    /// </summary>
    /// <param name="spawnPool"></param>
    /// <param name="waveCredits"></param>
    /// <returns></returns>
    EnemySpawningData ChooseEnemyFromSpawnPool(List<EnemySpawningData> spawnPool, ref uint waveCredits)
    {
        List<EnemySpawningData> affordableEnemies = CreateAffordableEnemiesList(spawnPool, waveCredits);

        float totalSpawnWeight = 0;
        foreach (EnemySpawningData enemy in affordableEnemies)
        {
            totalSpawnWeight += enemy.weight;
        }

        float randomNum = Random.Range(0, totalSpawnWeight);

        foreach (EnemySpawningData enemy in affordableEnemies)
        {
            // enemy chosen, pays cost and returns this enemy
            if (randomNum <= enemy.weight)
            {
                waveCredits -= (uint)enemy.cost;
                return enemy;
            }
            else
            {
                randomNum -= enemy.cost;
            }
        }

        // no enemy chosen
        return null;

    }

    /// <summary>
    /// parses a list of enemies and returns a list of all enemies that are each affordable at least once with the current credits
    /// </summary>
    /// <param name="spawnPool"></param>
    /// <param name="waveCredits"></param>
    /// <returns></returns>
    private List<EnemySpawningData> CreateAffordableEnemiesList(List<EnemySpawningData> spawnPool, uint waveCredits)
    {
        List<EnemySpawningData> affordableEnemies = new List<EnemySpawningData>();

        foreach (EnemySpawningData enemy in spawnPool)
        {
            if (enemy.cost <= waveCredits)
            {
                affordableEnemies.Add(enemy);
            }
        }

        return affordableEnemies;
    }

    /// <summary>
    /// creates a list of all enemies that will spawn in a wave, in the order they will spawn
    /// </summary>
    /// <param name="waveCredits"></param>
    /// <returns></returns>
    List<EnemySpawningData> CreateEnemiesInWave(uint waveCredits)
    {
        // claim any leftover credits
        waveCredits += leftoverCredits; leftoverCredits = 0;


        // figure out enemy spawn pool (made of enemies that can be purchased at least once in every group)
        List<EnemySpawningData> spawnPool = CreateWaveEnemySpawnPool(waveCredits);


        // purchase enemies with credits to fill enemiesInWave list
        List<EnemySpawningData> enemiesInWave = new List<EnemySpawningData>();

        bool canAffordEnemies = true;
        while (canAffordEnemies)
        {
            EnemySpawningData enemyToSpawn = ChooseEnemyFromSpawnPool(spawnPool, ref waveCredits);

            if (enemyToSpawn == null) { canAffordEnemies = false; }
            else
            {
                enemiesInWave.Add(enemyToSpawn);
            }
        }

        // leave leftover credits for next wave to use
        if (waveCredits > 0) { leftoverCredits = waveCredits; }

        return enemiesInWave;
    }

    List<uint> DivideWaveIntoGroups(List<EnemySpawningData> enemiesInWave)
    {
        List<uint> groupEnemyCounts = new List<uint>();
        uint ungroupedEnemies = (uint)enemiesInWave.Count;

        for (uint i = 0; i < groupsPerWave; i++ )
        {
            uint enemiesInGroup = 0;

            if (i < groupsPerWave - 1) // not last group
            {
                // divide enemies in wave by enemies in group, rounded down by int truncation
                enemiesInGroup = ungroupedEnemies / (groupsPerWave - i);
            }
            else // all other enemies split into groups, give all remaining enemies to last group
            {
                enemiesInGroup = ungroupedEnemies;
            }

            groupEnemyCounts.Add(enemiesInGroup);
        }

        return groupEnemyCounts;
    }

    void SpawnGroupOfEnemies(List<EnemySpawningData> enemySpawnOrder, uint groupSize)
    {
        for (uint i = 0; i < groupSize; i++)
        {
            SpawningManager.Instance.SpawnEnemy(enemySpawnOrder.First().EnemyPrefab);
            enemySpawnOrder.Remove(enemySpawnOrder.First());
        }
    }

    public void SpawnNextGroup()
    {
        SpawnGroupOfEnemies(enemySpawnOrder, groupSizes.First());
        groupSizes.Remove(groupSizes.First());
    }

    void AddSpawnBeat(uint groupIndex)
    {
        uint measure = ((groupIndex / groupsPerWave) + 1 ) * (composer.measureCount / wavesPerStage);
        uint quarter = groupIndex - ( (groupIndex / groupsPerWave) * groupsPerWave);

        spawnBeatAction.firingBeats.Add(new Beat((int)measure, (int)quarter, 0, 0));
    }

    [ContextMenu("Set Up Stage")]
    public void SetUpNewStage()
    {
        enemySpawnOrder.Clear();
        groupSizes.Clear();
        spawnBeatAction.firingBeats.Clear();



        List<uint> waveBudgets = AllocateCreditsForWaves();

        // for each wave, get a list of enemies to spawn and group sizes.
        // then add those to the global lists for the entire stage
        for (int i = 0; i < wavesPerStage; i++)
        {
            List<EnemySpawningData> enemiesInWave = CreateEnemiesInWave(waveBudgets[i]);
            List<uint> waveGroupSizes = DivideWaveIntoGroups(enemiesInWave);

            foreach (EnemySpawningData enemy in enemiesInWave)
            {
                enemySpawnOrder.Add(enemy);
            }

            foreach (uint groupSize in waveGroupSizes)
            {
                groupSizes.Add(groupSize);

                // add a firing beat to spawn this group
                AddSpawnBeat((uint)(groupSizes.Count - 1));
            }
        }
    }

}
