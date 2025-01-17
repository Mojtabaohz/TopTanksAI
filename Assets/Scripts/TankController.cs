﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

/// <summary>
/// The vehicle that will do battle. This is the same for every participant in the arena.
/// Its 'brains' (the AI you'll write) will be assigned by the <seealso cref="CompetitionManager"/>
/// </summary>
public class TankController : MonoBehaviour
{
    private bool loaded = false;
    //public bool active = false;
    private const float reloadTime = 2;
    public Transform spawnPoint;
    


    protected float Timer;
    // the bullets and the locations on the prefab where they spawn from
    public GameObject BulletPrefab = null;
    public Transform Emmitter = null;
    //public Transform CannonLeftSpawnPoint = null;
    //public Transform CannonRightSpawnPoint = null;

    // the 'scanner' that allows the ship to 'see' its surroundings
    public GameObject Turret = null;

    // states can be used to indicate the state of the ship (attacking, fleeing, searching etc.)
    public GameObject[] states = null;
    // navmesh agent to access functions for finding new target and moving patterns
    public NavMeshAgent navAgent = null;
    public Transform target = null;
    public GameObject[] defaultTargets = null;
    /// <summary>
    /// the AI that will control this Tank. Is set by <seealso cref="BattleManager"/>.
    /// </summary>
    private BaseAI ai = null;

    // create a level playing field. Every Tank has the same basic abilities
    private float TankSpeed = 10.0f;
    private float SeaSize = 50.0f;
    private float RotationSpeed = 180.0f;

    // Start is called before the first frame update
    void Start()
    {
        
        //active = true;
        gameObject.GetComponent<HealthBar>().maxHealth = 100;
        gameObject.GetComponent<HealthBar>().currentHealth = 100;
        navAgent.speed = TankSpeed;
        navAgent.angularSpeed = RotationSpeed;
        navAgent.stoppingDistance = 30;
        defaultTargets = GameObject.FindGameObjectsWithTag("Base");
        ReloadBullet();
        StartBattle();
    }

    public void Update()
    {
        ReloadBullet();
        
    }

    /// <summary>
    /// Assigns the AI that steers this instance
    /// </summary>
    /// <param name="_ai"></param>
    public void SetAI(BaseAI _ai) {
        ai = _ai;
        ai.Tank = this;
    }
    /// <summary>
    /// reload the bullet after firing
    /// </summary>
    private void ReloadBullet()
    {
        if (!loaded)
        {
            Timer += Time.deltaTime;
            if (Timer >= reloadTime)
            {
                Timer = 0;
                loaded = true;
            }
        }
    }

    public void Respawn()
    {
        
            //Debug.Log("tank respawned");
            GameObject tank = Instantiate(gameObject, gameObject.GetComponent<TankController>().spawnPoint.position,
                gameObject.GetComponent<TankController>().spawnPoint.rotation);
            TankController TankController = tank.GetComponent<TankController>();
            TankController.SetAI(new MojiAI());
            
    }
    /// <summary>
    /// Tell this ship to start battling
    /// Should be called only once
    /// </summary>
    public void StartBattle() {
        //Debug.Log("Battle starts");
        StartCoroutine(ai.RunAI());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    /// <summary>
    /// If a ship is inside the 'scanner', its information (distance and name) will be sent to the AI
    /// 
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerStay(Collider other) {
        if (other.tag == "tank")
        {
            ScannedRobotEvent scannedRobotEvent = new ScannedRobotEvent();
            scannedRobotEvent.Distance = Vector3.Distance(transform.position, other.transform.position);
            scannedRobotEvent.Name = other.name;
            scannedRobotEvent.Transform = other.transform;
            ai.OnScannedRobot(scannedRobotEvent);
        }
    }

    /// <summary>
    /// Move this ship ahead by the given distance
    /// </summary>
    /// <param name="distance">The distance to move</param>
    /// <returns></returns>
    public IEnumerator __Ahead(float distance) {
        int numFrames = (int)(distance / (TankSpeed * Time.fixedDeltaTime));
        for (int f = 0; f < numFrames; f++) {
            transform.Translate(new Vector3(0f, 0f, TankSpeed * Time.fixedDeltaTime), Space.Self);
            Vector3 clampedPosition = Vector3.Max(Vector3.Min(transform.position, new Vector3(SeaSize, 0, SeaSize)), new Vector3(-SeaSize, 0, -SeaSize));
            transform.position = clampedPosition;

            yield return new WaitForFixedUpdate();            
        }
    }

    public void updateTargetInfo()
    {
        
    }
    public IEnumerator __MoveToTarget(Transform target)
    {
        navAgent.SetDestination(target.position);
        
        yield return new WaitForFixedUpdate();
    }

    /// <summary>
    /// Move the ship backwards by the given distance
    /// </summary>
    /// <param name="distance">The distance to move</param>
    /// <returns></returns>
    public IEnumerator __Back(float distance) {
        int numFrames = (int)(distance / (TankSpeed * Time.fixedDeltaTime));
        for (int f = 0; f < numFrames; f++) {
            transform.Translate(new Vector3(0f, 0f, -TankSpeed * Time.fixedDeltaTime), Space.Self);
            Vector3 clampedPosition = Vector3.Max(Vector3.Min(transform.position, new Vector3(SeaSize, 0, SeaSize)), new Vector3(-SeaSize, 0, -SeaSize));
            transform.position = clampedPosition;

            yield return new WaitForFixedUpdate();            
        }
    }

    /// <summary>
    /// Turns the ship left by the given angle
    /// </summary>
    /// <param name="angle">The angle to rotate</param>
    /// <returns></returns>
    public IEnumerator __TurnLeft(float angle) {
        int numFrames = (int)(angle / (RotationSpeed * Time.fixedDeltaTime));
        for (int f = 0; f < numFrames; f++) {
            transform.Rotate(0f, -RotationSpeed * Time.fixedDeltaTime, 0f);

            yield return new WaitForFixedUpdate();            
        }
    }

    /// <summary>
    /// Turns the ship right by the given angle
    /// </summary>
    /// <param name="angle">The angle to rotate</param>
    /// <returns></returns>
    public IEnumerator __TurnRight(float angle) {
        int numFrames = (int)(angle / (RotationSpeed * Time.fixedDeltaTime));
        for (int f = 0; f < numFrames; f++) {
            transform.Rotate(0f, RotationSpeed * Time.fixedDeltaTime, 0f);

            yield return new WaitForFixedUpdate();            
        }
    }

    /// <summary>
    /// Sit and hold still for one (fixed!) update
    /// </summary>
    /// <returns></returns>
    public IEnumerator __DoNothing() {
        yield return new WaitForFixedUpdate();
    }

    /// <summary>
    /// Fire from the forward pointing cannon
    /// </summary>
    /// <param name="power">???</param>
    /// <returns></returns>
    public IEnumerator __Fire() {
        if (loaded)
        {
            GameObject newInstance = Instantiate(BulletPrefab, Emmitter.position, Emmitter.rotation);
            this.loaded = false;
        }
       
        yield return new WaitForFixedUpdate();
    }

    /// <summary>
    /// Fire from the left pointing cannon
    /// </summary>
    /// <param name="power">???</param>
    /// <returns></returns>
    //public IEnumerator __FireLeft(float power) {
    //    GameObject newInstance = Instantiate(BulletPrefab, Emmitter.position, Emmitter.rotation);
    //    yield return new WaitForFixedUpdate();
    //}

    /// <summary>
    /// fire from the right pointing cannon
    /// </summary>
    /// <param name="power">???</param>
    /// <returns></returns>
    //public IEnumerator __FireRight(float power) {
    //    GameObject newInstance = Instantiate(BulletPrefab, Emmitter.position, CannonRightSpawnPoint.rotation);
    //   yield return new WaitForFixedUpdate();
    //}

    /// <summary>
    /// Change the color of the states (for vanity or visualising state)
    /// </summary>
    /// <param name="color"></param>
    public void __SetColor(Color color) {
        foreach (GameObject state in states) {
            state.GetComponent<MeshRenderer>().material.color = color;
        }
    }

    /// <summary>
    /// Turn the sensor to the left by the given angle
    /// </summary>
    /// <param name="angle">The angle to rotate</param>
    /// <returns></returns>
    public IEnumerator __TurretLookAt(Transform target)
    {
        //Turret.transform.rotation = Quaternion.LookRotation(target.position);
        Turret.transform.LookAt(target);
        yield return new WaitForFixedUpdate();
    }
    public IEnumerator __TurnTurretLeft(float angle) {
        int numFrames = (int)(angle / (RotationSpeed * Time.fixedDeltaTime));
        for (int f = 0; f < numFrames; f++) {
            Turret.transform.Rotate(0f, -RotationSpeed * Time.fixedDeltaTime, 0f);

            yield return new WaitForFixedUpdate();            
        }
    }

    /// <summary>
    /// Turn the sensor to the right by the given angle
    /// </summary>
    /// <param name="angle">The angle to rotate</param>
    /// <returns></returns>
    public IEnumerator __TurnTurretRight(float angle) {
        int numFrames = (int)(angle / (RotationSpeed * Time.fixedDeltaTime));
        for (int f = 0; f < numFrames; f++) {
            Turret.transform.Rotate(0f, RotationSpeed * Time.fixedDeltaTime, 0f);

            yield return new WaitForFixedUpdate();            
        }
    }
}
