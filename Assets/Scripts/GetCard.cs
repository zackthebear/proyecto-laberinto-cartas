using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCard : MonoBehaviour
{
    private bool inside;
    public Cartas carta;
    private bool conseguir;
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        {
            if (other.tag == "Player")
            {
                inside = false;
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if(inside && Input.GetKeyDown(KeyCode.X))
        {
            conseguir = Deck.Instance.Agregar(carta);

            if(conseguir)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
