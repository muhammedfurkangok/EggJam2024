using System;
using UnityEngine;

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

    
}
