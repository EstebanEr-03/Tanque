using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvitarObstaculos : MonoBehaviour
{
    public float detectionDistance = 2.0f; // La distancia a la que el enemigo puede detectar el ladrillo.
    public LayerMask detectionLayer; // Capa en la que se encuentran los ladrillos para optimizar el raycast.
    private bool shouldTurnLeft = false; // Variable para alternar la direcci�n de giro

    void Update()
    {
        DetectAndAvoidBricks();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verifica si el objeto con el que ha colisionado es otro enemigo
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            Destroy(gameObject); // Destruye este enemigo
        }
    }

    private void DetectAndAvoidBricks()
    {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward) * detectionDistance;
        Debug.DrawRay(transform.position, forward, Color.green); // Dibuja un rayo en la vista de escena para depuraci�n

        // Realiza un raycast para detectar obst�culos
        if (Physics.Raycast(transform.position, transform.forward, out hit, detectionDistance, detectionLayer))
        {
            if (hit.collider.CompareTag("Brick") || hit.collider.CompareTag("Limites"))
            {
                AvoidObstacle(); // Llama a la funci�n de evasi�n si detecta un obst�culo
            }
        }
    }

    void AvoidObstacle()
    {
        // Decide si girar a la izquierda o a la derecha
        if (shouldTurnLeft)
        {
            transform.Rotate(0, -90, 0); // Gira 90 grados a la izquierda
            shouldTurnLeft = false; // Prepara para el pr�ximo giro a la derecha
        }
        else
        {
            transform.Rotate(0, 90, 0); // Gira 90 grados a la derecha
            shouldTurnLeft = true; // Prepara para el pr�ximo giro a la izquierda
        }

        // Actualiza la direcci�n de la velocidad para que coincida con la nueva direcci�n hacia adelante
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = transform.forward * 5.0f; // Asumiendo que 5.0f es la velocidad deseada
        }
    }
}
