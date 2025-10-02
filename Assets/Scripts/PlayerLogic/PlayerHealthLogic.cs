using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthLogic : MonoBehaviour
{
    [SerializeField]
    public TMP_Text _textMeshPro;

    [SerializeField]
    public Slider _slider;


    private GameManager gameManager;
    private int health = 3;

    private float damageCooldown = 2f;

    private float lastDamaged = 0f;

    private float shieldCharge = 1f;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindFirstObjectByType<GameManager>();
    }

    private void Update()
    {
        shieldUpdate();
        if (lastDamaged < 0)
            return;
        lastDamaged -= Time.deltaTime;
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision != null && collision.gameObject.tag == "Asteroid" && lastDamaged <= 0)
        {
            RemoveHealth();
            lastDamaged = damageCooldown;
            Destroy(collision.gameObject);
        }
    }

    public void addToShield()
    {
        if(shieldCharge < 100)
            shieldCharge += 10;
    }

    void shieldUpdate()
    {
        _slider.value = shieldCharge;
    }

    void RemoveHealth()
    {
        if(shieldCharge >= 100)
        {
            // Consumes the shield charge without costing a life
            shieldCharge = 0;
            shieldUpdate();
            return;
        }

        health--;

        _textMeshPro.text = $"Lives: {health}";

        if (health > 0) return;


        health = 0;
        gameManager.GameOver();
    }
}
