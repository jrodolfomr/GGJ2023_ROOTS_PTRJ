using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update.

    public int Health = 10;

    private AudioSource source;

    private Renderer myRenderer;
    public AudioClip destroyClip;
    public AudioClip hitClip;
    public GameObject EnemyProjectileObject;
    public float ProjectileSpeed;
    public float HitPercentage;
    public EnemySpawner spawner;
    public bool skipWait;
    public bool isProjectileBelowSprite;
    public bool autoDamage;

    public bool isEnemyActive = false;
    public bool isMoveUp = false;
    private void Awake()
    {
        GameObject audioSourceObjcect = GameObject.Find("Audio Source");
        source = audioSourceObjcect.GetComponent<AudioSource>();
        myRenderer = GetComponent<Renderer>();
    }

    void Start()
    {
        isEnemyActive = true;
   
        StartCoroutine(EnemyProjectileShoot());
       
    }
    IEnumerator EnemyProjectileShoot()
    {
        Renderer projectileRenderer = EnemyProjectileObject.GetComponent<Renderer>();
        projectileRenderer.sortingOrder = myRenderer.sortingOrder + 6;
        while (isEnemyActive) {
            if (!skipWait)
            {
                yield return new WaitForSeconds(Random.Range(0.5f, 1f) + ProjectileSpeed);
            }
            else
            {
                yield return new WaitForSeconds(0.3f);
            }
            if (Random.Range(0f, 1f) > HitPercentage)
            {
                if (isProjectileBelowSprite)
                {
                    projectileRenderer = EnemyProjectileObject.GetComponent<Renderer>();
                    projectileRenderer.sortingOrder = myRenderer.sortingOrder + 6;
                }
                GameObject Projectile = Instantiate(EnemyProjectileObject, transform.position + new Vector3(0f, 0f), transform.rotation, transform);

                Projectile.transform.parent = null;
            }
            if (!skipWait)
            {
                yield return new WaitForSeconds(Random.Range(0.2f, 0.5f));
            }
        
            if (isMoveUp)
                transform.localPosition =(new Vector3(Random.Range(-1,2), Random.Range(-1, 2)));
            else
                transform.localPosition = (new Vector3(Random.Range(-1, 2), Random.Range(-1, 2)));
            isMoveUp = !isMoveUp;
            if (autoDamage)
            {
                HitEnemy();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void HitEnemy()
    {
        Debug.Log("Enemy: was hit");
        Health -= 1;
        if(Health <= 0)
        {
            if (!skipWait)
            {
                source.PlayOneShot(destroyClip);
                source.PlayOneShot(destroyClip);
            }
            spawner.NotifySpawnerEnemyDown();
            Destroy(gameObject);
        }
        else
        {
            if(!skipWait)
            source.PlayOneShot(hitClip);
        }
    }
}
