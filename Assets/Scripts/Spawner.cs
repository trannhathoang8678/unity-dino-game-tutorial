using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject prefab;
        [Range(0f, 1f)]
        public float spawnChance;
    }

    public SpawnableObject[] objects;

    public float minSpawnRate = 1f;
    public float maxSpawnRate = 2f;

    private void OnEnable()
    {
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Spawn()
    {
        float spawnChance = Random.value;

        foreach (var obj in objects)
        {
            if (spawnChance < obj.spawnChance)
            {
                GameObject obstacle = Instantiate(obj.prefab);
                obstacle.transform.position = transform.position;
                //Debug.Log(obj.prefab.name);
                if (obj.prefab.name == "Bird_01")
                {
                    obstacle.transform.position = new Vector3(10f, 2f, 0f);
                }
                if (obj.prefab.name == "Meteorite_01")
                {
                    var x = Random.Range(-7f, 7f);
                    Debug.Log(x);
                    obstacle.transform.position = new Vector3(x, 5f, 0f);
                }
                if (obj.prefab.name == "Meteor")
                {
                    var x = Random.Range(0f, 10f);
                    Debug.Log(x);
                    obstacle.transform.position = new Vector3(x, 5f, 0f);
                }
                break;
            }

            spawnChance -= obj.spawnChance;
        }

        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

}
