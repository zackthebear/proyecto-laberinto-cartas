using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    public GameObject parent;
    private float hp = 15.0f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            float str = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControllerNapoleon>().str;
            hp -= str;
            if (hp <= 0)
                Destroy(parent);

            Destroy(other.gameObject);
        }
    }
}
