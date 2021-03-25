using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// The event data that is generated when another participant in the arena was 'seen'
/// </summary>
public class ScannedRobotEvent {
    public string Name;
    public float Distance;
    public Transform Transform;
}

public class BaseAI
{
    public TankController Tank = null;

    /// <summary>
    /// Another participant was 'seen'. Do something with the info stored in the even data
    /// </summary>
    /// <param name="e">The event data</param>
    public virtual void OnScannedRobot(ScannedRobotEvent e)
    {
        //Debug.Log("new tank scanned");
    }

    /// <summary>
    /// Move this Tank ahead by the given distance
    /// </summary>
    /// <param name="distance">The distance to move</param>
    /// <returns></returns>
    public IEnumerator Ahead(float distance) {
        yield return Tank.__Ahead(distance);
    }

    public IEnumerator MoveToTarget(Transform target)
    {
        yield return Tank.__MoveToTarget(target);
    }
    /// <summary>
    /// Move the ship backwards by the given distance
    /// </summary>
    /// <param name="distance">The distance to move</param>
    /// <returns></returns>
    public IEnumerator Back(float distance) {
        yield return Tank.__Back(distance);
    }

    /// <summary>
    /// Turn the sensor to the left by the given angle
    /// </summary>
    /// <param name="angle">The angle to rotate</param>
    /// <returns></returns>
    public IEnumerator TurretLookAt(Transform target)
    {
        yield return Tank.__TurretLookAt(target);
    }
    public IEnumerator TurnTurretLeft(float angle) {
        yield return Tank.__TurnTurretLeft(angle);
    }

    /// <summary>
    /// Turn the sensor to the right by the given angle
    /// </summary>
    /// <param name="angle">The angle to rotate</param>
    /// <returns></returns>
    public IEnumerator TurnTurretRight(float angle) {
        yield return Tank.__TurnTurretRight(angle);
    }

    /// <summary>
    /// Turns the ship left by the given angle
    /// </summary>
    /// <param name="angle">The angle to rotate</param>
    /// <returns></returns>
    public IEnumerator TurnLeft(float angle) {
        yield return Tank.__TurnLeft(angle);
    }

    /// <summary>
    /// Turns the ship right by the given angle
    /// </summary>
    /// <param name="angle">The angle to rotate</param>
    /// <returns></returns>
    public IEnumerator TurnRight(float angle) {
        yield return Tank.__TurnRight(angle);
    }

    /// <summary>
    /// Fire from the forward pointing cannon
    /// </summary>
    /// <param name="power">???</param>
    /// <returns></returns>
    public IEnumerator Fire(float power) {
        yield return Tank.__Fire(power);
    }

    /// <summary>
    /// Fire from the left pointing cannon
    /// </summary>
    /// <param name="power">???</param>
    /// <returns></returns>
    //public IEnumerator FireLeft(float power) {
    //    yield return Tank.__FireLeft(power);
    //}

    /// <summary>
    /// fire from the right pointing cannon
    /// </summary>
    /// <param name="power">???</param>
    /// <returns></returns>
    //public IEnumerator FireRight(float power) {
    //    yield return Tank.__FireRight(power);
    //}

    public virtual IEnumerator RunAI() {
        yield return null;
    }
}
