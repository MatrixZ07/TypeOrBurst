using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	Transform playerTransform;
    //maxVerticalSpeed von 2.4f ist gut
    public float verticalSpeed = 0.1f;

    void Start()
    {
        playerTransform=GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Rotate();
        Move();
    }

    private void Rotate()
    {
        Vector2 direction = transform.position - playerTransform.position ;
        direction.Normalize();

        Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * verticalSpeed * Time.deltaTime);
    }

	private void Move()
	{
		rb.MovePosition(base.transform.position - (transform.up.normalized * verticalSpeed * Time.deltaTime));
	}

	[SerializeField]
	private float rotationSpeed = 2.1f;
	private Rigidbody2D rb;
}
