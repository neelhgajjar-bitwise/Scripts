using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
//using System.Text;
//using Mono.Data.SqliteClient;
//using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class notesaveScript : MonoBehaviour
{

	int c1_counter = 0;
	int c2_counter = 0;
	int c3_counter = 0;
	int c4_counter = 0;
	int c5_counter = 0;
	int c6_counter = 0;
	int c7_counter = 0;
	int c8_counter = 0;
	int c9_counter = 0;

	string new_stickey_note_name = "";
	int[] soft_delete = new int[100];
	int[] projid = new int[100];
	int[] notenumber = new int[100];
	string[] stickey_note_name = new string[100];
	string[] title = new string[100];
	string[] content = new string[100];
	string[] remarks = new string[100];
	string[] createdate = new string[100];
	double[] version = new double[100];
	string[] colorofnotetitle = new string[100];
	string[] colorofnotebody = new string[100];
	string[] colorofnotestatusbar = new string[100];
	string[] typeOfContainer = new string[100];
	string[] updatedate = new string[100];
	float[] notePriority = new float[100];
	string[] notestatus = new string[100];
	string sqlQuerynotecounter;
	string titleofnote, contentofnote, remarksofnote, noteofcontainer, notecreatedate, noteupdatedate;
	int deletenote;
	private string connection;
	private IDbConnection dbcon, dbconn, dbconupdate, dbcondelete, dbcondelete1;
	private IDbCommand dbcmd, dbcmd2, dbcmdupdate, dbcmddelete;
	private IDataReader reader, reader1, readerupdate, readerdelete, readerdelete1;
	public static int notecounter = 0;
	int notecounterexisting;
	int datacounter = 0;
    int remarksindex = 0;
    string remarksbtnname = "";
	int recordsavecounter = 0;
	string[] name_of_container = new string[100];
	// int stickeynotecounter;
	GameObject[] dynamicnote = null;
	int c1 = 0, c2 = 0, c3 = 0, c4 = 0, c5 = 0, c6 = 0, c7 = 0, c8 = 0, c9 = 0;
	int i;
	int indexofnote;
	static int totalchild;
	int temp;
	bool delete_box_status = false;
	bool notestatusbool = true;
	string note_name = "";
	public bool updateon = true;
	bool boolManualUpdate = true;
	bool createnotecolor = false;
    bool addremarksbool = false;
    bool editremarksbool = false;
    public static bool editNoteGeneralPopup = false;
    public static bool editNoteRemarkPopup = false;

    public static int projectidnumber;
	// Use this for initialization
	void Start()
	{
		for (int i = 0; i < 100; i++)
		{
			soft_delete[i] = 0;
			stickey_note_name[i] = "";
		}
		//getdataofnote();
		TweenAlpha.Begin(GameObject.Find("editNoteSelectNoteStatusContainer"), 0f, 0);
       // TweenAlpha.Begin(GameObject.Find("noteRemarkLbl_"), 0f, 0);
        StartCoroutine(updateoff());
		dynamicnote = new GameObject[100];
		StartCoroutine(manualStart());
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
			File.WriteAllBytes(filepath, loadDB.bytes);
			}
			//open db connection
			connection = "URI=file:" + filepath;
			Debug.Log("Stablishing connection to: " + connection);
			dbcon = new SqliteConnection(connection);
			//GameObject.Find("pathlbl").GetComponent<UILabel>().text = "not connected";
			dbcon.Open();
			getdataofnote();
			//GameObject.Find("pathlbl").GetComponent<UILabel>().text = "connected";
			//getdata();
			}

			public void insertNoteIntoTable()
			{
			StartCoroutine(insertNoteIntoTableenum());
			}

			IEnumerator insertNoteIntoTableenum()
			{
			//789
			//dbcon.Close();
			//dbcon = null;
			dbconn = new SqliteConnection(connection);
			dbconn.Open();
			yield return new WaitForSeconds(2f);
			print("notecounter" + notecounter);
			//  print("----------> " + GameObject.Find("notetitle_" + notecounter).GetComponent<UILabel>().text);

			IDbCommand dbcmd3 = dbconn.CreateCommand();
			dbcmd3.CommandText = "select Count(*) from stickeynoteTable WHERE proj_id=" + projectidnumber + " AND typeOfContainer not like 'note_'";
			dbcmd3.CommandType = CommandType.Text;
			notecounterexisting = Convert.ToInt32(dbcmd3.ExecuteScalar());
			notecounter = notecounterexisting;
			print("notecounterexisting===>>" + notecounterexisting);
			if (notecounter == 0)
			{
			// titleofnote = GameObject.Find("notetitle_" + (notecounter)).GetComponent<UILabel>().text;
			// contentofnote = GameObject.Find("notecontent_" + (notecounter)).GetComponent<UILabel>().text;
			titleofnote = GameObject.Find("notetitle_" + (new_stickey_note_name)).GetComponent<UILabel>().text;
			contentofnote = GameObject.Find("notecontent_" + (new_stickey_note_name)).GetComponent<UILabel>().text;
			}
			else
			{
			// titleofnote = GameObject.Find("notetitle_" + (notecounter - 1)).GetComponent<UILabel>().text;
			// contentofnote = GameObject.Find("notecontent_" + (notecounter - 1)).GetComponent<UILabel>().text;
			titleofnote = GameObject.Find("notetitle_" + (new_stickey_note_name)).GetComponent<UILabel>().text;
			contentofnote = GameObject.Find("notecontent_" + (new_stickey_note_name)).GetComponent<UILabel>().text;
			}
			//
			int temp_index = int.Parse(new_stickey_note_name);
			stickey_note_name[temp_index] = titleofnote;
			notenumber[temp_index] = int.Parse(new_stickey_note_name);
			notecreatedate = System.DateTime.Now.ToString("dd/MM/yyyy");
			noteupdatedate = System.DateTime.Now.ToString("dd/MM/yyyy");
			//print("## updated note number : " + notecounterexisting);
			//string sqlQuery = "insert into stickeynoteTable(proj_id,note_number,note_title,note_content,note_remarks,note_color,typeOfContainer,created_date,updated_date,is_deleted)values("+ projectidnumber+"," + notecounterexisting + ",@title,@content,null,0,'note_',@createdate,@updatedate,0)";//"+"'"+ name_of_container[notecounter] +"'"+"
			string sqlQuery = "insert into stickeynoteTable(proj_id,note_number,note_title,note_content,note_remarks,note_color_title,note_color_body,note_color_statusbar,typeOfContainer,created_date,updated_date,is_deleted,note_status)values(" + projectidnumber + "," + recordsavecounter + ",@title,@content,null,'yellowTitle','yellowBody','yellowStatusbar','note_',@createdate,@updatedate,0,'Proven')";
			//print("bbb");
			dbcmd2 = dbconn.CreateCommand();
			dbcmd2.Parameters.Add(new SqliteParameter("@title", titleofnote));
			dbcmd2.Parameters.Add(new SqliteParameter("@content", contentofnote));
			dbcmd2.Parameters.Add(new SqliteParameter("@createdate", notecreatedate));
			dbcmd2.Parameters.Add(new SqliteParameter("@updatedate", noteupdatedate));
			dbcmd2.CommandText = sqlQuery;
			reader1 = dbcmd2.ExecuteReader();
			reader1.Close();
			reader1 = null;
			}

			public void getdataofnote()
			{
			StartCoroutine(getdataofnoteenum());
			}

			void destroyAllNotes()
			{
			//destroying all array values
			boolManualUpdate = false;
			for (int i = 0; i < 100; i++)
			{
			soft_delete[i] = 0;
			projid[i] = 0;
			notenumber[i] = 0;
			stickey_note_name[i] = "";
			title[i] = "";
			content[i] = "";
			remarks[i] = "";
			createdate[i] = "";
			version[i] = 0;
			colorofnotetitle[i] = "";
			colorofnotebody[i] = "";
			colorofnotestatusbar[i] = "";
			typeOfContainer[i] = "";
			updatedate[i] = "";
			}

			for (int i = 0; i < c1; i++)
			{
			Destroy(GameObject.Find(GameObject.Find("keypartnerGrid").GetComponent<UIGrid>().transform.GetChild(i).name));
			}
			for (int i = 0; i < c2; i++)
			{
			Destroy(GameObject.Find(GameObject.Find("KeyActivityGrid").GetComponent<UIGrid>().transform.GetChild(i).name));
			}
			for (int i = 0; i < c3; i++)
			{
			Destroy(GameObject.Find(GameObject.Find("ValuepropostionGrid").GetComponent<UIGrid>().transform.GetChild(i).name));
			}
			for (int i = 0; i < c4; i++)
			{
			Destroy(GameObject.Find(GameObject.Find("customerRelationshipGrid").GetComponent<UIGrid>().transform.GetChild(i).name));
			}
			for (int i = 0; i < c5; i++)
			{
			Destroy(GameObject.Find(GameObject.Find("customerSegmentGrid").GetComponent<UIGrid>().transform.GetChild(i).name));
			}
			for (int i = 0; i < c6; i++)
			{
			Destroy(GameObject.Find(GameObject.Find("KeyResourcesGrid").GetComponent<UIGrid>().transform.GetChild(i).name));
			}
			for (int i = 0; i < c7; i++)
			{
			Destroy(GameObject.Find(GameObject.Find("channelsGrid").GetComponent<UIGrid>().transform.GetChild(i).name));
			}
			for (int i = 0; i < c8; i++)
			{
			Destroy(GameObject.Find(GameObject.Find("costStructureGrid").GetComponent<UIGrid>().transform.GetChild(i).name));
			}
			for (int i = 0; i < c9; i++)
			{
			Destroy(GameObject.Find(GameObject.Find("revenueStreamGrid").GetComponent<UIGrid>().transform.GetChild(i).name));
			}
			Destroy(GameObject.Find(GameObject.Find("deletenoteimg").GetComponent<UISprite>().transform.GetChild(1).name));
			if (GameObject.Find("newnoteGrid").transform.childCount == 1)
			{
			Destroy(GameObject.Find(GameObject.Find("newnoteGrid").GetComponent<UISprite>().transform.GetChild(0).name));
			}
			if (GameObject.Find("note_").transform.childCount == 2)
			{
			Destroy(GameObject.Find(GameObject.Find("note_").GetComponent<UIWidget>().transform.GetChild(1).name));
			}
			datacounter = 0;
			new_stickey_note_name = "";
			StartCoroutine(IeGetAllDataFromDatabase());
			}

			IEnumerator IeGetAllDataFromDatabase()
			{
			yield return new WaitForSeconds(0f);
			//dbcmd = dbcon.CreateCommand();
			dbcondelete1 = new SqliteConnection(connection);
			dbcondelete1.Open();
			IDbCommand dbcmddelete1 = dbcondelete1.CreateCommand();
			dbcmd = dbcondelete1.CreateCommand();
			//
			string sqlDelete = "delete from stickeynoteTable where typeOfContainer like 'note_'";
			dbcmddelete1.CommandText = sqlDelete;
			readerdelete = dbcmddelete1.ExecuteReader();
			//
			string sqlQuery1 = "SELECT proj_id,note_number,note_title,note_content,note_remarks,note_color_title,note_color_body,note_color_statusbar,typeOfContainer,is_deleted,note_priority,note_status FROM stickeynoteTable WHERE proj_id=" + projectidnumber + " AND typeOfContainer not like 'note_' order by note_priority desc";//"SELECT proj_name,proj_remarks,createdate,proj_version FROM projectTable desc";
			dbcmd.CommandText = sqlQuery1;
			readerdelete1 = dbcmd.ExecuteReader();
			while (readerdelete1.Read())
			{
			stickey_note_name[datacounter] = readerdelete1.GetString(2);
			projid[datacounter] = readerdelete1.GetInt32(0);
			notenumber[datacounter] = readerdelete1.GetInt32(1);
			title[datacounter] = readerdelete1.GetString(2);
			content[datacounter] = readerdelete1.GetString(3);
			remarks[datacounter] = reader.GetString(4);
			colorofnotetitle[datacounter] = readerdelete1.GetString(5);
			colorofnotebody[datacounter] = readerdelete1.GetString(6);
			colorofnotestatusbar[datacounter] = readerdelete1.GetString(7);
			typeOfContainer[datacounter] = readerdelete1.GetString(8);
			//createdate[datacounter] = reader.GetString(2);
			//version[datacounter] = reader.GetDouble(3);
			soft_delete[datacounter] = readerdelete1.GetInt32(9);
			notePriority[datacounter] = readerdelete1.GetFloat(10);
			notestatus[datacounter] = readerdelete1.GetString(11);
			//print("proj_id" + datacounter + "=" + projid[datacounter] + "note_number" + datacounter + "=" + notenumber[datacounter] + "   note_title" + datacounter + "=" + title[datacounter] + " note_content" + datacounter + "=" + content[datacounter] + " note_remarks" + datacounter+"=" + remarks[datacounter] + " note_color" + datacounter + "=" + colorofnote[datacounter] + " typeOfContainer" + datacounter + "=" + typeOfContainer[datacounter]) ;
			datacounter++;
			}
			dbcmd.Dispose();
			dbcmd = null;
			dbcmddelete1.Dispose();
			dbcmddelete1 = null;
			IDbCommand dbcmd2 = dbcondelete1.CreateCommand();
			dbcmd2.CommandText = "select Count(*) from stickeynoteTable WHERE proj_id=" + projectidnumber + " AND typeOfContainer not like 'note_'";
			dbcmd2.CommandType = CommandType.Text;
			recordsavecounter = Convert.ToInt32(dbcmd2.ExecuteScalar());
			temp = recordsavecounter;
			print("recordsavecounter" + recordsavecounter);
			readerdelete1.Close();
			readerdelete1 = null;
			readerdelete.Close();
			readerdelete = null;

			dbcondelete1.Close();
			dbcondelete1 = null;
			dbcmd2.Dispose();
			dbcmd2 = null;
			setAllNoteSetRuntime();
			}

			IEnumerator getdataofnoteenum()
			{
			yield return new WaitForSeconds(1f);
			dbcmd = dbcon.CreateCommand();
			IDbCommand dbcmddelete = dbcon.CreateCommand();
			//
			string sqlDelete = "delete from stickeynoteTable where typeOfContainer like 'note_'";
			dbcmddelete.CommandText = sqlDelete;
			reader = dbcmddelete.ExecuteReader();
			//
			string sqlQuery1 = "SELECT proj_id,note_number,note_title,note_content,note_remarks,note_color_title,note_color_body,note_color_statusbar,typeOfContainer,is_deleted,note_priority,note_status FROM stickeynoteTable WHERE proj_id=" + projectidnumber + " AND typeOfContainer not like 'note_' order by note_priority desc";//"SELECT proj_name,proj_remarks,createdate,proj_version FROM projectTable desc";
			dbcmd.CommandText = sqlQuery1;
			reader = dbcmd.ExecuteReader();
			while (reader.Read())
			{
			stickey_note_name[datacounter] = reader.GetString(2);
			projid[datacounter] = reader.GetInt32(0);
			notenumber[datacounter] = reader.GetInt32(1);
			title[datacounter] = reader.GetString(2);
			content[datacounter] = reader.GetString(3);
			remarks[datacounter] = reader.GetString(4);
			colorofnotetitle[datacounter] = reader.GetString(5);
			colorofnotebody[datacounter] = reader.GetString(6);
			colorofnotestatusbar[datacounter] = reader.GetString(7);
			typeOfContainer[datacounter] = reader.GetString(8);
			//createdate[datacounter] = reader.GetString(2);
			//version[datacounter] = reader.GetDouble(3);
			soft_delete[datacounter] = reader.GetInt32(9);
			notePriority[datacounter] = reader.GetFloat(10);
			notestatus[datacounter] = reader.GetString(11);
			//print("proj_id" + datacounter + "=" + projid[datacounter] + "note_number" + datacounter + "=" + notenumber[datacounter] + "   note_title" + datacounter + "=" + title[datacounter] + " note_content" + datacounter + "=" + content[datacounter] + " note_remarks" + datacounter+"=" + remarks[datacounter] + " note_color" + datacounter + "=" + colorofnote[datacounter] + " typeOfContainer" + datacounter + "=" + typeOfContainer[datacounter]) ;
			datacounter++;
			}
			dbcmd.Dispose();
			dbcmd = null;
			dbcmddelete.Dispose();
			dbcmddelete = null;
			IDbCommand dbcmd1 = dbcon.CreateCommand();
			dbcmd1.CommandText = "select Count(*) from stickeynoteTable WHERE proj_id=" + projectidnumber + " AND typeOfContainer not like 'note_'";
			dbcmd1.CommandType = CommandType.Text;
			recordsavecounter = Convert.ToInt32(dbcmd1.ExecuteScalar());
			temp = recordsavecounter;
			print("recordsavecounter" + recordsavecounter);
			reader.Close();
			reader = null;
			dbcon.Close();
			dbcon = null;
			dbcmd1.Dispose();
			dbcmd1 = null;
            setAllNoteSetRuntime();
			}
            
			void counterIncrementFunction(GameObject note, string typeOfContainer)
			{
			if (typeOfContainer.Equals("keypartnerGrid"))
			{
			TweenPosition.Begin(note, 0, new Vector2(0, c1));
			c1 -= 150;
			}
			else if (typeOfContainer.Equals("KeyActivityGrid"))
			{
			TweenPosition.Begin(note, 0, new Vector2(0, c2));
			c2 -= 150;
			}
			else if (typeOfContainer.Equals("ValuepropostionGrid"))
			{
			TweenPosition.Begin(note, 0, new Vector2(0, c3));
			c3 -= 150;
			}
			else if (typeOfContainer.Equals("customerRelationshipGrid"))
			{
			TweenPosition.Begin(note, 0, new Vector2(0, c4));
			c4 -= 150;
			}
			else if (typeOfContainer.Equals("customerSegmentGrid"))
			{
			TweenPosition.Begin(note, 0, new Vector2(0, c5));
			c5 -= 150;
			}
			else if (typeOfContainer.Equals("KeyResourcesGrid"))
			{
			TweenPosition.Begin(note, 0, new Vector2(0, c6));
			c6 -= 150;
			}
			else if (typeOfContainer.Equals("channelsGrid"))
			{
			TweenPosition.Begin(note, 0, new Vector2(0, c7));
			c7 -= 150;
			}
			else if (typeOfContainer.Equals("costStructureGrid"))
			{
			TweenPosition.Begin(note, 0, new Vector2(c8, 0));
			c8 += 150;
			}
			else if (typeOfContainer.Equals("revenueStreamGrid"))
			{
			TweenPosition.Begin(note, 0, new Vector2(c9, 0));
			c9 += 150;
			}
			}
            
			void setAllNoteSetRuntime()
			{
			for (int j = 0; j < recordsavecounter; j++)
			{
			if (soft_delete[j] == 0)
			{
			dynamicnote[j] = (GameObject)NGUITools.AddChild(GameObject.Find(typeOfContainer[j]), GameObject.Find("stickeynote_"));
			print("typeOfContainer[j]" + typeOfContainer[j]);
			dynamicnote[j].GetComponent<UISprite>().name = ("stickeynote_" + j).ToString();
			counterIncrementFunction(dynamicnote[j], typeOfContainer[j]);
			GameObject.Find("stickeynote_" + j).transform.GetChild(0).name = ("stickeyNoteContainer_" + j).ToString();
			GameObject.Find("stickeynote_" + j).transform.GetChild(1).name = ("titleSprite_" + j).ToString();
			GameObject.Find("stickeynote_" + j).transform.GetChild(2).name = ("bodySprite_" + j).ToString();
			GameObject.Find("stickeynote_" + j).transform.GetChild(3).name = ("StatusbarContainer_" + j).ToString();
			GameObject.Find("titleSprite_" + j).transform.GetChild(0).name = ("titleShadowSprite_" + j).ToString();
			GameObject.Find("StatusbarContainer_" + j).transform.GetChild(0).name = ("statusbarSprite_" + j).ToString();
			GameObject.Find("StatusbarContainer_" + j).transform.GetChild(1).name = ("noteStatus_" + j).ToString();
            GameObject.Find("StatusbarContainer_" + j).transform.GetChild(3).name = ("noteInfoIcon_" + j).ToString();
           
            GameObject.Find("stickeyNoteContainer_" + j).transform.GetChild(2).name = ("mainbodyContainer_" + j).ToString();
			//  GameObject.Find("stickeyNoteContainer_" + j).transform.GetChild(3).name = ("Remarksnoteimg_" + j).ToString();
			//   GameObject.Find("stickeyNoteContainer_" + j).transform.GetChild(4).name = ("colorbyimg_" + j).ToString();
			//  GameObject.Find("RemarksContainer_" + j).transform.GetChild(0).name = ("Remarkstitlelbl_" + j).ToString();
			// GameObject.Find("RemarksContainer_" + j).transform.GetChild(1).name = ("Remarksbodylbl_" + j).ToString();
			GameObject.Find("mainbodyContainer_" + j).transform.GetChild(0).name = ("notetitle_" + j).ToString();
			GameObject.Find("mainbodyContainer_" + j).transform.GetChild(1).name = ("notecontent_" + j).ToString();
            GameObject.Find("mainbodyContainer_" + j).transform.GetChild(2).name = ("noteRemarkContent_" + j).ToString();
            GameObject.Find("mainbodyContainer_" + j).transform.GetChild(3).name = ("noteRemarkLbl_" + j).ToString();
            
            GameObject.Find("notetitle_" + j).GetComponent<UILabel>().text = title[j];
			GameObject.Find("notecontent_" + j).GetComponent<UILabel>().text = content[j];
			GameObject.Find("titleSprite_" + j).GetComponent<UISprite>().spriteName = colorofnotetitle[j];
			GameObject.Find("bodySprite_" + j).GetComponent<UISprite>().spriteName = colorofnotebody[j];
			GameObject.Find("statusbarSprite_" + j).GetComponent<UISprite>().spriteName = colorofnotestatusbar[j];
			GameObject.Find("noteStatus_" + j).GetComponent<UILabel>().text = notestatus[j];
            GameObject.Find("noteRemarkContent_" + j).GetComponent<UILabel>().text = remarks[j];
            TweenAlpha.Begin(GameObject.Find("noteRemarkLbl_" + j), 0, 0);
            TweenAlpha.Begin(GameObject.Find("noteRemarkContent_" + j), 0, 0);
            }
			}
			//refreshGrid();
			createnewnote();
			boolManualUpdate = true;
            }
			void refreshGrid()
			{
			GameObject.Find("keypartnerGrid").GetComponent<UIGrid>().enabled = true;
			GameObject.Find("KeyActivityGrid").GetComponent<UIGrid>().enabled = true;
			GameObject.Find("ValuepropostionGrid").GetComponent<UIGrid>().enabled = true;
			GameObject.Find("customerRelationshipGrid").GetComponent<UIGrid>().enabled = true;
			GameObject.Find("customerSegmentGrid").GetComponent<UIGrid>().enabled = true;
			GameObject.Find("KeyResourcesGrid").GetComponent<UIGrid>().enabled = true;
			GameObject.Find("channelsGrid").GetComponent<UIGrid>().enabled = true;
			GameObject.Find("costStructureGrid").GetComponent<UIGrid>().enabled = true;
			GameObject.Find("revenueStreamGrid").GetComponent<UIGrid>().enabled = true;
			}
			public void createnewnote()
			{
			recordsavecounter++;
			print("new note number " + recordsavecounter);
			new_stickey_note_name = recordsavecounter + "";
			CanvasScript.noteContainer[recordsavecounter] = (GameObject)NGUITools.AddChild(GameObject.Find("note_"), GameObject.Find("stickeynote_"));
			//print("noteContainer[noteCounter]" + noteContainer[noteCounter]);
			CanvasScript.noteContainer[recordsavecounter].GetComponent<UIWidget>().name = ("stickeynote_" + new_stickey_note_name).ToString();
			TweenAlpha.Begin(GameObject.Find("stickeynote_" + new_stickey_note_name), 0, 0.01f);//<---------- New note alpha 0
			//  stickeynoteContainer[noteCounter].GetComponent<UIWidget>().name = ("RemarksContainer_" + noteCounter).ToString();
			defineNameAtRuntime();
			GameObject.Find("stickeynote_" + recordsavecounter).GetComponent<UISprite>().GetComponent<UIWidget>().width = -CanvasScript.widthofsmalllineval - 38;
			TweenScale.Begin(GameObject.Find("stickeynote_"), 0, new Vector3(0, 0, 0));
			TweenAlpha.Begin(GameObject.Find("note_"), 0, 1);
			//StartCoroutine(visibleRemarksButton());
			// noteCounter++;
			insertNoteIntoTable();
			GameObject.Find("stickeynote_" + new_stickey_note_name).GetComponent<ExampleDragDropItem>().restriction = UIDragDropItem.Restriction.None;
			GameObject.Find("stickeynote_" + new_stickey_note_name).GetComponent<UIWidget>().width = 120;
			TweenScale.Begin(GameObject.Find("stickeynote_" + new_stickey_note_name), 0, new Vector2(0.5f, 0.5f));
			print("change size 0.5 of " + "stickeynote_" + new_stickey_note_name);
            TweenAlpha.Begin(GameObject.Find("noteRemarkLbl_" + new_stickey_note_name), 0, 0);
        	boolManualUpdate = true;
			}

			void defineNameAtRuntime()
			{
			GameObject.Find("stickeynote_" + recordsavecounter).transform.GetChild(0).name = ("stickeyNoteContainer_" + recordsavecounter).ToString();
			GameObject.Find("stickeynote_" + recordsavecounter).transform.GetChild(1).name = ("titleSprite_" + recordsavecounter).ToString();
			GameObject.Find("stickeynote_" + recordsavecounter).transform.GetChild(2).name = ("bodySprite_" + recordsavecounter).ToString();
			GameObject.Find("stickeynote_" + recordsavecounter).transform.GetChild(3).name = ("StatusbarContainer_" + recordsavecounter).ToString();
			GameObject.Find("titleSprite_" + recordsavecounter).transform.GetChild(0).name = ("titleShadowSprite_" + recordsavecounter).ToString();
			GameObject.Find("StatusbarContainer_" + recordsavecounter).transform.GetChild(0).name = ("statusbarSprite_" + recordsavecounter).ToString();
			GameObject.Find("StatusbarContainer_" + recordsavecounter).transform.GetChild(1).name = ("noteStatus_" + recordsavecounter).ToString();
            GameObject.Find("StatusbarContainer_" + recordsavecounter).transform.GetChild(3).name = ("noteInfoIcon_" + recordsavecounter).ToString();
            
        //  GameObject.Find("stickeyNoteContainer_" + recordsavecounter).transform.GetChild(0).name = ("ColorContainer_" + recordsavecounter).ToString();
        //  GameObject.Find("stickeyNoteContainer_" + recordsavecounter).transform.GetChild(1).name = ("RemarksContainer_" + recordsavecounter).ToString();
            GameObject.Find("stickeyNoteContainer_" + recordsavecounter).transform.GetChild(2).name = ("mainbodyContainer_" + recordsavecounter).ToString();
			//  GameObject.Find("stickeyNoteContainer_" + recordsavecounter).transform.GetChild(3).name = ("Remarksnoteimg_" + recordsavecounter).ToString();
			//  GameObject.Find("stickeyNoteContainer_" + recordsavecounter).transform.GetChild(4).name = ("colorbyimg_" + recordsavecounter).ToString();
			//  GameObject.Find("stickeyNoteContainer_" + noteCounter).transform.GetChild(5).name = ("notebodyimg_" + noteCounter).ToString();
			//  GameObject.Find("stickeyNoteContainer_" + noteCounter).transform.GetChild(6).name = ("notetitleimg_" + noteCounter).ToString();
			// GameObject.Find("ColorContainer_" + recordsavecounter).transform.GetChild(0).name = ("grey_" + recordsavecounter).ToString();
			// GameObject.Find("ColorContainer_" + recordsavecounter).transform.GetChild(1).name = ("yellow_" + recordsavecounter).ToString();
			// GameObject.Find("ColorContainer_" + recordsavecounter).transform.GetChild(2).name = ("pink_" + recordsavecounter).ToString();
			// GameObject.Find("ColorContainer_" + recordsavecounter).transform.GetChild(3).name = ("blue_" + recordsavecounter).ToString();
			// GameObject.Find("ColorContainer_" + recordsavecounter).transform.GetChild(4).name = ("green_" + recordsavecounter).ToString();
			// GameObject.Find("RemarksContainer_" + recordsavecounter).transform.GetChild(0).name = ("Remarkstitlelbl_" + recordsavecounter).ToString();
			// GameObject.Find("RemarksContainer_" + recordsavecounter).transform.GetChild(1).name = ("Remarksbodylbl_" + recordsavecounter).ToString();
			GameObject.Find("mainbodyContainer_" + recordsavecounter).transform.GetChild(0).name = ("notetitle_" + recordsavecounter).ToString();
			GameObject.Find("mainbodyContainer_" + recordsavecounter).transform.GetChild(1).name = ("notecontent_" + recordsavecounter).ToString();
            GameObject.Find("mainbodyContainer_" + recordsavecounter).transform.GetChild(2).name = ("noteRemarkContent_" + recordsavecounter).ToString();
            GameObject.Find("mainbodyContainer_" + recordsavecounter).transform.GetChild(3).name = ("noteRemarkLbl_" + recordsavecounter).ToString();
           
    }

			void Update()
			{
			//print (totalchild +"=="+getTotalNotes());
				if (totalchild > getTotalNotes())
				{
					boolManualUpdate = false;
				}
				else if (boolManualUpdate == false && totalchild == getTotalNotes())
				{
					boolManualUpdate = true;
				}
			}

			IEnumerator manualStart()
			{
				while (true)
				{
					if (GameObject.Find("deletenoteimg").GetComponent<UISprite>().transform.childCount == 2 && delete_box_status == false)
					{
						boolManualUpdate = false;
						childcount();
						GameObject.Find("deletenotecontainer").GetComponent<UIWidget>().alpha = 1;
					}

					if (boolManualUpdate == true)
						manualUpdate();
					yield return new WaitForSeconds(2f);
				}
			}

			void decreaseRecordSaveCounter()
			{
			recordsavecounter -= 2;
			}

			void manualUpdate()
			{
			string updatequery;
			GameObject.Find("containerlbltemp").GetComponent<UILabel>().text = "aaaa";
			int notechild = GameObject.Find("note_").GetComponent<UIWidget>().transform.childCount;
			//  print("notechild=======" + notechild);
			if (updateon == true)
			{
			GameObject.Find("containerlbltemp").GetComponent<UILabel>().text = "bbbb";
			dbconupdate = new SqliteConnection(connection);
			dbconupdate.Open();
			}

			//print("recordsavecounter ::::::::::::: " + recordsavecounter);
			for (i = 0; i < recordsavecounter; i++)
			{
			//stickey_note_name[index] = "";
			// print("tickey_note_name[" + i + "]:" + stickey_note_name[i]);           
			if (!stickey_note_name[i].Equals("") && soft_delete[i] == 0)
			{
			name_of_container[i] = GameObject.Find("stickeynote_" + i).GetComponent<UISprite>().transform.parent.name;
			title[i] = GameObject.Find("notetitle_" + i).GetComponent<UILabel>().text;
			content[i] = GameObject.Find("notecontent_" + i).GetComponent<UILabel>().text;
			colorofnotetitle[i] = GameObject.Find("titleSprite_" + i).GetComponent<UISprite>().spriteName;
			colorofnotebody[i] = GameObject.Find("bodySprite_" + i).GetComponent<UISprite>().spriteName;
			colorofnotestatusbar[i] = GameObject.Find("statusbarSprite_" + i).GetComponent<UISprite>().spriteName;
			notestatus[i]=GameObject.Find("noteStatus_"+i).GetComponent<UILabel>().text;
            remarks[i] = GameObject.Find("noteRemarkContent_" + i).GetComponent<UILabel>().text;
			//print("color==>>" + colorofnote[i]);
			//updatequery = "UPDATE stickeynoteTable SET typeOfContainer=" + "'" + name_of_container[i] + "'" + "WHERE note_number=" + i + " " + "AND proj_id=" + projectidnumber + "";
			//updatequery = "UPDATE stickeynoteTable SET note_title=" + "'" + title[i] + "'" + "," + "note_content=" + "'" + content[i] + "'" + "," + "typeOfContainer=" + "'" + name_of_container[i] + "'" + "WHERE note_number=" + i + " " + "AND proj_id=" + projectidnumber + "";
			//updatequery = "UPDATE stickeynoteTable SET note_title=" + "'" + title[i] + "'" + "," + "note_content=" + "'" + content[i] + "'" + "," + "typeOfContainer=" + "'" + name_of_container[i] + "'" + "WHERE note_number=" + notenumber[i] + " " + "AND proj_id=" + projectidnumber + "";
			if (name_of_container[i].Equals("costStructureGrid") || name_of_container[i].Equals("revenueStreamGrid"))
			{
			float xaxis = (GameObject.Find("stickeynote_" + i).transform.localPosition.x);
			if (xaxis > 0)
			xaxis *= -1;
			else if (xaxis < 0)
			xaxis = Math.Abs(xaxis);

			//updatequery = "UPDATE stickeynoteTable SET note_title=" + "'" + title[i] + "'" + "," + "note_content=" + "'" + content[i] + "'" + "," + "typeOfContainer=" + "'" + name_of_container[i] + "'" + "," + "note_priority=" + xaxis + "," + "note_color_title=" + "'" + colorofnotetitle[i] + "'" + "," + "note_color_body=" + "'" + colorofnotebody[i] + "'" + " WHERE note_number=" + notenumber[i] + " " + "AND proj_id=" + projectidnumber + "";
			updatequery = "UPDATE stickeynoteTable SET note_title=" + "'" + title[i] + "'" + "," + "note_content=" + "'" + content[i] + "'" + "," + "typeOfContainer=" + "'" + name_of_container[i] + "'" + "," + "note_priority=" + xaxis + "," + "note_color_title=" + "'" + colorofnotetitle[i] + "'" + "," + "note_color_body=" + "'" + colorofnotebody[i] + "'" + "," + "note_color_statusbar=" + "'" + colorofnotestatusbar[i] + "'" + "," + "note_status=" + "'" + notestatus[i] + "'"+"," + "note_remarks=" + "'" + remarks[i] + "'" + " WHERE note_number=" + notenumber[i] + " " + "AND proj_id=" + projectidnumber + "";
			}
			else
			{
			updatequery = "UPDATE stickeynoteTable SET note_title=" + "'" + title[i] + "'" + "," + "note_content=" + "'" + content[i] + "'" + "," + "typeOfContainer=" + "'" + name_of_container[i] + "'" + "," + "note_priority=" + (GameObject.Find("stickeynote_" + i).transform.localPosition.y) + "," + "note_color_title=" + "'" + colorofnotetitle[i] + "'" + "," + "note_color_statusbar=" + "'" + colorofnotestatusbar[i] + "'" + "," + "note_color_body=" + "'" + colorofnotebody[i] + "'" + "," + "note_status=" + "'" + notestatus[i] + "'" + "," + "note_remarks=" + "'" + remarks[i] + "'" + " WHERE note_number=" + notenumber[i] + " " + "AND proj_id=" + projectidnumber + "";
			}
			//updatequery = "UPDATE stickeynoteTable SET note_title=" + "'" + title[i] + "'" + "," + "note_content=" + "'" + content[i] + "'" + "," + "typeOfContainer=" + "'" + name_of_container[i] + "'" + "," + "note_priority=" + (GameObject.Find("stickeynote_" + i).transform.GetSiblingIndex()) + " WHERE note_number=" + notenumber[i] + " " + "AND proj_id=" + projectidnumber + "";
			//3.14
			dbcmdupdate = dbconupdate.CreateCommand();
			dbcmdupdate.CommandText = updatequery;
			readerupdate = dbcmdupdate.ExecuteReader();
			GameObject.Find("containerlbltemp").GetComponent<UILabel>().text = name_of_container[i];
			//    print("name_of_container[i] ::::::::::::: " + name_of_container[i]);
			}
			}

			//
			//int no_of_child;
			//no_of_child = GameObject.Find("deletenoteimg").GetComponent<UISprite>().transform.childCount;

			/*if (GameObject.Find("deletenoteimg").GetComponent<UISprite>().transform.childCount == 2 && delete_box_status == false)
			{
			//name_of_btn = GameObject.Find("deletenoteimg").GetComponent<UISprite>().transform.GetChild(1).name;
			//deletenote();
			//deletenoteyes();
			GameObject.Find("deletenotecontainer").GetComponent<UIWidget>().alpha = 1;
			}*/

			/*else if(GameObject.Find("deletenotecontainer").GetComponent<UIWidget>().alpha == 0)
			{
			delete_box_status = false;
			} */
			//

			childcount();
			/*if (temp == 0)
			{
			print("!@#$@$");
			temp = totalchild;
			}*/
			if (totalchild > temp && notechild == 1)
			{
			editNoteOnCreate();
			//createnewnote();
			//GameObject.Find("stickeynote_" + (totalchild-1)).GetComponent<ExampleDragDropItem>().restriction = ExampleDragDropItem.Restriction.PressAndHold;
			}
			}

			/*string getChildIndex(string containerName, string stickeyNoteName)
			{
			for(int i=0 ; i)
			return "";
			}*/

			public void deletenoteyes()
			{
			print(":::::::::::::::::::::DELETE::::::::::::::::::::::");
			//GameObject.Find("BlackbackgroundContainer").GetComponent<UIWidget>().alpha = 0;
			//print("BlackbackgroundContainer alpha : " + GameObject.Find("BlackbackgroundContainer").GetComponent<UIWidget>().alpha);

			GameObject.Find("deletenotecontainer").GetComponent<UIWidget>().alpha = 0;
			//print("deletenotecontainer alpha : " + GameObject.Find("deletenotecontainer").GetComponent<UIWidget>().alpha);

			GameObject.Find("middleContainer").GetComponent<UIWidget>().alpha = 1f;
			//print("middleContainer alpha : " + GameObject.Find("middleContainer").GetComponent<UIWidget>().alpha);

			string note_name = GameObject.Find("deletenoteimg").GetComponent<UISprite>().transform.GetChild(1).name;
			int index = getIndexNumber(note_name);
			stickey_note_name[index] = "";
			int note_number = notenumber[index];
			sqlDeleteQueryByNoteNumber(note_number);
			Destroy(GameObject.Find(note_name));
			//decreaseRecordSaveCounter();
			//Destroy(GameObject.Find(name_of_btn));   
			boolManualUpdate = true;
			}

			public void deletenoteno()
			{
			print("no.......");
			destroyAllNotes();
			GameObject.Find("deletenotecontainer").GetComponent<UIWidget>().alpha = 0;
			//GameObject.Find("BlackbackgroundContainer").GetComponent<UIWidget>().enabled = false;
			//GameObject.Find("deletenotecontainer").GetComponent<UIWidget>().enabled = false;
			//GameObject.Find("middleContainer").GetComponent<UIWidget>().alpha = 1f;
			}

			void sqlDeleteQueryByNoteNumber(int note_number)
			{
			dbcondelete = new SqliteConnection(connection);
			dbcondelete.Open();
			dbcmddelete = dbcondelete.CreateCommand();
			//string deletenotebuindex = "delete from stickeynoteTable where proj_id=" + projectidnumber+" AND note_number="+note_number+"";
			string deletenotebuindex = "UPDATE stickeynoteTable SET is_deleted=1 where proj_id=" + projectidnumber + " AND note_number=" + note_number + "";
			dbcmddelete.CommandText = deletenotebuindex;
			readerdelete = dbcmddelete.ExecuteReader();
			dbcondelete.Close();
			dbcondelete = null;
			dbcmddelete.Dispose();
			dbcmddelete = null;
			readerdelete.Close();
			readerdelete = null;
			}

			int getIndexNumber(string noteName)
			{
			int index = int.Parse(noteName.Substring(12));
			return index;
			}

			public void childcount()
			{
			temp = totalchild;
			c1 = GameObject.Find("keypartnerGrid").GetComponent<UIGrid>().transform.childCount;
			c2 = GameObject.Find("KeyActivityGrid").GetComponent<UIGrid>().transform.childCount;
			c3 = GameObject.Find("ValuepropostionGrid").GetComponent<UIGrid>().transform.childCount;
			c4 = GameObject.Find("customerRelationshipGrid").GetComponent<UIGrid>().transform.childCount;
			c5 = GameObject.Find("customerSegmentGrid").GetComponent<UIGrid>().transform.childCount;
			c6 = GameObject.Find("KeyResourcesGrid").GetComponent<UIGrid>().transform.childCount;
			c7 = GameObject.Find("channelsGrid").GetComponent<UIGrid>().transform.childCount;
			c8 = GameObject.Find("costStructureGrid").GetComponent<UIGrid>().transform.childCount;
			c9 = GameObject.Find("revenueStreamGrid").GetComponent<UIGrid>().transform.childCount;
			totalchild = c1 + c2 + c3 + c4 + c5 + c6 + c7 + c8 + c9;
			//print("total===" + totalchild);
			}

			int getTotalNotes()
			{
			int total_notes = GameObject.Find("keypartnerGrid").GetComponent<UIGrid>().transform.childCount;
			total_notes += GameObject.Find("KeyActivityGrid").GetComponent<UIGrid>().transform.childCount;
			total_notes += GameObject.Find("ValuepropostionGrid").GetComponent<UIGrid>().transform.childCount;
			total_notes += GameObject.Find("customerRelationshipGrid").GetComponent<UIGrid>().transform.childCount;
			total_notes += GameObject.Find("customerSegmentGrid").GetComponent<UIGrid>().transform.childCount;
			total_notes += GameObject.Find("KeyResourcesGrid").GetComponent<UIGrid>().transform.childCount;
			total_notes += GameObject.Find("channelsGrid").GetComponent<UIGrid>().transform.childCount;
			total_notes += GameObject.Find("costStructureGrid").GetComponent<UIGrid>().transform.childCount;
			total_notes += GameObject.Find("revenueStreamGrid").GetComponent<UIGrid>().transform.childCount;
			return total_notes;
			}

			void editNoteOnCreate()
			{
             editNoteGeneralPopup = true;
             editNoteRemarkPopup = false;

            print("--------------------------------------------> " + (totalchild - 1));
            TweenAlpha.Begin(GameObject.Find("EditNoteRemarkTitle_"), 0, 0);
            TweenAlpha.Begin(GameObject.Find("EditNoteRemarkBody_"), 0, 0);
            //TweenScale.Begin(GameObject.Find("stickeynote_" + (totalchild-1)), 0, new Vector2(1f, 1f));
             createnotecolor = true;
			 notestatusbool = true;
			colorBoxToNormalSize();
			TweenScale.Begin(GameObject.Find("yellowColorBox"), 0, new Vector3(0.15f, 0.3f, 0));
			GameObject.Find("EdittitleSprite").GetComponent<UISprite>().spriteName = "yellowTitle";
			GameObject.Find("EditbodySprite").GetComponent<UISprite>().spriteName = "yellowBody";
			GameObject.Find("editStatusBarSprite").GetComponent<UISprite>().spriteName = "yellowStatusbar";
			GameObject.Find("selecteditStatusBarSprite").GetComponent<UISprite>().spriteName = "yellowStatusbar";
			TweenAlpha.Begin(GameObject.Find("stickeynote_" + new_stickey_note_name), 0, 1);//<---------- New note alpha 1
			GameObject.Find("stickeynote_" + new_stickey_note_name).GetComponent<UISprite>().GetComponent<UIButton>().defaultColor = Color.white;
			TweenScale.Begin(GameObject.Find("stickeynote_" + new_stickey_note_name), 0, new Vector2(1f, 1f));
			TweenAlpha.Begin(GameObject.Find("BlackbgContainerfirst"), 0.5f, 0.8f);
			TweenScale.Begin(GameObject.Find("EditNote_"), 0.5f, new Vector3(1, 1, 0));
            GameObject.Find("EditNotebody_").GetComponent<UIInput>().GetComponent<Collider>().enabled = true;
            GameObject.Find("EditNoteTitle_").GetComponent<UIInput>().GetComponent<Collider>().enabled = true;
            TweenAlpha.Begin(GameObject.Find("EditNotebody_"), 0, 1);
            TweenAlpha.Begin(GameObject.Find("EditNoteTitle_"), 0, 1);
            TweenAlpha.Begin(GameObject.Find("middleContainer"), 0, 0.1f);
			GameObject.Find("EditNoteTitle_").GetComponent<UIInput>().value = GameObject.Find("notetitle_" + new_stickey_note_name).GetComponent<UILabel>().text;
			GameObject.Find("EditNotebody_").GetComponent<UIInput>().value = GameObject.Find("notecontent_" + new_stickey_note_name).GetComponent<UILabel>().text;
			GameObject.Find("editNoteStatus").GetComponent<UILabel>().text = GameObject.Find("noteStatus_" + new_stickey_note_name).GetComponent<UILabel>().text;
            GameObject.Find("EditNoteRemarkBody_").GetComponent<UIInput>().value = GameObject.Find("noteRemarkContent_" + new_stickey_note_name).GetComponent<UILabel>().text;
            
            }

			public void clickOnBlackBgOnCreate()
			{
			print("##saved note : " + "notetitle_" + new_stickey_note_name);
			TweenAlpha.Begin(GameObject.Find("BlackbgContainerfirst"), 0.5f, 0f);
			TweenScale.Begin(GameObject.Find("EditNote_"), 0.5f, new Vector3(0, 0, 0));
			TweenAlpha.Begin(GameObject.Find("middleContainer"), 0, 1f);
			GameObject.Find("notetitle_" + new_stickey_note_name).GetComponent<UILabel>().text = GameObject.Find("EditNoteTitle_").GetComponent<UIInput>().value;
			GameObject.Find("notecontent_" + new_stickey_note_name).GetComponent<UILabel>().text = GameObject.Find("EditNotebody_").GetComponent<UIInput>().value;
            GameObject.Find("noteRemarkContent_" + new_stickey_note_name).GetComponent<UILabel>().text = GameObject.Find("EditNoteRemarkBody_").GetComponent<UIInput>().value;
            GameObject.Find("stickeynote_" + new_stickey_note_name).GetComponent<ExampleDragDropItem>().restriction = ExampleDragDropItem.Restriction.PressAndHold;
            TweenAlpha.Begin(GameObject.Find("noteRemarkContent_" + new_stickey_note_name), 0, 0);
			createnewnote();
			}

			public void colorBoxToNormalSize()
			{
			TweenScale.Begin(GameObject.Find("yellowColorBox"), 0, new Vector3(0.15f,0.15f,0));
			TweenScale.Begin(GameObject.Find("blueColorBox"), 0, new Vector3(0.15f,0.15f,0));
			TweenScale.Begin(GameObject.Find("greenColorBox"), 0, new Vector3(0.15f,0.15f,0));
			TweenScale.Begin(GameObject.Find("greyColorBox"), 0, new Vector3(0.15f,0.15f,0));
			TweenScale.Begin(GameObject.Find("pinkColorBox"), 0, new Vector3(0.15f,0.15f,0));
			TweenScale.Begin(GameObject.Find("whiteColorBox"), 0, new Vector3(0.15f,0.15f,0));

			}
			public void changeColorOfNote()
			{
			note_name = UIButton.current.name;
			indexofnote = int.Parse(note_name.Substring(12));
			print("note_name" + note_name);
			print("index of note===" + indexofnote);
			colorBoxToNormalSize();
			string str = GameObject.Find("titleSprite_" + indexofnote).GetComponent<UISprite>().spriteName;
			print("str====>>>" + str);
			string colorname = str.Replace("Title", "");
			createnotecolor = false;
			notestatusbool = false;
			print("colorname===..........." + colorname);
			GameObject.Find("EdittitleSprite").GetComponent<UISprite>().spriteName = GameObject.Find("titleSprite_" + indexofnote).GetComponent<UISprite>().spriteName;
			GameObject.Find("EditbodySprite").GetComponent<UISprite>().spriteName = GameObject.Find("bodySprite_" + indexofnote).GetComponent<UISprite>().spriteName;
			GameObject.Find("selecteditStatusBarSprite").GetComponent<UISprite>().spriteName = GameObject.Find("statusbarSprite_" + indexofnote).GetComponent<UISprite>().spriteName;
			GameObject.Find("editStatusBarSprite").GetComponent<UISprite>().spriteName = GameObject.Find("statusbarSprite_" + indexofnote).GetComponent<UISprite>().spriteName;

			TweenScale.Begin(GameObject.Find(colorname + "ColorBox"), 0, new Vector3(0.15f, 0.3f, 0));
			}

			public void changeColorToYellow()
			{
			if (createnotecolor == false)
			{
			GameObject.Find("EdittitleSprite").GetComponent<UISprite>().spriteName = "yellowTitle";
			GameObject.Find("EditbodySprite").GetComponent<UISprite>().spriteName = "yellowBody";
			GameObject.Find("editStatusBarSprite").GetComponent<UISprite>().spriteName = "yellowStatusbar";
			GameObject.Find("selecteditStatusBarSprite").GetComponent<UISprite>().spriteName= "yellowStatusbar";
			colorBoxToNormalSize();
			TweenScale.Begin(GameObject.Find("yellowColorBox"), 0, new Vector3(0.15f, 0.3f, 0));
			GameObject.Find("titleSprite_" + indexofnote).GetComponent<UISprite>().spriteName = "yellowTitle";
			GameObject.Find("bodySprite_" + indexofnote).GetComponent<UISprite>().spriteName = "yellowBody";
			GameObject.Find("statusbarSprite_" + indexofnote).GetComponent<UISprite>().spriteName = "yellowStatusbar";
			}
			else
			{
			GameObject.Find("EdittitleSprite").GetComponent<UISprite>().spriteName = "yellowTitle";
			GameObject.Find("EditbodySprite").GetComponent<UISprite>().spriteName = "yellowBody";
			GameObject.Find("editStatusBarSprite").GetComponent<UISprite>().spriteName = "yellowStatusbar";
			GameObject.Find("selecteditStatusBarSprite").GetComponent<UISprite>().spriteName = "yellowStatusbar";
			colorBoxToNormalSize();
			TweenScale.Begin(GameObject.Find("yellowColorBox"), 0, new Vector3(0.15f, 0.3f, 0));
			GameObject.Find("titleSprite_" + new_stickey_note_name).GetComponent<UISprite>().spriteName = "yellowTitle";
			GameObject.Find("bodySprite_" + new_stickey_note_name).GetComponent<UISprite>().spriteName = "yellowBody";
			GameObject.Find("statusbarSprite_" + new_stickey_note_name).GetComponent<UISprite>().spriteName = "yellowStatusbar";
			}
			}

			public void changeColorToBlue()
			{
			if (createnotecolor == false)
			{
			GameObject.Find("EdittitleSprite").GetComponent<UISprite>().spriteName = "blueTitle";
			GameObject.Find("EditbodySprite").GetComponent<UISprite>().spriteName = "blueBody";
			GameObject.Find("editStatusBarSprite").GetComponent<UISprite>().spriteName = "blueStatusbar";
			GameObject.Find("selecteditStatusBarSprite").GetComponent<UISprite>().spriteName = "blueStatusbar";
			colorBoxToNormalSize();
			TweenScale.Begin(GameObject.Find("blueColorBox"), 0, new Vector3(0.15f, 0.3f, 0));
			GameObject.Find("titleSprite_" + indexofnote).GetComponent<UISprite>().spriteName = "blueTitle";
			GameObject.Find("bodySprite_" + indexofnote).GetComponent<UISprite>().spriteName = "blueBody";
			GameObject.Find("statusbarSprite_" + indexofnote).GetComponent<UISprite>().spriteName = "blueStatusbar";
			}
			else
			{
			GameObject.Find("EdittitleSprite").GetComponent<UISprite>().spriteName = "blueTitle";
			GameObject.Find("EditbodySprite").GetComponent<UISprite>().spriteName = "blueBody";
			GameObject.Find("editStatusBarSprite").GetComponent<UISprite>().spriteName = "blueStatusbar";
			GameObject.Find("selecteditStatusBarSprite").GetComponent<UISprite>().spriteName = "blueStatusbar";
			colorBoxToNormalSize();
			TweenScale.Begin(GameObject.Find("blueColorBox"), 0, new Vector3(0.15f, 0.3f, 0));
			GameObject.Find("titleSprite_" + new_stickey_note_name).GetComponent<UISprite>().spriteName = "blueTitle";
			GameObject.Find("bodySprite_" + new_stickey_note_name).GetComponent<UISprite>().spriteName = "blueBody";
			GameObject.Find("statusbarSprite_" + new_stickey_note_name).GetComponent<UISprite>().spriteName = "blueStatusbar";
			}
			}

			public void changeColorToGreen()
			{
			if (createnotecolor == false)
			{
			GameObject.Find("EdittitleSprite").GetComponent<UISprite>().spriteName = "greenTitle";
			GameObject.Find("EditbodySprite").GetComponent<UISprite>().spriteName = "greenBody";
			GameObject.Find("editStatusBarSprite").GetComponent<UISprite>().spriteName = "greenStatusbar";
			GameObject.Find("selecteditStatusBarSprite").GetComponent<UISprite>().spriteName = "greenStatusbar";
			colorBoxToNormalSize();
			TweenScale.Begin(GameObject.Find("greenColorBox"), 0, new Vector3(0.15f, 0.3f, 0));
			GameObject.Find("titleSprite_" + indexofnote).GetComponent<UISprite>().spriteName = "greenTitle";
			GameObject.Find("bodySprite_" + indexofnote).GetComponent<UISprite>().spriteName = "greenBody";
			GameObject.Find("statusbarSprite_" + indexofnote).GetComponent<UISprite>().spriteName = "greenStatusbar";
			}
			else
			{
			GameObject.Find("EdittitleSprite").GetComponent<UISprite>().spriteName = "greenTitle";
			GameObject.Find("EditbodySprite").GetComponent<UISprite>().spriteName = "greenBody";
			GameObject.Find("editStatusBarSprite").GetComponent<UISprite>().spriteName = "greenStatusbar";
			GameObject.Find("selecteditStatusBarSprite").GetComponent<UISprite>().spriteName = "greenStatusbar";
			colorBoxToNormalSize();
			TweenScale.Begin(GameObject.Find("greenColorBox"), 0, new Vector3(0.15f, 0.3f, 0));
			GameObject.Find("titleSprite_" + new_stickey_note_name).GetComponent<UISprite>().spriteName = "greenTitle";
			GameObject.Find("bodySprite_" + new_stickey_note_name).GetComponent<UISprite>().spriteName = "greenBody";
			GameObject.Find("statusbarSprite_" + new_stickey_note_name).GetComponent<UISprite>().spriteName = "greenStatusbar";
			}
			}

			public void changeColorToGrey()
			{
			if (createnotecolor == false)
			{
			GameObject.Find("EdittitleSprite").GetComponent<UISprite>().spriteName = "greyTitle";
			GameObject.Find("EditbodySprite").GetComponent<UISprite>().spriteName = "greyBody";
			GameObject.Find("editStatusBarSprite").GetComponent<UISprite>().spriteName = "greyStatusbar";
			GameObject.Find("selecteditStatusBarSprite").GetComponent<UISprite>().spriteName = "greyStatusbar";
			colorBoxToNormalSize();
			TweenScale.Begin(GameObject.Find("greyColorBox"), 0, new Vector3(0.15f, 0.3f, 0));
			GameObject.Find("titleSprite_" + indexofnote).GetComponent<UISprite>().spriteName = "greyTitle";
			GameObject.Find("bodySprite_" + indexofnote).GetComponent<UISprite>().spriteName = "greyBody";
			GameObject.Find("statusbarSprite_" + indexofnote).GetComponent<UISprite>().spriteName = "greyStatusbar";
			}

			else
			{
			GameObject.Find("EdittitleSprite").GetComponent<UISprite>().spriteName = "greyTitle";
			GameObject.Find("EditbodySprite").GetComponent<UISprite>().spriteName = "greyBody";
			GameObject.Find("editStatusBarSprite").GetComponent<UISprite>().spriteName = "greyStatusbar";
			GameObject.Find("selecteditStatusBarSprite").GetComponent<UISprite>().spriteName = "greyStatusbar";
			colorBoxToNormalSize();
			TweenScale.Begin(GameObject.Find("greyColorBox"), 0, new Vector3(0.15f, 0.3f, 0));
			GameObject.Find("titleSprite_" + new_stickey_note_name).GetComponent<UISprite>().spriteName = "greyTitle";
			GameObject.Find("bodySprite_" + new_stickey_note_name).GetComponent<UISprite>().spriteName = "greyBody";
			GameObject.Find("statusbarSprite_" + new_stickey_note_name).GetComponent<UISprite>().spriteName = "greyStatusbar";
			}
			}

			public void changeColorToPink()
			{
			if (createnotecolor == false)
			{
			GameObject.Find("EdittitleSprite").GetComponent<UISprite>().spriteName = "pinkTitle";
			GameObject.Find("EditbodySprite").GetComponent<UISprite>().spriteName = "pinkBody";
			GameObject.Find("editStatusBarSprite").GetComponent<UISprite>().spriteName = "pinkStatusbar";
			GameObject.Find("selecteditStatusBarSprite").GetComponent<UISprite>().spriteName = "pinkStatusbar";
			colorBoxToNormalSize();
			TweenScale.Begin(GameObject.Find("pinkColorBox"), 0, new Vector3(0.15f, 0.30f, 0));
			GameObject.Find("titleSprite_" + indexofnote).GetComponent<UISprite>().spriteName = "pinkTitle";
			GameObject.Find("bodySprite_" + indexofnote).GetComponent<UISprite>().spriteName = "pinkBody";
			GameObject.Find("statusbarSprite_" + indexofnote).GetComponent<UISprite>().spriteName = "pinkStatusbar";
			}
			else
			{
			GameObject.Find("EdittitleSprite").GetComponent<UISprite>().spriteName = "pinkTitle";
			GameObject.Find("EditbodySprite").GetComponent<UISprite>().spriteName = "pinkBody";
			GameObject.Find("editStatusBarSprite").GetComponent<UISprite>().spriteName = "pinkStatusbar";
			GameObject.Find("selecteditStatusBarSprite").GetComponent<UISprite>().spriteName = "pinkStatusbar";
			colorBoxToNormalSize();
			TweenScale.Begin(GameObject.Find("pinkColorBox"), 0, new Vector3(0.15f, 0.3f, 0));
			GameObject.Find("titleSprite_" + new_stickey_note_name).GetComponent<UISprite>().spriteName = "pinkTitle";
			GameObject.Find("bodySprite_" + new_stickey_note_name).GetComponent<UISprite>().spriteName = "pinkBody";
			GameObject.Find("statusbarSprite_" + new_stickey_note_name).GetComponent<UISprite>().spriteName = "pinkStatusbar";
			}
			}

			public void Addremarks()
			{
                StartCoroutine(AddRemarksenum());
                //LeanTween.scaleX(GameObject.Find("stickeynote_" + indexofnote),1,1);
            }


     IEnumerator AddRemarksenum()
     {
        remarksbtnname = UIButton.current.name;
        remarksindex = int.Parse(remarksbtnname.Substring(13, (remarksbtnname.Length - 13)));

        if (GameObject.Find("noteRemarkLbl_" + remarksindex).GetComponent<UILabel>().alpha == 0)
             {
                LeanTween.scaleX(GameObject.Find("stickeynote_" + remarksindex), 0, 1);
                TweenAlpha.Begin(GameObject.Find("notetitle_" + remarksindex), 0.5f, 0);
                TweenAlpha.Begin(GameObject.Find("notecontent_" + remarksindex), 0.5f, 0);
                yield return new WaitForSeconds(1f);
                LeanTween.scaleX(GameObject.Find("stickeynote_" + remarksindex), 1, 1);
                TweenAlpha.Begin(GameObject.Find("noteRemarkLbl_" + remarksindex), 0.5f, 1);
                TweenAlpha.Begin(GameObject.Find("noteRemarkContent_" + remarksindex), 0.5f, 1);
             }
            
        
        else
        {
            remarksbtnname = UIButton.current.name;
            remarksindex = int.Parse(remarksbtnname.Substring(13, (remarksbtnname.Length - 13)));
            if (GameObject.Find("noteRemarkLbl_" + remarksindex).GetComponent<UILabel>().alpha == 1)
            {
                LeanTween.scaleX(GameObject.Find("stickeynote_" + remarksindex), 0, 1);
                TweenAlpha.Begin(GameObject.Find("notetitle_" + remarksindex), 0.5f, 1);
                TweenAlpha.Begin(GameObject.Find("notecontent_" + remarksindex), 0.5f, 1);
                yield return new WaitForSeconds(1f);
                LeanTween.scaleX(GameObject.Find("stickeynote_" + remarksindex), 1, 1);
                TweenAlpha.Begin(GameObject.Find("noteRemarkLbl_" + remarksindex), 0.5f, 0);
                TweenAlpha.Begin(GameObject.Find("noteRemarkContent_" + remarksindex), 0.5f, 0);
            }
        }
     }

            public void EditAddremarks()
            {
                StartCoroutine(EditAddremarksenum());
            }
            
            public IEnumerator EditAddremarksenum()
            {
                if (editNoteGeneralPopup == true/*GameObject.Find("Editnotetitlelbl_").GetComponent<UILabel>().alpha==1*/)
                {
                    editNoteGeneralPopup = false;
                    editNoteRemarkPopup = true;
                    print("in if");
                    TweenAlpha.Begin(GameObject.Find("EditNoteTitle_"), 0.5f, 0);
                    TweenAlpha.Begin(GameObject.Find("EditNotebody_"), 0.5f, 0);
                    GameObject.Find("EditNoteTitle_").GetComponent<UIInput>().GetComponent<Collider>().enabled = false;
                    GameObject.Find("EditNotebody_").GetComponent<UIInput>().GetComponent<Collider>().enabled = false;
                    GameObject.Find("EditNoteRemarkBody_").GetComponent<UIInput>().GetComponent<Collider>().enabled = true;
                    LeanTween.scaleX(GameObject.Find("EditNote_"), 0, 1f);
                    yield return new WaitForSeconds(1f);
                    LeanTween.scaleX(GameObject.Find("EditNote_"), 1f, 1f);
                    TweenAlpha.Begin(GameObject.Find("EditNoteRemarkTitle_"), 0.5f, 1);
                    TweenAlpha.Begin(GameObject.Find("EditNoteRemarkBody_"), 0.5f, 1);
                }
                else if(editNoteRemarkPopup == true)
                {
                    editNoteGeneralPopup = true;
                    editNoteRemarkPopup = false;
                    print("in else");
                    TweenAlpha.Begin(GameObject.Find("EditNoteRemarkTitle_"), 0.5f, 0);
                    TweenAlpha.Begin(GameObject.Find("EditNoteRemarkBody_"), 0.5f, 0);
                    GameObject.Find("EditNoteTitle_").GetComponent<UIInput>().GetComponent<Collider>().enabled = true;
                    GameObject.Find("EditNotebody_").GetComponent<UIInput>().GetComponent<Collider>().enabled = true;
                    GameObject.Find("EditNoteRemarkBody_").GetComponent<UIInput>().GetComponent<Collider>().enabled = false;
                    LeanTween.scaleX(GameObject.Find("EditNote_"), 0, 1f);
                    yield return new WaitForSeconds(1f);
                    LeanTween.scaleX(GameObject.Find("EditNote_"), 1f, 1f);
                    TweenAlpha.Begin(GameObject.Find("EditNoteTitle_"), 0.5f,1);
                    TweenAlpha.Begin(GameObject.Find("EditNotebody_"), 0.5f, 1);
                }
            }
            
    		public void changeStatusOfNote()
			{
			    TweenAlpha.Begin(GameObject.Find("editNoteSelectNoteStatusContainer"), 0.5f, 1);
			    TweenPosition.Begin(GameObject.Find("editNoteSelectNoteStatusContainer"), 0.5f, new Vector2(-4, -135));
			}
            
			public void changeStatusToProven()
			{
			if (notestatusbool == false)
			{
			print("proved...false");
			GameObject.Find("editNoteStatus").GetComponent<UILabel>().text = "Proven";
			GameObject.Find("noteStatus_" + indexofnote).GetComponent<UILabel>().text = "Proven";
			TweenAlpha.Begin(GameObject.Find("editNoteSelectNoteStatusContainer"), 0.5f, 0);
			TweenPosition.Begin(GameObject.Find("editNoteSelectNoteStatusContainer"), 0.5f, new Vector2(-4, -190));
			}
			else
			{
			print("proved...true");
			GameObject.Find("editNoteStatus").GetComponent<UILabel>().text = "Proven";
			GameObject.Find("noteStatus_" + new_stickey_note_name).GetComponent<UILabel>().text = "Proven";
			TweenAlpha.Begin(GameObject.Find("editNoteSelectNoteStatusContainer"), 0.5f, 0);
			TweenPosition.Begin(GameObject.Find("editNoteSelectNoteStatusContainer"), 0.5f, new Vector2(-4, -190));
			}
			}

			public void changeStatusToNotProven()
			{
			if (notestatusbool == false)
			{
			print("notproved...false");
			GameObject.Find("editNoteStatus").GetComponent<UILabel>().text = "Not Proven";
			GameObject.Find("noteStatus_" + indexofnote).GetComponent<UILabel>().text = "Not Proven";
			TweenAlpha.Begin(GameObject.Find("editNoteSelectNoteStatusContainer"), 0.5f, 0);
			TweenPosition.Begin(GameObject.Find("editNoteSelectNoteStatusContainer"), 0.5f, new Vector2(-4, -190));
			}
			else
			{
			print("notproved...true");
			GameObject.Find("editNoteStatus").GetComponent<UILabel>().text = "Not Proven";
			GameObject.Find("noteStatus_" + new_stickey_note_name).GetComponent<UILabel>().text = "Not Proven";
			TweenAlpha.Begin(GameObject.Find("editNoteSelectNoteStatusContainer"), 0.5f, 0);
			TweenPosition.Begin(GameObject.Find("editNoteSelectNoteStatusContainer"), 0.5f, new Vector2(-4, -190));
			}
			}
            
			public void changeStatusToNotValidated()
			{

            if (notestatusbool == false)
			{
			print("notvalidate...false");
			GameObject.Find("editNoteStatus").GetComponent<UILabel>().text = "Not Validated";
			GameObject.Find("noteStatus_" + indexofnote).GetComponent<UILabel>().text = "Not Validated";
			TweenAlpha.Begin(GameObject.Find("editNoteSelectNoteStatusContainer"), 0.5f, 0);
			TweenPosition.Begin(GameObject.Find("editNoteSelectNoteStatusContainer"), 0.5f, new Vector2(-4, -190));
			}

            else
			{
			print("notvalidate...true");
			GameObject.Find("editNoteStatus").GetComponent<UILabel>().text = "Not Validated";
			GameObject.Find("noteStatus_" + new_stickey_note_name).GetComponent<UILabel>().text = "Not Validated";
			TweenAlpha.Begin(GameObject.Find("editNoteSelectNoteStatusContainer"), 0.5f, 0);
			TweenPosition.Begin(GameObject.Find("editNoteSelectNoteStatusContainer"), 0.5f, new Vector2(-4, -190));
			}
			}

			IEnumerator updateoff()
			{
			yield return new WaitForSeconds(3f);
			updateon = false;
			}
			}

