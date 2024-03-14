using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Bird[] birdPrefabs;
    // thoi gian tao chim
    public float spawnTime;
    public int timeLimit;

    public int m_curTimeLimit { get; set; }
    public int BirdKilled { get; set; }
    public bool m_isGameover { get; set; }
    // Start is called before the first frame update
    public override void Awake()
    {
        MakeSingleton(false);
        m_curTimeLimit = timeLimit;
        
    }
    public override void Start()
    {
        BirdKilled = 0;
        GameGUIManager.Ins.ShowGameGui(false);
        GameGUIManager.Ins.UpdateKilledCounting(m_curTimeLimit);
    }

    public void PlayGame()
    {
        StartCoroutine(GameSpawn());
        StartCoroutine(TimeCountDown());
        GameGUIManager.Ins.ShowGameGui(true);
    }

    IEnumerator TimeCountDown()
    {
        while (m_curTimeLimit > 0)
        {
            yield return new WaitForSeconds(1f);
            m_curTimeLimit--;
            if (m_curTimeLimit <= 0)
            {
                m_isGameover = true;
                Bird[] birdPrefabs = GameObject.FindObjectsOfType<Bird>();

                // Duyệt qua từng game object và làm điều gì đó với chúng
                foreach (Bird bird in birdPrefabs)
                {
                    bird.Die();
                }
                //GameGUIManager.Ins.gameDialog.UpdateDialog("YOUR BEST", "BEST KILLED : x"+ BirdKilled);

                if (BirdKilled > Prefs.bestScore)
                {
                    GameGUIManager.Ins.gameDialog.UpdateDialog("NEW BEST", "BEST KILLED : x" + BirdKilled);
                }
                else if((BirdKilled <= Prefs.bestScore))
                {
                    GameGUIManager.Ins.gameDialog.UpdateDialog("YOUR BEST", "BEST KILLED : x" + Prefs.bestScore);

                }
                Prefs.bestScore = BirdKilled;
                Debug.Log(Prefs.bestScore);
                GameGUIManager.Ins.gameDialog.Show(true);
                GameGUIManager.Ins.m_curDialog = GameGUIManager.Ins.gameDialog;

                
            }
            GameGUIManager.Ins.UpdateTimer(IntToTime(m_curTimeLimit));

        }
    }
    IEnumerator GameSpawn()
    {
        while (!m_isGameover)
        {
            SpawnBird();
            yield return new WaitForSeconds(spawnTime);
        }
    }

    void SpawnBird()
    {
        Vector3 spawnPos = Vector3.zero;
        float randCheck = Random.Range(0f, 1f);
        if(randCheck > 0.5f)
        {
            spawnPos = new Vector3(12, Random.Range(1.5f, 4f), 0);
        }
        else
        {
            spawnPos = new Vector3(-12, Random.Range(1.5f, 4f), 0);

        }

        if(birdPrefabs != null)
        {
            int randIdx = Random.Range(0, birdPrefabs.Length);
            if (birdPrefabs[randIdx] != null)
            {
                Bird birdClone = Instantiate(birdPrefabs[randIdx], spawnPos, Quaternion.identity);
            }
        }
    }
    string IntToTime(int time)
    {
        float minute = Mathf.Floor(time / 60);
        float seconds = Mathf.RoundToInt(time % 60);
        return minute.ToString("00") + " : " + seconds.ToString("00");
    }
}
