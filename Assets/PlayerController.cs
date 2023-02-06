using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    

    private GameObject[] PlayerPoint;
    private GameObject[] EnemyPoint;

    private Vector2 playerPosition;

    public AudioSource source;
    public AudioClip playerShoot;
    public AudioClip playerHit;
    public AudioClip destroyClip;

    public GameObject PlayerProjectile;

    private Animator m_Animator;

    public Text playerHealthText;

    public GameObject GameOverScreen;

    public int Health = 100;
    void Start()
    {
       playerPosition = new Vector2(1f, 1f);
       PlayerPoint = GameObject.FindGameObjectsWithTag("PlayerPoint");

       foreach(GameObject point in PlayerPoint)
        {
            Debug.Log($"Player:{point.name}");

        }

        EnemyPoint = GameObject.FindGameObjectsWithTag("EnemyPoint");

        foreach (GameObject point in EnemyPoint)
        {
            Debug.Log($"Enemy:{point.name}");

        }
        Debug.Log($"Player starts at position ({playerPosition})");

        GameObject playerSprite = transform.Find("Circle").gameObject;
        playerSprite = transform.Find("Circle")?.gameObject;
        Debug.Log("IIIIIIIIHAZ THE SPRITE:"+ playerSprite);
        if (playerSprite)
            m_Animator = playerSprite.GetComponent<Animator>();

        playerHealthText.text = $"HP: { Health}";

        }

    private float debounce = 0.0f;
    private float repeat = 0.1f;  // reduce to speed up auto-repeat input
    public float vAxis;
    public float hAxis;
    public float cooldown;
    public float cooldownValue = 0.1f;
    private bool canBeHit = true;
    private bool canMove = true;
    GameObject GetQuestBoxItem(GameObject[] g, string name)
    {
        for (int i = 0; i < g.Length; i++)
        {
            if (g[i].name == name)
                return g[i];
        }

        Debug.Log("No item has the name '" + name + "'.");
        return null;
    }

    // Update is called once per frame
    private void SetPlayer()
    {
        Debug.Log($"Player is in position ({playerPosition})");
        cooldown = Time.time + cooldownValue;
        GameObject targetPoint = GameObject.Find($"PZ{playerPosition.x}{playerPosition.y}");
        transform.position = targetPoint.transform.position;
    }
    void Update()
    {
     /*  float horizontal = Input.GetAxis("Vertical");
       float vertical = Input.GetAxis("Horizontal");

        if (horizontal > 0)
            playerPosition.x = Mathf.Clamp(playerPosition.x-1,0f,2f);
        if (horizontal < 0)
            playerPosition.x = Mathf.Clamp(playerPosition.x - 1, 0f, 2f);*/

         vAxis = Input.GetAxisRaw("Vertical");
         hAxis = Input.GetAxisRaw("Horizontal");


        if (canMove)
        {
            // check if user let go of the stick; if so, reset the input bounce control
            if (Mathf.Abs(vAxis) < 0.1f && Mathf.Abs(hAxis) < 0.1f)
                debounce = 0.0f;

            // if it's been long enough since the last input, then we allow it

            // do all your input sensing here

            if (vAxis > 0.1f && Time.time > cooldown) // UP W or yAxis.UP
            {
                playerPosition.y = Mathf.Clamp(playerPosition.y - 1, 0f, 2f);
                SetPlayer();

            }
            if (vAxis < -0.1f && Time.time > cooldown) // DOWN W or yAxis.DOWN
            {
                playerPosition.y = Mathf.Clamp(playerPosition.y + 1, 0f, 2f);
                SetPlayer();
            }
            if (hAxis > 0.1f && Time.time > cooldown) // RIGHT  W or yAxis.RIGHT
            {
                playerPosition.x = Mathf.Clamp(playerPosition.x + 1, 0f, 2f);
                SetPlayer();
            }
            if (hAxis < -0.1f && Time.time > cooldown) // LEFT W or yAxis.LEFT
            {
                playerPosition.x = Mathf.Clamp(playerPosition.x - 1, 0f, 2f);
                SetPlayer();
            }


            if (Input.GetKeyDown("space"))
            {
                print("space key was pressed");
                source.PlayOneShot(playerShoot);

                GameObject Projectile = Instantiate(PlayerProjectile, transform.position + new Vector3(0f, 1f), transform.rotation, transform);
                Projectile.transform.parent = null;

            } 
        }


    }

    IEnumerator PlayerRecoil()
    {
        yield return new WaitForSeconds(0.5f);
        m_Animator.ResetTrigger("PlayerHit");
        m_Animator.SetTrigger("PlayerRecover");
        canBeHit = true;
        canMove = true;
    }
    IEnumerator PlayerDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        playerHealthText.text = $"Game Over";
        Destroy(gameObject);
        GameOverScreen.SetActive(true);
    }
  
    public void TakeDamage(int value)
    {
        if (canBeHit)
        {
            canBeHit = false;
            canMove = false;
            Health += value ;
            playerHealthText.text = $"HP: { Health}";
            if (Health <= 0)
            {
                source.PlayOneShot(destroyClip);
                source.PlayOneShot(destroyClip);
                m_Animator.ResetTrigger("PlayerRecover");
                m_Animator.SetTrigger("PlayerDestroy");
                StartCoroutine(PlayerDestroy());
            }
            else
            {
                source.PlayOneShot(playerHit);
                m_Animator.ResetTrigger("PlayerRecover");
                m_Animator.SetTrigger("PlayerHit");
            
                 StartCoroutine(PlayerRecoil());
            }
        }
    }
}
