using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using Random=UnityEngine.Random;


public class YanAI : BaseAI {                 

    public Transform mainTarget;
    public List<String> targetName = new List<String>();
    public List<float> targetDistance = new List<float>();
    public List<Transform> targetTransform = new List<Transform>();
    
    public override IEnumerator RunAI() {
        while (true)
        {
            if (Tank.target)
            {
                yield return MoveToTarget(Tank.target);
                yield return TurretLookAt(Tank.target);
                yield return Fire();
            }
            else
            {
                if (Tank.navAgent.speed <= 1) {
                    Debug.Log(Tank.navAgent.isStopped);
                    LookForNewTarget();
                    yield return MoveToTarget(Tank.defaultTargets[1].transform);
                   };
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
            // Tank found to target
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
            Sightseeing();
        }
    }

    private IEnumerator Sightseeing()
    {
        int rnd = Random.Range(0, 3);
        yield return Back(10);
        yield return MoveToTarget(Tank.defaultTargets[rnd].transform);
        yield return TurretLookAt(Tank.defaultTargets[rnd].transform);
    }
}
