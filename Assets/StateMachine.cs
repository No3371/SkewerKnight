using UnityEngine;
using System.Collections;

public class StateMachine : MonoBehaviour {

    public static StateMachine Instance;

    enum GameState
    {
        Starting,
        Menu,
        Menu_About,
        Menu_Collections,
        StartGame,
        Normal,
        GameOver
    }

    GameState gameState;

	// Use this for initialization
	void Start () {
        if (Instance == null) Instance = this;
        if (Instance != this) Destroy(this);
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void Starting()
    {
        
    }
}
