using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public int enemyHitValue;
    public int speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerUnit")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            Debug.Log("PLAYERHIT Do something else here");

            Destroy(gameObject);
           PlayerController SelectedPlayer = collision.gameObject.GetComponentInParent<PlayerController>();
            SelectedPlayer.TakeDamage(enemyHitValue);
        }
    }

}
