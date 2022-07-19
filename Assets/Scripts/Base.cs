using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Base : MonoBehaviour
{
    public TMP_Text healthText; 
    int baseMaxHealth = 100;
    int baseHealth;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            ReduceHealth(collision.GetComponent<EnemyAI>().damage);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        baseHealth = baseMaxHealth;
    }

    public void ReduceHealth(int damage)
    {
        baseHealth -= damage;
        healthText.text = baseHealth.ToString();
    }
}
