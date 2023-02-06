using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovment : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource source;
    public AudioClip hitClip;

    private void Awake()
    {
        GameObject audioSourceObjcect = GameObject.Find("Audio Source");
        source = audioSourceObjcect.GetComponent<AudioSource>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime*20f);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyUnit")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            Debug.Log("trigger Do something else here");
         
            Destroy(gameObject);
            Enemy SelectedEnemy = collision.gameObject.GetComponentInParent<Enemy>();
            SelectedEnemy.HitEnemy();
        }
    }

}
