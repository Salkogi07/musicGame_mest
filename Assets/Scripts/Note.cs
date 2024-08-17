using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public enum Item { None, Heal, MonsterPause, Energy, MissDefense, Timer, Perfect }

    public Item item;

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
            if (item == Item.Heal)
            {
                GameManager.Instance.Change_PlayerHp(50);
            }
            else if (item == Item.MonsterPause)
            {
                GameManager.Instance.itemMonsterPauseTimer = 2;
            }
            else if (item == Item.Energy)
            {
                GameManager.Instance.itemMonsterPauseTimer = 4;
            }
            else if (item == Item.MissDefense)
            {
                GameManager.Instance.itemMissDefenseCount = 10;
            }
            else if (item == Item.Timer)
            {
                GameManager.Instance.itemTimerTimer = 2;
            }
            else if (item == Item.Perfect)
            {
                GameManager.Instance.itemPerfectTimer = 5;
            }
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
