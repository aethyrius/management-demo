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
        script.spawner = this;
        script.order = database.GetRandomOrder(OrderManager.Instance.GetNextOrderNum());
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

        BoxCollider2D[] colliders = register.waitingArea.GetComponents<BoxCollider2D>();
        BoxCollider2D collider = colliders[Random.Range(0, colliders.Length)];

        if (collider == null) return register.waitingArea.transform.position;

        Bounds bounds = collider.bounds;

        Vector2 randomPoint = new Vector2(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y)
        );

        if (collider.OverlapPoint(randomPoint))
        {
            float z = register.waitingArea.transform.position.z;
            Debug.Log("Going to: " + "(" + randomPoint.x + ", " + randomPoint.y + ", " + z);
            return new Vector3(randomPoint.x, randomPoint.y, z);
        }

        return register.waitingArea.transform.position;
    }
}
