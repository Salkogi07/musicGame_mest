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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsGhost())
        {
            // 히든노트 했던 것처럼 고스트 효과 넣기
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
