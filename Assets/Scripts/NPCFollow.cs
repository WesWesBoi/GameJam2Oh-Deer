using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFollow : MonoBehaviour
{

    public float speed = 3f;

    public GameObject Garbage;



    void Update()

    {

        if (Garbage == null) return;



        // move toward garbage

        transform.position = Vector3.MoveTowards(transform.position, Garbage.transform.position, speed * Time.deltaTime);



        // rotate to face player

        Vector3 direction = Garbage.transform.position - transform.position;

        direction.y = 0; // keep upright

        if (direction.magnitude > 0.1f)

            transform.rotation = Quaternion.LookRotation(direction);

    }



    private void OnCollisionEnter(Collision collision)

    {

        if (collision.gameObject.tag.Equals("Garbage"))

        {

            Destroy(collision.gameObject);

        }

    }

}