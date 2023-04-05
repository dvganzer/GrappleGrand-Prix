using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonController : MonoBehaviour
{
    public GameObject button;
    public Text countdownText;
    
    public float countdownDuration = 3f;
    public List<GameObject> objectsToActivate;
    public Animation anim;

    public LapManager lapMananager;

    public bool buttonPressed = false;
    public bool startLap = false;
    public float countdownTimer = 0f;
    public AudioSource music;
    public AudioSource city;

    public void OnButtonClick(InputAction.CallbackContext context)
    {
        if (context.performed && !buttonPressed)
        {
            anim.Play();
            countdownText.gameObject.SetActive(true);
            buttonPressed = true;
            countdownTimer = countdownDuration;

            // Set objectsToActivate to active
            foreach (GameObject obj in objectsToActivate)
            {
                obj.SetActive(true);
            }
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

