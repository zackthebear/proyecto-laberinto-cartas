using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NuevaCarta", menuName = "Cartas/Nueva carta")]
public class Cartas : ScriptableObject
{
    public string nombre;
    public Texture2D imagen;
    public bool instancear;

    // Stats
    public int HP;
    public int STR;
    public int DEF;
    public int SPEED;

    // Instancear
    public Transform creatura;

}
