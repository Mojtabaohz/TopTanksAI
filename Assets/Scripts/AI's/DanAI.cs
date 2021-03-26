using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanAI : BaseAI
{
    public override IEnumerator RunAI() {

        /*while(true) {
            yield return MoveToTarget(Tank.target);
                /// Scan environment
            
        };*/

        //Placeholder behaviour
        for (int i = 0; i < 10; i++)
        {
            yield return Ahead(200);
            yield return Fire(1);
            yield return TurnTurretLeft(90);
            yield return TurnLeft(360);
            yield return Fire(1);
            yield return TurnTurretRight(180);
            yield return Back(200);
            yield return Fire(1);
            yield return TurnTurretLeft(90);
            yield return TurnRight(90);
        };
    }

    /// <summary>
    /// Method <c>Calculates</c> calculates if it is worth engaging an enemy, based on health values of both parties
    /// </summary>
    /// <returns></returns>
    /*private bool engageWorth() {
        int worth = Tank.health - Tank.target.health;
        if (worth > 0) {
            return true;
        } else {
            return false;
        }
    }*/

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override void OnScannedRobot(ScannedRobotEvent e)
    {
        Debug.Log("Ship detected: " + e.Name + " at distance: " + e.Distance);
        /*if (engageWorth()) {
            initiateBattle(Tank.target);
        }*/
    }

    public void initiateBattle() {
        return;
    }
}
