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
        uiContainer = GameObject.Find("PlayerCanvas").transform.Find($"P{player.playerID}Container");

        SetUpComponents();
        SetUpEvents();
    }

    private void SetUpComponents()
    {
        healthBar = uiContainer.Find("Health").GetComponent<Slider>();
        portrait = uiContainer.Find("Portrait").GetComponent<Image>();
        ammoText = uiContainer.Find("Ammo").GetComponent<TextMeshProUGUI>();
        ammoText.text = player.attackController.currentAmmo.ToString();
        stockText = uiContainer.Find("Stock").GetComponent<TextMeshProUGUI>();
        stockText.text = player.attackController.ammoStock.ToString();
    }

    private void SetUpEvents()
    {
        player.attackController.onAmmoChanged += ChangeAmmo;
        player.attackController.onStockChanged += ChangeStock;
    }

    private void OnEnable()
    {
        if (player) SetUpEvents();
    }
    private void OnDisable()
    {
        player.attackController.onAmmoChanged -= ChangeAmmo;
        player.attackController.onStockChanged -= ChangeStock;
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
