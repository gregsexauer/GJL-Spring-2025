using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class DeathWishDave : MonoBehaviour
{
    [SerializeField] TimeOfDayManager timeOfDayManager;
    [SerializeField] float walkSpeed;
    [SerializeField] List<Transform> wayPoints;
    [SerializeField] GameManager gameManager;
    [SerializeField] Animator animator;
    [SerializeField] GameObject cigar;
    [SerializeField] Sprite litCigar;
    [SerializeField] Sprite smokingCigar;
    [SerializeField] GameObject dynamite;
    [SerializeField] Sprite litDynamite;
    [SerializeField] Sprite explodedDynamite;
    [SerializeField] AudioClip zap;
    [SerializeField] AudioClip explode;
    int _waypointIndex;
    bool _isDead = false;
    AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

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
        else if (other.gameObject.name == "Dynamite")
        {
            StartCoroutine(Smoke(other.GetComponent<Dynamite>().IsCigar));
            other.gameObject.SetActive(false);
        }
    }

    IEnumerator Flatten()
    {
        Die();
        animator.SetTrigger("Idle");
        transform.DOScaleY(.1f, .15f);
        yield return new WaitForSeconds(.15f);
        gameManager.FailLoop("Piano");
    }

    IEnumerator Electrocute()
    {
        Die();
        _audioSource.clip = zap;
        _audioSource.Play();
        animator.SetTrigger("Electrocute");

        yield return new WaitForSeconds(.5f);

        gameManager.FailLoop("Electrocution");

        yield return new WaitForSeconds(.5071f);

        _audioSource.Play();
    }

    IEnumerator Smoke(bool isCigar)
    {
        GetComponent<DialogueInteractable>().IsActive = false;
        animator.SetTrigger("Smoke");
        if (isCigar)
        {
            cigar.SetActive(true);
            yield return new WaitForSeconds(1f);
            cigar.GetComponent<SpriteRenderer>().sprite = litCigar;
            yield return new WaitForSeconds(2f);
            cigar.GetComponent<SpriteRenderer>().sprite = smokingCigar;
            gameManager.CompleteQuest("Dave");
        }
        else
        {
            dynamite.SetActive(true);
            yield return new WaitForSeconds(1f);
            dynamite.GetComponent<SpriteRenderer>().sprite = litDynamite;
            _audioSource.PlayOneShot(explode);
            yield return new WaitForSeconds(2f);
            dynamite.GetComponent<SpriteRenderer>().sprite = explodedDynamite;
            animator.SetTrigger("Explode");
            yield return new WaitForSeconds(.5f);
            gameManager.FailLoop("Dynamite");
        }
    }

    void Die()
    {
        _isDead = true;
        GetComponent<DialogueInteractable>().IsActive = false;
    }
}
