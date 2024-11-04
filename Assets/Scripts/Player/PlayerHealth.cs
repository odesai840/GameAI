using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    [SerializeField] Image healthBar;

    private PlayerControls playerControls;
    private float health;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0f, maxHealth);
        if (Input.GetKeyDown(KeyCode.G))
        {
            TakeDamage(5);
        }
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        healthBar.fillAmount = (health / maxHealth);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log(health);
    }
}
