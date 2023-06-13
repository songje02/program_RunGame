using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public enum GameState //���ӻ���
{
    Start,
    Play,
    End,
    Pause
}

public class GameManager : MonoBehaviour
{
    //�̺�Ʈ
    public UnityEvent GameOverEvent;
    public UnityEvent PlayEvent;
    
    //����, �Ͻ����� ��ư
    public GameObject playBtn;
    public GameObject pauseBtn;

    //�ݶ��̴� ����
    public checkCollider playerCol;
    public checkCollider zone;
    public checkCollider groundCol;
    public checkCollider fixObCol;
    public checkCollider moveObCol;

    public GameState gamestate;

    //��ֹ� ���� Ÿ�̸�
    private float ObstacleInterval = 1.3f;
    private float Obstacletimer = 0f;

    public GameObject restartBtn;

    //���������δ�
    public TextAsset data;
    private AllData datas;

    //��ֹ� 
    public GameObject FixObstacle;
    public GameObject MoveObstacle;

    //������Ʈ Ǯ
    public int poolsize;
    public List<GameObject> ObstaclePool;
    public List<checkCollider> poolCol;

    //Ÿ��Ʋ ȭ��
    public GameObject Title_bg;
    public GameObject Title_text;
    public GameObject Title_startBtn;
    public GameObject Title_exBtn;


    //���������δ� ������
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

        //���������δ�
        datas = JsonUtility.FromJson<AllData>(data.text);
        poolsize = datas.map_Stage.Length;
        ObstaclePool = new List<GameObject>();

        //���������δ��� �����ϰ� List�� add�Ͽ� ������Ʈ Ǯ ���
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
        playGame(); //��� ��ư Ŭ����
    }

    public void zoneCheck() //ȭ��ۿ� ������ �ٽ� List�� ������
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

    public void objCreate() //�ð��ֱ⸶�� ������Ʈ Ǯ�� �ִ� ��ֹ� ���̵��� 
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

    public void objcolCheck() //���ӿ��� üũ
    {
        for (int i = 0; i < poolsize; i++)
        {
            //�浹
            if (poolCol[i].CheckCollision(playerCol))
            {
                gamestate = GameState.End;
            }
        }
    }
    public void GameOver() //���ӿ��� üũ
    {
        Time.timeScale = 0;
        pauseBtn.SetActive(false);
        playBtn.SetActive(false);
    }

    public void reStart() //�ٽ� ���۽� 
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void pauseGame() //���� ���� ��ư
    {
        gamestate = GameState.Pause;
        Time.timeScale = 0;
        pauseBtn.SetActive(false);
        playBtn.SetActive(true);
    }

    public void playGame() //���� �÷��� ��ư
    {
        gamestate = GameState.Play;
        Time.timeScale = 1;
        playBtn.SetActive(false);
        pauseBtn.SetActive(true);
    }
}
