using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuringEffect : MonoBehaviour
{
    public float druingEffect = 1f;

    void Start()
    {
        Destroy(gameObject,druingEffect);
    }
}
