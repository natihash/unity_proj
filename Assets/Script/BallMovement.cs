using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Leap;
using UnityEngine.SceneManagement;
// using Leap.Unity; // It's good practice to include this namespace

public enum InteractionType {Keyboard, LeapMotion}

public class BallMovement : MonoBehaviour
{
    public LeapProvider leapProvider;
    public Chirality handToTrack = Chirality.Right;

    public float grabDistance = 1.0f; 
    public float grabThreshold = 0.5f;
    public float throwForceMultiplier = 2.0f;
    public float releaseThreshold = 0.5f;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI modeText;
    public GameObject restartButton;

    private Rigidbody rb;
    private int score;
    private bool isHoldingBall = false;
    private Hand currentlyTrackedHand = null;

    // public static InteractionType SelectedInteraction;
    public static InteractionType SelectedInteraction = InteractionType.Keyboard; // Default to Keyboard

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        score = 0;
        UpdateScoreText();
        UpdateModeText();

        if (winText != null)
        {
            winText.gameObject.SetActive(false);
        }

        if (restartButton != null) // <-- Hide restart button at start
        {
            restartButton.SetActive(false);
        }
    }

    void Update()
    {

        if (SelectedInteraction == InteractionType.Keyboard)
        {
            float z = Input.GetAxis("Vertical");
            float x = Input.GetAxis("Horizontal");
            Vector3 forces = new Vector3(x, 0, z);
            rb.AddForce(forces);
        }
        else // It must be LeapMotion
        {
            Frame currentFrame = leapProvider.CurrentFrame;
            Hand hand = currentFrame.GetHand(handToTrack);
            if (isHoldingBall)
            {
                HandleBallHeld(hand);
            }
            else
            {
                leapProvider.transform.position = rb.transform.position;
                leapProvider.transform.position -= new Vector3(0.0f, 2.0f, 0.0f);
                HandleBallFree(hand);
            }
        }

        
    }

    public void SwitchInteractionType()
    {
        if (SelectedInteraction == InteractionType.Keyboard)
        {
            SelectedInteraction = InteractionType.LeapMotion;
        }
        else
        {
            SelectedInteraction = InteractionType.Keyboard;
        }

        UpdateModeText();
    }


    private void HandleBallFree(Hand hand)
    {
        if (hand != null)
        {
            float distanceToHand = Vector3.Distance(transform.position, hand.PalmPosition);

            if (distanceToHand < grabDistance && hand.GrabStrength > grabThreshold)
            {
                GrabBall(hand);
            }
        }
    }

    private void HandleBallHeld(Hand hand)
    {
        transform.position = hand.PalmPosition;

        if (hand.GrabStrength < releaseThreshold)
        {
            ThrowBall(hand);
        }
        
    }

    private void GrabBall(Hand hand)
    {
        isHoldingBall = true;
        currentlyTrackedHand = hand;
        rb.isKinematic = true;
        Debug.Log("Ball Grabbed!");
    }

    private void ThrowBall(Hand hand)
    {
        isHoldingBall = false;
        rb.isKinematic = false; // Re-enable physics

        // Apply the hand's velocity to throw the ball
        // If the hand is null (e.g., tracking lost), it will just drop with 0 velocity.
        if (hand != null)
        {
            rb.velocity = hand.PalmVelocity * throwForceMultiplier;
            Debug.Log("Ball Thrown with velocity: " + rb.velocity);
        }
        
        currentlyTrackedHand = null;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectibles"))
        {
            score++;
            UpdateScoreText();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
        if (score >= 12 && winText != null)
        {
            winText.gameObject.SetActive(true);
            if (restartButton != null)
            {
                restartButton.SetActive(true);
            }
        }
    }
    private void UpdateModeText()
    {
        if (modeText != null)
        {
            modeText.text = "Mode: " + SelectedInteraction.ToString();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}