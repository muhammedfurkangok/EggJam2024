using DG.Tweening;
using System.Collections;
using UnityEngine;

public class EyeDoLookAt : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform eyeParent;
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private float minBlinkTime = 0.2f;
    [SerializeField] private float maxBlinkTime = 1f;

    private void Start()
    {
        StartCoroutine(BlinkRoutine());
        transform.rotation.eulerAngles.Set(105, 0, 0);
        target = FindFirstObjectByType<PlayerController>().transform;
    }

    private void Update()
    {
        eyeParent.LookAt(target.position);
    }

    private IEnumerator BlinkRoutine()
    {
        while (true)
        {
            float blinkTime = UnityEngine.Random.Range(minBlinkTime, maxBlinkTime);
            yield return new WaitForSeconds(blinkTime);
            characterAnimator.SetTrigger("Blink");
        }
    }
}