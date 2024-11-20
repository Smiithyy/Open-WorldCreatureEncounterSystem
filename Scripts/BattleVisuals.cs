using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleVisuals : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject player;

    private int currentHealth;
    private int maxHealth;
    private int level;

    private Animator anim;

    private const string LEVEL_ABB = "Lvl: ";

    private const string IS_ATTACK_PARAM = "IsAttack";
    private const string IS_HIT_PARAM = "IsHit";
    private const string IS_DEAD_PARAM = "IsDead";

    // Start is called before the first frame update
    void Awake()
    {
        anim = player.GetComponent<Animator>();
    }

    public void SetStartingValues(int currentHealth, int maxHealth, int level)
    {
        this.currentHealth = currentHealth;
        this.maxHealth = maxHealth;
        this.level = level;
        levelText.text = LEVEL_ABB + this.level.ToString();
        UpdateHealthBar();
    }

    public void ChangeHealth(int currentHealth)
    {
        this.currentHealth = currentHealth;

        if (currentHealth <= 0)
        {
            PlayDeathAnimation();
            Destroy(gameObject, 1.5f);
        }
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    public void PlayAttackAnimation()
    {
        anim.SetTrigger(IS_ATTACK_PARAM);
    }

    public void PlayHitAnimation()
    {
        anim.SetTrigger(IS_HIT_PARAM);
    }

    public void PlayDeathAnimation()
    {
        anim.SetTrigger(IS_DEAD_PARAM);
    }
}
