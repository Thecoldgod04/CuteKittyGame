using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieParticleSelfDestroy : MonoBehaviour
{
    float lifeTime = 2f;
    float livedFor = 0;
    // Update is called once per frame
    void Update()
    {
        if(livedFor >= lifeTime)
        {
            Destroy(this.gameObject);
        }
        else
        {
            livedFor += Time.deltaTime;
        }
    }
}
