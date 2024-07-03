using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : MonoBehaviour
{

    public GameObject bullet;
    public Transform firepos;
    public HandCtrl boolng;
    public AudioSource source;
    public AudioClip bang;
    private float timer;
    public Animation Animation;
    public ParticleSystem muzle;
    public int BulletCount = 0;
    bool DontShot = false;
    void Start()
    {
        boolng = this.gameObject.GetComponent<HandCtrl>();
        timer = Time.time;
        muzle.Stop();
        
    }


    void Update()
    {
        if (Input.GetMouseButton(0) )
        {
            if (Time.time - timer > 0.2f)
            {
                if (boolng.is_runing == false && !DontShot)
                {
                    timer = Time.time;
                    fire();
                    
                    ++BulletCount;
                    muzle.Play();
                    // 원하는 시간간격 만큼 메서드를 호출
                    //          메서드 명    시간
                    Invoke("MuzzflashDisable", 0.2f);

                    if (BulletCount == 10)
                    {
                        // 코르틴 함수 시작
                        // 게임중 개발자가 원하는 프레임을 
                        // 만들려고 할때 사용
                        StartCoroutine(Reload());
                        // 아래 Reload() 호출

                    }
                }
                


            }
            
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());

        }
        //if (Input.GetMouseButtonUp(0))
        //{
        //    muzle.Stop();
        //} 
    }
    

    private void fire()
    {
        Instantiate(bullet, firepos.position, firepos.rotation);
        
        source.PlayOneShot(bang);
        Animation.Play("fire");
       
       
    }
    IEnumerator Reload()
    {
        DontShot = true;
        Animation.Play("reloadStart");
        // 0.5초 후에 Reload함수실행
        yield return new WaitForSeconds(1f);
        Animation.Stop("reloadStart");
        Animation.Play("reloadStop");
        BulletCount = 0;
        DontShot = false;
        

    }
    void MuzzflashDisable()
    {
        muzle.Stop();
    }
}
