using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject EnemyOne;
    public GameObject EnemyBoss;
    public GameObject EnemyTreasure;
    public GameObject stageClear;

   public  AudioSource BGMSelection;
    public AudioClip BGM1;
    public AudioClip BGM2;
    public AudioClip BGM3;
    public AudioClip LevelEnd;
    public AudioClip Victory;

    public Text levelText;
    public GameObject victoryScreen;

    private int NumberOfEnemies;
    private int Level;
    void Start()
    {
          
        


        GameObject go = Instantiate(EnemyOne, transform.position, transform.rotation,transform);
        go.transform.localPosition += new Vector3(1f, -1f);
        go.GetComponent<Enemy>().spawner = this;
        go = Instantiate(EnemyOne, transform.position, transform.rotation, transform);
        go.transform.localPosition += new Vector3(0f, 0f);
        go.GetComponent<Enemy>().spawner = this;
        NumberOfEnemies = 2;
        Level = 1;
        BGMSelection.clip = BGM1;
        BGMSelection.Play();
        levelText.text = $"Level: {Level}";
    }

    public void NotifySpawnerEnemyDown()
    {
        NumberOfEnemies -= 1;
        if(NumberOfEnemies == 0)
        {
            Level++;
            switch (Level)
            {
                case 2: StartCoroutine(SpawnLevelTwoEnemies()); break;
                case 3: StartCoroutine(SpawnLevelTreasure()); break;
                case 4: StartCoroutine(SpawnLevelBoss()); break;
                case 5: StartCoroutine(SpawnLevelWin()); break;
            }
        }
    }
    IEnumerator SpawnLevelTwoEnemies()
    {
        //show victory screen
        yield return new WaitForSeconds(0.5f);
        victoryScreen.SetActive(true);
        yield return new WaitForSeconds(2);
        victoryScreen.SetActive(false);
        GameObject go = Instantiate(EnemyOne, transform.position, transform.rotation, transform);
        go.transform.localPosition += new Vector3(1f, -1f);
        go.GetComponent<Enemy>().spawner = this;
        go = Instantiate(EnemyOne, transform.position, transform.rotation, transform);
        go.transform.localPosition += new Vector3(0f, 0f);
        go.GetComponent<Enemy>().spawner = this;
        go = Instantiate(EnemyOne, transform.position, transform.rotation, transform);
        go.transform.localPosition += new Vector3(-1f, -1f);
        go.GetComponent<Enemy>().spawner = this;
        levelText.text = $"Level: {Level}";
        NumberOfEnemies = 3;
        BGMSelection.clip = BGM2;
        BGMSelection.Play();
    }
    IEnumerator SpawnLevelBoss()
    {
        //show victory screen
        yield return new WaitForSeconds(0.5f);
        victoryScreen.SetActive(true);
        yield return new WaitForSeconds(2);
        victoryScreen.SetActive(false);
        GameObject go = Instantiate(EnemyBoss, transform.position, transform.rotation, transform);
        go.transform.localPosition += new Vector3(0f, 0f);
        go.GetComponent<Enemy>().spawner = this;
        levelText.text = $"Level: Boss";
        NumberOfEnemies = 1;
        BGMSelection.clip = BGM3;
        BGMSelection.Play();
    }
    IEnumerator SpawnLevelTreasure()
    {
        yield return new WaitForSeconds(0.5f);
        victoryScreen.SetActive(true);
        yield return new WaitForSeconds(2);
        victoryScreen.SetActive(false);
        //change to treasure
        GameObject go = Instantiate(EnemyTreasure, transform.position, transform.rotation, transform);
        go.transform.localPosition += new Vector3(-1f, 0f);
        go.GetComponent<Enemy>().spawner = this;
         
        levelText.text = $"Level: Treasure";
        NumberOfEnemies = 1;

    }
    IEnumerator SpawnLevelWin()
    {
        //show victory screen
        yield return new WaitForSeconds(0.5f);
        victoryScreen.SetActive(true);
        yield return new WaitForSeconds(2);
        victoryScreen.SetActive(false);
        levelText.text = $"You WIN";
        BGMSelection.clip = LevelEnd;
        BGMSelection.Play();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
