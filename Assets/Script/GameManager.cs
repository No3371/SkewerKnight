using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public GameObject MainCamera;

    public int Score;
    float ScrollSpeed;
    float Difficulty;

    float GameStartTime;


	// Use this for initialization
	void Start () {
        if (Instance == null) Instance = this;
        if (Instance != this) Destroy(this);
        DontDestroyOnLoad(this);

        GameStartTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
