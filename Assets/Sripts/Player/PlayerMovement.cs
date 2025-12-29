using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    public bool isBoosting;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float energy;
    [SerializeField] private float maxEnergy;
    [SerializeField] private float energyRegen;

    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private ParticleSystem engineEffect;

    
    private ObjectPooler destroyEffectpool;
    private Vector2 playerDirection;
    
    

    private Rigidbody2D rb;
    private Animator anim;
    

    private FlashWhite flash;

    [SerializeField] private int experience;
    [SerializeField] private int currentLevel;
    [SerializeField] private int maxLevel; 
    [SerializeField] private List<int> playerLevels;




    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }
    
    void Start()
    {
        destroyEffectpool = GameObject.Find("Boom1Pool").GetComponent<ObjectPooler>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
       
        flash = GetComponent<FlashWhite>();

        for (int i = playerLevels.Count; i < maxLevel; i++) {
            playerLevels.Add(Mathf.CeilToInt(playerLevels[playerLevels.Count-1]*1.1f+15));
        }

        energy = maxEnergy;
        UIController.Instance.UpdateEnergySlider(energy, maxEnergy);

        health = maxHealth;
        UIController.Instance.UpdateHealthSlider(health, maxHealth);

        experience = 0;
        UIController.Instance.UpdateExperienceSlider(experience, playerLevels[currentLevel]);

    }

   
    void Update()
    {
        if (Time.timeScale > 0) {
            float directionX = Input.GetAxisRaw("Horizontal");
            float directionY = Input.GetAxisRaw("Vertical");
            anim.SetFloat("moveX", directionX);
            anim.SetFloat("moveY", directionY);
            playerDirection = new Vector2(directionX, directionY).normalized;

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire2"))
            {
                AudioManager.Instance.playSound(AudioManager.Instance.turbo);
                EnterBoost();
            }
            else if (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Fire2"))
            {
                ExitBoost();
            }

            if (Input.GetKeyDown(KeyCode.RightShift) || Input.GetButtonDown("Fire1"))
            {
                PhaserWeapon.Instance.Shoot();
            }
        }
        

    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(playerDirection.x*moveSpeed, playerDirection.y*moveSpeed);

        if (isBoosting)
        {
            if (energy >= 0.5)
            {
                energy -= 0.5f;
               
            }
            else
            {
                ExitBoost();
            }
        }
        else
        {
            if (energy < maxEnergy)
            {
                energy += energyRegen;
            }
        }
        UIController.Instance.UpdateEnergySlider(energy, maxEnergy);
    }
    private void EnterBoost()
    {
        if (energy > 10)
        {
            engineEffect.Play();
            anim.SetBool("Boosting", true);
            GameManager.Instance.SetWorldSpeed(5f);
            isBoosting = true;
        }
        
    }
    public void ExitBoost()
    {
        anim.SetBool("Boosting", false);
        GameManager.Instance.SetWorldSpeed(1f);
        isBoosting = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))

        {
            Asteroid asteroid = collision.gameObject.GetComponent<Asteroid>();
            if (asteroid)
                asteroid.TakeDamage(1,false);
            
        }else if (collision.gameObject.CompareTag("Enemy"))

        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy)
                enemy.TakeDamage(1);

        }

    }

   public void TakeDamage(int damage)
    {
        health-=damage;
        AudioManager.Instance.playSound(AudioManager.Instance.hit);
        UIController.Instance.UpdateHealthSlider(health, maxHealth);
        flash.Flash();
        
        if (health <= 0)
        {
            ExitBoost();
            GameManager.Instance.SetWorldSpeed(0f);



            gameObject.SetActive(false);
            GameObject destroyEffect = destroyEffectpool.GetPoolGameObjects();
            destroyEffect.transform.position=transform.position;
            destroyEffect.transform.rotation=transform.rotation;
            destroyEffect.SetActive(true);
           
            
            GameManager.Instance.GameOver();
            AudioManager.Instance.playSound(AudioManager.Instance.dead);
        }
    }
    public void GetExperience(int exp)
    {
        experience += exp;
        UIController.Instance.UpdateExperienceSlider(experience, playerLevels[currentLevel]);
        if (experience > playerLevels[currentLevel])
            LevelUp();

    }

    public void LevelUp()
    {
        experience -= playerLevels[currentLevel];

        if (currentLevel < maxLevel - 1)
            currentLevel++;
        UIController.Instance.UpdateExperienceSlider(experience, playerLevels[currentLevel]);
        PhaserWeapon.Instance.LevelUp();
        maxHealth++;
        health=maxHealth;
        UIController.Instance.UpdateHealthSlider(health, maxHealth);
    }
   
 

}
