// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Collectible : MonoBehaviour
// {
//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     void OnTriggerEnter(Collider other){
//         if (other.gameObject.CompareTag("Player")){
//             Destroy(gameObject);
//         }
//         // Destroy(gameObject);
//     }
//     // Update is called once per frame
//     void Update()
//     {
//         transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
//     }
// }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private CollectibleManager manager;

    void Start()
    {
        // Find manager in parent or scene
        manager = GetComponentInParent<CollectibleManager>();
        if (manager == null)
            manager = FindObjectOfType<CollectibleManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Notify manager, then destroy/hide self
            if (manager != null)
                manager.OnCollectibleCollected();
            else
                Debug.LogError("CollectibleManager not found!");

            // Optionally destroy, but since manager disables, you can just hide
            // Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
    }
}