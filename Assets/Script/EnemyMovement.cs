using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D enemRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        enemRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x > 0)
        {
            enemRigidBody.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            enemRigidBody.velocity = new Vector2(-moveSpeed, 0f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(enemRigidBody.velocity.x)), 1f);
    }
}
