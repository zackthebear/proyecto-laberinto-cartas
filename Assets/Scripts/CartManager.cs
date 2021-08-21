using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CartManager : MonoBehaviour
{
    private Image imagen;
    private int cardNumber;
    private bool canUse;
    public Sprite transparent;
    // Start is called before the first frame update
    void Start()
    {
        imagen = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Deck.Instance.deckcartas.Count <= 0)
        {
            imagen.sprite = transparent;
            canUse = false;
            return;
        }

        if(cardNumber >= Deck.Instance.deckcartas.Count)
        {
            cardNumber = 0;
        }

        if (cardNumber < 0)
        {
            cardNumber = Deck.Instance.deckcartas.Count - 1;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            cardNumber--;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            cardNumber++;
        }

        if (cardNumber > -1)
        {
            if(cardNumber < Deck.Instance.deckcartas.Count)
            { 
            //imagen.sprite = Deck.Instance.deckcartas[cardNumber].imagen;
                canUse = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Z) && canUse)
        {
            Deck.Instance.Usar(Deck.Instance.deckcartas[cardNumber]);
        }
    }
}
