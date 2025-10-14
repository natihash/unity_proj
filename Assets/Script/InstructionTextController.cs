using UnityEngine;
using TMPro;

public class InstructionTextController : MonoBehaviour
{
    public TextMeshProUGUI instructionText;
    public GameObject player; // Assign your player GameObject
    private bool hasMoved = false;

    void Start()
    {
        // if (instructionText != null)
        // {
        //     instructionText.text = "Use arrow keys or Leap Motion to move the ball.\nCollect all the collectibles!";
        //     instructionText.gameObject.SetActive(true);
        // }
    }

    void Update()
    {
        if (!hasMoved)
        {
            // Keyboard movement detection
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                HideInstructions();
            }
            // Leap motion movement detection (assumes BallMovement script is used)
            else if (BallMovement.SelectedInteraction == InteractionType.LeapMotion)
            {
                // Check if the ball's rigidbody is moving
                Rigidbody rb = player.GetComponent<Rigidbody>();
                if (rb != null && rb.velocity.magnitude > 0.05f)
                {
                    HideInstructions();
                }
                // Or if the ball is grabbed, you could also hide instructions
                // (requires access to BallMovement.isHoldingBall)
            }
        }
    }

    void HideInstructions()
    {
        hasMoved = true;
        if (instructionText != null)
        {
            instructionText.gameObject.SetActive(false);
        }
    }
}