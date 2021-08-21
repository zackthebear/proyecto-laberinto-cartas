using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject[] cartas;
    public Vector3[] spawns;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawns.Length; i++)
        {
            int randomIndexCarta = Random.Range(0, cartas.Length - 1);
            Instantiate(cartas[randomIndexCarta], spawns[i], Quaternion.identity);
        }
    }

}
