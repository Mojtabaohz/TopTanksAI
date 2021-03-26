using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using Random=UnityEngine.Random;


public class MojiAI : BaseAI
{


    public Transform mainTarget;
    public List<String> targetName = new List<String>();
    public List<float> targetDistance = new List<float>();
    public List<Transform> targetTransform = new List<Transform>();
    
    public override IEnumerator RunAI() {
        while (true)
        {
            if (Tank.target)
            {
                //Debug.Log(Tank.GetComponent<HealthBar>().currentHealth);
                yield return MoveToTarget(Tank.target);
                yield return TurretLookAt(Tank.target);
                yield return Fire();

            }
            else
            {
                if (Tank.navAgent.speed <= 1)
                {
                    Debug.Log(Tank.navAgent.isStopped);
                    LookForNewTarget();
                    yield return MoveToTarget(Tank.defaultTargets[1].transform);
                }
                yield return MoveToTarget(Tank.defaultTargets[1].transform);
                yield return TurretLookAt(Tank.defaultTargets[1].transform);
            } 
        }

    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override void OnScannedRobot(ScannedRobotEvent e)
    {
        if (!targetName.Contains(e.Name))
        {
            targetName.Add(e.Name);
            targetDistance.Add(e.Distance);
            targetTransform.Add(e.Transform);
            //Debug.Log("Tank detected: " + e.Name + " at distance: " + e.Distance + " target " + e.Transform);
            Tank.target = e.Transform;
        }
    }

    private void LookForNewTarget()
    {
        if (Tank.target)
        {
            mainTarget = Tank.target;
        }
        else
        {
            WonderAround();
        }
    }

    private IEnumerator WonderAround()
    {
        int rnd = Random.Range(0, 3);
        yield return MoveToTarget(Tank.defaultTargets[rnd].transform);
    }
}
