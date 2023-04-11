using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject EnemyFlight;
    public int EnemyFlightCount = 10;

    public List<GameObject> EnemyFlightList;

    private void Awake()
    {
        EnemyFlightList = new List<GameObject>();
        for (int i = 0; i < EnemyFlightCount; i++)
        {
            GameObject EnemyFlightGo = GameObject.Instantiate(EnemyFlight, transform);
            EnemyFlightList.Add(EnemyFlightGo);
        }
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }
}
