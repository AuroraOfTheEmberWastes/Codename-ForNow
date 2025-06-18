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

    void Start()
    {
        phoneOffButton.SetActive(false);
    }

    public void PullOut()
    {
        pullOutButton.SetActive(false);
        animator.SetBool("pullOut", true);
        animator.SetBool("PhoneOff", false);
        phoneOffButton.SetActive(true);

        StartCoroutine(PulloutEnable());
    }

    public void PhoneOff()
    {
        animator.SetBool("pullOut", false);
        animator.SetBool("PhoneOff", true);
        pullOutButton.SetActive(true);

        StartCoroutine(PhoneOffEnable());
    }

    private IEnumerator PulloutEnable()
    {
        yield return new WaitForSeconds(1f);
        hungerUI.SetActive(true);
        happinessUI.SetActive(true);
    }

    private IEnumerator PhoneOffEnable()
    {
        yield return new WaitForSeconds(1f);
        hungerUI.SetActive(false);
        happinessUI.SetActive(false);    
    }
}
