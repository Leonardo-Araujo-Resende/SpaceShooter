using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimDestroy : MonoBehaviour
{
    public float time;

    void Update()
    {
        Destroy(this.gameObject, time);

    }
}
