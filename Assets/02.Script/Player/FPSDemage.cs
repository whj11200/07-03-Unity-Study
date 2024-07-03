using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;


public class FPSDemage : MonoBehaviour
{
    
    
    public Image HpBar;
    public Text HpTxt;
    public int Hp = 0;
    public int MaxHp = 100;
    public string attackTag = "Attack";
    public string zombieattacktag = "Zattack";
    public string Mosterattacktag = "Mattack";
    public bool isPlayerDie = false;
    
    public GameObject Deadimage;
    void Start()
    {
        Deadimage = GameObject.Find("PlayerUi").transform.GetChild(5).gameObject;
        Hp = MaxHp;
        HpBar.color = Color.yellow;
        Hpinpo();
        
    }

    private void Hpinpo()
    {
        HpTxt.text = $"HP: <color=#ff0000>{Hp.ToString()}</color>";
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(attackTag))
        {
            Deamge();
        }
        if(other.gameObject .CompareTag(zombieattacktag))
        {
            Deamge();
        }
        if (other.gameObject.CompareTag(Mosterattacktag))
        {
            Deamge();
        }
        if(Hp <= 0f)
        {
            PlayerDie();
        }
        
    }

    void Deamge()
    {
        Hp -= 10;
        Hp = Mathf.Clamp(Hp, 0, 100); 
        HpBar.fillAmount = (float)Hp / (float)MaxHp;
        HpTxt.text = $"<color=#83FF96>hp:{Hp.ToString()}</color>";
        
    }
    
    void PlayerDie()
    {
        isPlayerDie = true;
        GameObject[] enemise = GameObject.FindGameObjectsWithTag("enemy");
        // 런타임에서 enemy 태그들을 가진 오브젝트들을 enemies라는 게임오브젝트 배열에
        // 저장 한다.
        for(int i = 0; i < enemise.Length; i++)
        {
            enemise[i].gameObject.SendMessage("PlayerDeath", SendMessageOptions.DontRequireReceiver);
            // 다른 게임오브젝트 있는 메서드 호출 하는 기능을 가진 메서드

            Invoke("NextSceneMove", 3.0f);
            Deadimage.SetActive(true);
        }
    }
    void NextSceneMove()
    {
        SceneManager.LoadScene("EndScene");
    }

}
