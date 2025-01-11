using Cysharp.Threading.Tasks;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    [Header("SWORD")]
    [SerializeField]
    private GameObject swordLeft, swordRight;

    public float SwordAttackRate = 2f;
    public float SwordAttackDuration = 0.4f;

    private void Start()
    {
        SwordWeaponAttack().Forget(); // Forget ile ateşle. Fire and Forget
    }
    #region Attack1:SwordWeaponAttack
    async UniTaskVoid SwordWeaponAttack()
    {
        do
        {
            await Attack1();
            await UniTask.Delay((int)(SwordAttackRate * 1000) + (int)(SwordAttackDuration * 1000));
        }
        while (true);
    }
    async UniTask Attack1()
    {
        swordLeft.SetActive(true);
        swordRight.SetActive(true);
        await UniTask.Delay((int)(SwordAttackDuration * 1000));
        swordLeft.SetActive(false);
        swordRight.SetActive(false);
    }
    #endregion
}
