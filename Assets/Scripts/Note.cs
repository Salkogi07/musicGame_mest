using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public bool isMultiNode;

    public int position;
    public int energy;

    public int position_multi;
    public int second_energy;

    public bool isPositionPressed;
    public bool isPositionMultiPressed;
    public bool isIgnoreMiss;

    SpriteRenderer spriteRenderer;

    SpriteRenderer[] spriteRenderers;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (isMultiNode)
        {
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        Ghost();
    }

    private void Ghost()
    {
        if (GameManager.Instance.IsGhost())
        {
            float seconds = GameManager.Instance.GetRemainingSeconds(gameObject);
            float alpha = Mathf.Lerp(1f, 0f, Mathf.InverseLerp(1f, 0f, seconds));
            if (isMultiNode)
            {
                spriteRenderers[0].color = new Color(1, 1, 1, alpha);
                spriteRenderers[1].color = new Color(1, 1, 1, alpha);
            }
            else
            {
                spriteRenderer.color = new Color(1, 1, 1, alpha);
            }
        }
        else
        {
            if (isMultiNode)
            {
                spriteRenderers[0].color = new Color(1, 1, 1, 1);
                spriteRenderers[1].color = new Color(1, 1, 1, 1);
            }
            else
            {
                spriteRenderer.color = new Color(1, 1, 1, 1);
            }
        }
    }

    public bool IsCorrectKey(int index)
    {
        if (!isMultiNode)
            return position == index;
        else
            return (position == index && !isPositionPressed) || (position_multi == index && !isPositionMultiPressed);
    }

    public void HitNote(int index)
    {
        if (index == position && !isPositionPressed)
        {
            isPositionPressed = true;
        }
        else if (index == position_multi && !isPositionMultiPressed)
        {
            isPositionMultiPressed = true;
        }
    }

    public bool IsBothPressed()
    {
        return isPositionPressed && isPositionMultiPressed;
    }
}
