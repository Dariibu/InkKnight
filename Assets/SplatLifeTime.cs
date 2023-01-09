using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatLifeTime : MonoBehaviour
{
    float LifeTime = 0;
    public bool wantToDestroy = false;

    void Update()
    {
        if (wantToDestroy)
        {
            LifeTime += Time.deltaTime;
            if (LifeTime >= 5f)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
