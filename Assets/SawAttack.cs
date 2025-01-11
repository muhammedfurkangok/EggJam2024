using Cysharp.Threading.Tasks;
using UnityEngine;

public class SawAttack : MonoBehaviour
{

    [Header("SAW")]
    [SerializeField]
    private Transform sawPivot;

    public float SawSpinRate = 2f;
    public float SawAttackDuration = 0.4f;

    private SpinningFormation spinnigSawProvider;
    private GameObject[] saws;

    private void Start()
    {
        spinnigSawProvider = GetComponent<SpinningFormation>();
    }

    async UniTaskVoid SawWeaponAttack() //TODO: ... devamke...
    {
        do
        {
            await Attack2();
            await UniTask.Delay((int)(SawAttackDuration * 1000));
        }
        while (true);
    }
    async UniTask Attack2()
    {
        //write me a circular saw attack where this object's origin is the center of the circle and the saw is spinning around it

    }
}