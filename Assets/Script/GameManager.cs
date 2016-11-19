using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public GameObject MainCamera, Character;
    public GameObject GameOverScreen;
    public GameObject GameStartScreen;
    public GameObject ScoringBoard;
    public GameObject SettingScreen;
    public GameObject Spear;
    public GameObject AchieveScreen;
    public GameObject Text;
    public AchievementsData achievementData;
    public int BustScore;
    public int Score = 0, ScoreforDiff = 0;
    public float ScrollSpeed, BaseSpeed = 4f; //Character moving speed, based on difficulty
    float Difficulty; //Difficulty scale based on Score and Game time
    public float DifficultyModifer = 0.74f;
    float GameStartTime;
    public bool Busted = false;
    string Record;
    bool testcheck = false;
    Coroutine scoring;
    public bool IsPlayed = false;
    public AudioSource Menu, ButtonDo, ButtonUp ,Giggle;
    AudioSource[] Audiolist;
    // Use this for initialization
    void Start() {
        if (Instance == null) Instance = this;
        if (Instance != this) Destroy(this);
        DontDestroyOnLoad(this);
        GameStartScreen.SetActive(true);
        Achieved = new List<int>();
        Audiolist = GetComponents<AudioSource>();
        Menu = Audiolist[0];
        ButtonDo = Audiolist[1];
        ButtonUp = Audiolist[2];
        Giggle = Audiolist[3];
    }

    // Update is called once per frame
    void Update() {
        if (IsPlayed)
        {
            if (!Busted) ScrollSpeed = UpdateSpeed();
            else ScrollSpeed = 20f;
            Difficulty = UpdateDifficulty();
        }
    }

    float UpdateSpeed() //Update scorl speed accoring to current Difficulty.
    {
        return BaseSpeed + (Difficulty * Difficulty * DifficultyModifer * DifficultyModifer);
    }

    float UpdateDifficulty()
    {
        return (ScoreforDiff / 3000) + (Time.time - GameStartTime) / 50;
    }

    void RecordAdd(int i) //Add new caught mob id to the record string, if record is longer then 5, CheckAchievemrnt().
    {
        Record += i.ToString();
        if (Record.Length >= 5)
        {
            Record.Remove(0, 5);
            CheckAchievement(Record.Substring(0, 5));
        }
    }

    List<int> Achieved;

    void CheckAchievement(string str) //Take the first 5 chracter in the record string and verify with Achievement Database
    {
        int[] Count = { 0, 0, 0, 0, 0, 0, 0 };
        while (str.Length > 0)
        {
            Count[int.Parse(str.Substring(0, 1))] += 1;
        }
        foreach (AchievementsData.Achievement a in achievementData.List)
        {
            int AId = 0;
            int[] count = { 0, 0, 0, 0, 0, 0, 0 };
            while (str.Length > 0)
            {
                Count[int.Parse(str.Substring(0, 1))] += 1;
            }

            if (count == Count)
            {
                Debug.Log("Achieved: " + a.Name);
                Achieved.Add(AId);
            }

            AId++;
        }
    }

    IEnumerator ScoringByTime()
    {
        while (true)
        {
            Score += (int)((Time.time - GameStartTime) * (Time.time - GameStartTime) / 30);
            ScoreforDiff += (int)((Time.time - GameStartTime) * (Time.time - GameStartTime) / 30);
            if (Score >= BustScore && !testcheck)
            {
                testcheck = true;
                Busted = true;
                ScrollSpeed = 20f;
                BustScore += BustScore;
                StartCoroutine(Character.GetComponent<Character>().Bust());
            }
            yield return new WaitForSeconds(1f);
        }
    }
    public void GameOver()
    {
        IsPlayed = false;
        Debug.Log("GAME OVER.");
        StopCoroutine(scoring);
        ScoringBoard.SetActive(false);
        GameOverScreen.SetActive(true);
        MainCamera.GetComponent<AudioSource>().Stop();
        Cursor.visible = true;
    }

    public void GameStart()
    {
        GameStartTime = Time.time;
        Menu.Stop();
        Spear.GetComponent<Animator>().Play("Enter");
        scoring = StartCoroutine(ScoringByTime());
        GameStartScreen.SetActive(false);
        ScoringBoard.SetActive(true);
        Cursor.visible = false;
        MainCamera.GetComponent<AudioSource>().Play();
        IsPlayed = true;
    }
    public void BacktoMenu()
    {
        Menu.Play();
        GameOverScreen.SetActive(false);
        GameStartScreen.SetActive(true);
        GameStartScreen.transform.GetChild(2).gameObject.SetActive(true);
        GameStartScreen.transform.GetChild(3).gameObject.SetActive(false);
    }
    public void Restart()
    {
        Menu.Stop();
        Spear.GetComponent<Animator>().Play("Enter");
        scoring = StartCoroutine(ScoringByTime());
        Cursor.visible = false;
        Score = 0;
        ScoreforDiff = 0;
        GameStartTime = Time.time;
        GameOverScreen.SetActive(false);
        GameStartScreen.SetActive(false);
        ScoringBoard.SetActive(true);
        for (int i = 5; i < Spear.transform.childCount; i++) Destroy(Spear.transform.GetChild(i).gameObject);
        Spear.GetComponent<Spear>().Caught.Clear();
        Spear.GetComponent<Spear>().Count = 0;
        MainCamera.GetComponent<AudioSource>().Play();
        IsPlayed = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Setting()
    {
        SettingScreen.SetActive(true);
    }

    public void SettingExit()
    {
        SettingScreen.SetActive(false);
    }

    public void Achieve()
    {
        Text.GetComponent<Text>().text = "";
        foreach (AchievementsData.Achievement a in achievementData.List)
        {
            if (PlayerPrefs.HasKey(a.Code))
            {
                Text.GetComponent<Text>().text += (a.Name + "\n");
                Debug.Log("add!");
            }
        }
        AchieveScreen.SetActive(true);
    }

    public void AchieveExit()
    {
        AchieveScreen.SetActive(false);
    }
    
    public void ButtonU()
    {
        ButtonUp.Play();
    }
}
