using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonController : MonoBehaviour
{
    public GameObject button;
    public Text countdownText;
    public Transform finishLine;
    public float countdownDuration = 3f;
    public Animation anim;
    public GameObject Signs;
    public GameObject Checkpoints;
    public LapManager lapMananager;

    public bool buttonPressed = false;
    public bool startLap = false;
    public float countdownTimer = 0f;
    public AudioSource music;
    public AudioSource city;
    public AudioSource countdown;


    public void OnButtonClick(InputAction.CallbackContext context)
    {
        if (context.performed && !buttonPressed)
        {
            anim.Play();
            countdownText.gameObject.SetActive(true);
            buttonPressed = true;
            countdownTimer = countdownDuration;
            transform.position = finishLine.position;
            countdown.Play();


            Instantiate(Signs);
            

        }



       
    }
    private void Start()
    {
        lapMananager = GetComponent<LapManager>();
        
    }
    private void Update()
    {
        countdownText.text = countdownTimer.ToString();
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.gameObject.name == "Button")
            {
                
                Debug.Log("BUTTON!");
            }
            if (buttonPressed)
            {
                countdownTimer -= Time.deltaTime;
                if (countdownTimer <= 0f)
                {
                    
                    lapMananager.lapStarted = true;
                    lapMananager.StartLap();
                    music.Play();
                    city.Stop();
                    Debug.Log("Countdown Finished!");
                    countdownText.gameObject.SetActive(false);
                    buttonPressed = false;
                }
            }
        }

    }
}

