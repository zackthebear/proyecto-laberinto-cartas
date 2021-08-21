using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public static Deck Instance;
    public Transform spawnPoint; 

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public int tamaño = 30; 

    public List<Cartas> deckcartas = new List<Cartas>();

    public bool Agregar(Cartas carta)
    {
        if (deckcartas.Count >= tamaño)
        {
            return false; 
        }

        deckcartas.Add(carta);
        return true; 
    }


    public void Usar(Cartas carta)
    {
        spawnPoint = GameObject.FindGameObjectWithTag("CardArea").transform;
        GameManager.Instance.HP += carta.HP;
        GameManager.Instance.STR += carta.STR;
        GameManager.Instance.DEF += carta.DEF;

        if(carta.instancear)
        {
            Instantiate(carta.creatura, spawnPoint.position, spawnPoint.rotation);
        }

        deckcartas.Remove(carta);
    }
}
