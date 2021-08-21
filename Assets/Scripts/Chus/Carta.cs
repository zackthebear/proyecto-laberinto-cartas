using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carta : MonoBehaviour
{
    public Cartas Stats;

    public void Start()
    {
        Material material = new Material(Shader.Find("Unlit/Texture"));
        material.mainTexture = (Texture)Stats.imagen;
        gameObject.GetComponent<MeshRenderer>().material = material;
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            Destroy(gameObject);
        }
    }
}
