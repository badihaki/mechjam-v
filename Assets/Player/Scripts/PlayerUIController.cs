using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    private Player player;
    [SerializeField] private Transform uiContainer;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Image portrait;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI stockText;

    public void InitializeController(Player _player)
    {
        player = _player;
        uiContainer = GameObject.Find("PlayerCanvas").transform.Find($"P{player.PlayerID}Container");
        if(!uiContainer) uiContainer = GameObject.Find("PlayerCanvas").transform.Find($"P1Container");

		SetUpComponents();
        SetUpEvents();
    }

    private void SetUpComponents()
    {
        healthBar = uiContainer.Find("Health").GetComponent<Slider>();
        healthBar.maxValue = player.Health.maxHealth;
        healthBar.value = player.Health.currentHealth;

        portrait = uiContainer.Find("Portrait").GetComponent<Image>();
        
        ammoText = uiContainer.Find("Ammo").GetComponent<TextMeshProUGUI>();
        ammoText.text = player.AttackController.currentAmmo.ToString();
        
        stockText = uiContainer.Find("Stock").GetComponent<TextMeshProUGUI>();
        stockText.text = player.AttackController.ammoStock.ToString();
    }

    private void SetUpEvents()
    {
        player.AttackController.onAmmoChanged += ChangeAmmo;
        player.AttackController.onStockChanged += ChangeStock;
        player.Health.onHealthChange += ChangeHealth;
    }

    private void OnEnable()
    {
        if (player) SetUpEvents();
    }
    private void OnDisable()
    {
        player.AttackController.onAmmoChanged -= ChangeAmmo;
        player.AttackController.onStockChanged -= ChangeStock;
        player.Health.onHealthChange -= ChangeHealth;
    }

    public void ChangeHealth(int health)
    {
        // maybe use a coroutine to change health
        healthBar.value = health;
    }

    public void ChangePortrait(Sprite image) => portrait.sprite = image;

    public void ChangeAmmo(int value) => ammoText.text = value.ToString();
    public void ChangeStock(int value) => stockText.text = value.ToString();
}
