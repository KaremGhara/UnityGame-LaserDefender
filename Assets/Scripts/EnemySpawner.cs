using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> WaveConfigs;
    [SerializeField]int startingWave = 0;
    [SerializeField] bool loopin = false;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return  StartCoroutine(SpawnAllWaves());
        }
        while (loopin);
    }
    private IEnumerator SpawnAllWaves()
    {
        for(int WaveIndex=startingWave; WaveIndex<WaveConfigs.Count;WaveIndex++)
        {
            var currentWave = WaveConfigs[WaveIndex];
            yield return StartCoroutine(SpawnAllEnimiesInWave(currentWave));

        }
    }
    private IEnumerator SpawnAllEnimiesInWave(WaveConfig currWave)
    {
        for (int enemyCount = 0; enemyCount < currWave.GetNumberOfEnimes(); enemyCount++)
        {
            var newWEnemy=Instantiate(currWave.GetEnemyPrefab(), currWave.GetWaypoint()[0].transform.position, Quaternion.identity);
            newWEnemy.GetComponent<EnemeyPathing>().SetWaveConfig(currWave);
            yield return new WaitForSeconds(currWave.GetTimeBetweenSpawns());
        }
    }
}
