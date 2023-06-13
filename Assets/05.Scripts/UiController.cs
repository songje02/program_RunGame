using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class UiController : MonoBehaviour //UI ��ũ��Ʈ
{
    public float timeSpped;
    public GameObject endUI;
    public GameObject restartBtn;

    private float timer = 0f;
    private float numtimer = 0f;
    private float scoreInterval = 1f;

    public List<int> RankScore;
    public int score;
    public Text RankText;
    public Text ScoreText;

    private string filePath; // ���� ���

    //Ÿ��Ʋ ȭ��
    public GameObject Title_bg;
    public GameObject Title_text;
    public GameObject Title_startBtn;
    public GameObject Title_exBtn;

    //���� ȭ��
    public GameObject EX_bg;
    public GameObject EX_text;
    public GameObject EX_startBtn;

    void Start()
    {
        filePath = Application.persistentDataPath + "/RankScore.txt"; // ���� ��� ����

        RankScore = new List<int>(); // ����Ʈ �ʱ�ȭ

        LoadScore();

        restartBtn.SetActive(false);
        endUI.gameObject.SetActive(false);

        score = 0;
    }

    public void EndUI() //���ӿ����� �� ����Ǵ� UI�κ�
    {
        endUI.SetActive(true);
        restartBtn.SetActive(true);
        AddScore(score);
        EndRank();
    }

    public void EndRank()
    {
        RankText.text = ""; // ���� �ؽ�Ʈ �ʱ�ȭ

        int rankCount = Mathf.Min(10, RankScore.Count); // RankScore�� ��� ���� 10 �� ���� ���� ���
        for (int i = 0; i < rankCount; i++)
        {
            RankText.text += (i + 1).ToString() + ".      " + RankScore[i].ToString() + "\n";
        }
    }

    public void numScore() //����, ����UI 
    {
        numtimer += Time.deltaTime;
        if (numtimer >= scoreInterval)
        {
            score++;
            numtimer = 0f;
        }
        ScoreText.text = "[SCORE : " + score.ToString() + "]";
    }

    public void GameSart()
    {
        Title_bg.SetActive(false);
        Title_text.SetActive(false);
        Title_startBtn.SetActive(false);
        Title_exBtn.SetActive(false);
        EX_bg.SetActive(false);
        EX_text.SetActive(false);
        EX_startBtn.SetActive(false);
    }

    public void Explanation()
    {
        EX_bg.SetActive(true);
        EX_text.SetActive(true);
        EX_startBtn.SetActive(true);
    }

    //��ŷ ���� 
    public void LoadScore() 
    {
        if (File.Exists(filePath)) // ������ �����ϴ��� Ȯ��
        {
            string scoreString = File.ReadAllText(filePath);
            RankScore = DeserializeList(scoreString);
        }
        else
        {
            RankScore = new List<int>(); // ������ ���� ��� ���ο� ����Ʈ ����
        }
    }

    private void OnDestroy()
    {
        SaveScore();
    }

    public void AddScore(int score)
    {
        if (!RankScore.Contains(score)) // �ߺ��� ������ ���� ��쿡�� �߰�
        {
            RankScore.Add(score); // ����Ʈ�� �� �߰�
            RankScore.Sort((a, b) => b.CompareTo(a)); // ����Ʈ ���� ����

            if (RankScore.Count > 10) // ����Ʈ ũ�⸦ 10���� ����
            {
                RankScore.RemoveAt(RankScore.Count - 1);
            }
        }
    }

    public void SaveScore()
    {
        string scoreString = SerializeList(RankScore);
        File.WriteAllText(filePath, scoreString);
    }

    // ����Ʈ�� ���ڿ��� ����ȭ�ϴ� �Լ�
    private string SerializeList(List<int> list)
    {
        return string.Join(",", list.ToArray());
    }

    // ���ڿ��� ����Ʈ�� ������ȭ�ϴ� �Լ�
    private List<int> DeserializeList(string str)
    {
        List<int> list = new List<int>();
        string[] elements = str.Split(',');
        foreach (string element in elements)
        {
            int value;
            if (int.TryParse(element, out value))
            {
                list.Add(value);
            }
        }
        return list;
    }
}
