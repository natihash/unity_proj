using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public List<GameObject> collectibles; // Assign in Inspector or find in Start()
    private int currentIndex = 0;

    void Start()
    {
        // Option 1: Assign collectibles in Inspector
        // Option 2: Automatically find collectibles
        if (collectibles.Count == 0)
        {
            foreach (Transform child in transform)
            {
                collectibles.Add(child.gameObject);
            }
        }

        // Disable all collectibles except the first
        for (int i = 0; i < collectibles.Count; i++)
        {
            collectibles[i].SetActive(i == 0);
        }
    }

    // Called by Collectible when collected
    public void OnCollectibleCollected()
    {
        // Hide current collectible
        collectibles[currentIndex].SetActive(false);
        currentIndex++;

        // Show next collectible if exists
        if (currentIndex < collectibles.Count)
        {
            collectibles[currentIndex].SetActive(true);
        }
        else
        {
            // All collected, trigger win logic if needed
            Debug.Log("All collectibles collected!");
        }
    }
}