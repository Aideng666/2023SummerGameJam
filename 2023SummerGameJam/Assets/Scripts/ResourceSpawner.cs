using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{

    public int numFruit, numWood;
    public GameObject fruitPrefab, woodPrefab;
    public float radius;

    //The GameObjects to place the resouces under to keep organized
    public GameObject fruitObjects, woodObjects;

    private Vector3[] randomLocs;
    // Start is called before the first frame update
    void Start()
    {
        int n = numFruit + numWood;
        randomLocs = createSpawnPoints(n);
        for (int i = 0; i < n; i++)
        {
            Quaternion randomRotation = Quaternion.Euler(Random.Range(0.0f, 360f), 0, 0);
            GameObject resource;
            if (i < numFruit) {
                resource = Instantiate(fruitPrefab, randomLocs[i], randomRotation);
                resource.transform.parent = fruitObjects.transform;
                //Debug.Log("Spawned resource " + i);
            }
            else
            {
                resource = Instantiate(woodPrefab, randomLocs[i], randomRotation);
                resource.transform.parent = woodObjects.transform;
                //Debug.Log("Spawned resource " + i);
            }
        }
    }

    Vector3[] createSpawnPoints(int n)
    {
        Vector3[] randomLocs = new Vector3[numFruit + numWood];
        for (int i = 0; i < n; i++)
        {
            Vector2 randomXZ = Random.insideUnitCircle * radius;
            Vector3 raycastPoint = transform.position + new Vector3(randomXZ.x, 100, randomXZ.y);
            RaycastHit hit;
            Physics.Raycast(raycastPoint, Vector3.down, out hit);
            randomLocs[i] = hit.point + new Vector3(0.0f, 1.0f, 0.0f);
        }
        return randomLocs;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
