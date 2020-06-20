using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

namespace TowerDefence.Enemy
{
    [RequireComponent(typeof(Path))]
    public class Spawner : MonoBehaviour
    {

        [SerializeField]
        private GameObject enemyParent = null;

        [SerializeField]
        private List<GameObject> enemyPrefabs = new List<GameObject>();

        [SerializeField]
        private List<int> enemyChance = new List<int>();//1 element / sum of all elements

        private List<int> chanceList = new List<int>();//example: enemyChance[0] = 8, enemyChance[1] = 2; chanceList{0,0,0,0,0,0,0,0,1,1}

        [SerializeField]
        private int enemyRandomCount = 10;

        [SerializeField]
        private int waveCount = 0;

        [SerializeField]
        private float nextWavePause = 2f;

        [SerializeField]
        private float waveTime = 10f;

        private int enemyCount = 0;

        private List<Vector3> startPoints = new List<Vector3>();

        private Path path;

        private int startPointIndex = 0;

        private bool gameStarted = false;

        private List<GameObject> livedEnemy = new List<GameObject>();

        private void Start()
        {
            path = GetComponent<Path>();
            for (int i = 0; i < path.GetWayCount(); i++)
            {
                startPoints.Add(path.GetWay(i).wayPoints[0].position);
            }

            for (int i = 0; i < enemyPrefabs.Count; i++)
            {
                int tempChance;
                if (i >= enemyChance.Count)
                {
                    tempChance = 2;
                }
                else
                {
                    tempChance = enemyChance[i];
                }

                for (int k = 0; k < tempChance; k++)
                {
                    chanceList.Add(i);
                }
            }

            #if!UNITY_EDITOR
            CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            ci.NumberFormat.CurrencyDecimalSeparator = ".";
            if (File.Exists(@".\config.txt"))
            {
                string[] config = File.ReadAllLines(@".\config.txt");

                for (int i = 1; i < config.Length; i++)
                {

                    string numberInString = config[i].Substring(config[i].IndexOf(":") + 1, config[i].Length - config[i].IndexOf(":") - 1);

                    if (numberInString.Contains(","))
                    {
                        numberInString.Replace(",", ".");
                    }

                    if (config[i].Contains("Wave time:"))
                    {
                        waveTime = float.Parse(numberInString, ci);
                    }else if(config[i].Contains("Next wave pause:")){
                        nextWavePause = float.Parse(numberInString, ci);
                    }
                }
            }
            else
            {
                //File.Create(@".\config.txt");

                string[] lines = { "Wave", "Wave time:" + waveTime, "Next wave pause:" + nextWavePause };
                File.WriteAllLines(@".\config.txt", lines);
            }
            #endif
        }

        public int GetWaveCount()
        {
            return waveCount;
        }

        public void DestroyEnemy(GameObject enemy)
        {
            livedEnemy.Remove(enemy);
            Destroy(enemy);

        }

        public void DestroyAllEnemys()
        {
            for (int i = 0; i < livedEnemy.Count; i++)
            {
                Destroy(livedEnemy[i]);
            }
            livedEnemy.Clear();
        }

        private void StartWave()
        {
            enemyCount = Random.Range(waveCount, waveCount + enemyRandomCount);

            Debug.Log("Enemy count: " + enemyCount);

            StartCoroutine(NextEnemy((waveTime * 0.5f) / enemyCount));
        }

        private IEnumerator NextEnemy(float pouseTime)
        {
            while (enemyCount > 0)
            {
                int enemyIndex = chanceList[Random.Range(0, chanceList.Count)];

                GameObject tempEnemy = Instantiate(enemyPrefabs[enemyIndex], startPoints[startPointIndex], Quaternion.identity);

                Path.Way tempWay = path.GetWay(startPointIndex);

                tempEnemy.GetComponent<Enemy>().SetWave(waveCount);
                tempEnemy.GetComponent<Enemy>().SetWay(tempWay);
                tempEnemy.GetComponent<Enemy>().SetSpawner(this);

                tempEnemy.transform.forward = (tempWay.wayPoints[1].position - tempWay.wayPoints[0].position).normalized;

                tempEnemy.transform.parent = enemyParent.transform;

                livedEnemy.Add(tempEnemy);

                startPointIndex++;

                if (startPointIndex >= startPoints.Count)
                {
                    startPointIndex = 0;
                }

                enemyCount--;
                yield return new WaitForSeconds(pouseTime);
            }
        }

        private IEnumerator StartGame()
        {
            while (gameStarted)
            {
                waveCount++;
                StartWave();

                Debug.Log("Start next wave!");

                yield return new WaitForSeconds(waveTime + nextWavePause);
            }
        }

        public void StartGameCourutine()
        {
            gameStarted = true;
            StartCoroutine(StartGame());
        }

        public void StopGame()
        {
            gameStarted = false;
            enemyCount = 0;
            StopAllCoroutines();
        }

    }
}
