using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
       public GameObject entityToSpawn;

       public ScriptableForm [] spawnManagerValues;

       int instanceNumber = 1;

       public Text t;

    IEnumerator Start()
        {
            yield return StartCoroutine(Text321.set321(t));
            yield return StartCoroutine(Text321.set321Start(t));
            SpawnElement();
        }

    public void SpawnElement()
        {
            int r = Random.Range(0, spawnManagerValues.Length);
            instanceNumber = 1;
            GameObject currentEntity;
            GameObject element = new GameObject();
            for (int i = 0; i < 4; i++)
            {
            // Creates an instance of the prefab at the current spawn point.
            element.transform.position = transform.position;
            currentEntity = Instantiate(entityToSpawn, transform.position+spawnManagerValues[r].spawnPoints[i], Quaternion.identity);

            // Sets the name of the instantiated entity to be the string defined in the ScriptableObject and then appends it with a unique number. 
            currentEntity.name = spawnManagerValues[r].prefabName + instanceNumber;


            element.name = "Current Figure";
            currentEntity.transform.parent = element.transform;

                // Moves to the next spawn point index. If it goes out of range, it wraps back to the start.

                instanceNumber++;
            }
        }
}
