using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    
    [SerializeField] private List<ParticleSystem> grassParticles;

    [SerializeField] private GameObject arrowNavigator;
    
    [SerializeField] private ParticleSystem leftDustParticles;
    [SerializeField] private ParticleSystem rightDustParticles;

    private AudioController audioController;
    private GameConfig gameConfig;
    private Rigidbody rb;
    private Vector3 moveInput;

    private int currentGrassParticle;
    private bool inStackingPlace;

    public bool rotateBlade => moveInput != Vector3.zero && !Globals.instance.fullCapacity;
    public bool showDustParticles => moveInput != Vector3.zero;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameConfig = Globals.instance.gameConfig;
        audioController = AudioController.instance;

        Globals.instance.usedCapacity = 0;
        
        arrowNavigator.SetActive(false);
        GameAction.upgradePlayer += CheckCapacityUpgrade;
    }

    private void OnDestroy()
    {
        GameAction.upgradePlayer -= CheckCapacityUpgrade;
    }

    void Update()
    {
        moveInput = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        
        leftDustParticles.enableEmission = showDustParticles;
        rightDustParticles.enableEmission = showDustParticles;
    }
    
    void FixedUpdate()
    {
        Movement();
        Turn();
    }
    
    private void Movement()
    {
        rb.velocity = transform.forward * (moveInput.magnitude * Globals.instance.PlayerSpeed);
    }
    
    private void Turn()
    {
        if (moveInput == Vector3.zero) return;

        var rot = Quaternion.LookRotation(moveInput, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, gameConfig.playerTurnSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grass"))
        {
            if(Globals.instance.fullCapacity) return;
            
            other.gameObject.SetActive(false);
            Globals.instance.usedCapacity++;
            if (Globals.instance.fullCapacity)
                arrowNavigator.SetActive(true);
            GameAction.updateCapacityIndicator?.Invoke();
            
            //todo планую додати автоматичне створення монеток на шкалі в залежності від цього числа (але поки що залишу так)
            if (Globals.instance.usedCapacity % 40 == 0)
            {
                GameAction.grainSale?.Invoke();
            }

            grassParticles[currentGrassParticle].transform.position = other.transform.position;
            grassParticles[currentGrassParticle].Play();
            
            currentGrassParticle++;
            if (currentGrassParticle >= grassParticles.Count)
                currentGrassParticle = 0;
        }

        if (other.CompareTag("StackingPlace"))
        {
            inStackingPlace = true;
            StartCoroutine(StackGrass());
            if (Globals.instance.usedCapacity > 0)
                GameAction.grassFall?.Invoke(true);
        }
        else if (other.CompareTag("UpgradingPlace"))
        {
            GameAction.stayAtUpgradingPlace?.Invoke(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("StackingPlace"))
        {
            inStackingPlace = false;
            GameAction.grassFall?.Invoke(false);
        }
        else if (other.CompareTag("UpgradingPlace"))
        {
            GameAction.stayAtUpgradingPlace?.Invoke(false);
        }
    }

    void CheckCapacityUpgrade()
    {
        if (!Globals.instance.fullCapacity) arrowNavigator.SetActive(false);
    }

    IEnumerator StackGrass()
    {
        arrowNavigator.SetActive(false);
        
        while (Globals.instance.usedCapacity >= 1)
        {
            if (!inStackingPlace) yield break;
            Globals.instance.usedCapacity -= gameConfig.stackGrassCount;
            GameAction.updateProgressIndicator?.Invoke();
            yield return new WaitForSeconds(Time.deltaTime);
        }
        if (Globals.instance.usedCapacity < 0) Globals.instance.usedCapacity = 0;
        GameAction.grassFall?.Invoke(false);
    }
}
