using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CustomerSpawner : MonoBehaviour
{
    public ItemDatabase database;
    
    public GameObject customerPrefab; // only one type for now, make list of data later
    public Transform registerPoint;
    public int maxCustomers = 3;
    public float maxSpawnTime = 20f;
    public float minSpawnTime = 10f;

    public List<Customer> activeCustomers = new List<Customer>();

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

            Debug.Log("SpawnRoutine timer ended");

            if (activeCustomers.Count < maxCustomers)
            {
                SpawnCustomer();
            }
        }
    }

    private void SpawnCustomer()
    {
        GameObject customer = Instantiate(customerPrefab);
        Customer script = customer.AddComponent<Customer>();
        script.targetPoint = registerPoint;
        script.desiredItem = database.GetRandomDrink();
        activeCustomers.Add(script);

        Debug.Log("Spawned customer");
    }

    public Customer GetNextCustomer()
    {
        if (activeCustomers.Count == 0) return null;
        return activeCustomers[0];
    }
}
