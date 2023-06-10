using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public float timeSpped;
    public GameObject endUI;
    public GameObject restartBtn;

    public bg_Move Ground;
    public bg_Move Back;
    public bg_Move Sky;

    private float timer = 0f;
    private float numtimer = 0f;
    private float scoreInterval = 1f;

    public List<int> RankScore;
    public int score = 0;
    public Text RankText;
    public Text ScoreText;
    void Start()
    {
        //랭킹 추가
        LoadScore();
        string scoreString = PlayerPrefs.GetString("RankScore");
        RankScore = DeserializeList(scoreString);
        RankScore.Sort((a, b) => b.CompareTo(a));

        restartBtn.SetActive(false);
        endUI.gameObject.SetActive(false);
    }

    void Update()
    {
        Ground.scrollspeed -= timeSpped;
        Back.scrollspeed -= timeSpped;
        Sky.scrollspeed -= timeSpped;
    }

    public void EndUI()
    {
        endUI.SetActive(true);
        restartBtn.SetActive(true);
        AddScore(score);
        EndRank();
    }
    public void EndRank()
    {
        for(int i = 0; i < 10; i++)
        {
            RankText.text += (i+1).ToString() + ".      " + RankScore[i].ToString() + "\n";
        }
    }

    public void numScore()
    {
        numtimer += Time.deltaTime;
        if (numtimer >= scoreInterval)
        {
            score++;
            numtimer = 0f;
        }
        ScoreText.text = "[SCORE : " + score.ToString() + "]";
    }

    public void LoadScore()
    {
        int saveScore = PlayerPrefs.GetInt("socre", 0);
        score = saveScore;
    }

    private void OnDestroy()
    {
        // PlayerPrefs에 리스트 값 저장
        string scoreString = SerializeList(RankScore);
        PlayerPrefs.SetString("RankScore", scoreString);
        PlayerPrefs.Save();
    }

    public void AddScore(int score)
    {
        RankScore.Add(score); // 리스트에 값 추가
        RankScore.Sort((a, b) => b.CompareTo(a)); // 리스트 역순 정렬
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

    public void speedUP()
    {
        timer += Time.deltaTime;
        if (timer >= scoreInterval)
        {
            timeSpped += 0.00003f;
            timer = 0f;
        }
    }
}
