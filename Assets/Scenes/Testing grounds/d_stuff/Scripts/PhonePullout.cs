using UnityEngine;
using System.Collections;

public class PhonePullout : MonoBehaviour
{
    public Animator animator;
    public GameObject phone;
    public GameObject pullOutButton;
    public GameObject phoneOffButton;
    public GameObject hungerUI;
    public GameObject happinessUI;
    public GameObject extra;
    public GameObject cypher;

    void Start()
    {
        phoneOffButton.SetActive(false);
    }

    public void PullOut()
    {
        //pullOutButton.SetActive(false);
        animator.SetBool("pullOut", true);
        animator.SetBool("PhoneOff", false);

        StartCoroutine(PulloutEnable());
    }

    public void PhoneOff()
    {
        animator.SetBool("pullOut", false);
        animator.SetBool("PhoneOff", true);
        //pullOutButton.SetActive(true);

        StartCoroutine(PhoneOffEnable());
    }

    private IEnumerator PulloutEnable()
    {
        yield return new WaitForSeconds(1f);
        if (cypher != null)
        {
            cypher.SetActive(true);
        }
        hungerUI.SetActive(true);
        happinessUI.SetActive(true);
        phoneOffButton.SetActive(true);
        extra.SetActive(true);

    }

    private IEnumerator PhoneOffEnable()
    {
        yield return new WaitForSeconds(1f);
        if (cypher != null)
        {
            cypher.SetActive(false);
        }
        phoneOffButton.SetActive(false);
        hungerUI.SetActive(false);
        happinessUI.SetActive(false);
        extra.SetActive(false);
    }
}
