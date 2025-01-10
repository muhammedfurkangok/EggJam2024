using UnityEngine;

public class ReplaceEyes : MonoBehaviour
{
    [SerializeField] private Transform[] children;
    [SerializeField] private GameObject replacement;

    private void Start()
    {
        if(children is null) return;
        foreach (Transform child in children)
        {
            Transform bucket = child;
            GameObject newEye = Instantiate(replacement, bucket.position, bucket.rotation,this.transform);
            Destroy(bucket.gameObject);
        }
    }
}
