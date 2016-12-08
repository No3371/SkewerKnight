using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    public static InputManager Instance;
    public bool jump;
    public bool bind;
    public bool bindup;
    public bool attack;
    int i;
    void Start ()
    {
        if (Instance == null) Instance = this;
        if (Instance != this) Destroy(this);
        DontDestroyOnLoad(this);
        jump = false;
        bind = false;
        attack = false;
        bindup = false;
    }
	
	// Update is called once per frame
	void Update () {
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
        //MobileInput();
#else
        if (Input.GetKeyDown(KeyCode.W) && !bind)
        {
            jump = true;
            bind = false;
        }
        else if (Input.GetKeyDown(KeyCode.S) && !jump)
        {
            bind = true;
            jump = false;
        }
        else if (Input.GetKeyUp(KeyCode.S) && bind)
        {
            bind = false;
            bindup = true;
        }
        if (Input.GetMouseButtonDown(0)) attack = true;
#endif
    }
    void MobileInput()
    {
        Vector2 start = new Vector2();
        int RightNumber = 0;
        int n = -1;
        for (int i = 0; i < Input.touchCount; i++) if (Input.touches[i].position.x > 0) RightNumber++;
        if (RightNumber > 1) return;
        else
        {
            for (int i = 0; i < Input.touchCount; i++) if (Input.touches[i].position.x < 0) n = i;
            if (Input.touches[n].phase == TouchPhase.Began) start = Input.touches[n].position;
            else if (Input.touches[n].phase == TouchPhase.Ended) MobileHandle(start, Input.touches[n].position);
        }
    }
    void MobileHandle(Vector2 start , Vector2 end)
    {
        if(Mathf.Abs(start.y - end.y) > Mathf.Abs(start.x - end.x))
        {
            if(start.y - end.y > 0) //下
            {

            }
        }
    }
}
