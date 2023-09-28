using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpawnEnemyManager : MonoBehaviour
{
    [Header("EnemySpawnComponents")]
    [SerializeField] private float cooldownSpawnEnemy, waitTime, distanSpawn, rangeSpawn;
    [SerializeField] private int spawned, lowestCountEnemyCanSpawnInATurn, maxCountEnemyCanSpawnInATurn, maxEnemyScreen;
    [SerializeField] private GameObject[] enemyPrefab;
    [SerializeField] private Text waveText;
    private Transform enemyTotal;
    private float spawnRadius;

    [Header("Player")]
    private GameObject player;
    


    // Start is called before the first frame update
    void Awake()
    {
        SetUp();
    }

    // Update is called once per frame
    void Update()
    {
        if (ScreensManager.Instance.currentScreen == ScreensManager.Instance.gamePanel)
        {
            rangeSpawn = Random.Range(13f, 15f);
            SpawnEnemy(lowestCountEnemyCanSpawnInATurn, maxCountEnemyCanSpawnInATurn, cooldownSpawnEnemy, rangeSpawn);
            IncreaseMaxEnemyInScreen();
        }

    }

    void SpawnEnemy(int lowestCount, int maxCount, float cooldownSpawnEnemy, float range)
    {
        /* sau mỗi  1 khoảng cooldownSpawnEnemy thời gian(s) thì spawn ngẫu nhiên 1 lượng enemy khoảng lowestCount - maxCount 
         * gán enemy được spawn làm child của 1 object cha và giới hạn số enemy được spawn trên màn hình dựa trên số _maxEnemyScreen  
         */

        waitTime += Time.deltaTime;
        if (waitTime >= cooldownSpawnEnemy && enemyTotal.childCount < maxEnemyScreen)
        {
            int enemyCount = Random.Range(lowestCount, maxCount);
            //Debug.Log("so luong duoc sinh ra " + enemyCount);
            for (int i = 0; i < enemyCount && enemyTotal.childCount < maxEnemyScreen; i++)
            {
                Vector3 enemySpawnPosition = RandomEnemySpawnPosition(range);
                Collider2D[] colliders = Physics2D.OverlapCircleAll(enemySpawnPosition, spawnRadius);
                

                while (colliders.Length > 0)
                {
                    Debug.Log("Codeliders,Length ; " + colliders.Length);
                    enemySpawnPosition = RandomEnemySpawnPosition(range);
                    colliders = Physics2D.OverlapCircleAll(enemySpawnPosition, spawnRadius);
                }
                Debug.Log("enemySpawnPosition : " + enemySpawnPosition);

                int chisoenemy = Random.Range(0, enemyPrefab.Length);
                GameObject enemy = Instantiate(enemyPrefab[chisoenemy], enemySpawnPosition, Quaternion.identity); // BUG??
                enemy.transform.SetParent(enemyTotal);
                spawned++;
                DecreaseCooldownSpawnEnemy();
                //int soluongprefab = enemyPrefab.Length;
                //Debug.Log("chi so enemy: " + chisoenemy);
                //Debug.Log("sinh ra enemy " + chisoenemy);



            }
            waitTime = 0;

        }
    }

    void DecreaseCooldownSpawnEnemy()
    {
        if (cooldownSpawnEnemy >= 2f)
        {
            cooldownSpawnEnemy -= 0.1f;
        }
    }
    void IncreaseMaxEnemyInScreen()
    {
        if (spawned > 10 && maxEnemyScreen < 7)
        {
            maxEnemyScreen++;
            spawned = 0;
        }
    }
    void UnEnablesEnemyFollow()
    {
        for (int i = 0; i < enemyTotal.transform.childCount; i++)
        {
            enemyTotal.transform.GetChild(i).GetComponent<EnemyFollow>().speed = 0f;
        }
    }
    //void SpawnEnemy(int lowestCount, int maxCount, float cooldownSpawnEnemy, float range)
    //{
    //    /* sau mỗi  1 khoảng cooldownSpawnEnemy thời gian(s) thì spawn ngẫu nhiên 1 lượng enemy khoảng lowestCount - maxCount 
    //     * gán enemy được spawn làm child của 1 object cha và giới hạn số enemy được spawn trên màn hình dựa trên số _maxEnemyScreen
         
         
    //     */

    //    waitTime += Time.deltaTime;
    //    if (waitTime >= cooldownSpawnEnemy && enemyTotal.childCount < maxEnemyScreen)
    //    {
    //        int enemyCount = Random.Range(lowestCount, maxCount);
    //        Debug.Log("so luong duoc sinh ra " + enemyCount);
    //        for (int i = 0; i < enemyCount && enemyTotal.childCount < maxEnemyScreen; i++)
    //        {
    //            //int soluongprefab = enemyPrefab.Length;
    //            Vector3 spawnDirec = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
    //            Vector3 positionSpawnEnemy = player.transform.position + (spawnDirec.normalized * range);
    //            int chisoenemy = Random.Range(0, enemyPrefab.Length);
    //            //Debug.Log("chi so enemy: " + chisoenemy);
    //            GameObject enemy = Instantiate(enemyPrefab[chisoenemy], positionSpawnEnemy, Quaternion.identity); // BUG??
    //            //Debug.Log("sinh ra enemy " + chisoenemy);
    //            enemy.transform.SetParent(enemyTotal);
    //            spawned++;
    //            DecreaseCooldownSpawnEnemy();



    //        }
    //        waitTime = 0;

    //    }
    //}
    Vector3 RandomEnemySpawnPosition(float range)
    {

        Vector3 spawnDirec = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f),0f);
        Vector3 positionSpawnEnemy = player.transform.position + (spawnDirec.normalized * range);
        return positionSpawnEnemy;
    }
    void SetUp()
    {
        /*setup và spawn lập tức vài enemy khi bắt đầu game */
        spawned = 0;
        enemyTotal = GameObject.Find("EnemyTotal").transform;
        player = GameObject.FindGameObjectWithTag("Player");
        //maxEnemyScreen = 6;
        lowestCountEnemyCanSpawnInATurn = 2;
        maxCountEnemyCanSpawnInATurn = 3;
        cooldownSpawnEnemy = 5f;
        waitTime = 0;
        spawnRadius = 3f;
        SpawnEnemy(3, 4, 0f, Random.Range(4f, 6f));
        UnEnablesEnemyFollow();
    }
}
