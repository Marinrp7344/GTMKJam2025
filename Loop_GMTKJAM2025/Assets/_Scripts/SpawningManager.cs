using UnityEngine;

public class SpawningManager : MonoBehaviour
{
    
    public static SpawningManager Instance;
    private Collider2D worldBoundsCollider;


    [Space]
    [Header("Enemies")]
    [SerializeField] private GameObject commmonEnemy;
    [SerializeField] private GameObject dashingEnemy;

    [Space]
    [Header("Debug")]
    [SerializeField] private int currentWave;
    [SerializeField] private int maxSpawnableEnemies;
    [SerializeField] private int currentlySpawnedEnemyTotal;

    [Space]
    [Header("World Space Statitics")]
    [SerializeField] private float horizontalCenterDistance;
    [SerializeField] private float verticalCenterDistance;
    [SerializeField] private float NSHorizontalCenterDistance;//Non spawnable distance from center
    [SerializeField] private float NSVerticalCenterDistance;//Non spawnable distance from center

    [Space]
    [Header("World Space Edit")]
    [Range(0, 1)]
    [SerializeField] private float unspawnableAreaPercentage;
    private void Start()
    {
        SpawningManager.Instance = this;
        worldBoundsCollider = GetComponent<Collider2D>();
        SetSpawnableBounds();
    }

    private void SetSpawnableBounds()
    {
        horizontalCenterDistance = worldBoundsCollider.bounds.max.x;
        verticalCenterDistance = worldBoundsCollider.bounds.max.y;
        NSHorizontalCenterDistance = unspawnableAreaPercentage * horizontalCenterDistance;
        NSVerticalCenterDistance = unspawnableAreaPercentage * verticalCenterDistance;
    }

    /*
    public void StartWave()
    {
        currentWave = 0;
    }

    public void EndWave()
    {

    }*/

    /// <summary>
    /// Spawns the enemy according to the bounding box of the world
    /// </summary>
    public void SpawnEnemy()
    {

    }

    public Vector2 ChooseSpawnLocation()
    {
        float randomXLocation = Random.Range(-horizontalCenterDistance, horizontalCenterDistance);
        float randomYLocation = 0;
        if(randomXLocation >= NSHorizontalCenterDistance && randomXLocation <= horizontalCenterDistance)
        {
            randomYLocation = Random.Range(-verticalCenterDistance, verticalCenterDistance);
        }
        else if(randomXLocation < -NSHorizontalCenterDistance && randomXLocation >= - horizontalCenterDistance)
        {
            randomYLocation = Random.Range(-verticalCenterDistance, verticalCenterDistance);
        }
        else
        {
            int upOrDownLocation = Random.Range(0, 2);

            switch(upOrDownLocation)
            {
                case 0:
                    randomYLocation = Random.Range(NSVerticalCenterDistance, verticalCenterDistance);
                    break;
                case 1:
                    randomYLocation = Random.Range(-verticalCenterDistance, -NSVerticalCenterDistance);
                    break;
            }
            
        }


        Vector2 spawnLocation = new Vector2(randomXLocation, randomYLocation);
        return spawnLocation;
    }

}
