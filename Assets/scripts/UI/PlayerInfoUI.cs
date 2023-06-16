using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour
{
    [SerializeField] private Image playerBanner;
    [SerializeField] private Image HPBack;
    [SerializeField] private Image HPFront;
    [SerializeField] private Text levelNumber;

    private readonly float updateRate = 0.1f;
    private float _timer;

    private Creature player;
    private float currentHealth;
    private float currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.MainPlayerEntity;
        currentHealth = player.CurrentHealth;
        HPBack.fillAmount = player.CurrentHealth / player.MaxHealth;
        HPFront.fillAmount = player.CurrentHealth / player.MaxHealth;
        levelNumber.text = player.Level.ToString();
        currentLevel = player.Level;
    }

    private void updateHP()
    {
        currentHealth = player.CurrentHealth;

        if (currentHealth < player.CurrentHealth) 
        {            
            HPBack.fillAmount = player.CurrentHealth / player.MaxHealth;
            HPFront.fillAmount = player.CurrentHealth / player.MaxHealth;
        }
        else
        {
            HPFront.fillAmount = player.CurrentHealth / player.MaxHealth;
        }
    }

    private void Update()
    {
        if (_timer <= 0)
        {
            _timer = updateRate;
            if (Mathf.Abs(currentHealth - player.CurrentHealth) > 1)
            {
                updateHP();
            }

            if (currentLevel != player.Level)
            {
                levelNumber.text = player.Level.ToString();
                currentLevel = player.Level;
            }

            
        }
        else
        {
            _timer -= Time.deltaTime;
        }

        if (HPBack.fillAmount > HPFront.fillAmount)
        {
            HPBack.fillAmount -= Time.deltaTime/5f;
        }
    }
}
