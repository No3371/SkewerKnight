using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spear : MonoBehaviour {
	public bool Attacking = false;
	public float BaseMouseY;
	public GameObject Character;
    Animator animator;
    AudioSource Sound;

    public int Count; //0~5

    List<AudioClip> SoundList = new List<AudioClip>();

    float LastAttackTime;

    public bool Lock = false;
	// Use this for initialization
	void Start () {
        BaseMouseY = Screen.height / 2;
        animator = GetComponent<Animator>();

        SoundList.AddRange(Resources.LoadAll<AudioClip>("Sounds"));
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - LastAttackTime > 0.5f) Attacking = false;
        if (!Lock)
        {
            UpdateAngle();
            if (Input.GetMouseButtonDown(0))
            {
                Attacking = true;
                animator.SetTrigger("Push");
                LastAttackTime = Time.time;
            }

        }
	}

    void OnTriggerEnter2D (Collider2D other)
    {
        if(other.gameObject.layer == 9)
        {
            //other.GetComponent<Mob>().isCaught = true;
            //other.GetComponent<Mob>().Spear = this;
            
        }
    }

	public void ToggleLock() {
        Lock = !Lock;
        GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
	}

	void UpdateAngle()
    {
        float angle = (Input.mousePosition.y - BaseMouseY) / 10;
        if (angle > 0) angle = (Input.mousePosition.y - BaseMouseY) / 5;
        if (angle < 0) angle += 360f;
        if (angle > 90 && angle < 180) angle = 90f;
        else if (angle > 180 && angle < 340) angle = 340f;
        this.transform.localEulerAngles = new Vector3(0, 0, angle);
        //Debug.Log(Input.mousePosition.y + "/" + angle);

    }
}
