using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class NotePrefabInfo
{
    public string name;
    public GameObject prefab;
}

public class MusicGame : MonoBehaviour
{
    private const float RADIUS = 0.5f;

    private AudioSource audio;
    public GameObject notes;
    
    public GameObject[] noteInstances;
    public GameObject[] judges;

    public GameObject[] judgeEffect;
    public NotePrefabInfo[] notePrefabs;
    private Dictionary<string, GameObject> notePrefabDictionary = new Dictionary<string, GameObject>();

    public TextAsset scoreFile;
    private string[] lines;

    public float offset;
    public float bpm;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    void Start()
    {
        audio.Play();

        foreach (var pair in notePrefabs)
        {
            notePrefabDictionary.Add(pair.name, pair.prefab);
        }

        string text = scoreFile.text.Replace("\r", "");
        lines = text.Split("\n");

        noteInstances = new GameObject[lines.Length];

        for (int i = 0; i < lines.Length; i++)
        {
            if (!notePrefabDictionary.ContainsKey(lines[i]))
                continue;

            GameObject prefab = notePrefabDictionary[lines[i]];

            if (!prefab.GetComponent<Note>().isMultiNode)
            {
                int noteIndex = prefab.GetComponent<Note>().position - 1;
                GameObject noteInstance = Instantiate(prefab, notes.transform);
                noteInstance.transform.localPosition = Vector3.right * judges[noteIndex].transform.localPosition.x + Vector3.up * GameManager.SPEED * (60 / bpm * i + offset);

                noteInstances[i] = noteInstance;
            }
            else
            {
                int noteIndex = prefab.GetComponent<Note>().position - 1;
                GameObject noteInstance = Instantiate(prefab, notes.transform);
                noteInstance.transform.localPosition = Vector3.up * GameManager.SPEED * (60 / bpm * i + offset);

                noteInstances[i] = noteInstance;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        audio.pitch = GameManager.Instance.itemTimerTimer > 0 ? 0.5f : 1;

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
        return Mathf.Clamp(Mathf.RoundToInt((audio.time - offset) * bpm / 60), 0, lines.Length - 1);
    }

    int GetLastNoteIndex() // 지나간 노트의 인덱스를 찾아야 할 때, -1 있을 수 있음 주의!!!
    {
        return Mathf.Clamp(Mathf.RoundToInt((audio.time - offset) * bpm / 60) - 1, -1, lines.Length - 1);
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
        if (scoreIndex < 0 || scoreIndex >= noteInstances.Length || noteInstances[scoreIndex] == null)
            return;

        Note currentNode = noteInstances[scoreIndex].GetComponent<Note>();

        if (currentNode.IsCorrectKey(noteIndex))
        {
            // 판정 관련 내용들
            currentNode.HitNote(noteIndex);

            if (currentNode.item == Note.Item.None)
            {
                float positionDifference = Mathf.Abs(GetCurrentNotesPosition() - (-notes.transform.localPosition.y));
                bool early = GetCurrentNotesPosition() > (-notes.transform.localPosition.y);

                if (positionDifference < 0.25f * RADIUS)
                {
                    TryJudge(scoreIndex, isGround, GameManager.Judge.Perfect);
                }
                else if (positionDifference < 0.8f * RADIUS)
                {
                    TryJudge(scoreIndex, isGround, GameManager.Judge.Good);
                }
                else
                {
                    TryJudge(scoreIndex, isGround, GameManager.Judge.Miss);
                }
            }

            if (!currentNode.isMultiNode || currentNode.IsBothPressed())
                DestroyNote(scoreIndex);
        }
        else if (noteIndex == 0)
        {
            if (currentNode.item == Note.Item.None)
            {
                TryJudge(scoreIndex, isGround, GameManager.Judge.Miss);
            }
        }
    }

    void TryJudge(int scoreIndex, bool isGround, GameManager.Judge judge)
    {
        if (judge == GameManager.Judge.Miss && GameManager.Instance.itemMissDefenseCount > 0)
        {
            GameManager.Instance.itemMissDefenseCount -= 1;
            TryJudge(scoreIndex, isGround, GameManager.Judge.Good);
            return;
        }

        if (judge == GameManager.Judge.Good && GameManager.Instance.itemPerfectTimer > 0)
        {
            TryJudge(scoreIndex, isGround, GameManager.Judge.Perfect);
            return;
        }

        if (judge == GameManager.Judge.Perfect)
        {
            GameManager.Instance.judgePerfectCount += 1;
            GameManager.Instance.noteCount += 1;
        }
        else if (judge == GameManager.Judge.Good)
        {
            GameManager.Instance.judgeGoodCount += 1;
            GameManager.Instance.noteCount += 1;
        }
        else if (judge == GameManager.Judge.Miss)
        {
            GameManager.Instance.judgeMissCount += 1;
            GameManager.Instance.noteCount+= 1;
        }

        Note currentNode = noteInstances[scoreIndex].GetComponent<Note>();
        float positionDifference = Mathf.Abs(GetCurrentNotesPosition() - (-notes.transform.localPosition.y));
        bool early = GetCurrentNotesPosition() > (-notes.transform.localPosition.y);

        if (judge == GameManager.Judge.Perfect)
        {
            GameObject effectInstance = Instantiate(judgeEffect[0], transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            effectInstance.GetComponent<JudgeEffect>().SetText(early, Mathf.RoundToInt(positionDifference / RADIUS * 100));
        }
        else if (judge == GameManager.Judge.Good)
        {
            GameObject effectInstance = Instantiate(judgeEffect[1], transform.position + new Vector3(0, 1, 0), Quaternion.identity);
            effectInstance.GetComponent<JudgeEffect>().SetText(early, Mathf.RoundToInt(positionDifference / RADIUS * 100));
        }
        else if (judge == GameManager.Judge.Miss)
            Instantiate(judgeEffect[2], transform.position + new Vector3(0, 1, 0), Quaternion.identity);

        if (isGround)
            GameManager.Instance.HitNoteWithGround(currentNode, judge);
        else
            GameManager.Instance.HitNoteWithAir(currentNode, judge);
    }

    void DestroyNote(int index)
    {
        Destroy(noteInstances[index]);
        noteInstances[index] = null;
    }
}
