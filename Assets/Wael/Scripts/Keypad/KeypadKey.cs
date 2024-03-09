using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadKey : MonoBehaviour
{

    [SerializeField] private string key;
    public void SendKey()
    {
        this.transform.GetComponentInParent<KeypadController>().PasswordEntry(key);
    }
}
