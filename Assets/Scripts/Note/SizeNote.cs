using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeNote : MonoBehaviour
{

    private void Awake()
    {

    }

    void Start()
    {
        
    }

    void Update()
    {
        float seconds = GameManager.Instance.GetRemainingSeconds(gameObject);
        float size = Mathf.Lerp(0.1f, 1f, Mathf.InverseLerp(1.5f, 0.5f, seconds));
        transform.localScale = Vector3.one * size;
    }
}
