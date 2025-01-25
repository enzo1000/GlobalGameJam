using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Bubble bubble;
    private PlayerManager player;

    private float VictoryGoal;
    // List<Action> visibleActions = new List<Action>();

    private float newsTimer;
    private float timeBetweenNews;
    private float newsProba;
    void Start()
    {
        bubble = new Bubble();
    }

    void Update()
    {
        
    }
}
