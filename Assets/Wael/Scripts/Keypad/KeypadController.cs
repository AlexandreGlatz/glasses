using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class KeypadController : MonoBehaviour
{
    public DoorController doorLinkedToPad;
    [SerializeField] private string password;
    public string passwordTestedOnKeypad = "";
    public TMP_Text displayedPassword;

    [SerializeField] private GameObject greenLight;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip correctSound;
    public AudioClip hitKeySound;
    private void Start()
    {
        displayedPassword.text = "";
    }

    private void Clear()
    {
        passwordTestedOnKeypad = "";
        displayedPassword.text = "";
    }
    public void PasswordEntry(string number)
    {
        if (audioSource != null)
            audioSource.PlayOneShot(hitKeySound);
        
        int passwordLength = passwordTestedOnKeypad.Length;
        if (passwordLength < 4)
        {
            passwordTestedOnKeypad = passwordTestedOnKeypad + number;
            if (passwordTestedOnKeypad.Length == 4)
            {
                if (passwordTestedOnKeypad == password)
                {
                    greenLight.SetActive(true);
                    if (audioSource != null)
                        audioSource.PlayOneShot(correctSound);

                    doorLinkedToPad.lockedByPassword = false;
                    doorLinkedToPad.OpenClose();

                    StartCoroutine(waitAndClear());
                }
                else
                {
                    StartCoroutine(waitAndClear());
                }
            }
        }
        
        displayedPassword.text = passwordTestedOnKeypad;
    }

    IEnumerator waitAndClear()
    {
        yield return new WaitForSeconds(0.75f);
        Clear();
        greenLight.SetActive(false);
    }
}
