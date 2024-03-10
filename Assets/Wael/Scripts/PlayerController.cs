using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Metrics")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float groundAcceleration = 30f;
    [SerializeField] private float airAcceleration = 10f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravity = 15f;
    [SerializeField] private float AuthorizedJumpDelay = 0.2f;//Permet d'ajouter un d�lais pour que le joueur puisse sauter un certain temps apr�s avoir quitter la plateforme

    [Header("Inputs Binding")]
    [SerializeField] private string HorizontalInput = "Horizontal";
    [SerializeField] private string VerticalInput = "Vertical";
    [SerializeField] private string RunInput = "Fire3";
    [SerializeField] private string JumpInput = "Jump";

    private CharacterController controller;

    private Vector3 velocity;
    private float verticalVelocity;
    private Vector3 moveInput;
    private bool isSliding;
    private float lastTimeGrounded;
    private bool jumpUsed;


    //Calcul de la vitesse de saut par rapport � la gravit� et la hauteur de saut souhaiter : Vitesse = racineCarr�(2 x Gravit� x Hauteur)
    private float jumpSpeed => Mathf.Sqrt(2 * jumpHeight * gravity);

    //Appeler une fois lors du chargement de ce script, utile pour l'initialisation
    void Start()
    {
        //R�cup�ration de la r�f�rence au CharacterController (l'�l�ment qui g�re la physique)
        controller = GetComponent<CharacterController>();
    }

    //Appeler 50 fois par seconde, permet de calculer la physique de mani�re pr�cise
    void FixedUpdate()
    {
        velocity = controller.velocity;//Recup�ration de la vitesse r�el du personnage apr�s la simulation (prend en compte les collisions)
        velocity.y = 0;//Suppression de la composante verticale, nous utilisons verticalVelocity pour garder le contr�le

        GroundCheck();
        ProcessInput();//Transforme ZQSD en un Vector3, plus facile � manipuler
        ProcessMove();//Applique le mouvement par rapport � l'input
        ProcessGravity();//Calcul la gravit�
        ProcessSlide();//Fait glisser le personnage lorsqu'il est sur un sol trop pentu


        //Application de la velocit� calculer
        velocity.y = verticalVelocity;
        controller.Move(velocity * Time.fixedDeltaTime);
    }

    //Appeler � chaque rafraichissement de l'image
    private void Update()
    {
        //Les inputs de type GetButtonDown et GetButtonUp sont appel�es sur des frames uniques, il ne peuvent pas �tre g�rer dans FixedUpdate()
        ProcessJump();//Fait sauter le personnage en fonction de l'input

        //Ici on r�applique un mouvement "factice" pour �viter que le joueur est l'impression que le personnage se d�place � 50FPS (fr�quence de la frame physique)
        transform.position += controller.velocity * Time.deltaTime;
    }


    void ProcessInput()//Transforme ZQSD en un Vector3, plus facile � manipuler
    {
        moveInput.x = Input.GetAxisRaw(HorizontalInput);
        moveInput.z = Input.GetAxisRaw(VerticalInput);
        moveInput.Normalize();
    }

    void ProcessMove()//Applique le mouvement par rapport � l'input
    {
        //Si le bouton RunInput est press�, on utilise la vitesse de course, sinon la vitesse de marche
        float currentSpeed = Input.GetButton(RunInput) ? runSpeed : walkSpeed;

        Vector3 wantedVelocity = transform.TransformDirection(moveInput) * currentSpeed;//La velocit� d�sir�e par le joueur selon ces inputs et la direction de son perso.
        Vector3 diffVelocity = wantedVelocity - velocity; //L'acc�leration n�cessaire pour obtenir la vitesse souhaiter

        //Si le joueur est au sol, on utilise l'acceleration au sol, sinon l'acceleration des airs;
        float acceleration = controller.isGrounded ? groundAcceleration : airAcceleration;

        float accelForFrame = acceleration * Time.fixedDeltaTime;//L'acceleration maximum autoris�e

        if (diffVelocity.magnitude > accelForFrame)//Limite l'acceleration souhait�e pour la faire correspondre au maximum autoris�.
            diffVelocity = diffVelocity.normalized * accelForFrame;

        velocity += diffVelocity;//Application du calcul � la v�locit� finale;
    }

    void ProcessGravity()//Calcul la gravit�
    {
        //On empeche la vitesse vertical d'�tre inf�rieur � z�ro si le joueur est au sol
        if (controller.isGrounded && !isSliding && verticalVelocity < -0.1f)//On utilise -0.1 plutot que 0 car le calcul de CharacterControler.isGrounded � besoin d'aller vers le sol pour avoir une bonne valeur
            verticalVelocity = -0.1f;
        else
            verticalVelocity -= gravity * Time.fixedDeltaTime;//On applique la gravit�
    }

    void ProcessJump()//Fait sauter le joueur selon l'input
    {
        if (isSliding || jumpUsed) return;//Annuler le saut si le joueur est dans une glissade ou s'il a d�j� utiliser son saut post-plateforme
        if (!controller.isGrounded && Time.time - lastTimeGrounded > AuthorizedJumpDelay) return; //Si le joueur n'est plus au sol & qu'il a d�passer le delais pour sauter, on empeche le saut
        if (Input.GetButtonDown(JumpInput))//Si on a appuyer sur le saut � cette frame (different de GetButton => Si la touche est maintenu)
        {
            verticalVelocity = jumpSpeed;
            jumpUsed = true;
        }
    }

    void GroundCheck()//Test si le joueur est au sol, utile pour le delais de saut
    {
        if (verticalVelocity > 0) return;//Le CharacterController � une frame de retard sur le isGrounded, permet d'�viter de faire un double saut
        if (controller.isGrounded)//Si le joueur est au sol
        {
            lastTimeGrounded = Time.time;//On enregistre le timing actuel
            jumpUsed = false;//On r�initialise le fait que le joueur peut sauter
        }
    }

    void ProcessSlide()//Fait glisser le personnage sur les zone trop pentu
    {
        if (!controller.isGrounded)//Ne fait rien si le personnage est en l'air
        {
            isSliding = false;
            return;
        }

        Ray ray = new Ray(transform.position + Vector3.up, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 2))//Obtention d'information sur le sol et sur sa pr�sence
        {
            float angle = Vector3.Angle(hit.normal, Vector3.up);//Calcul de l'angle de la pente
            isSliding = angle > controller.slopeLimit;
            if (isSliding)
            {
                velocity = Vector3.ProjectOnPlane(new Vector3(0, verticalVelocity, 0), hit.normal);//Calcul de la vitesse de glissade selon la vitesse de chute
                velocity.y = 0;//Suppression de la composante verticale, d�j� contenu dans verticalVelocity
            }
        }
        else//R�initialisation du sliding pour ne pas empecher le joueur de sauter si son personnage est bloqu� dans un trou
            isSliding = false;
    }

}
