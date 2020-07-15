using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : ModelCharacterController
{
    private bool movingForward = false;
    private bool movingBackward = false;
    private bool movingLeft = false;
    private bool movingRight = false;
    
    public GameObject[] AttackPoints;
    private GameObject finalPosition;
    private ParticleSystem particleSystem;

    public GameObject berserkerLayer;
    public GameObject winLayer;
    public Image soulsBar;
    public Text soulsCountText;
    public bool isImmortal = false;

    [SerializeField]
    private float killStreakTimeBetweenKills;
    [SerializeField]
    private float killStreakBonusDuration;
    [SerializeField]
    private float killStreakPowerDuration;
    [SerializeField]
    private float killStreakMaxPowerDuration = 5;

    private int killStreakCount;
    private float timeBetweenKills = 0;
    private float powerDuration = 0;

    [SerializeField]
    private int soulsCount = 0;
    private float soulsMeter;
    private float maxSoulsMeter = 10;
    private float singleSoulTime = 5;

    private float dmgTimer = 5;
    private float dmgTimerBase = 5;

    private TileManager tileManager;
    private void Start()
    {
        tileManager = FindObjectOfType<TileManager>();
        soulsMeter = maxSoulsMeter * singleSoulTime;
        soulsCountText.text = ("Souls: " + soulsCount);
        particleSystem = GetComponent<ParticleSystem>();

        foreach (GameObject s in AttackPoints)
        {
            if (s.activeInHierarchy)
                s.SetActive(false);
        }
    }
    protected override void Awake()
    {
        base.Awake();

        finalPosition = new GameObject();
        finalPosition.name = this.name + "MovementTransform";
    }

    public void RegisterKill()
    {
        soulsCount++;
        soulsCountText.text = ("Souls: " + soulsCount);
        if(soulsCount >= ((tileManager.GetVillagesCount+1) * 4))
        {
            winLayer.SetActive(true);
            GetComponent<CharacterController>().enabled = false;

            GetComponent<PlayerController>().enabled = false;
        }
        
        if(soulsMeter + singleSoulTime <= maxSoulsMeter * singleSoulTime)
        {
            soulsMeter += singleSoulTime;
        }
        else
        {
            soulsMeter = maxSoulsMeter * singleSoulTime;
        }

        if((killStreakCount >= 0 && timeBetweenKills > 0) || killStreakCount == 0)
        {
            killStreakCount++;
            timeBetweenKills = killStreakTimeBetweenKills;
            if(killStreakCount > 3)
            {
                if (powerDuration + killStreakBonusDuration > killStreakMaxPowerDuration)
                {
                    powerDuration = killStreakMaxPowerDuration;
                    
                }
                else
                {
                    powerDuration += killStreakBonusDuration;
                   
                }
               
            }
        }

        if (killStreakCount == 3)
        {
            powerDuration = killStreakPowerDuration;
            Debug.Log("Killstreak!!!");
        }
            
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if(powerDuration >= 0)
        {
            powerDuration -= Time.deltaTime;
            //this.GetComponent<HealthScript>().enabled = false;
            isImmortal = true;
            timeBetweenKills = 0;
            berserkerLayer.SetActive(true);
        }
        else if(powerDuration <= 0)
        {
            //this.GetComponent<HealthScript>().enabled = true;
            isImmortal = false;
            berserkerLayer.SetActive(false);
  
            powerDuration = -1;
        }
        else if(powerDuration < 0 && powerDuration != -1)
        {
            timeBetweenKills = 0;
        }
        timeBetweenKills -= Time.deltaTime;
        if (timeBetweenKills < 0)
            killStreakCount = 0;

        if (soulsMeter >= 0)
        {
            soulsMeter -= Time.deltaTime * 0.5f;
            dmgTimer = 5;
            soulsBar.fillAmount = (soulsMeter / (maxSoulsMeter * singleSoulTime));
            dmgTimer = dmgTimerBase;
        }
        else
        {
            if (dmgTimer >= 0)
                dmgTimer -= Time.deltaTime;
            else
            {
                GetComponent<PlayerHealth>().TrueDamage(5f);
                dmgTimer = dmgTimerBase;
            }
           
        }

        moving = 0;

        movingForward = false;
        movingBackward = false;
        movingLeft = false;
        movingRight = false;

        if (Input.GetKey(KeyCode.W))
        {
            movingForward = true;
            movingBackward = false;
            moving = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            movingBackward = true;
            movingForward = false;
            moving = 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            movingLeft = true;
            movingRight = false;
            moving = 1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movingRight = true;
            movingLeft = false;
            moving = 1;
        }
        

        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Dash();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Kick();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if(CanDoAction)
                GetComponentInChildren<ItemsDetector>()?.SwapWeapons();
        }

        UpdateCharacterRotation();

        Move(moving);
    }

    /*private void FixedUpdate()
    {
        Move(moving);
    }*/

    private void UpdateCharacterRotation()
    {
        if (moving != 0 && CanRotate)
        {
            finalPosition.transform.rotation = Quaternion.identity;

            if (movingForward)
            {
                if (movingLeft)
                    finalPosition.transform.Rotate(0, -45, 0);
                else if (movingRight)
                    finalPosition.transform.Rotate(0, 45, 0);
            }
            else if (movingBackward)
            {
                finalPosition.transform.Rotate(0, 180, 0);

                if (movingLeft)
                    finalPosition.transform.Rotate(0, 45, 0);
                else if (movingRight)
                    finalPosition.transform.Rotate(0, -45, 0);
            }
            else if (movingLeft)
                finalPosition.transform.Rotate(0, -90, 0);
            else if (movingRight)
                finalPosition.transform.Rotate(0, 90, 0);

            if (this.transform.rotation != finalPosition.transform.rotation)
            {
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, finalPosition.transform.rotation, Time.deltaTime * 5f);
            }
        }
    }
    public void Activate_AttackPoint()
    {
       
        for(int i=0;i<2;i++)
        {
            AttackPoints[i].SetActive(true);
        }
    }
    public void Deactivate_AttackPoint()
    {
       
        for (int i = 0; i < 2; i++)
        {
            AttackPoints[i].SetActive(false);
        }
    }
    public void Activate_AttackPointKick()
    {

      
            AttackPoints[2].SetActive(true);
        
    }
    public void Deactivate_AttackPointKick()
    {

       
            AttackPoints[2].SetActive(false);
        
    }
    public void Activate_collider()
    {
        GetComponent<PlayerHealth>().enabled = true;
    }
    public void Deactivate_collider()
    {
        GetComponent<PlayerHealth>().enabled = false;

    }

}
