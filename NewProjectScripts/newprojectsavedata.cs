using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using System.Text;
//using Mono.Data.SqliteClient;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class newprojectsavedata : MonoBehaviour
{
    string titleinput,remarksinput,dateinput, sqlQuery;
    //  string title, remarks, date,tempstr;
    string[] title = new string[30];    
    string[] remarks = new string[30];
    string[] date = new string[30];
    double[] version=new double[30];
    string[] image = new string[30];
    int recordsavecounter,recordsavecounter1;
    float positionofcontainer;
    GameObject[] tableContainer = null;
    string[] titleContainer=null;
    int tableCounter = 0;
    int dynamiccontainercounter = 0;
    int widthofmiddleContainer;
    string name_of_btn_project;
    public static int proj_id_counter=0;
    private string connection;
    private IDbConnection dbcon;
    private IDbCommand dbcmd;
    private IDataReader reader;
    string sqlqueryprojid;
  //  private StringBuilder builder;
    void Start()
    {
        TweenPosition.Begin(GameObject.Find("DynamicContainer_"), 0, new Vector3(-2000, 0, 0));
        TweenAlpha.Begin(GameObject.Find("midleContainer"), 0, 0);
        //StartCoroutine(sizeOfContainer());
        //StartCoroutine(getDynamicSize());
        tableContainer = new GameObject[31];
        titleContainer = new string[31];
       // proj_id_counter= notesaveScript.projectidnumber;
        //remove after bulid........ getdata();
    }

    IEnumerator sizeOfContainer()
    {
        yield return new WaitForSeconds(1f);
        widthofmiddleContainer= GameObject.Find("midleContainer").GetComponent<UIWidget>().width;
        print("widthofmiddleContainer" + widthofmiddleContainer);
    }
   
    public void OpenDB(string p)
    {
        Debug.Log("Call to OpenDB:" + p);
        // check if file exists in Application.persistentDataPath
        string filepath = Application.persistentDataPath + "/" + p;
       // GameObject.Find("pathlbl").GetComponent<UILabel>().text = filepath;
        //string filepath = Application.dataPath + "/" + p;
        if (!File.Exists(filepath))
        {
            print("hiiii");
            Debug.LogWarning("File \"" + filepath + "\" does not exist. Attempting to create from \"" +
                             Application.dataPath + "!/assets/" + p);
            // if it doesn't ->
            // open StreamingAssets directory and load the db -> 
            string path = "";
            #if UNITY_EDITOR
            path = "file://" + Application.dataPath + "/StreamingAssets/" + p;
            #elif UNITY_ANDROID
			path ="jar:file://" + Application.dataPath + "!/assets/"+p;
            #elif UNITY_IOS || UNITY_IPHONE
			path = "file://"+Application.dataPath + "/Raw/"+p;
            #endif
            WWW loadDB = new WWW(path);
            while (!loadDB.isDone) { }
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath,loadDB.bytes);
        }
        //open db connection
        connection = "URI=file:" + filepath;
        Debug.Log("Stablishing connection to: " + connection);
        dbcon = new SqliteConnection(connection);
      //  GameObject.Find("pathlbl").GetComponent<UILabel>().text = "not connected";
        dbcon.Open();
     //   GameObject.Find("pathlbl").GetComponent<UILabel>().text = "connected";
        getdata();
    }

    public void CloseDB()
    {
       // reader.Close(); // clean everything up
       // reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbcon.Close();
        dbcon = null;
    }
   
	bool getBoolProjectName()
	{
		string title_input = GameObject.Find ("TitleInput").GetComponent<UIInput> ().value;
		print ("title_input :" + title_input);
		if (title_input.Equals ("")) 
		{
			//GameObject.Find ("TitleInput").GetComponent<UIInput> ().label.text = "Enter Project Title";
			return false;
		}

		return true;
	}

    public void insertprojectdata()
    {

		if (getBoolProjectName ()) {
			try {
				print ("recordsavecounter===>>" + recordsavecounter);
				IDbCommand dbcmd2;
				IDbConnection dbconn;
				dbconn = new SqliteConnection (connection);
				dbconn.Open ();
				//dbcon.Open();
				titleinput = GameObject.Find ("TitleInput").GetComponent<UIInput> ().value;
				remarksinput = GameObject.Find ("RemarksInput").GetComponent<UIInput> ().value;
				dateinput = System.DateTime.Now.ToString ("dd/MM/yyyy");
				//print(dateinput);
				//print("aaa");
				dbcmd2 = dbconn.CreateCommand ();
				dbcmd2.CommandText = "select count(*) from projectTable";
				dbcmd2.CommandType = CommandType.Text;
				recordsavecounter1 = Convert.ToInt32 (dbcmd2.ExecuteScalar ());
				//print("recordsavecounter1............." + recordsavecounter1);
				sqlQuery = "insert into projectTable(projectTable_id,user_id,proj_id,proj_version,proj_name,proj_remarks,canvas_id,backgroundimg_id,createdate)values(1,1," + recordsavecounter1 + ",1.0,@title,@remarks,1,0,@date)";
				//sqlQuery = "insert into projectTable(projectTable_id,user_id,proj_id,proj_version,proj_name,proj_remarks,canvas_id,backgroundimg_id,createdate)values(2,2,2,2.0,'2222','2222',2,2,'22/22/2222')";
				//proj_id_counter++;
				//print("proj_id_counter" + proj_id_counter);
				//print("bbb");
				dbcmd2 = dbconn.CreateCommand ();
				notesaveScript.projectidnumber = recordsavecounter1;
				dbcmd2.Parameters.Add (new SqliteParameter ("@title", titleinput));
				dbcmd2.Parameters.Add (new SqliteParameter ("@remarks", remarksinput));
				dbcmd2.Parameters.Add (new SqliteParameter ("@date", dateinput));
				dbcmd2.CommandText = sqlQuery;
				IDataReader reader1 = dbcmd2.ExecuteReader ();
				print ("zzzzzz: " + dbcmd2.CommandText);
				print ("zzzzzz: " + reader1);
				dbcmd2.Dispose ();
				dbcmd2 = null;
				reader1.Close ();
				reader1 = null;
				// dbcon.Close();
				// dbcon = null;
				dbconn.Close ();
				dbconn = null;
				saveData ();
				//CloseDB();
			} catch (Exception e) {
				print (e);
				TweenAlpha.Begin (GameObject.Find ("popupContainer"), 0, 0);
				//GameObject.Find("pathlbl").GetComponent<UILabel>().text = e.ToString();
			}
		}
    }
    
    public void getdata()
    {
      //  string conn = "URI=file:" + Application.dataPath + "/BMCDatabase.db"; //Path to database.
      //  IDbConnection dbconn;
      //  dbconn = (IDbConnection)new SqliteConnection(conn);
        dbcmd = dbcon.CreateCommand();
        string sqlQuery1 = "SELECT proj_name,proj_remarks,createdate,proj_version FROM projectTable desc";
        dbcmd.CommandText = sqlQuery1;
        reader = dbcmd.ExecuteReader();
        while(reader.Read())
        {            
             title[tableCounter] = reader.GetString(0);
             remarks[tableCounter] = reader.GetString(1);
             date[tableCounter] = reader.GetString(2);
             version[tableCounter] = reader.GetDouble(3);
             print("title" + tableCounter + "=" + title[tableCounter]+ "    remarks" + tableCounter + "=" + remarks[tableCounter] + "   date" + tableCounter + "=" + date[tableCounter] + " version" + tableCounter + "=" + version[tableCounter]);
             tableCounter++;
        }
        dbcmd.Dispose();
        dbcmd = null;
        IDbCommand dbcmd1 = dbcon.CreateCommand();
        dbcmd1.CommandText = "select Count(*) from projectTable";
        dbcmd1.CommandType = CommandType.Text;
        recordsavecounter = Convert.ToInt32(dbcmd1.ExecuteScalar());
      //  GameObject.Find("pathlbl").GetComponent<UILabel>().text = recordsavecounter.ToString();
        print("recordsavecounter............." + recordsavecounter);
        reader.Close();
        reader = null;
        dbcon.Close();
        dbcon = null;
        dbcmd1.Dispose();
        dbcmd1 = null;
        StartCoroutine(sizeOfContainer());
        StartCoroutine(getDynamicSize());
    }

    IEnumerator getDynamicSize()
    {
        float container_width,container_height;
        yield return new WaitForSeconds(1f);
        //"midleContainer"
        print("recordsavecounter---------" + recordsavecounter);
        container_height = GameObject.Find("midleContainer").GetComponent<UIWidget>().height;
        container_width = GameObject.Find("midleContainer").GetComponent<UIWidget>().width;
       float imgSize = container_width / 4;
       container_width = container_width / 2;
       container_width = container_width / 2;
       //TweenPosition.Begin(GameObject.Find("DynamicContainer_"), 0, new Vector3(-2000, 0, 0));
        GameObject.Find("DynamicContainer_").GetComponent<UIWidget>().width = (int)imgSize;
        GameObject.Find("DynamicContainer_").GetComponent<UIWidget>().height = (int)imgSize;
        GameObject.Find("image_").GetComponent<UISprite>().GetComponent<UIWidget>().width = (int)imgSize;
        TweenPosition.Begin(GameObject.Find("image_"),0,new Vector2(0,0));
        TweenPosition.Begin(GameObject.Find("titlelbl_"), 0, new Vector2((0-(imgSize/2)), (0-(int)imgSize)));
        GameObject.Find("DynamicContainer_").GetComponent<UIWidget>().height = (GameObject.Find("DynamicContainer_").GetComponent<UIWidget>().height) + 18;
        TweenPosition.Begin(GameObject.Find("versionlbl_"), 0, new Vector2((0 - (imgSize / 2)), 0 - (GameObject.Find("DynamicContainer_").GetComponent<UIWidget>().height)));
        GameObject.Find("DynamicContainer_").GetComponent<UIWidget>().height = (GameObject.Find("DynamicContainer_").GetComponent<UIWidget>().height) + 18;
        TweenPosition.Begin(GameObject.Find("datelbl_"), 0, new Vector2((0 - (imgSize / 2)), 0 - (GameObject.Find("DynamicContainer_").GetComponent<UIWidget>().height)));
        GameObject.Find("DynamicContainer_").GetComponent<UIWidget>().height = (GameObject.Find("DynamicContainer_").GetComponent<UIWidget>().height) + 18;
        print("Container height :" + GameObject.Find("DynamicContainer_").GetComponent<UIWidget>().height);
        print("Container width :" + GameObject.Find("DynamicContainer_").GetComponent<UIWidget>().width);
        DynamicContainerGetdata();
    }

    public void DynamicContainerGetdata()
    {
        dynamiccontainercounter = recordsavecounter-1;
        while (dynamiccontainercounter >= 0)
        {
            tableContainer[dynamiccontainercounter] = (GameObject)NGUITools.AddChild(GameObject.Find("middleContainerScroll View"), GameObject.Find("DynamicContainer_"));
            tableContainer[dynamiccontainercounter].GetComponent<UIWidget>().name = ("DynamicContainer_" +dynamiccontainercounter).ToString();
            GameObject.Find("DynamicContainer_" + dynamiccontainercounter).transform.GetChild(0).name = "image_" + dynamiccontainercounter;
            GameObject.Find("DynamicContainer_" + dynamiccontainercounter).transform.GetChild(1).name = "titlelbl_" + dynamiccontainercounter;
            GameObject.Find("DynamicContainer_" + dynamiccontainercounter).transform.GetChild(2).name = "versionlbl_" + dynamiccontainercounter;
            GameObject.Find("DynamicContainer_" + dynamiccontainercounter).transform.GetChild(3).name = "datelbl_" + dynamiccontainercounter;
            GameObject.Find("titlelbl_" + dynamiccontainercounter).GetComponent<UILabel>().text = title[dynamiccontainercounter];
            GameObject.Find("versionlbl_" + dynamiccontainercounter).GetComponent<UILabel>().text = "v" + version[dynamiccontainercounter].ToString();
            GameObject.Find("datelbl_" + dynamiccontainercounter).GetComponent<UILabel>().text = date[dynamiccontainercounter];
            dynamiccontainercounter--;
        }
        StartCoroutine(setDynamicContainers());
    }

    IEnumerator setDynamicContainers()
    {
		int containerDistance = GameObject.Find ("DynamicContainer_").GetComponent<UIWidget> ().height + 10;
        int counter = 0;
        yield return new WaitForSeconds(1f);
        int x = -(widthofmiddleContainer / 3);
        int y = 140;
        while((recordsavecounter-1)>=0)
        {
            TweenPosition.Begin(GameObject.Find("DynamicContainer_" + (recordsavecounter - 1)), 0, new Vector3(x,y,0));
            x = x+(widthofmiddleContainer / 3);
            recordsavecounter--;
            counter++;
            if(counter==3)
            {
                //y = y - 200;
				y = y - (containerDistance);
                x = -(widthofmiddleContainer / 3);
                counter = 0;
            }
        }
        yield return new WaitForSeconds(1f);
        TweenAlpha.Begin(GameObject.Find("midleContainer"), 1, 1);
		GameObject.Find ("newprojectselectImg1").GetComponent<BoxCollider> ().enabled = true;
    }
    
    IEnumerator newProjectanimationleft()
    {
        int yPosition = 10;
        yield return new WaitForSeconds(1f);
        for(int i=recordsavecounter-1; i>=0 ; i--)
        {
            TweenPosition.Begin(GameObject.Find("DynamicContainer_" + title[i]), 1f, new Vector2(0, yPosition));
            yield return new WaitForSeconds(1f);
            yPosition -= 100;
        }
    }

    IEnumerator setDynamicContainersonstart()
    {
        GameObject.Find("DynamicContainer_" + title[dynamiccontainercounter]).GetComponent<UIWidget>().width = GameObject.Find("BackGroundContainer").GetComponent<UIWidget>().width/4;
        print("GameObject.Find+title[dynamiccontainercounter]).GetComponent<UIWidget>().width" + GameObject.Find("DynamicContainer_" + title[dynamiccontainercounter]).GetComponent<UIWidget>().width);
        yield return new WaitForSeconds(0.1f);
    }

    public void saveData()
    {
        StartCoroutine(projectCreated());
    }
    
    IEnumerator  projectCreated()
    {
        CanvasScript.canvasTitle = GameObject.Find("TitleInput").GetComponent<UIInput>().value;
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Canvas");
    }

    public void existingProjectOpen()
    {
        name_of_btn_project = UIButton.current.name;
        string titleofproject;
        string substr = name_of_btn_project.Substring(17, (name_of_btn_project.Length - 17));
        titleofproject = GameObject.Find("titlelbl_" + substr).GetComponent<UILabel>().text;
        print("title===>>>" + title);
        CanvasScript.canvasTitle = titleofproject;
        notesaveScript.projectidnumber = int.Parse(substr);
        SceneManager.LoadScene("Canvas");
    }
    
     public void DynamicContainerInsertdata()
     {
         tableContainer[tableCounter] = (GameObject)NGUITools.AddChild(GameObject.Find("Scroll View"), GameObject.Find("DynamicContainer_"));
         tableContainer[tableCounter].GetComponent<UIWidget>().name = ("DynamicContainer_"+ titleinput).ToString();
         TweenPosition.Begin(GameObject.Find("DynamicContainer_" + titleinput), 0.5f, new Vector3(0, -70, 0));
         tableCounter++;
     }
}

//ascending order......
/*public void LeftPanelDynamicContainerGetdata()
   {
       positionofcontainer = 10;
       while (dynamiccontainercounter < recordsavecounter)
       {
           print("dynamiccontainercounter==>>>" + dynamiccontainercounter);
           tableContainer[dynamiccontainercounter] = (GameObject)NGUITools.AddChild(GameObject.Find("Scroll View"), GameObject.Find("DynamicContainer_"));
           tableContainer[dynamiccontainercounter].GetComponent<UIWidget>().name = ("DynamicContainer_" + title[dynamiccontainercounter]).ToString();
           GameObject.Find("DynamicContainer_" + title[dynamiccontainercounter]).transform.GetChild(0).name = "image_" + title[dynamiccontainercounter];
           GameObject.Find("DynamicContainer_" + title[dynamiccontainercounter]).transform.GetChild(1).name = "titlelbl_" + title[dynamiccontainercounter];
           GameObject.Find("DynamicContainer_" + title[dynamiccontainercounter]).transform.GetChild(2).name = "versionlbl_" + title[dynamiccontainercounter];
           GameObject.Find("DynamicContainer_" + title[dynamiccontainercounter]).transform.GetChild(3).name = "datelbl_" + title[dynamiccontainercounter];
           print("positionofcontainer" + positionofcontainer);
           GameObject.Find("titlelbl_" + title[dynamiccontainercounter]).GetComponent<UILabel>().text = title[dynamiccontainercounter];
           GameObject.Find("versionlbl_" + title[dynamiccontainercounter]).GetComponent<UILabel>().text = version[dynamiccontainercounter].ToString();
           GameObject.Find("datelbl_" + title[dynamiccontainercounter]).GetComponent<UILabel>().text = date[dynamiccontainercounter];
           StartCoroutine(setDynamicContainers());
           //GameObject.Find("image_" + title[dynamiccontainercounter]).GetComponent<UISprite>().te
           dynamiccontainercounter++;
           //print("dynamiccontainercounter" + dynamiccontainercounter);
           TweenPosition.Begin(GameObject.Find("DynamicContainer_" + title[dynamiccontainercounter-1]), 0f, new Vector2(350, positionofcontainer));
           positionofcontainer -= 100;
       }
       StartCoroutine(XYZanimation());
   }*/

//older version savedata in unity editor
/* public void saveData()
{
    string conn = "URI=file:" + Application.dataPath + "/BMCDatabase.db"; //Path to database.
    IDbConnection dbconn;
    dbconn = (IDbConnection)new SqliteConnection(conn);
    dbconn.Open(); //Open connection to the database.
    print("hhh");
    //string sqlQuery = "insert into PlaceSequence(value,name,randomSequence) values(3,'hello',35)";
    titleinput = GameObject.Find("TitleInput").GetComponent<UIInput>().value;
    remarksinput = GameObject.Find("RemarksInput").GetComponent<UIInput>().value;
    dateinput = System.DateTime.Now.ToString("dd/MM/yyyy");
    print(dateinput);
    print("aaa");
    sqlQuery = "insert into newProject(title,remarks,date,version)values(@title,@remarks,@date,1)";
    print("bbb");
    IDbCommand dbcmd = dbconn.CreateCommand();

    dbcmd.Parameters.Add(new SqliteParameter("@title", titleinput));
    dbcmd.Parameters.Add(new SqliteParameter("@remarks", remarksinput));
    dbcmd.Parameters.Add(new SqliteParameter("@date", dateinput));
    dbcmd.CommandText = sqlQuery;
    IDataReader reader = dbcmd.ExecuteReader();

    reader.Close();
    reader = null;
    dbcmd.Dispose();
    dbcmd = null;
    dbconn.Close();
    dbconn = null;
    DynamicContainerInsertdata();
   // StartCoroutine(projectCreated());
}
*/
