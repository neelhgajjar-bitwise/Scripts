using UnityEngine;
using System.Collections;

public class NewProjectOpendatabase : MonoBehaviour {
    private string description;
    // Use this for initialization
    void Start () {
        Debug.Log("starting SQLiteLoad app");

        // Retrieve next word from database
        description = "something went wrong with the database";

        newprojectsavedata db = GetComponent<newprojectsavedata>();

        db.OpenDB("BMCDatabase.db");
        //db.CloseDB();
    }
	
}
