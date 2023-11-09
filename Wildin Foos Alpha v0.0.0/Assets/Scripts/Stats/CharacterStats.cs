using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    Animator animator;
    public GameObject health;
    public Slider healthSlider;
    public Slider easeHealthSlider;
    private float lerpSpeed = 0.005f;
    public int maxHealth = 100;
    public int currentHealth { get; private set; }
    public bool isEnemy;

    int isDeadHash;
    int isHitHash;

    public Stat damage;
    public Stat armor;

    public float healthBarDisplayTime = 5.0f;
    private float timeSinceLastDamage = 0.0f;
    private bool displayHealthBar = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        isDeadHash = Animator.StringToHash("isDead");
        isHitHash = Animator.StringToHash("isHit");

        health.SetActive(false);
    }

    void Awake()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {

        if (healthSlider.value != currentHealth)
        {
            healthSlider.value = currentHealth;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }

        if (healthSlider.value != easeHealthSlider.value)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, currentHealth, lerpSpeed);
        }

        if (isEnemy && displayHealthBar)
        {
            timeSinceLastDamage += Time.deltaTime;
            if (timeSinceLastDamage >= healthBarDisplayTime)
            {
                HideHealthBar();
            }
        }
    }

    public virtual void TakeDamage(int damage)
    {
        if (isEnemy)
        {
            bool isHit = animator.GetBool(isHitHash);
            if (!isHit)
            {
                animator.SetBool(isHitHash, true);

            }
            if (isHit)
            {
                animator.SetBool(isHitHash, false);
            }
        }

        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
        

        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");
        
        if (currentHealth <= 0)
        {
            bool isDead = animator.GetBool(isDeadHash);
            if (isEnemy && !isDead)
            {
                animator.SetBool(isDeadHash, true);
                GetComponent<CactusAnimationController>().enabled = false;
            }
            Die();
        }

        ShowHealthBar();
    }

    public virtual void Die()
    {
        // die in some way
        // method to be overwritten
        Debug.Log(transform.name + " died.");
    }

    void ShowHealthBar()
    {
        if (healthSlider != null && easeHealthSlider != null)
        {
            health.SetActive(true);
            displayHealthBar = true;
            timeSinceLastDamage = 0.0f;
        }
    }

    void HideHealthBar()
    {
        if (healthSlider != null && easeHealthSlider != null)
        {
            health.SetActive(false);
            displayHealthBar = false;
        }
    }

}
