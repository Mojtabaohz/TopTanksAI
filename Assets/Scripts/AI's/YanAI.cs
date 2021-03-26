using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YanAI : BaseAI
{
    public override IEnumerator RunAI() {
        for (int i = 0; i < 10; i++)
        {
            yield return Ahead(10);
            //yield return Fire(1);
            yield return TurnTurretLeft(90);
            yield return TurnLeft(360);
            //yield return Fire(1);
            yield return TurnTurretRight(180);
            yield return Back(10);
            //yield return Fire(1);
            yield return TurnTurretLeft(90);
            yield return TurnRight(90);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override void OnScannedRobot(ScannedRobotEvent e)
    {
        //Debug.Log("Ship detected: " + e.Name + " at distance: " + e.Distance);
    }
}