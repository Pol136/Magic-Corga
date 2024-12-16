using UnityEngine;
using System.Collections;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject[] portals;
    public string pathToLevelSettings = "C:\\Users\\polin\\University\\Magic-Corga\\project_magic_corga\\Assets\\Levels\\level1.json";

    private Dictionary<int, List<EnemyData>> levelData;
    private int currentWave = 0;
    private bool waveInProgress = false;

    void Start()
    {
        LoadLevelData();
    }

    private void LoadLevelData()
    {
        if (string.IsNullOrEmpty(pathToLevelSettings))
        {
            Debug.LogError("Path to level settings is not specified!");
            return;
        }
        string jsonString = File.ReadAllText(pathToLevelSettings);

        try
        {
            levelData = JsonConvert.DeserializeObject<Dictionary<int, List<EnemyData>>>(jsonString);
        }
        catch (JsonReaderException ex)
        {
            Debug.LogError($"Error parsing JSON: {ex.Message}");
        }

        if (levelData == null)
        {
            Debug.LogError("Failed to deserialize level data!");
        }
    }

    private void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && currentWave < levelData.Count)
        {
            GenerateWave(currentWave);
            currentWave++;
        }
    }

    private void GenerateWave(int waveNumber)
    {
        if (levelData.ContainsKey(waveNumber))
        {
            waveInProgress = true;
            List<EnemyData> waveEnemies = levelData[waveNumber];
            foreach (EnemyData enemyData in waveEnemies)
            {
                GenerateEnemy(enemyData.portal, enemyData.speed, enemyData.health);
            }
        }
    }


    public void GenerateEnemy(int portalNumber, float speedEnemy, string healthEnemy)
    {
        GameObject enemy = Instantiate(enemyPrefab, portals[portalNumber].transform.position, Quaternion.identity);

        //Debug.Log(speedEnemy+" "+healthEnemy);

        enemy.GetComponent<Movement>().SetSpeed(speedEnemy);
        enemy.GetComponent<Enemy>().SetHealth(healthEnemy);
    }

    [System.Serializable]
    private class EnemyData
    {
        public string health;
        public float speed;
        public int portal;
    }
}

