using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public enum GameState //게임상태
{
    Start,
    Play,
    End,
    Pause
}

public class GameManager : MonoBehaviour
{
    //이벤트
    public UnityEvent GameOverEvent;
    public UnityEvent PlayEvent;
    
    //실행, 일시정지 버튼
    public GameObject playBtn;
    public GameObject pauseBtn;

    //콜라이더 정보
    public checkCollider playerCol;
    public checkCollider zone;
    public checkCollider groundCol;
    public checkCollider fixObCol;
    public checkCollider moveObCol;

    public GameState gamestate;

    //장애물 생성 타이머
    private float ObstacleInterval = 1.3f;
    private float Obstacletimer = 0f;

    public GameObject restartBtn;

    //스테이지로더
    public TextAsset data;
    private AllData datas;

    //장애물 
    public GameObject FixObstacle;
    public GameObject MoveObstacle;

    //오브젝트 풀
    public int poolsize;
    public List<GameObject> ObstaclePool;
    public List<checkCollider> poolCol;

    //타이틀 화면
    public GameObject Title_bg;
    public GameObject Title_text;
    public GameObject Title_startBtn;
    public GameObject Title_exBtn;


    //스테이지로더 데이터
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
        gamestate = GameState.Start;

        //스테이지로더
        datas = JsonUtility.FromJson<AllData>(data.text);
        poolsize = datas.map_Stage.Length;
        ObstaclePool = new List<GameObject>();

        //스테이지로더로 생성하고 List에 add하여 오브젝트 풀 사용
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
            case GameState.Start:
                break;
            case GameState.Play:
                PlayEvent.Invoke();
                break;
            case GameState.End:
                GameOverEvent.Invoke();
                break;
            case GameState.Pause:
                break;
        }
    }

    public void GameSart()
    {
        gamestate = GameState.Play;
        playGame(); //재생 버튼 클릭시
    }

    public void zoneCheck() //화면밖에 나가면 다시 List에 들어가도록
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

    public void objCreate() //시간주기마다 오브젝트 풀에 있는 장애물 보이도록 
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

    public void objcolCheck() //게임오버 체크
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
    public void GameOver() //게임오버 체크
    {
        Time.timeScale = 0;
        pauseBtn.SetActive(false);
        playBtn.SetActive(false);
    }

    public void reStart() //다시 시작시 
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void pauseGame() //게임 멈춤 버튼
    {
        gamestate = GameState.Pause;
        Time.timeScale = 0;
        pauseBtn.SetActive(false);
        playBtn.SetActive(true);
    }

    public void playGame() //게임 플레이 버튼
    {
        gamestate = GameState.Play;
        Time.timeScale = 1;
        playBtn.SetActive(false);
        pauseBtn.SetActive(true);
    }
}
