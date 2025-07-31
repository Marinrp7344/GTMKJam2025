using UnityEngine;
using System.Collections.Generic;

public class SpawnDirector : MonoBehaviour
{

    public uint credits = 100;

    [SerializeField] uint wavesPerStage = 4;
    uint groupsPerWave;

    [Space]
    [SerializeField] List<EnemySpawningData> allEnemies = new List<EnemySpawningData>();
    [SerializeField] List<float> waveSizeMultipliers = new List<float>();

    Metronome metronome;

    private void Start()
    {
        metronome = Metronome.Singleton;

        groupsPerWave = metronome.quartersPerMeasure;
    }

    [ContextMenu("allocate credits for waves")]
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



}
