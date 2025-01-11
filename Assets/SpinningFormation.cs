using UnityEngine;

public class SpinningFormation : MonoBehaviour
{
    [Header("Formation Settings")]
    [SerializeField] private float radius = 2f;
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private int initialObjectCount = 1;
    [SerializeField] private float objectScale = 1f;

    [Header("Runtime Properties")]
    [SerializeField] private Transform[] spawnedObjects;
    private float currentAngle;
    private const int MAX_OBJECTS = 8;

    private void Start()
    {
        // Initialize array
        spawnedObjects = new Transform[MAX_OBJECTS];

        // Spawn initial objects
        for (int i = 0; i < initialObjectCount; i++)
        {
            AddObject();
        }
    }

    private void Update()
    {
        // Update rotation
        //currentAngle += rotationSpeed * Time.deltaTime;

        // Update positions of all active objects
        UpdateObjectPositions();
    }

    public void AddObject()
    {
        // Find first empty slot
        int emptyIndex = -1;
        for (int i = 0; i < MAX_OBJECTS; i++)
        {
            if (spawnedObjects[i] == null)
            {
                emptyIndex = i;
                break;
            }
        }

        if (emptyIndex == -1)
        {
            Debug.LogWarning("Maximum number of objects (8) reached");
            return;
        }

        // Spawn new object
        GameObject newObj = Instantiate(objectPrefab, Vector3.zero,Quaternion.identity);
        newObj.transform.localScale = Vector3.one * objectScale;
        newObj.transform.parent = transform;
        newObj.transform.rotation = Quaternion.Euler(new(0f,0f,0f));
        spawnedObjects[emptyIndex] = newObj.transform;

        // Update positions to maintain equal spacing
        UpdateObjectPositions();
    }

    public void RemoveObject()
    {
        // Find last active object
        for (int i = MAX_OBJECTS - 1; i >= 0; i--)
        {
            if (spawnedObjects[i] != null)
            {
                Destroy(spawnedObjects[i].gameObject);
                spawnedObjects[i] = null;
                break;
            }
        }

        UpdateObjectPositions();
    }

    private void UpdateObjectPositions()
    {
        int activeCount = 0;

        // Count active objects
        for (int i = 0; i < MAX_OBJECTS; i++)
        {
            if (spawnedObjects[i] != null)
                activeCount++;
        }

        if (activeCount == 0) return;

        float arcBetweenObjects = 2f * Mathf.PI / activeCount;
        int currentIndex = 0;

        // Update positions
        for (int i = 0; i < MAX_OBJECTS; i++)
        {
            if (spawnedObjects[i] != null)
            {
                float angle = currentAngle + (currentIndex * arcBetweenObjects);
                Vector3 newPosition = new Vector3(
                    Mathf.Cos(angle) * radius,
                    0f,
                    Mathf.Sin(angle) * radius
                );
                spawnedObjects[i].localPosition = newPosition;
                currentIndex++;
            }
        }
    }

    private void ShootSaws()
    {
        if (spawnedObjects!=null)
        {
            for (int i = 0; i < spawnedObjects.Length; i++)
            {
                if (spawnedObjects[i] != null)
                {
                   
                }
            }
            
        }
    }

    // Public methods to control the formation
    public void SetRadius(float newRadius) => radius = newRadius;
    public void SetObjectScale(float newScale)
    {
        objectScale = newScale;
        for (int i = 0; i < MAX_OBJECTS; i++)
        {
            if (spawnedObjects[i] != null)
            {
                spawnedObjects[i].localScale = Vector3.one * objectScale;
            }
        }
    }
}