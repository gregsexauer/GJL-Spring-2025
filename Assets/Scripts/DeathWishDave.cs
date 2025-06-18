using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class DeathWishDave : MonoBehaviour
{
    [SerializeField] TimeOfDayManager timeOfDayManager;
    [SerializeField] float walkSpeed;
    [SerializeField] List<Transform> wayPoints;
    [SerializeField] GameManager gameManager;
    [SerializeField] SpriteRenderer spriteRenderer;
    int _waypointIndex;
    bool _isDead = false;

    private void Update()
    {
        if (timeOfDayManager.IsPaused || _isDead || _waypointIndex >= wayPoints.Count)
            return;

        MoveToPosition(wayPoints[_waypointIndex]);
    }

    void MoveToPosition(Transform wayPoint)
    {
        Vector2 moveTarget = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.z),
            new Vector2(wayPoint.transform.position.x, wayPoint.transform.position.z),
            walkSpeed * Time.deltaTime);

        transform.position = new Vector3(moveTarget.x, transform.position.y, moveTarget.y);

        if (transform.position.x.Equals(wayPoint.transform.position.x)
            && transform.position.z.Equals(wayPoint.transform.position.z))
        {
            _waypointIndex++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Piano")
        {
            StartCoroutine(Flatten());
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Electricity")
        {
            StartCoroutine(Electrocute());
        }
    }

    IEnumerator Flatten()
    {
        _isDead = true;
        transform.DOScaleY(.1f, .1f);
        yield return new WaitForSeconds(.1f);
        gameManager.FailLoop("Piano");
    }

    IEnumerator Electrocute()
    {
        _isDead = true;
        for (int i = 0; i <= 3; i++)
        {
            yield return new WaitForSeconds(.05f);
            spriteRenderer.color = Color.black;
            yield return new WaitForSeconds(.05f);
            spriteRenderer.color = Color.white;
        }

        gameManager.FailLoop("Electrocution");

        for (int i = 0; i <= 99; i++)
        {
            yield return new WaitForSeconds(.05f);
            spriteRenderer.color = Color.black;
            yield return new WaitForSeconds(.05f);
            spriteRenderer.color = Color.white;
        }
    }
}
