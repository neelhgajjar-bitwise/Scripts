using UnityEngine;
using System.Collections;
using System.IO;
using System.Data;
using Mono.Data.Sqlite;

public class ChangeWallpaper : MonoBehaviour 
{
	GameObject wallpaperConainter,wallpaperPopupContainer;
	GameObject currentWallpaper;
	GameObject wallpaper1Container,wallpaper2Container,wallpaper3Container;
	GameObject closePopupButton;
	GameObject selectYourBackgroundLbl;
	string connection;
	IDbConnection dbcon;
	IDbCommand dbcmd;
	IDataReader reader;

	void Start()
	{		
		wallpaperConainter = GameObject.Find ("Wallpaper Container");
		wallpaperPopupContainer = GameObject.Find ("Wallpaper Popup Container");
		wallpaper1Container = GameObject.Find ("wallpaper1");
		wallpaper2Container = GameObject.Find ("wallpaper2");
		wallpaper3Container = GameObject.Find ("wallpaper3");
		currentWallpaper = GameObject.Find ("BackgroundImg");
		closePopupButton = GameObject.Find ("Background Container");
		selectYourBackgroundLbl = GameObject.Find ("selectYourBackgroundLbl");
		changeWallpaperOnStartFunction ();
	}

	public void closeWallpaerPopup()
	{
		StartCoroutine (closeWallpaerPopupAnimation ());
	}

	IEnumerator closeWallpaerPopupAnimation()
	{		
		closePopupButton.GetComponent<BoxCollider> ().enabled = false;
		selectYourBackgroundLbl.GetComponent<UILabel> ().enabled = false;
		TweenScale.Begin (wallpaper1Container, 0.2f, new Vector2 (0,0));
		TweenScale.Begin (wallpaper2Container, 0.2f, new Vector2 (0,0));
		TweenScale.Begin (wallpaper3Container, 0.2f, new Vector2 (0,0));
		yield return new WaitForSeconds (0.2f);

		TweenScale.Begin (wallpaperPopupContainer,0.4f,new Vector2(1,0.3f));
		yield return new WaitForSeconds (0.4f);
		TweenScale.Begin (wallpaperPopupContainer,0.6f,new Vector2(0,0.3f));
		yield return new WaitForSeconds (0.6f);

		wallpaperConainter.GetComponent<UIWidget> ().alpha = 0;
	}

	public void openWallpaperPopup()
	{
		wallpaperConainter.GetComponent<UIWidget> ().alpha = 1;
		StartCoroutine (openWallpaperPopupAnimation ());
	}

	IEnumerator openWallpaperPopupAnimation ()
	{
		selectYourBackgroundLbl.GetComponent<UILabel> ().enabled = false;
		closePopupButton.GetComponent<BoxCollider> ().enabled = false;
		TweenScale.Begin (wallpaperPopupContainer,0f,new Vector2(0,0.3f));
		TweenScale.Begin (wallpaperPopupContainer,0.6f,new Vector2(1,0.3f));
		yield return new WaitForSeconds (0.6f);
		TweenScale.Begin (wallpaperPopupContainer,0.4f,new Vector2(1,1f));
		yield return new WaitForSeconds (0.4f);

		TweenScale.Begin (wallpaper1Container, 0f, new Vector2 (0,0));
		TweenScale.Begin (wallpaper2Container, 0f, new Vector2 (0,0));
		TweenScale.Begin (wallpaper3Container, 0f, new Vector2 (0,0));

		TweenScale.Begin (wallpaper1Container, 0.5f, new Vector2 (1,1));
		yield return new WaitForSeconds (0.3f);
		TweenScale.Begin (wallpaper2Container, 0.5f, new Vector2 (1,1));
		yield return new WaitForSeconds (0.3f);
		TweenScale.Begin (wallpaper3Container, 0.5f, new Vector2 (1,1));
		selectYourBackgroundLbl.GetComponent<UILabel> ().enabled = true;
		closePopupButton.GetComponent<BoxCollider> ().enabled = true;
	}	
		
	public void changeWallpaperFunction ()
	{				
		closePopupButton.GetComponent<BoxCollider> ().enabled = false;
		string wallpaperName = UIButton.current.name;

		// DATABASE
		//print ("PROJECT ID " + notesaveScript.projectidnumber);
		string filepath = Application.persistentDataPath + "/" + "BMCDatabase.db";
		if (!File.Exists (filepath)) {
			print ("in file path");
			string path = "";
			#if UNITY_EDITOR
			path = "file://" + Application.dataPath + "/StreamingAssets/" + "BMCDatabase.db";
			#elif UNITY_ANDROID
			path ="jar:file://" + Application.dataPath + "!/assets/"+ "BMCDatabase.db";
			#elif UNITY_IOS || UNITY_IPHONE
			path = "file://"+Application.dataPath + "/Raw/"+ "BMCDatabase.db";
			#endif
			WWW loadDB = new WWW (path);
			while (!loadDB.isDone) {
			}
			File.WriteAllBytes (filepath, loadDB.bytes);
		}
		connection = "URI=file:" + filepath;
		//Debug.Log ("Stablishing connection to: " + connection);
		dbcon = new SqliteConnection (connection);
		dbcon.Open ();
		//dbcmd = dbcon.CreateCommand();
		IDbCommand dbcmUpdateWallpaper = dbcon.CreateCommand ();					
		string sqlUpdateWallpaper = "UPDATE projectTable SET backgroundimg_name="+ "'" + wallpaperName + "'" +" where proj_id=" + notesaveScript.projectidnumber;
		dbcmUpdateWallpaper.CommandText = sqlUpdateWallpaper;
		reader = dbcmUpdateWallpaper.ExecuteReader ();

		dbcon.Close ();
		dbcon = null;
		dbcmUpdateWallpaper.Dispose ();
		dbcmUpdateWallpaper = null;
		
		//
		currentWallpaper.GetComponent<UISprite> ().spriteName = wallpaperName;
		closeWallpaerPopup ();
	}

	public void changeWallpaperOnStartFunction ()
	{		
		
		// DATABASE
		string filepath = Application.persistentDataPath + "/" + "BMCDatabase.db";
		if (!File.Exists (filepath)) {
		print ("in file path");
		string path = "";
		#if UNITY_EDITOR
			path = "file://" + Application.dataPath + "/StreamingAssets/" + "BMCDatabase.db";
		#elif UNITY_ANDROID
			path ="jar:file://" + Application.dataPath + "!/assets/"+"BMCDatabase.db";
		#elif UNITY_IOS || UNITY_IPHONE
			path = "file://"+Application.dataPath + "/Raw/"+"BMCDatabase.db";
		#endif
		WWW loadDB = new WWW (path);
		while (!loadDB.isDone) {
		}
		File.WriteAllBytes (filepath, loadDB.bytes);
		}
		connection = "URI=file:" + filepath;
		Debug.Log ("Stablishing connection to: " + connection);
		dbcon = new SqliteConnection (connection);
		dbcon.Open ();
		//dbcmd = dbcon.CreateCommand();
		IDbCommand dbcmUpdateWallpaper = dbcon.CreateCommand ();					
		//string sqlUpdateWallpaper = "UPDATE projectTable SET backgroundimg_name="+ "'" + wallpaperName + "'" +" where proj_id=" + notesaveScript.projectidnumber;
		string sqlUpdateWallpaper = "select backgroundimg_name from projectTable where proj_id=" + notesaveScript.projectidnumber;
		dbcmUpdateWallpaper.CommandText = sqlUpdateWallpaper;
		reader = dbcmUpdateWallpaper.ExecuteReader ();
		while (reader.Read ()) 
		{
			currentWallpaper.GetComponent<UISprite> ().spriteName = reader.GetString (0);
		}
		dbcon.Close ();
		dbcon = null;
		dbcmUpdateWallpaper.Dispose ();
		dbcmUpdateWallpaper = null;		
	}
}

