using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDespawner : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Despawner"))
        {
            // Desativa o objeto atual
            gameObject.SetActive(false);
        }

    }
}
