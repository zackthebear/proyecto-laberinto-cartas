using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject characterToFollow;
    private float hp = 10.0f;

    private void Start()
    {
        characterToFollow = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        agent.SetDestination(characterToFollow.transform.position);
        transform.LookAt(characterToFollow.transform);
    }

}
