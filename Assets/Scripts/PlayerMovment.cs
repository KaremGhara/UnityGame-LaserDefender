using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovment : MonoBehaviour
{

    [Header("Player")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] int health = 500;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip shootSound;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 0.4f;
    [SerializeField] [Range(0, 1)] float deathSoundVolume = 0.75f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] GameObject laserPrefabPowerUp;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;
    [SerializeField] Coroutine FiringCoroutine;
    [SerializeField] GameObject Healpil;
    [SerializeField] int SpawnHealDur = 60;
    int counterHP = 0;

    float x;
    float y;
    GameObject laser;
    GameObject laser1;
    DamageDealer damageDealer;

    float xMin, xMax, Ymin, Ymax;
    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundries();
        StartCoroutine(SpawnDuration());
        
        
    }

    private void SetUpMoveBoundries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        Ymin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        Ymax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
        
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            FiringCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(FiringCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(shootSound, Camera.main.transform.position, shootSoundVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }

    }






    private void Move()
    {
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, Ymin, Ymax);
        transform.position = new Vector2(newXPos, newYPos);


    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "HP")
        {
            PowerUp bounsHeal = other.GetComponent<PowerUp>();
            HealBouns(bounsHeal);
            Destroy(other.gameObject);
            counterHP = 0;
            StartCoroutine(SpawnDuration());
            
        }
      

       
            DamageDealer damageDealer = other.GetComponent<DamageDealer>();
            beenHit(damageDealer);
        


    }

   




    private void beenHit(DamageDealer damageDealer)
    {

        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }

    }

    public int GetHealth()
    {
        if (health >= 0)
        {
            return health;
        }
        else
        { return 0; }
    }
    private void Die()
    {

        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);



    }



    private void SpawnHeallpill()
    {
        bool pillSpawned = false;
        Vector3 healpos = new Vector3(UnityEngine.Random.Range(0,transform.position.x),UnityEngine.Random.Range(0, transform.position.y), 0f);
        while (!pillSpawned)
        {
            if ((healpos - transform.position).magnitude < 3) continue;

            else
            {
                Instantiate(Healpil, healpos, Quaternion.identity);
                pillSpawned = true;
                counterHP++;
            }

        }
    }

  


    IEnumerator SpawnDuration()
    {
        if (counterHP == 0)
        {
            yield return new WaitForSeconds(SpawnHealDur);
            SpawnHeallpill();
        }
    }

    private void HealBouns(PowerUp heal)
    {
        health += heal.GetHeal();
    }

}

    

 

   

