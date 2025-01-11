using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PCSequencesManager : MonoBehaviour
{
    public void LoginButton(string password)
    {
        if (password == "1234")
        {
            Debug.Log("Login successful");
        }
        else
        {
            Debug.Log("Login failed");
        }
    }

    public async void EnterTheFirstGame()
    {
        await UniTask.WaitForSeconds(2f);
        SceneManager.LoadScene("SurvivorsScene");
    }

    public async void EnterTheSecondGame()
    {
        await UniTask.WaitForSeconds(2f);

        SceneManager.LoadScene("PuzzleScene");
    }
}