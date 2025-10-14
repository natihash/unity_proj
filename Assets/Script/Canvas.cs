using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas : MonoBehaviour
{
    public GameObject collectibles;
    public int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 12 - collectibles.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        // new_score = 12 - collectibles.transform.childCount;
    }
}
