using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    //hp
    public float MaxHp;
    [SerializeField] float HP;
    public Image HealthBar;
    //coin
    public Text coinNumber;
    int coin;

    private void Start()
    {
        HP = MaxHp;
    }

    public void Update()
    {
        //show the current hp in image
        HealthBar.fillAmount = (HP / MaxHp);
        //count coin
        coinNumber.text = "x" + coin.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Drain blood when touching an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            HP -= 10;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Coin"))
        {
            coin ++;
            //other.gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
    }
}
