using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PropRandomizer : MonoBehaviour
{
    public List<GameObject> propSpawnPoints;
    public List<GameObject> propPrefabs;

    private void OnEnable()
    {

    }

    private void Start()
    {
        SpawProps();
    }

    private void Update()
    {

    }

    private void OnDisable()
    {

    }

    private void SpawProps()
    {
        foreach (var prop in propSpawnPoints)
        {
            int rand = UnityEngine.Random.Range(0, propPrefabs.Count);
            GameObject go = GameObject.Instantiate(propPrefabs[rand], prop.transform.position, Quaternion.identity);
            go.transform.parent = prop.transform;
        }
    }
}
