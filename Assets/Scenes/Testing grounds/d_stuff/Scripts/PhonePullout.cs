using UnityEngine;

public class PhonePullout : MonoBehaviour
{
    public Animator animator;
    public GameObject phone;
    public GameObject pullOutButton;
    public GameObject phoneOffButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        phoneOffButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PullOut()
    {
        pullOutButton.SetActive(false);
        animator.SetBool("pullOut", true);
        animator.SetBool("PhoneOff", false);
        phoneOffButton.SetActive(true);
    }

    public void PhoneOff()
    {
        animator.SetBool("pullOut", false);
        animator.SetBool("PhoneOff", true);
        pullOutButton.SetActive(true);
    }
}