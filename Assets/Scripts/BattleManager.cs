using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the class / component that manages the arena.
/// It generates / Instantiates the AI game objects and lets the games begin!
/// </summary>
public class BattleManager : MonoBehaviour
{
    // the prefab for the participants in the battle
    public GameObject PlayerPrefab = null;

    // The positions where the participants will be instantiated
    // set in the inspector by dragging 4 gameobjects in the slots of the array
    public Transform[] SpawnPoints = null;

    // the list that keeps track of all the participants
    private List<TankController> Tanks = new List<TankController>();

    /// <summary>
    /// creates the 4 tanks that will do battle
    /// 4 tank prefabs will be instantated and each will be assigned an AI derived from BaseAI
    /// </summary>
    void Start()
    {
        BaseAI[] aiArray = new BaseAI[] {
            new MojiAI(), 
            new IljaAI(), 
            new YanAI(), 
            new DanAI()
        };

        for (int i = 0; i < 4; i++)
        {
            GameObject tank = Instantiate(PlayerPrefab, SpawnPoints[i].position, SpawnPoints[i].rotation);
            TankController TankController = tank.GetComponent<TankController>();
            TankController.SetAI(aiArray[i]);
            Tanks.Add(TankController);
        }
    }

    /// <summary>
    /// Start Battle obviously should be called only once.
    /// Otherwise the tanks will run multiple coroutines that manage their AI
    /// <seealso cref="TankController"/>
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            foreach (var tank in Tanks) {
                tank.StartBattle();
            }
        }
    }
}
