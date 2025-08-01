using UnityEngine;

public class SpawningManager : MonoBehaviour
{
    public static SpawningManager Instance;
    private Collider2D worldBoundsCollider;
    public bool spawn;
    [SerializeField] private bool spawningEnded;
    [SerializeField] private Transform player;
    [Space]
    [SerializeField] private GameObject wallObject;
    [SerializeField] private float wallToBoundOffset;
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

    [Space]
    [Header("Enemy Spawn Rates")]
    [Range(0, 100)]
    [SerializeField] private float commonEnemyChances;
    [Range(0, 100)]
    [SerializeField] private float dashEnemyChances;
    [Range(0, 100)]
    [SerializeField] private float spawnerEnemyChances;
    [SerializeField] private int amountofEnemiesPerSpawn;
    


    private void Start()
    {
        SpawningManager.Instance = this;
        worldBoundsCollider = GetComponent<Collider2D>();
        SetSpawnableBounds();
        SetUpWalls();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
    }

    
    private void SetSpawnableBounds()
    {
        horizontalCenterDistance = worldBoundsCollider.bounds.max.x;
        verticalCenterDistance = worldBoundsCollider.bounds.max.y;
        NSHorizontalCenterDistance = unspawnableAreaPercentage * horizontalCenterDistance;
        NSVerticalCenterDistance = unspawnableAreaPercentage * verticalCenterDistance;
    }

    private void SetUpWalls()
    {
        //Upper wall
        Vector2 upperWallSpawnPoint = new Vector2(0, verticalCenterDistance + wallToBoundOffset);
        GameObject upperWall = Instantiate(wallObject, upperWallSpawnPoint, Quaternion.identity);
        BoxCollider2D upperWallBoxCollider = upperWall.GetComponent<BoxCollider2D>();
        upperWallBoxCollider.size = new Vector2(horizontalCenterDistance * 2, upperWallBoxCollider.size.y);

        //down wall
        Vector2 downWallSpawnPoint = new Vector2(0, -verticalCenterDistance - wallToBoundOffset);
        GameObject downWall = Instantiate(wallObject, downWallSpawnPoint, Quaternion.identity);
        BoxCollider2D downWallBoxCollider = downWall.GetComponent<BoxCollider2D>();
        downWallBoxCollider.size = new Vector2(horizontalCenterDistance * 2, downWallBoxCollider.size.y);

        //Right wall
        Vector2 rightWallSpawnPoint = new Vector2(horizontalCenterDistance + wallToBoundOffset, 0);
        GameObject rightWall = Instantiate(wallObject, rightWallSpawnPoint, Quaternion.identity);
        BoxCollider2D rightWallBoxCollider = rightWall.GetComponent<BoxCollider2D>();
        rightWallBoxCollider.size = new Vector2(rightWallBoxCollider.size.x, verticalCenterDistance * 2);

        //Left wall
        Vector2 leftWallSpawnPoint = new Vector2(-horizontalCenterDistance - wallToBoundOffset, 0);
        GameObject leftWall = Instantiate(wallObject, leftWallSpawnPoint, Quaternion.identity);
        BoxCollider2D leftWallBoxCollider = leftWall.GetComponent<BoxCollider2D>();
        leftWallBoxCollider.size = new Vector2(leftWallBoxCollider.size.x, verticalCenterDistance * 2);

    }


    public void SpawningEnded()
    {
        spawningEnded = true;
    }


    public void CheckEnemiesStillOnStage()
    {
        GameObject[] enemiesRemaining = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemiesRemaining.Length <= 0)
        {
            UpdgradeMenu.Instance.StartNewRound();
            spawningEnded = false;
        }
    }

    private void Update()
    {
        if (spawningEnded)
        {
            CheckEnemiesStillOnStage();
        }
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
    public void SpawnEnemy(GameObject enemy)
    {
        int spawnEnemy = Random.Range(0, 100);
        GameObject decidedEnemy = enemy;
        Instantiate(decidedEnemy, ChooseSpawnLocation(), Quaternion.identity);


    }

    /*
    public GameObject DecideEnemy(int chosenEnemy)
    {
        GameObject decideEnemy = commonEnemy;
        if(chosenEnemy >= 0 && chosenEnemy <= commonEnemyChances)
        {
            decideEnemy = commonEnemy;
        }
        else if (chosenEnemy > commonEnemyChances && chosenEnemy <= dashEnemyChances)
        {
            decideEnemy = dashingEnemy;
        }
        else if (chosenEnemy > dashEnemyChances && chosenEnemy <= spawnerEnemyChances)
        {
            Debug.Log("Spawner Enemy");
        }

        return decideEnemy;
    }
    */
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
