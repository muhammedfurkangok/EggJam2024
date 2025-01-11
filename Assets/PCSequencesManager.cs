using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PCSequencesManager : MonoBehaviour
{
    public void LoginButton(string password)
    {
        if(password == "1234")
        {
            Debug.Log("Login successful");
        }
        else
        {
            Debug.Log("Login failed");
        }
    }

    public void EnterTheFirstGame()
    {
        SceneManager.LoadScene("SurvivorsScene");
    }

    public void EnterTheSecondGame()
    {
        SceneManager.LoadScene("PuzzleScene");
    }

    
}
