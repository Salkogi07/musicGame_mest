using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeNote : MonoBehaviour
{

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float seconds = GameManager.Instance.GetRemainingSeconds(gameObject);
        float size = Mathf.Lerp(0.1f, 1f, Mathf.InverseLerp(1.5f, 0.5f, seconds));
        transform.localScale = Vector3.one * size;
    }
}
