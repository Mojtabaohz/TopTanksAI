using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MojiAI : BaseAI
{
    // Start is called before the first frame update
    
    public override IEnumerator RunAI() {
        for (int i = 0; i < 10; i++)
        {
            if (Tank.target)
            {
                yield return MoveToTarget(Tank.target);
                yield return TurretLookAt(Tank.target);
            }
            else
            {
                yield return MoveToTarget(Tank.defaultTargets[1].transform);
                yield return TurretLookAt(Tank.defaultTargets[1].transform);
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
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override void OnScannedRobot(ScannedRobotEvent e)
    {
        Debug.Log("Tank detected: " + e.Name + " at distance: " + e.Distance + " target " + e.Transform);
        
    }
}
