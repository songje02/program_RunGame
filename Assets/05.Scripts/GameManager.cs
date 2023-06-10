using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public enum GameState //게임상태
{
    Play,
    End,
    Pause
}

public class GameManager : MonoBehaviour
{


    //이벤트
    public UnityEvent UIEvent;
    public UnityEvent GameOverEvent;
    public UnityEvent PlayEvent;
    public UnityEvent PauseEvent;

    public GameObject playBtn;
    public GameObject pauseBtn;

    public checkCollider playerCol;
    public checkCollider zone;
    public checkCollider groundCol;
    public checkCollider fixObCol;
    public checkCollider moveObCol;

    public GameState gamestate;


    private float timer = 0f;
    private float scoreInterval = 1f;
    private float ObstacleInterval = 2f;
    private float Obstacletimer = 0f;

    public GameObject restartBtn;

    //스테이지로더
    public TextAsset data;
    private AllData datas;

    public GameObject FixObstacle;
    public GameObject MoveObstacle;

    public int poolsize;

    public List<GameObject> ObstaclePool;
    public List<checkCollider> poolCol;


    [System.Serializable]
    public class AllData
    {
        public mapData[] map_Stage;
    }

    [System.Serializable]
    public class mapData
    {
        public string objectname;
        public int x;
        public int y;
    }
    void Start()
    {
        
        playGame();

        gamestate = GameState.Play;
        //스테이지로더
        datas = JsonUtility.FromJson<AllData>(data.text);
        poolsize = datas.map_Stage.Length;
        ObstaclePool = new List<GameObject>();


        for (int i = 0; i < poolsize; i++)
        {
            GameObject fixobstacle;

            switch (datas.map_Stage[i].objectname)
            {
                case "FixObstacle":
                    fixobstacle = Instantiate(FixObstacle, new Vector3(9.5f, -2.7f, -2), Quaternion.identity);
                    ObstaclePool.Add(fixobstacle);
                    fixobstacle.SetActive(false);
                    break;
                case "MoveObstacle":
                    fixobstacle = Instantiate(MoveObstacle, new Vector3(9.5f, -2.7f, -2), Quaternion.identity);
                    ObstaclePool.Add(fixobstacle);
                    fixobstacle.SetActive(false);
                    break;
            }
            poolCol.Add(ObstaclePool[i].gameObject.GetComponent<checkCollider>());
        }
    }
    
    void Update()
    {
        switch (gamestate)
        {
            case GameState.Play:
                UIEvent.Invoke();
                PlayEvent.Invoke();
                break;
            case GameState.End:
                GameOverEvent.Invoke();
                pauseGame();
                break;
            case GameState.Pause:
                PauseEvent.Invoke();
                break;
        }
    }

    public void zoneCheck()
    {
        for (int i = 0; i < poolsize; i++)
        {
            if (poolCol[i].CheckCollision(zone))
            {
                poolCol[i].gameObject.transform.position = new Vector3(9.5f, -2.7f, -3);
                poolCol[i].gameObject.SetActive(false);
                ObstaclePool.Add(poolCol[i].gameObject);
            }
        }
    }

    public void objCreate()
    {
        if (ObstaclePool.Count > 0)
        {
            Obstacletimer += Time.deltaTime;
            if (Obstacletimer >= ObstacleInterval)
            {
                GameObject obstacle = ObstaclePool[0];
                obstacle.SetActive(true);
                ObstaclePool.Remove(obstacle);
                obstacle.transform.position = new Vector3(9.5f, -2.7f, -2);
                Obstacletimer = 0;
            }
        }
    }

    public void GameOver()
    {
        for (int i = 0; i < poolsize; i++)
        {
            //충돌
            if (poolCol[i].CheckCollision(playerCol))
            {
                gamestate = GameState.End;
            }
        }
    }

    public void reStart()
    {
        SceneManager.LoadScene("SampleScene");
    }
        
    public void pauseGame() 
    {
        gamestate = GameState.Pause;
        Time.timeScale = 0;
        pauseBtn.SetActive(false);
        playBtn.SetActive(true);
    }

    public void playGame() 
    {
        gamestate = GameState.Play;
        Time.timeScale = 1;
        playBtn.SetActive(false);
        pauseBtn.SetActive(true);
    }
}
