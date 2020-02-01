using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Vector3 offset;
    Transform targetTransform;

    public float moveSpeed = 0.1f;
    public float rotateSpeed = 0.1f;

    public Vector3 target;

    public bool start = false;

    private void Update()
    {
        if(start == true)
        {
           // MoveToPosition(Vector3.zero);
            start = false;
        }



        float rotateStep = rotateSpeed * Time.deltaTime;
        // transform.rotation = Quaternion.RotateTowards(transform.rotation, target, rotateStep);

        Vector3 targetDirection = target - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateStep);
    }

    public void MoveToPosition(Vector3 moveToPosition)
    {
        StartCoroutine(moveToFace(moveToPosition));
    }





    IEnumerator moveToFace(Vector3 moveToPosition)
    {
        float rotateStep = rotateSpeed * Time.deltaTime;


       // while (transform.rotation != target)
        {
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, target, rotateStep);
        }


        yield return null;
    }
}
