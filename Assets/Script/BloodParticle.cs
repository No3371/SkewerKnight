using UnityEngine;
using System.Collections;

public class BloodParticle : MonoBehaviour
{
    // Use this for initialization
    ParticleSystem ps;
    Quaternion iniRot;
    bool check;
    float preSpearz;
    public float angle;
    void Awake()
    {
        iniRot = transform.rotation;
        ps = GetComponent<ParticleSystem>();
        check = false;
    }
    void Start()
    {
        var em = ps.emission;
        em.rate = 1;
        var sh = ps.shape;
        sh.angle = 50;
        ps.startLifetime = 0.28f;
        ps.startSize = 0.15f;
    }

    // Update is called once per frame
    void Update()
    {
        angle = 0.2f;
        var em = ps.emission;
        Vector3 v = new Vector3();
        float spearz;
        //if (enabled == true && !check)
        //{
        //    check = !check;
        //    transform.rotation = iniRot;
        //    StartCoroutine(PS());
        //}
        //spearz = GameManager.Instance.Spear.transform.localEulerAngles.z;
        //if (spearz > 90) spearz -= 360;
        ////else if (spearz < 90) spearz -= 360;
        //if ((preSpearz - spearz) != 0)
        //{
        //    ps.startLifetime = 0.5f;
        //    v = transform.rotation.eulerAngles;
        //    v.x -= (preSpearz - spearz) *angle;
        //    Debug.Log((preSpearz - spearz) * angle);
        //    transform.rotation = Quaternion.Euler(v);
        //    em.rate = em.rate.constant * 1.2f;
        //    //ps.startSpeed = 5;
        //}
        //else
        //{
        //    em.rate = em.rate.constant;
        //    transform.rotation = Quaternion.Euler(20, 270, 0);
        //}
        ////ps.startSpeed = 2;
        ////Debug.Log();
        //preSpearz = spearz;
    }

    IEnumerator PS()
    {
        float rate = 1;
        var em = ps.emission;
        for(int i = 0;i < 10; i++)
        {
            for (float j = 0; j < 0.2; j += Time.deltaTime)
            {
                yield return 0;
            }
            rate += 1.5f;
            em.rate = rate;
        }
        while (true)
        {
            for (float i = 0; i < 2; i += Time.deltaTime)
            {
                yield return 0;
            }
            em.rate = ++rate;
        }
    }
}
