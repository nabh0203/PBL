using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeTrigger : MonoBehaviour
{
    public GameObject itemPrefabs;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("delete") || other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Ex"))
        {
            Destroy(gameObject);
            Vector3 itemSpawnPosition = transform.position;
  
            Instantiate(itemPrefabs, itemSpawnPosition, Quaternion.identity);
        }
    }
}
