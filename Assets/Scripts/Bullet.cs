using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10.0f; // Velocidad de la bala
    public float lifetime = 2.0f; // Tiempo de vida de la bala

    private void Start()
    {
        // Destruir la bala despu�s de un tiempo
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Mover la bala hacia adelante
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Detectar colisiones con otros objetos
        // Puedes agregar aqu� l�gica adicional, como da�o a un enemigo o efectos de part�culas
        // Por ejemplo, puedes destruir la bala al colisionar con un enemigo
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
