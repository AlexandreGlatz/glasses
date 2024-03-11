using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllowTP2 : MonoBehaviour
{
    public Player_Movement player;
    private bool isTpAllowed = true;

    private void OnTriggerEnter(Collider other)
    {
        player.isTpAllowed = true;
    }

}
