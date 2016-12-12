using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour {
    public static InputManager Instance;
    public bool jump;
    public bool bind;
    public bool bindup;
    public bool attack;
    public float RightSide;
    int i;
    Vector2 start;
    void Start ()
    {
        if (Instance == null) Instance = this;
        if (Instance != this) Destroy(this);
        DontDestroyOnLoad(this);
        RightSide = Screen.width / 2;
        jump = false;
        bind = false;
        attack = false;
        bindup = false;
        start = new Vector2();
    }
	
	// Update is called once per frame
	void Update () {
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
        MobileInput();
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
        int RightNumber = 0;
        int n = -1;
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.touches[i].position.x > RightSide)
            {
                n = i;
                RightNumber++;
            }
        }
        if (RightNumber != 1)
        {
            if(bind)
            {
                bind = false;
                bindup = true;
            }
            else return;
        }
        else
        {
            if (Input.touches[n].phase == TouchPhase.Began) start = Input.touches[n].position;
            else if (Input.touches[n].phase == TouchPhase.Moved && (start.y - Input.touches[n].position.y) > 0 && !jump)
            {
                bind = true;
                jump = false;
            }
            else if (Input.touches[n].phase == TouchPhase.Ended)
            {
                MobileHandle(start, Input.touches[n].position);
            }
        }
    }
    void MobileHandle(Vector2 start , Vector2 end)
    {
        if(start == end) attack = true;
        else if (Mathf.Abs(start.y - end.y) > Mathf.Abs(start.x - end.x))
        {
            if ((start.y - end.y) > 0 && bind) //下
            {
                bind = false;
                bindup = true;
            }
            else if (((start.y - end.y) < 0) && !bind) //上
            {
                jump = true;
                bind = false;
            }
        }
    }
}
