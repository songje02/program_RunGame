using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class UiController : MonoBehaviour //UI 스크립트
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

    private string filePath; // 파일 경로

    //타이틀 화면
    public GameObject Title_bg;
    public GameObject Title_text;
    public GameObject Title_startBtn;
    public GameObject Title_exBtn;

    //설명 화면
    public GameObject EX_bg;
    public GameObject EX_text;
    public GameObject EX_startBtn;

    void Start()
    {
        filePath = Application.persistentDataPath + "/RankScore.txt"; // 파일 경로 설정

        RankScore = new List<int>(); // 리스트 초기화

        LoadScore();

        restartBtn.SetActive(false);
        endUI.gameObject.SetActive(false);

        score = 0;
    }

    public void EndUI() //게임오버일 때 실행되는 UI부분
    {
        endUI.SetActive(true);
        restartBtn.SetActive(true);
        AddScore(score);
        EndRank();
    }

    public void EndRank()
    {
        RankText.text = ""; // 기존 텍스트 초기화

        int rankCount = Mathf.Min(10, RankScore.Count); // RankScore의 요소 수와 10 중 작은 값을 사용
        for (int i = 0; i < rankCount; i++)
        {
            RankText.text += (i + 1).ToString() + ".      " + RankScore[i].ToString() + "\n";
        }
    }

    public void numScore() //점수, 점수UI 
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

    //랭킹 점수 
    public void LoadScore() 
    {
        if (File.Exists(filePath)) // 파일이 존재하는지 확인
        {
            string scoreString = File.ReadAllText(filePath);
            RankScore = DeserializeList(scoreString);
        }
        else
        {
            RankScore = new List<int>(); // 파일이 없을 경우 새로운 리스트 생성
        }
    }

    private void OnDestroy()
    {
        SaveScore();
    }

    public void AddScore(int score)
    {
        if (!RankScore.Contains(score)) // 중복된 점수가 없을 경우에만 추가
        {
            RankScore.Add(score); // 리스트에 값 추가
            RankScore.Sort((a, b) => b.CompareTo(a)); // 리스트 역순 정렬

            if (RankScore.Count > 10) // 리스트 크기를 10으로 제한
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

    // 리스트를 문자열로 직렬화하는 함수
    private string SerializeList(List<int> list)
    {
        return string.Join(",", list.ToArray());
    }

    // 문자열을 리스트로 역직렬화하는 함수
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
