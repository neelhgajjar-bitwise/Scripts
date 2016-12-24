using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CanvasScript : MonoBehaviour
{
	public GameObject rearrange_New_NoteContainer = null;
	public GameObject note_Container = null;
	public GameObject note_grid_Container = null;

    bool delete_box_status = false;
    int widthofscreenvalue, widthOfLeftPanel, pos_of_x_leftpanel;
    int widthofmiddlecontainer, widthoficoncontainer, heightofmiddlecontainer;
    int vonelineposx, vtwolineposx, vthreelineposx, vfourlineposx, vfivelineposx, vsixlineposx;
    string widthofscreen;
    string hsmalllinewidth, hlonglinewidth;
    float hsmalllinewidthval, hlonglineval;
    float yposofverticalline;
    float xpos2line, xpos3line, xpos6line, xpos1line, xpos4line, xpos5line;
    public static int widthofsmalllineval, widthoflonglineval;
    string notename;
    string name_of_btn;
    float xposstickey, yposstickey;
    public static string canvasTitle = "";
    public Collider deletecollider;
    public static GameObject[] noteContainer = null;
    GameObject[] stickeynoteContainer = null;
    GameObject[] colorContainer = null;
    int noteCounter = 0;
    bool commentval = false;
    public GameObject deletecontainer, blackbgcontainer;
    public Collider notecollider;
    int indexofcomment, indexofcolor;
    int indexofcolorselected;
    string nameofcolornote, notecolor;
    bool viewnotebool = false;
    string globalnumberforedit;
    string btnname;
    //Use this for initialization

	bool flagNewNoteRearrange = false;
	void Update()
	{
		if ((note_Container.GetComponent<UIWidget> ().transform.childCount + note_grid_Container.GetComponent<UIGrid> ().transform.childCount) == 2) 
		{
			if (flagNewNoteRearrange == true) 
			{
				rearrange_New_NoteContainer.GetComponent<UIWidget> ().alpha = 0;
				flagNewNoteRearrange = false;
			}
		}
		else
		{
			if (flagNewNoteRearrange == false) 
			{				
				rearrange_New_NoteContainer.GetComponent<UIWidget> ().alpha = 1;
				flagNewNoteRearrange = true;
			}
		}
	}

	void setAlphaToIcons()
	{
		TweenAlpha.Begin (GameObject.Find ("viewnotebycolorimg"), 0, 0.1f);
	}

    void Start()
    {
		//setAlphaToIcons ();
		rearrange_New_NoteContainer = GameObject.Find ("rearrangeNewNote");
		note_Container = GameObject.Find ("note_");
		note_grid_Container = GameObject.Find ("newnoteGrid");

        GameObject.Find("titlelbl").GetComponent<UILabel>().text = canvasTitle + " v1";

        TweenPosition.Begin(GameObject.Find("LeftPanelContainer"), 0, new Vector2(-1000, 0));
        TweenPosition.Begin(GameObject.Find("IconContainer"), 0, new Vector2(-1000, 0));
        // TweenAlpha.Begin(GameObject.Find("note_0"),0,0.01f);
        hideIcon();
        disablebackgroundIconButtons();
        hideallbackgroundIconButtons();
        StartCoroutine(calculatewidth());
        TweenAlpha.Begin(GameObject.Find("middleContainer"), 0, 0.01f);
        noteContainer = new GameObject[100];
        stickeynoteContainer = new GameObject[100];
        colorContainer = new GameObject[100];
        TweenAlpha.Begin(GameObject.Find("note_"), 0, 0);
        StartCoroutine(manualStart());
        //setallverticalline();
    }

    void hideIcon()
    {
        TweenAlpha.Begin(GameObject.Find("newnoteimg"), 0, 0);
        TweenAlpha.Begin(GameObject.Find("deletenoteimg"), 0, 0);
        TweenAlpha.Begin(GameObject.Find("viewnotebycolorimg"), 0, 0);
        TweenAlpha.Begin(GameObject.Find("pptimg"), 0, 0);
        TweenAlpha.Begin(GameObject.Find("titleviseimg"), 0, 0);
        TweenAlpha.Begin(GameObject.Find("backgroundselectimg"), 0, 0);
        TweenAlpha.Begin(GameObject.Find("walletimg"), 0, 0);
    }

    void disablebackgroundIconButtons()
    {
        GameObject.Find("newnoteBGimg").GetComponent<UISprite>().enabled = false;
        GameObject.Find("deletenoteBGimg").GetComponent<UISprite>().enabled = false;
        GameObject.Find("viewnotebyBGimg").GetComponent<UISprite>().enabled = false;
        GameObject.Find("pptBGimg").GetComponent<UISprite>().enabled = false;
        GameObject.Find("titleviseBGimg").GetComponent<UISprite>().enabled = false;
        GameObject.Find("backgroundselectBGimg").GetComponent<UISprite>().enabled = false;
        GameObject.Find("WalletBGimg").GetComponent<UISprite>().enabled = false;
    }

    void hideallbackgroundIconButtons()
    {
        TweenAlpha.Begin(GameObject.Find("newnoteBGimg"), 0, 0);
        TweenAlpha.Begin(GameObject.Find("deletenoteBGimg"), 0, 0);
        TweenAlpha.Begin(GameObject.Find("viewnotebyBGimg"), 0, 0);
        TweenAlpha.Begin(GameObject.Find("pptBGimg"), 0, 0);
        TweenAlpha.Begin(GameObject.Find("titleviseBGimg"), 0, 0);
        TweenAlpha.Begin(GameObject.Find("backgroundselectBGimg"), 0, 0);
        TweenAlpha.Begin(GameObject.Find("WalletBGimg"), 0, 0);
    }

    IEnumerator calculatewidth()
    {
        yield return new WaitForSeconds(0.01f);
        widthofscreenvalue = GameObject.Find("wholeContainer").GetComponent<UIWidget>().width;
        print("widthofscreenvalue===>>" + widthofscreenvalue);
        widthOfLeftPanel = ((widthofscreenvalue * 13) / 100);
        print("widthOfLeftPanel" + widthOfLeftPanel);
        GameObject.Find("LeftPanelContainer").GetComponent<UIWidget>().width = widthOfLeftPanel;
        pos_of_x_leftpanel = widthofscreenvalue / 2;
        //widthofmiddlecontainer = GameObject.Find("middleContainer").GetComponent<UIWidget>().width/2;
        print("pos_of_x_leftpanel" + pos_of_x_leftpanel);
        //print("widthofmiddlecontainer===>>" + widthofmiddlecontainer);
        StartCoroutine(leftpanelAnimation());
    }

    IEnumerator leftpanelAnimation()
    {
        yield return new WaitForSeconds(1f);
        TweenPosition.Begin(GameObject.Find("LeftPanelContainer"), 1f, new Vector3(-pos_of_x_leftpanel, 0, 0));
        widthoficoncontainer = GameObject.Find("IconContainer").GetComponent<UIWidget>().width;
        StartCoroutine(IconContainer());
    }

    IEnumerator IconContainer()
    {
        yield return new WaitForSeconds(0.5f);
        //GameObject.Find("IconContainer").GetComponent<UIWidget>().width = GameObject.Find("LeftPanelContainer").GetComponent<UIWidget>().width;
        TweenPosition.Begin(GameObject.Find("IconContainer"), 0.5f, new Vector2(-pos_of_x_leftpanel, 156));
        StartCoroutine(movefirstIcon());
    }

    IEnumerator movefirstIcon()
    {
        yield return new WaitForSeconds(0.15f);
        TweenAlpha.Begin(GameObject.Find("newnoteimg"), 0, 1);
        TweenPosition.Begin(GameObject.Find("newnoteimg"), 0.15f, new Vector2((widthoficoncontainer / 2) + 20, 80));
        yield return new WaitForSeconds(0.15f);
        TweenPosition.Begin(GameObject.Find("newnoteimg"), 0.15f, new Vector2(widthoficoncontainer / 2, 80));
        GameObject.Find("newnoteimg").GetComponent<UISprite>().GetComponent<UIButton>().defaultColor = Color.white;
        StartCoroutine(movesecondIcon());
    }

    IEnumerator movesecondIcon()
    {
        //TweenPosition.Begin(GameObject.Find("deletenoteimg"), 0, new Vector2(100,-74));
        TweenAlpha.Begin(GameObject.Find("deletenoteimg"), 0, 1);
        TweenPosition.Begin(GameObject.Find("deletenoteimg"), 0.15f, new Vector2((widthoficoncontainer / 2) + 20, -10));
        yield return new WaitForSeconds(0.15f);
        TweenPosition.Begin(GameObject.Find("deletenoteimg"), 0.15f, new Vector2((widthoficoncontainer / 2), -10));
        // GameObject.Find("deletenoteimg").GetComponent<UISprite>().GetComponent<UIButton>().defaultColor = Color.white;
        StartCoroutine(movethirdIcon());
    }

    IEnumerator movethirdIcon()
    {
        //TweenPosition.Begin(GameObject.Find("viewnotebycolorimg"), 0, new Vector2(100, -149));
        TweenAlpha.Begin(GameObject.Find("viewnotebycolorimg"), 0, 1);
        TweenPosition.Begin(GameObject.Find("viewnotebycolorimg"), 0.15f, new Vector2((widthoficoncontainer / 2) + 20, -100));
        yield return new WaitForSeconds(0.15f);
        TweenPosition.Begin(GameObject.Find("viewnotebycolorimg"), 0.15f, new Vector2((widthoficoncontainer / 2), -100));
        GameObject.Find("viewnotebycolorimg").GetComponent<UISprite>().GetComponent<UIButton>().defaultColor = Color.white;
        StartCoroutine(movefourthIcon());
    }

    IEnumerator movefourthIcon()
    {
        //TweenPosition.Begin(GameObject.Find("pptimg"), 0, new Vector2(100, -224));
        TweenAlpha.Begin(GameObject.Find("pptimg"), 0, 1);
        TweenPosition.Begin(GameObject.Find("pptimg"), 0.15f, new Vector2((widthoficoncontainer / 2) + 20, -190));
        yield return new WaitForSeconds(0.15f);
        TweenPosition.Begin(GameObject.Find("pptimg"), 0.15f, new Vector2((widthoficoncontainer / 2), -190));
        GameObject.Find("pptimg").GetComponent<UISprite>().GetComponent<UIButton>().defaultColor = Color.white;
        StartCoroutine(movefifthIcon());
    }

    IEnumerator movefifthIcon()
    {
        // TweenPosition.Begin(GameObject.Find("titleviseimg"), 0, new Vector2(100, -300));
        TweenAlpha.Begin(GameObject.Find("titleviseimg"), 0, 1);
        TweenPosition.Begin(GameObject.Find("titleviseimg"), 0.15f, new Vector2((widthoficoncontainer / 2) + 20, -280));
        yield return new WaitForSeconds(0.15f);
        TweenPosition.Begin(GameObject.Find("titleviseimg"), 0.15f, new Vector2((widthoficoncontainer / 2), -280));
        GameObject.Find("titleviseimg").GetComponent<UISprite>().GetComponent<UIButton>().defaultColor = Color.white;
        StartCoroutine(movesixthIcon());
    }

    IEnumerator movesixthIcon()
    {
        TweenAlpha.Begin(GameObject.Find("backgroundselectimg"), 0, 1);
        TweenPosition.Begin(GameObject.Find("backgroundselectimg"), 0.15f, new Vector2((widthoficoncontainer / 2) + 20, -370));
        yield return new WaitForSeconds(0.15f);
        TweenPosition.Begin(GameObject.Find("backgroundselectimg"), 0.15f, new Vector2((widthoficoncontainer / 2), -370));
        GameObject.Find("backgroundselectimg").GetComponent<UISprite>().GetComponent<UIButton>().defaultColor = Color.white;
        StartCoroutine(moveseventhIcon());
    }

    IEnumerator moveseventhIcon()
    {
        TweenAlpha.Begin(GameObject.Find("walletimg"), 0, 1);
        TweenPosition.Begin(GameObject.Find("walletimg"), 0.15f, new Vector2((widthoficoncontainer / 2) + 20, -460));
        yield return new WaitForSeconds(0.15f);
        TweenPosition.Begin(GameObject.Find("walletimg"), 0.15f, new Vector2((widthoficoncontainer / 2), -460));
        //TweenAlpha.Begin(GameObject.Find("middleContainer"), 1f, 1);
        widthofmiddlecontainer = GameObject.Find("middleContainer").GetComponent<UIWidget>().width / 2;
        heightofmiddlecontainer = GameObject.Find("middleContainer").GetComponent<UIWidget>().height;
        print("widthofmiddlecontainer===>>" + widthofmiddlecontainer);
        print("heightofmiddlecontainer" + heightofmiddlecontainer);
        GameObject.Find("walletimg").GetComponent<UISprite>().GetComponent<UIButton>().defaultColor = Color.white;
        StartCoroutine(setAlllines());
    }

    IEnumerator setAlllines()
    {
        yield return new WaitForSeconds(0.5f);
        yposofverticalline = (heightofmiddlecontainer * 10) / 100;
        TweenPosition.Begin(GameObject.Find("1verticalline"), 0, new Vector2((-(widthofmiddlecontainer * 95) / 100), yposofverticalline));
        TweenPosition.Begin(GameObject.Find("2verticalline"), 0, new Vector2((-(widthofmiddlecontainer * 57) / 100), yposofverticalline));
        TweenPosition.Begin(GameObject.Find("3verticalline"), 0, new Vector2((-(widthofmiddlecontainer * 19) / 100), yposofverticalline));
        TweenPosition.Begin(GameObject.Find("4verticalline"), 0, new Vector2(((widthofmiddlecontainer * 19) / 100), yposofverticalline));
        TweenPosition.Begin(GameObject.Find("5verticalline"), 0, new Vector2(((widthofmiddlecontainer * 57) / 100), yposofverticalline));
        TweenPosition.Begin(GameObject.Find("6verticalline"), 0, new Vector2(((widthofmiddlecontainer * 95) / 100), yposofverticalline));
        //yield return new WaitForSeconds(2f);
        xpos1line = GameObject.Find("1verticalline").GetComponent<UISprite>().transform.localPosition.x;
        xpos2line = GameObject.Find("2verticalline").GetComponent<UISprite>().transform.localPosition.x;
        xpos3line = GameObject.Find("3verticalline").GetComponent<UISprite>().transform.localPosition.x;
        xpos4line = GameObject.Find("4verticalline").GetComponent<UISprite>().transform.localPosition.x;
        xpos5line = GameObject.Find("5verticalline").GetComponent<UISprite>().transform.localPosition.x;
        xpos6line = GameObject.Find("6verticalline").GetComponent<UISprite>().transform.localPosition.x;
        print("xpos2line==" + xpos2line + "  " + "xpos3line==" + xpos3line);
        hsmalllinewidthval = xpos2line - xpos3line;
        hlonglineval = xpos1line - xpos6line;
        hsmalllinewidth = hsmalllinewidthval.ToString();
        hlonglinewidth = hlonglineval.ToString();
        print("hsmalllinewidth==" + hsmalllinewidth);
        widthofsmalllineval = int.Parse(hsmalllinewidth);
        widthoflonglineval = int.Parse(hlonglinewidth);
        GameObject.Find("smallHorizontalLine1").GetComponent<UISprite>().GetComponent<UIWidget>().width = -widthofsmalllineval;
        GameObject.Find("smallHorizontalLine2").GetComponent<UISprite>().GetComponent<UIWidget>().width = -widthofsmalllineval;
        GameObject.Find("horizontalLongLine").GetComponent<UISprite>().GetComponent<UIWidget>().width = -widthoflonglineval;
        // print("x===" + x);
        TweenPosition.Begin(GameObject.Find("smallHorizontalLine1"), 0, new Vector2(float.Parse(hsmalllinewidth), ((heightofmiddlecontainer * 10) / 100)));
        TweenPosition.Begin(GameObject.Find("smallHorizontalLine2"), 0, new Vector2(-float.Parse(hsmalllinewidth), ((heightofmiddlecontainer * 10) / 100)));
        TweenPosition.Begin(GameObject.Find("horizontalLongLine"), 0, new Vector2(0, -(heightofmiddlecontainer / 4.5f)));
        TweenPosition.Begin(GameObject.Find("smallVerticalLine"), 0, new Vector2(0, -(heightofmiddlecontainer / 2.75f)));
        StartCoroutine(setTopbar());
    }

    IEnumerator setTopbar()
    {
        yield return new WaitForSeconds(0.1f);
        TweenPosition.Begin(GameObject.Find("titlelbl"), 0, new Vector2(-(widthofmiddlecontainer) + 15f, (heightofmiddlecontainer / 2) - 30));
        //TweenPosition.Begin(GameObject.Find("savelbl"), 0, new Vector2(xpos6line, (heightofmiddlecontainer / 2) - 30));
        //TweenPosition.Begin(GameObject.Find("savebtn"), 0, new Vector2(xpos6line - 50, (heightofmiddlecontainer / 2) - 30));
        //TweenPosition.Begin(GameObject.Find("backlbl"), 0, new Vector2(xpos6line - 90, (heightofmiddlecontainer / 2) - 30));
        //TweenPosition.Begin(GameObject.Find("backbtn"), 0, new Vector2(xpos6line - 125, (heightofmiddlecontainer / 2) - 30));
        StartCoroutine(setAllninecontainerLbl());
    }

    IEnumerator setAllninecontainerLbl()
    {
        yield return new WaitForSeconds(.1f);
        TweenAlpha.Begin(GameObject.Find("middleContainer"), 1f, 1);
    }

    public void createnewnote()
    {
        print("called..............." + notesaveScript.notecounter);
        noteCounter = notesaveScript.notecounter;
        print("notecounter---------------------------------------" + noteCounter);
        noteContainer[noteCounter] = (GameObject)NGUITools.AddChild(GameObject.Find("note_"), GameObject.Find("stickeynote_"));
        //print("noteContainer[noteCounter]" + noteContainer[noteCounter]);
        noteContainer[noteCounter].GetComponent<UIWidget>().name = ("stickeynote_" + noteCounter).ToString();
        //  stickeynoteContainer[noteCounter].GetComponent<UIWidget>().name = ("RemarksContainer_" + noteCounter).ToString();
        defineNameAtRuntime();
        GameObject.Find("stickeynote_" + noteCounter).GetComponent<UISprite>().GetComponent<UIWidget>().width = -widthofsmalllineval - 38;

        //t = this.gameObject.transform.GetChild(0);
        //for all devices note size....
        /*GameObject.Find("grey_").GetComponent<UISprite>().GetComponent<UIWidget>().width= (-widthofsmalllineval - 38)/5;
          GameObject.Find("yellow_").GetComponent<UISprite>().GetComponent<UIWidget>().width = (-widthofsmalllineval - 38)/5;
          GameObject.Find("pink_").GetComponent<UISprite>().GetComponent<UIWidget>().width = (-widthofsmalllineval - 38)/5;
          GameObject.Find("blue_").GetComponent<UISprite>().GetComponent<UIWidget>().width = (-widthofsmalllineval - 38)/5;
          GameObject.Find("green_").GetComponent<UISprite>().GetComponent<UIWidget>().width = (-widthofsmalllineval - 38)/5;
       */
        TweenScale.Begin(GameObject.Find("stickeynote_"), 0, new Vector3(0, 0, 0));
        TweenAlpha.Begin(GameObject.Find("note_"), 0, 1);
        //StartCoroutine(visibleRemarksButton());
        noteCounter++;
    }

    /* IEnumerator visibleRemarksButton()
     {
         yield return new WaitForSeconds(1f);
         string strnote = GameObject.Find("stickeynote_" + noteCounter).GetComponent<UISprite>().parent.ToString();
         print("strnote" + strnote);
         StartCoroutine(visibleRemarksButton());
     }
     */
    void defineNameAtRuntime()
    {
        GameObject.Find("stickeynote_" + noteCounter).transform.GetChild(0).name = ("stickeyNoteContainer_" + noteCounter).ToString();
       // GameObject.Find("stickeynote_" + noteCounter).transform.GetChild(1).name = ("stickeyNoteContainer_" + noteCounter).ToString();
       // GameObject.Find("stickeynote_" + noteCounter).transform.GetChild(2).name = ("stickeyNoteContainer_" + noteCounter).ToString();
       // GameObject.Find("stickeynote_" + noteCounter).transform.GetChild(0).name = ("stickeyNoteContainer_" + noteCounter).ToString();
        // GameObject.Find("stickeyNoteContainer_" + noteCounter).transform.GetChild(0).name = ("ColorContainer_" + noteCounter).ToString();
        // GameObject.Find("stickeyNoteContainer_" + noteCounter).transform.GetChild(1).name = ("RemarksContainer_" + noteCounter).ToString();
        GameObject.Find("stickeyNoteContainer_" + noteCounter).transform.GetChild(2).name = ("mainbodyContainer_" + noteCounter).ToString();
    //    GameObject.Find("stickeyNoteContainer_" + noteCounter).transform.GetChild(3).name = ("Remarksnoteimg_" + noteCounter).ToString();
      //  GameObject.Find("stickeyNoteContainer_" + noteCounter).transform.GetChild(4).name = ("colorbyimg_" + noteCounter).ToString();
        //  GameObject.Find("stickeyNoteContainer_" + noteCounter).transform.GetChild(5).name = ("notebodyimg_" + noteCounter).ToString();
        //  GameObject.Find("stickeyNoteContainer_" + noteCounter).transform.GetChild(6).name = ("notetitleimg_" + noteCounter).ToString();
       // GameObject.Find("ColorContainer_" + noteCounter).transform.GetChild(0).name = ("grey_" + noteCounter).ToString();
       // GameObject.Find("ColorContainer_" + noteCounter).transform.GetChild(1).name = ("yellow_" + noteCounter).ToString();
       // GameObject.Find("ColorContainer_" + noteCounter).transform.GetChild(2).name = ("pink_" + noteCounter).ToString();
       // GameObject.Find("ColorContainer_" + noteCounter).transform.GetChild(3).name = ("blue_" + noteCounter).ToString();
       // GameObject.Find("ColorContainer_" + noteCounter).transform.GetChild(4).name = ("green_" + noteCounter).ToString();
      //  GameObject.Find("RemarksContainer_" + noteCounter).transform.GetChild(0).name = ("Remarkstitlelbl_" + noteCounter).ToString();
       // GameObject.Find("RemarksContainer_" + noteCounter).transform.GetChild(1).name = ("Remarksbodylbl_" + noteCounter).ToString();
        GameObject.Find("mainbodyContainer_" + noteCounter).transform.GetChild(0).name = ("notetitle_" + noteCounter).ToString();
        GameObject.Find("mainbodyContainer_" + noteCounter).transform.GetChild(1).name = ("notecontent_" + noteCounter).ToString();
       // GameObject.Find("grey_" + noteCounter).transform.GetChild(0).name = ("greyselect_" + noteCounter).ToString();
       // GameObject.Find("yellow_" + noteCounter).transform.GetChild(0).name = ("yellowselect_" + noteCounter).ToString();
       // GameObject.Find("pink_" + noteCounter).transform.GetChild(0).name = ("pinkselect_" + noteCounter).ToString();
       // GameObject.Find("blue_" + noteCounter).transform.GetChild(0).name = ("blueselect_" + noteCounter).ToString();
       // GameObject.Find("green_" + noteCounter).transform.GetChild(0).name = ("greenselect_" + noteCounter).ToString();
    }

    void deletenote()
    {
        delete_box_status = true;
        print("#*#*#*#*#*#*#*#*#*#*#*#*#*#*#*#*#*#*#*#*");
        // deletecontainer.SetActive(true);
        // blackbgcontainer.SetActive(true);
        string name_of_btncontainer = GameObject.Find(name_of_btn).transform.GetChild(0).name;
        TweenAlpha.Begin(GameObject.Find(name_of_btncontainer), 0, 0.3f);
        GameObject.Find("BlackbackgroundContainer").GetComponent<UIWidget>().alpha = 0.8f;
        GameObject.Find("middleContainer").GetComponent<UIWidget>().alpha = 0.15f;
        GameObject.Find("deletenotecontainer").GetComponent<UIWidget>().alpha = 1;
    }

    public void deleteBoxFlagFalse()
    {
        //delete_box_status = false;
    }

    void hidedeletecontainer()
    {
        // GameObject.Find("BlackbackgroundContainer").GetComponent<UIWidget>().alpha = 0;
        // GameObject.Find("deletenotecontainer").GetComponent<UIWidget>().alpha = 0;
    }

    public void deletenoteyes()
    {
        Destroy(GameObject.Find(name_of_btn));
        GameObject.Find("BlackbackgroundContainer").GetComponent<UIWidget>().alpha = 0;
        GameObject.Find("deletenotecontainer").GetComponent<UIWidget>().alpha = 0;
        GameObject.Find("middleContainer").GetComponent<UIWidget>().alpha = 1f;
    }

   

    public void backToNewproject()
    {
        SceneManager.LoadScene("createNewproj");
    }

    public void Addcomment()
    {
        print("UIButton.current.name" + UIButton.current.name);
        string nameofcmtbtn = UIButton.current.name;
        string substr = nameofcmtbtn.Substring(15, (nameofcmtbtn.Length - 15));
        indexofcomment = int.Parse(substr);
        //print("............"+nameofcmtbtn.Substring();
        if (commentval == false)
        {
            TweenAlpha.Begin(GameObject.Find("mainbodyContainer_" + indexofcomment), 0, 0);
            LeanTween.rotateY(GameObject.Find("stickeynote_" + indexofcomment), 180, 1.25f);
            TweenAlpha.Begin(GameObject.Find("RemarksContainer_" + indexofcomment), 2, 1);
            commentval = true;
        }
        else
        {
            TweenAlpha.Begin(GameObject.Find("RemarksContainer_" + indexofcomment), 0, 0);
            LeanTween.rotateY(GameObject.Find("stickeynote_" + indexofcomment), 0, 1.25f);
            TweenAlpha.Begin(GameObject.Find("mainbodyContainer_" + indexofcomment), 2, 1);
            commentval = false;
        }
    }

    void deSelectNote()
    {
        GameObject.Find("greyselect_" + indexofcolorselected).GetComponent<UISprite>().enabled = false;
        GameObject.Find("yellowselect_" + indexofcolorselected).GetComponent<UISprite>().enabled = false;
        GameObject.Find("pinkselect_" + indexofcolorselected).GetComponent<UISprite>().enabled = false;
        GameObject.Find("blueselect_" + indexofcolorselected).GetComponent<UISprite>().enabled = false;
        GameObject.Find("greenselect_" + indexofcolorselected).GetComponent<UISprite>().enabled = false;
    }

    public void Changecolorofnote()
    {
        print("UIButton.current.name" + UIButton.current.name);
        string nameofcolorbtn = UIButton.current.name;
        string substr = nameofcolorbtn.Substring(11, (nameofcolorbtn.Length - 11));
        notecolor = nameofcolorbtn.Substring(0, nameofcolorbtn.Length - 2);
        indexofcolor = int.Parse(substr);
        TweenAlpha.Begin(GameObject.Find("ColorContainer_" + indexofcolor), 1, 1);
        TweenPosition.Begin(GameObject.Find("ColorContainer_" + indexofcolor), 1, new Vector2(0, -20));
    }

    public void colorgreynote()
    {
        string nameofcolor = UIButton.current.name;
        string substr = nameofcolor.Substring(5, (nameofcolor.Length - 5));
        indexofcolorselected = int.Parse(substr);
        deSelectNote();
        GameObject.Find("greyselect_" + indexofcolorselected).GetComponent<UISprite>().enabled = true;
        GameObject.Find("stickeynote_" + indexofcolorselected).GetComponent<UISprite>().spriteName = "grey note";
    }

    public void coloryellownote()
    {
        string nameofcolor = UIButton.current.name;
        string substr = nameofcolor.Substring(7, (nameofcolor.Length - 7));
        indexofcolorselected = int.Parse(substr);
        deSelectNote();
        GameObject.Find("yellowselect_" + indexofcolorselected).GetComponent<UISprite>().enabled = true;
        GameObject.Find("stickeynote_" + indexofcolorselected).GetComponent<UISprite>().spriteName = "yellow note";
    }

    public void colorpinknote()
    {
        string nameofcolor = UIButton.current.name;
        string substr = nameofcolor.Substring(5, (nameofcolor.Length - 5));
        indexofcolorselected = int.Parse(substr);
        deSelectNote();
        GameObject.Find("pinkselect_" + indexofcolorselected).GetComponent<UISprite>().enabled = true;
        GameObject.Find("stickeynote_" + indexofcolorselected).GetComponent<UISprite>().spriteName = "pink note";
    }

    public void colorbluenote()
    {
        string nameofcolor = UIButton.current.name;
        string substr = nameofcolor.Substring(5, (nameofcolor.Length - 5));
        indexofcolorselected = int.Parse(substr);
        deSelectNote();
        GameObject.Find("blueselect_" + indexofcolorselected).GetComponent<UISprite>().enabled = true;
        GameObject.Find("stickeynote_" + indexofcolorselected).GetComponent<UISprite>().spriteName = "blue note";
    }

    public void colorgreennote()
    {
        string nameofcolor = UIButton.current.name;
        string substr = nameofcolor.Substring(6, (nameofcolor.Length - 6));
        indexofcolorselected = int.Parse(substr);
        deSelectNote();
        GameObject.Find("greenselect_" + indexofcolorselected).GetComponent<UISprite>().enabled = true;
        GameObject.Find("stickeynote_" + indexofcolorselected).GetComponent<UISprite>().spriteName = "green note";
    }

    public void viewNoteByColor()
    {
        TweenAlpha.Begin(GameObject.Find("viewnotebyBGimg"), 0, 1);
        GameObject.Find("viewnotebyBGimg").GetComponent<UISprite>().enabled = true;
        TweenAlpha.Begin(GameObject.Find("viewnotebyBGimg"), 0, 1);
        if (viewnotebool == false)
        {
            TweenPosition.Begin(GameObject.Find("viewnotebyBGimg"), 1.5f, new Vector3(200, -100, 0));
            TweenAlpha.Begin(GameObject.Find("ColorContainer"), 2.5f, 1);
            viewnotebool = true;
        }
        else
        {
            TweenPosition.Begin(GameObject.Find("viewnotebyBGimg"), 1.5f, new Vector3(-200, -100, 0));
            TweenAlpha.Begin(GameObject.Find("ColorContainer"), 2.5f, 0);
            viewnotebool = false;
        }
    }

    public void viewGreyColorNote()
    {
        for (int i = 0; i < noteCounter; i++)
        {
            if (GameObject.Find("stickeynote_" + i).GetComponent<UISprite>().spriteName.Equals("grey note"))
            {
                GameObject.Find("stickeynote_" + i).GetComponent<UISprite>().enabled = false;
                //  GameObject greydis = GameObject.Find("stickeynote_" + i);
                // NGUITools.SetActive(greydis, false);
                TweenAlpha.Begin(GameObject.Find("stickeyNoteContainer_" + i), 0.5f, 0);
            }
        }
    }

    public void viewyellowColorNote()
    {
        for (int i = 0; i < noteCounter; i++)
        {
            if (GameObject.Find("stickeynote_" + i).GetComponent<UISprite>().spriteName.Equals("yellow note"))
            {
                GameObject.Find("stickeynote_" + i).GetComponent<UISprite>().enabled = false;
                TweenAlpha.Begin(GameObject.Find("stickeyNoteContainer_" + i), 0.5f, 0);
            }
        }
    }

    public void viewpinkColorNote()
    {
        for (int i = 0; i < noteCounter; i++)
        {
            if (GameObject.Find("stickeynote_" + i).GetComponent<UISprite>().spriteName.Equals("pink note"))
            {
                GameObject.Find("stickeynote_" + i).GetComponent<UISprite>().enabled = false;
                TweenAlpha.Begin(GameObject.Find("stickeyNoteContainer_" + i), 0.5f, 0);
            }
        }
    }

    public void viewblueColorrNote()
    {
        for (int i = 0; i < noteCounter; i++)
        {
            if (GameObject.Find("stickeynote_" + i).GetComponent<UISprite>().spriteName.Equals("blue note"))
            {
                GameObject.Find("stickeynote_" + i).GetComponent<UISprite>().enabled = false;
                TweenAlpha.Begin(GameObject.Find("stickeyNoteContainer_" + i), 0.5f, 0);
            }
        }
    }

    public void viewgreenColorNote()
    {
        for (int i = 0; i < noteCounter; i++)
        {
            if (GameObject.Find("stickeynote_" + i).GetComponent<UISprite>().spriteName.Equals("green note"))
            {
                GameObject.Find("stickeynote_" + i).GetComponent<UISprite>().enabled = false;
                TweenAlpha.Begin(GameObject.Find("stickeyNoteContainer_" + i), 0.5f, 0);
            }
        }
    }

    public void EditNote()
    {
		string stickeynoteName1 = "";
		string stickeynoteName2 = "";

		if(GameObject.Find("note_").transform.childCount == 2)
			stickeynoteName1 = GameObject.Find("note_").transform.GetChild(1).name;
		if(GameObject.Find("newnoteGrid").transform.childCount == 1)
			stickeynoteName2 = GameObject.Find("newnoteGrid").transform.GetChild(0).name;
		btnname = UIButton.current.name;
        globalnumberforedit = btnname.Substring(12, btnname.Length - 12);
        if (GameObject.Find("notetitle_" + globalnumberforedit).GetComponent<UILabel>().alpha == 0)
        {
            notesaveScript.editNoteGeneralPopup = false;
            notesaveScript.editNoteRemarkPopup = true;
            TweenAlpha.Begin(GameObject.Find("EditNoteRemarkTitle_"), 0, 1);
            TweenAlpha.Begin(GameObject.Find("EditNoteRemarkBody_"), 0, 1);
            GameObject.Find("EditNoteTitle_").GetComponent<UIInput>().GetComponent<Collider>().enabled = false;
            GameObject.Find("EditNotebody_").GetComponent<UIInput>().GetComponent<Collider>().enabled = false;
            GameObject.Find("EditNoteRemarkBody_").GetComponent<UIInput>().GetComponent<Collider>().enabled = true;
            TweenAlpha.Begin(GameObject.Find("EditNoteTitle_"), 0, 0);
            TweenAlpha.Begin(GameObject.Find("EditNotebody_"), 0, 0);
        }
        else
        {
            notesaveScript.editNoteGeneralPopup = true;
            notesaveScript.editNoteRemarkPopup = false;
            TweenAlpha.Begin(GameObject.Find("EditNoteRemarkTitle_"),0,0);
            TweenAlpha.Begin(GameObject.Find("EditNoteRemarkBody_"), 0,0);
            GameObject.Find("EditNoteTitle_").GetComponent<UIInput>().GetComponent<Collider>().enabled = true;
            GameObject.Find("EditNotebody_").GetComponent<UIInput>().GetComponent<Collider>().enabled = true;
            GameObject.Find("EditNoteRemarkBody_").GetComponent<UIInput>().GetComponent<Collider>().enabled = false;
            TweenAlpha.Begin(GameObject.Find("EditNoteTitle_"), 0, 1);
            TweenAlpha.Begin(GameObject.Find("EditNotebody_"), 0, 1);
        }

        if (stickeynoteName1.Equals (btnname) || stickeynoteName2.Equals (btnname)) 
		{
			
		} 
		else 
		{
			TweenAlpha.Begin(GameObject.Find("BlackbgContainer"), 0.5f, 0.8f);
			TweenScale.Begin(GameObject.Find("EditNote_"), 0.5f, new Vector3(1f, 1f, 0));
			TweenAlpha.Begin(GameObject.Find("middleContainer"), 0, 0.1f);        
			print("name_of_btn========" + btnname);
			print("globalnumberforedit" + globalnumberforedit);
			GameObject.Find("EditNoteTitle_").GetComponent<UIInput>().value = GameObject.Find("notetitle_" + globalnumberforedit).GetComponent<UILabel>().text;
			GameObject.Find("EditNotebody_").GetComponent<UIInput>().value = GameObject.Find("notecontent_" + globalnumberforedit).GetComponent<UILabel>().text;
			GameObject.Find("editNoteStatus").GetComponent<UILabel>().text = GameObject.Find("noteStatus_" + globalnumberforedit).GetComponent<UILabel>().text;
            GameObject.Find("EditNoteRemarkBody_").GetComponent<UIInput>().value = GameObject.Find("noteRemarkContent_" + globalnumberforedit).GetComponent<UILabel>().text;
		}
    }

    public void clickonBlackBackground()
    {
        TweenAlpha.Begin(GameObject.Find("middleContainer"), 0, 1f);
        TweenScale.Begin(GameObject.Find("EditNote_"), 0.5f, new Vector3(0, 0, 0));
        TweenAlpha.Begin(GameObject.Find("BlackbgContainer"), 0.5f, 0f);
        GameObject.Find("notetitle_" + globalnumberforedit).GetComponent<UILabel>().text = GameObject.Find("EditNoteTitle_").GetComponent<UIInput>().value;
        GameObject.Find("notecontent_" + globalnumberforedit).GetComponent<UILabel>().text = GameObject.Find("EditNotebody_").GetComponent<UIInput>().value;
        GameObject.Find("noteRemarkContent_" + globalnumberforedit).GetComponent<UILabel>().text = GameObject.Find("EditNoteRemarkBody_").GetComponent<UIInput>().value;
        GameObject.Find("noteRemarkContent_" + globalnumberforedit).GetComponent<UILabel>().text = GameObject.Find("EditNoteRemarkBody_").GetComponent<UIInput>().value;
    }

    IEnumerator manualStart()
    {
		yield return new WaitForSeconds(0);
        /*
		while(true)
        {
            manualUpdate();
            yield return new WaitForSeconds(1);
        }
		*/
    }

    /*void manualUpdate()
    {
        int no_of_child;
        no_of_child = GameObject.Find("deletenoteimg").GetComponent<UISprite>().transform.childCount;
        if (no_of_child == 2 && delete_box_status == false)
        {
            name_of_btn = GameObject.Find("deletenoteimg").GetComponent<UISprite>().transform.GetChild(1).name;
            deletenote();
        }     
        else if(GameObject.Find("deletenotecontainer").GetComponent<UIWidget>().alpha == 0)
        {
            delete_box_status = false;
        }  
    }*/


}
