using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    public float rotatepeedMovement = 0.0f;
    Tank hero = new Tank();
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            //Checking if the raycast shot hits something that uses the navmesh system.
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, hit.point, 3 * Time.deltaTime);
                Quaternion rotation = Quaternion.LookRotation(hit.point - transform.position, Vector3.up);
                transform.rotation = Quaternion.Slerp(rotation, transform.rotation, hero.rotationSpeed);
            }
        }
    }





    IEnumerator movement(float rotationSpeed)
    {
        RaycastHit hit;
        //Checking if the raycast shot hits something that uses the navmesh system.
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
        {         
            //rotation
            Quaternion rotationToLookAt = Quaternion.LookRotation(hit.point - transform.position);
            float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                rotationToLookAt.eulerAngles.y,
                ref hero.rotationSpeed,
                rotatepeedMovement * (Time.deltaTime * 5));
            transform.eulerAngles = new Vector3(0, rotationY, 0);

            yield return new WaitForSeconds(0.01f);
            Debug.Log(rotationToLookAt.eulerAngles.y - transform.eulerAngles.y);

            //movement
            //have the player move to the raycast/hit point.
            //agent.SetDestination(hit.point);
        }
    }
}
