// using UnityEngine

// public enum InteractionType {Keyboard, LeapMotion}

// public class GameManager : MonoBehaviour
// {
//     public static InteractionType SelectedInteraction;

//     public static GameManager instance;

//     private int collectiblesCount;

//     void Awake()
//     {
//         if (instance == null){
//             instance = this;
//         }
//         else{
//             Destroy(gameObject)
//             return;
//         }
//         Time.timeScale = 0f;

//     }
//     public void StartGame(InteractionType type){
//         SelectedInteraction = type;
//         collectiblesCount = GameObject.FindGameObjectWithTag("Collectibles").Length;
        
//     }

//     public void CollectiblePickedUp(){

//     }
// }

