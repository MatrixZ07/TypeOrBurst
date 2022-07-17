using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform playerTransform;
    Transform spriteTransform;

    Rigidbody2D rb;
    Vector2 force;
    public float rotationSpeed;
    //maxVerticalSpeed von 2.4f ist gut
    public float verticalSpeed = 0.1f;
    //Highscore/Level Referenz um Speed zu manipulieren

    // Start is called before the first frame update
    void Start()
    {

        playerTransform=GameObject.FindGameObjectWithTag("Player").transform;
        spriteTransform = transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Rotation
        Vector2 direction = spriteTransform.position - playerTransform.position ;
        direction.Normalize();
        Quaternion newRotation = Quaternion.LookRotation(Vector3.forward, direction);
        spriteTransform.rotation = Quaternion.RotateTowards(spriteTransform.rotation, newRotation, rotationSpeed*verticalSpeed * Time.deltaTime);

        //Movement
        rb.MovePosition(transform.position + spriteTransform.up.normalized*-1*verticalSpeed*Time.deltaTime);
        /*//transform.SetPositionAndRotation(new Vector3 (Mathf.Lerp(transform.position.x, playerTransform.position.x,Time.deltaTime/20f),transform.position.y,transform.position.z),Quaternion.identity);
        if (playerTransform.position.x - transform.position.x <= 0)
        {
            transform.Translate(-hspeed * Time.deltaTime, 0f, 0f);

        }
        else
        {
            transform.Translate(hspeed * Time.deltaTime, 0f, 0f);
        }*/


        //transform.Translate(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime, 0f);
        //transform.position = Vector3.Lerp(transform.position, playerTransform.position, Time.deltaTime * speed);
        //transform.Translate(0f, speed*Time.deltaTime, 0f);
    }
}
