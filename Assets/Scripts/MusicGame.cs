using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicGame : MonoBehaviour
{
    private const float RADIUS = 1;

    private AudioSource audio;
    public GameObject notes;
    
    public GameObject[] noteInstances;
    public GameObject[] judges;

    public GameObject[] score;
    public float offset;
    public float bpm;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    void Start()
    {
        audio.Play();

        noteInstances = new GameObject[score.Length];

        for (int i = 0; i < score.Length; i++)
        {
            if (!score[i].GetComponent<Note>().isMultiNode)
            {
                int noteIndex = score[i].GetComponent<Note>().position - 1;
                GameObject noteInstance = Instantiate(score[i], notes.transform);
                noteInstance.transform.localPosition = Vector3.right * judges[noteIndex].transform.localPosition.x + Vector3.up * GameManager.SPEED * (60 / bpm * i + offset);

                noteInstances[i] = noteInstance;
            }
            else
            {
                int noteIndex = score[i].GetComponent<Note>().position - 1;
                GameObject noteInstance = Instantiate(score[i], notes.transform);
                noteInstance.transform.localPosition = Vector3.up * GameManager.SPEED * (60 / bpm * i + offset);

                noteInstances[i] = noteInstance;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        notes.transform.localPosition = Vector3.down * audio.time * GameManager.SPEED;

        // 입력 처리
        HandleInputs();

        // 지나간 노트 지우기
        if (GetLastNoteIndex() != -1 && noteInstances[GetLastNoteIndex()] != null)
        {
            TryHit(GetLastNoteIndex(), 0, true);
            DestroyNote(GetLastNoteIndex());
        }
    }

    void HandleInputs()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            TryHit(GetCurrentNoteIndex(), 1, true);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            TryHit(GetCurrentNoteIndex(), 2, true);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            TryHit(GetCurrentNoteIndex(), 3, true);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            TryHit(GetCurrentNoteIndex(), 4, true);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            TryHit(GetCurrentNoteIndex(), 1, false);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            TryHit(GetCurrentNoteIndex(), 2, false);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            TryHit(GetCurrentNoteIndex(), 3, false);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            TryHit(GetCurrentNoteIndex(), 4, false);
        }
    }

    int GetCurrentNoteIndex() // 있는 노트의 인덱스를 찾아야 할 때
    {
        return Mathf.Clamp(Mathf.RoundToInt((audio.time - offset) * bpm / 60), 0, score.Length - 1);
    }

    int GetLastNoteIndex() // 지나간 노트의 인덱스를 찾아야 할 때, -1 있을 수 있음 주의!!!
    {
        return Mathf.Clamp(Mathf.RoundToInt((audio.time - offset) * bpm / 60) - 1, -1, score.Length - 1);
    }

    GameObject CurrentNote()
    {
        return noteInstances[GetCurrentNoteIndex()];
    }

    float GetCurrentNotesPosition()
    {
        return GameManager.SPEED * (60 / bpm * GetCurrentNoteIndex() + offset);
    }

    void TryHit(int scoreIndex, int noteIndex, bool isGround)
    {
        Note currentNode = noteInstances[scoreIndex].GetComponent<Note>();

        if (currentNode.IsCorrectKey(noteIndex))
        {
            // 판정 관련 내용들
            currentNode.HitNote(noteIndex);

            float positionDifference = Mathf.Abs(GetCurrentNotesPosition() - (-notes.transform.localPosition.y));
            bool early = GetCurrentNotesPosition() > (-notes.transform.localPosition.y);

            if (positionDifference < 0.25f * RADIUS)
            {
                if (isGround)
                    GameManager.Instance.HitNoteWithGround(currentNode, GameManager.Judge.Perfect);
                else
                    GameManager.Instance.HitNoteWithAir(currentNode, GameManager.Judge.Perfect);
            }
            else if (positionDifference < 0.8f * RADIUS)
            {
                if (isGround)
                    GameManager.Instance.HitNoteWithGround(currentNode, GameManager.Judge.Good);
                else
                    GameManager.Instance.HitNoteWithAir(currentNode, GameManager.Judge.Good);
            }
            else
            {
                if (isGround)
                    GameManager.Instance.HitNoteWithGround(currentNode, GameManager.Judge.Miss);
                else
                    GameManager.Instance.HitNoteWithAir(currentNode, GameManager.Judge.Miss);
            }

            if (!currentNode.isMultiNode || currentNode.IsBothPressed())
                DestroyNote(scoreIndex);
        }
    }

    void DestroyNote(int index)
    {
        Destroy(noteInstances[index]);
        noteInstances[index] = null;
    }
}
