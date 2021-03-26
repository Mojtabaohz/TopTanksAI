using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanAI : BaseAI {

    public List<String> targetName = new List<String>();
    public List<float> targetDistance = new List<float>();
    public List<Transform> targetTransform = new List<Transform>();

    public override IEnumerator RunAI() {

        while (true) {
            
            if (Tank.target) {
                yield return MoveToTarget(Tank.target);
                yield return TurretLookAt(Tank.target);
                yield return Fire();
            } else {
                yield return MoveToTarget(Tank.defaultTargets[1].transform);
                yield return TurretLookAt(Tank.defaultTargets[1].transform);
            } 
        }
    }

    /// <summary>
    /// Method <c>Calculates</c> calculates if it is worth engaging an enemy, based on health values of both parties
    /// </summary>
    /// <returns></returns>
    /*private bool engageWorth(int targetHealth) {
        int worth = Tank.health - targetHealth;
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
        //if (engageWorth(e.health)) {
            //initiateBattle(e);
        //}
        if (!targetName.Contains(e.Name)) {
            targetName.Add(e.Name);
            targetDistance.Add(e.Distance);
            targetTransform.Add(e.Transform);
            Debug.Log("Tank detected: " + e.Name + " at distance: " + e.Distance + " target " + e.Transform);
            Tank.target = e.Transform;
        }
    }

    public void initiateBattle(ScannedRobotEvent e) {
        
    }
}
