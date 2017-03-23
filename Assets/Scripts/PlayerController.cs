using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text finishedText;
    public Text timerText;
    public float seconds, minutes;
    public AudioSource aSource;
    public float nominalSpeed;

    private Rigidbody rb;
    private int count;
   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 12;
        nominalSpeed = 1f;
        timerText.text = "";
        SetCountText();
        finishedText.text = "";
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        Debug.Log(movement);
        rb.AddForce(movement * speed);
        moveSound();
        if (count != 0)
            SetTimerText();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count - 1;
            SetCountText();       
        }
    }

    void SetCountText()
    {
        countText.text = "Balls Remaining: " + count.ToString();
        if (count == 0)
        {
            finishedText.text = "Finished!";
        }
    }

    void SetTimerText()
    {
        minutes = (int)(Time.time / 60f);
        seconds = (int)(Time.time % 60f);
        timerText.text = "Time Elapsed: " + minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    void moveSound()
    {
        
        aSource.volume = rb.velocity.magnitude / nominalSpeed;
        //aSource.pitch = rb.velocity.magnitude / nominalSpeed;
    }
}

