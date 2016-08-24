using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public GameObject MainCamera;

    public AchievementsData achievementData;

    public int Score;
    public float ScrollSpeed, BaseSpeed = 4.0f; //Character moving speed, based on difficulty
    float Difficulty, DifficultyModifer = 1.0f; //Difficulty scale based on Score and Game time

    float GameStartTime;

    string Record;


	// Use this for initialization
	void Start () {
        if (Instance == null) Instance = this;
        if (Instance != this) Destroy(this);
        DontDestroyOnLoad(this);

        GameStartTime = Time.time;

        Achieved = new List<int>();
	}
	
	// Update is called once per frame
	void Update () {
        ScrollSpeed = UpdateSpeed();
        Difficulty = UpdateDifficulty();
	}

    float UpdateSpeed() //Update scorl speed accoring to current Difficulty.
    {
        return BaseSpeed + (Difficulty * Difficulty) * DifficultyModifer;
    }

    float UpdateDifficulty()
    {
        return (Score / 250) * 0.5f + (Time.time - GameStartTime) / 10 * 0.5f;
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
    List<int[]> achievementList;

    void CheckAchievement(string str) //Take the first 5 chracter in the record string and verify with Achievement Database
    {
        int[] Count = { 0, 0, 0, 0, 0, 0, 0 };
        while (str.Length > 0)
        {
            Count[int.Parse(str.Substring(0, 1))] += 1;
        }

        for(int i = 0; i < achievementList.Count; i++)
        {
            if (Count == achievementList[i])
            {
                Debug.Log("Achieved: " + achievementData.List[i].Name);
                Achieved.Add(i);
            }
        }
    }

    void GenerateAchievementList()
    {
        foreach (AchievementsData.Achievement a in achievementData.List)
        {
            int[] count = { 0, 0, 0, 0, 0, 0, 0 };
            
            for (int i = 0; i < a.Code.Length; i++)
            {
                count[int.Parse(a.Code.Substring(i, i + 1))] += 1;
            }

            achievementList.Add(count);
        }


    }

    void GameOver()
    {

    }
}
