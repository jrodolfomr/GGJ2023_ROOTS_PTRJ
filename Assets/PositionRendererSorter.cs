using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRendererSorter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int SortingOrderBase = 5000;
    [SerializeField]
    private int Offset = 0;
    [SerializeField]
    private bool OnlyRunOnce = false;

    private float timer;
    private float timerMax = 0.01f;
    private GameObject playerSprite;
    private GameObject playerEffect;
    private Renderer myRenderer;
    private Renderer playerRenderer;
    private Renderer effectRenderer;
    private void Awake()
    {
        myRenderer = GetComponent<Renderer>();
        playerSprite = transform.Find("Circle")?.gameObject;
        if(playerSprite)
        playerRenderer = playerSprite.GetComponent<Renderer>();

        playerEffect = transform.Find("Circle1")?.gameObject;
        if(playerEffect)
        effectRenderer = playerEffect.GetComponent<Renderer>();
    }
    private void Start()
    {
      
        //playerEffect = transform.Find("Circle1")?.gameObject;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = timerMax;
            myRenderer.sortingOrder = (int)(SortingOrderBase - transform.position.y*3 - Offset);
            if (playerSprite)
            {
                playerRenderer.sortingOrder = (int)(SortingOrderBase - transform.position.y*3+1 - Offset);
            }
            if (playerEffect)
            {
                effectRenderer.sortingOrder = (int)(SortingOrderBase - transform.position.y*3+2 - Offset);
            }
            if (OnlyRunOnce)
            {
                Destroy(this);
            }
        }
    }

}
