using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class CustomerSpawner : MonoBehaviour
{
    public ItemDatabase database;
    
    public GameObject customerPrefab; // only one type for now, make list of data later
    public Register register;
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

            if (activeCustomers.Count < maxCustomers)
            {
                SpawnCustomer();
            }
        }
    }

    private void SpawnCustomer()
    {
        GameObject customer = Instantiate(customerPrefab, transform.position, transform.rotation);
        Customer script = customer.GetComponent<Customer>();
        script.desiredItem = database.GetRandomDrink();
        script.spawner = this;
        script.registerPoint = register.orderPoint;
        activeCustomers.Add(script);

        Debug.Log("Spawned customer");
    }

    public Customer GetPrevCustomerInQueue(Customer customer)
    {
        int index = -1;
        foreach (Customer curr in activeCustomers)
        {
            if ((index > -1) && (customer == curr))
            {
                while (activeCustomers[index] != null)
                {
                    if (activeCustomers[index].state == Customer.CustomerState.Queueing)
                    {
                        return activeCustomers[index];
                    }

                    index--;

                    if (index < 0)
                    {
                        return null;
                    }
                }
            }

            index++;
        }

        return null;
    }
    public Customer GetNextCustomer()
    {
        if (activeCustomers.Count == 0) return null;
        return activeCustomers[0];
    }

    public Vector3 GetRandomWaitingPosition()
    {
        if (register.orderPoint == null) return Vector2.zero;

        BoxCollider2D collider = register.waitingArea.GetComponent<BoxCollider2D>();
        Bounds bounds = collider.bounds;

        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            0f
        );
    }
}
