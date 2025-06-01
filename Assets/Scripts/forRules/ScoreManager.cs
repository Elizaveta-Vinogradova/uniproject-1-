using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    [SerializeField] public TMP_Text  hpText;

    [SerializeField] public TMP_Text  coffeeText;

    private int hp;
    private int coffee;
    [SerializeField] private Health health;
    private void Awake()
    {
        coffee = 0;
        hp = health.startingHealth;
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hpText.text = $"{hp.ToString()}";
        coffeeText.text = $"{coffee.ToString()}";
    }

    public void AddCoffeePoint(int amount)
    {
        coffee += amount;
        coffeeText.text = $"{coffee.ToString()}";
    }

    public void LoseHP(int amount)
    {
        hp -= amount;
        hpText.text = $"{hp.ToString()}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
