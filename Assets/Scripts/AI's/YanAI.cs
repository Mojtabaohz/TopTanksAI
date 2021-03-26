using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class YanAI : BaseAI {                                                         /// Placeholder AI from Daniel

    public List<String> targetName = new List<String>();
    public List<float> targetDistance = new List<float>();
    public List<Transform> targetTransform = new List<Transform>();

    public bool baseTargeted = false;
    public int targetBaseID = 3;
    public const int homeBaseID = 2;

    public override IEnumerator RunAI() {

        while (true) {
            
            if (Tank.target && engageWorth()) {
                //Debug.Log("Attacking enemy");
                yield return MoveToTarget(Tank.target);
                yield return TurretLookAt(Tank.target);
                yield return Fire();
            } else if (baseTargeted) {
                //Debug.Log("Attacking base " + targetBaseID);
                yield return MoveToTarget(Tank.defaultTargets[targetBaseID].transform);
                yield return TurretLookAt(Tank.defaultTargets[targetBaseID].transform);
            } else {
                targetBase();
                //Debug.Log("Targeted base and attacking base " + targetBaseID);
                yield return MoveToTarget(Tank.defaultTargets[targetBaseID].transform);
                yield return TurretLookAt(Tank.defaultTargets[targetBaseID].transform);
            }
        }
    }

    /// <summary>
    /// Method <c>Calculates</c> calculates if it is worth engaging an enemy, based on health values of both parties
    /// </summary>
    /// <returns></returns>
    private bool engageWorth() {
        if(Tank.GetComponent<HealthBar>().currentHealth > Tank.GetComponent<HealthBar>().maxHealth / 2) {
            return true;
        } else {
            return false;
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override void OnScannedRobot(ScannedRobotEvent e)
    {
        //Debug.Log("Ship detected: " + e.Name + " at distance: " + e.Distance);
        if (engageWorth()) {
            initiateBattle(e);
        }
    }

    public void initiateBattle(ScannedRobotEvent e) {
        baseTargeted = false;
        if (!targetName.Contains(e.Name)) {
            targetName.Add(e.Name);
            targetDistance.Add(e.Distance);
            targetTransform.Add(e.Transform);
            //Debug.Log("Tank detected: " + e.Name + " at distance: " + e.Distance + " target " + e.Transform);
            Tank.target = e.Transform;
        }
    }

    public void targetBase() {
        int i = Random.Range(1, 4);
        while (i == homeBaseID) {
            i = Random.Range(1,4);
        }
        //Debug.Log("Random int roll " + i);
        targetBaseID = i;
        baseTargeted = true;
    }
}
