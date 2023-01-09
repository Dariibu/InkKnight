using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatController : MonoBehaviour
{
    public static SplatController instance;

    public GameObject[] splats;
    public Color[] colors;

    int splatOrder;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void MakeSplat(Transform position)
    {
        Vector3 spawnrot = new Vector3(0, 0, Random.Range(0, 360f));

        GameObject newSplat = Instantiate(splats[Random.Range(0, splats.Length)], position.position, transform.rotation);
        newSplat.GetComponent<SplatLifeTime>().wantToDestroy = true; //true si quiero destruir la pintura.
        SpriteRenderer newSplatSR = newSplat.GetComponent<SpriteRenderer>();

        newSplatSR.color = colors[Random.Range(0, colors.Length)];

        splatOrder++;
        newSplatSR.sortingOrder = splatOrder;
    }

    void Destroythis(GameObject splat)
    {
        Destroy(splat);
    }
}
