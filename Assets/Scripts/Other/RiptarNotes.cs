using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiptarNotes : MonoBehaviour
{
    private GameObject[] notes;
    void Start()
    {
        notes = GameObject.FindGameObjectsWithTag("Note");
    }
    public void ChangeLayer(int layer)
    {
        foreach (GameObject note in notes)
        {
            note.layer = layer;
        }
    }

}
