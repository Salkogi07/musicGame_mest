using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenNote : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float seconds = GameManager.Instance.GetRemainingSeconds(gameObject);
        float alpha = Mathf.Lerp(0f, 1f, Mathf.InverseLerp(1, 0, seconds));
        spriteRenderer.color = new Color(1, 1, 1, alpha);
    }
}
