using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UnityEvent OnDie;

    [SerializeField]
    private int health;
    [SerializeField]
    private GameObject hitFX;

    public void GetHit(int damage)
    {
        health -= damage;
        Debug.Log(gameObject.name + " health is " + health);
        Instantiate(hitFX, transform.position, Quaternion.identity);
        if (health <= 0)
        {
            OnDie?.Invoke();
            Debug.Log(gameObject.name + " died!");
        }

    }
}
