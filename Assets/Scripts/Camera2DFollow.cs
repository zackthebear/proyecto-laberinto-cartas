using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2DFollow : MonoBehaviour
{
    public float yPosition = 17.0f;
    private void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        //Vector3 target = new Vector3(player.position.x, verticalDistance, player.position.z - distance);
        //Vector3 newPos = Vector3.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
        transform.position = new Vector3(playerPosition.x, yPosition, playerPosition.z - 3.0f);
    }
}
