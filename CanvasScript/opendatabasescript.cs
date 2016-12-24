using UnityEngine;
using System.Collections;

public class opendatabasescript : MonoBehaviour
{
    private string description;
    // Use this for initialization
    void Start()
    {
        Debug.Log("starting SQLiteLoad app");

        // Retrieve next word from database
        description = "something went wrong with the database";
        notesaveScript db1 = GetComponent<notesaveScript>();
        db1.OpenDB("BMCDatabase.db");
        //db.CloseDB();
    }

    // Update is called once per frame
    void Update()
    {

    }
}