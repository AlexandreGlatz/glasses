using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter2 : MonoBehaviour
{
    public Player_Movement playerTP;

    public Transform player;
    public Transform reciever;

    private bool playerIsOverlapping = false;

    // Update is called once per frame
    void Update()
    {
        
        
            

    }

    void OnTriggerEnter(Collider other)
    {

        print(playerIsOverlapping + " 1");
        print(playerTP.isTpAllowed + " 1a");

        //le joueur doit se tp 1 fois, je dois mettre une sécurité qui évite de TP plusieurs fois --> isTpAllowed vrai et joueur qui passe
        if (other.tag == "Player" && playerTP.isTpAllowed == true)
        {


            //Je dis que le joueur traverse le TP
            playerIsOverlapping = true;

            print(playerIsOverlapping + " 2");
            print(playerTP.isTpAllowed + " 2a");

            if (playerIsOverlapping && playerTP.isTpAllowed == true)
            {

                print("first ok");
                Vector3 portalToPlayer = player.position - transform.position;
                float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

                // If this is true: The player has moved across the portal
                //if (dotProduct < 0)
                //{
                //print("Enter");
                // Teleport him!
                float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);
                rotationDiff += 180;
                player.Rotate(Vector3.up, rotationDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                player.position = reciever.position + positionOffset;

                playerTP.isTpAllowed = false;
                playerIsOverlapping = false;

                //}
            }
        }
        else
        {
            print("belekkkkkkkkkkkkkkkkkkkkkkkkkkkkkk");
            playerIsOverlapping = false;
            print(playerIsOverlapping + " 3");
            print(playerTP.isTpAllowed + " 3a");
        }
    }

    void OnTriggerExit(Collider other)
    {
        print(playerIsOverlapping + " 4");
        print(playerTP.isTpAllowed + " 4a");
        if (other.tag == "Player" && playerIsOverlapping == true)
        {
            playerTP.isTpAllowed = false;
            print("exit");
            playerIsOverlapping = false;
            print(playerIsOverlapping + " 5");
            print(playerTP.isTpAllowed + " 5a");

        }
    }
}
