using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieParticleManager : MonoBehaviour
{
    public GameObject dieParticle;

    public void spawnParticle(Vector2 pos)
    {
        Instantiate(dieParticle, pos, Quaternion.identity);
    }
}
