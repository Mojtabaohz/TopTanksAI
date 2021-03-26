using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MojiAI : BaseAI
{
    // Start is called before the first frame update
    
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
                yield return MoveToTarget(Tank.defaultTargets[1].transform);
                yield return TurretLookAt(Tank.defaultTargets[1].transform);
            } 
        }
        
            
            
            //yield return Fire(1);
            //yield return TurnTurretLeft(90);
            //yield return TurnLeft(90);
            //yield return Fire(1);
            //yield return TurnTurretRight(180);
            //yield return Back(10);
            //yield return Fire(1);
            //yield return TurnTurretLeft(90);
            //yield return TurnRight(90);


        
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
            Debug.Log("Tank detected: " + e.Name + " at distance: " + e.Distance + " target " + e.Transform);
            Tank.target = e.Transform;
        }
        

    }
}
