using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerCombat2D : MonoBehaviour
{
    [SerializeField]
    private GameObject leftAttack, rightAttack;
       
    public float attackRate = 2f;
    public float attackDuration = 0.4f;

    private async void Start()
    {
        do
        {
            await Attack();
            await UniTask.Delay((int)(attackRate * 1000) + (int)(attackDuration * 1000));
        }
        while (true);
    }

    // Update is called once per frame
    async UniTask Attack()
    {
        leftAttack.SetActive(true);
        rightAttack.SetActive(true);
        await UniTask.Delay((int)(attackDuration * 1000));
        leftAttack.SetActive(false);
        rightAttack.SetActive(false);
    }
}
