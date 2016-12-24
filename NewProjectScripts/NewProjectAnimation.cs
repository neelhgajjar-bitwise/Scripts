using UnityEngine;
using System.Collections;

public class NewProjectAnimation : MonoBehaviour
{
    string widthofscreen,heightofscreen;
    float widthofScreenvalue, heightofscreenvalue;
    public Collider blackbgcollider;
    
    // Use this for initialization
    void Start ()
    {        
        hideAll();
        TweenPosition.Begin(GameObject.Find("StartprojImg"), 1f, new Vector3(115,200, 0));
        hidecanvastype();
        //GameObject.Find("newprojectContainer").GetComponent<UIWidget>().alpha = 0;
        StartCoroutine(LeftPanelAnimation());
    }

    void hidecanvastype()
    {
        GameObject.Find("newprojectselectImg1").GetComponent<UISprite>().enabled =false;
        GameObject.Find("newprojectselectImg2").GetComponent<UISprite>().enabled = false;
        GameObject.Find("newprojectselectImg3").GetComponent<UISprite>().enabled = false;
        GameObject.Find("newprojectselectImg4").GetComponent<UISprite>().enabled = false;
        GameObject.Find("newprojectselectImg5").GetComponent<UISprite>().enabled= false;
        GameObject.Find("newprojectselectImg6").GetComponent<UISprite>().enabled= false;
    } 
    
    void hideAll()
    {
		TweenAlpha.Begin(GameObject.Find("startANewProjectLbl"), 0f, 0.01f);
        TweenAlpha.Begin(GameObject.Find("CanvastypeLbl"), 0f, 0.01f);
        print("widthofscreen===>>" + widthofScreenvalue);
        //TweenAlpha.Begin(GameObject.Find("StartprojImg"), 0, 0.01f);
        TweenAlpha.Begin(GameObject.Find("WorkOnLbl"),0,0f);
    }

    void ShowAll()
    {
		TweenAlpha.Begin(GameObject.Find("startANewProjectLbl"),0,1);
        TweenAlpha.Begin(GameObject.Find("CanvastypeLbl"),0,1);
        //TweenAlpha.Begin(GameObject.Find("popupContainer"),0,1);
        print("widthofscreen===>>" + widthofScreenvalue);
        //TweenAlpha.Begin(GameObject.Find("StartprojImg"),0,1f);
    }
    
    IEnumerator LeftPanelAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        widthofscreen = GameObject.Find("BackGroundContainer").GetComponent<UIWidget>().width.ToString();
        //convert string to float
        widthofScreenvalue = float.Parse(widthofscreen);
        heightofscreen = GameObject.Find("BackGroundContainer").GetComponent<UIWidget>().width.ToString();
        //convert string to float
        heightofscreenvalue = float.Parse(heightofscreen);
        TweenPosition.Begin(GameObject.Find("LeftPanelImg"), 0, new Vector3(-(widthofScreenvalue / 4f), 0, 0));
        GameObject.Find("LeftPanelImg").GetComponent<UIWidget>().width = (int)(widthofScreenvalue / 4);
        TweenPosition.Begin(GameObject.Find("LeftPanelImg"), 0.5f, new Vector3((0 - (widthofScreenvalue/2)), 0, 0));
       // TweenPosition.Begin(GameObject.Find("DynamicContainer_"), 0f, new Vector3(widthofScreenvalue / 2, 11, 0));
      //  GameObject.Find("Image").GetComponent<UIWidget>().width= GameObject.Find("ScrollContainer").GetComponent<UIWidget>().width/2;
        //TweenPosition.Begin(GameObject.Find("Image"), 0, new Vector3((GameObject.Find("ScrollContainer").GetComponent<UIWidget>().width) / 3, 116, 0));
       // print("heightimg"+GameObject.Find("Image").GetComponent<UIWidget>().height);
        //print("heightcont"+GameObject.Find("ScrollContainer").GetComponent<UIWidget>().height / 3);
        StartCoroutine(StartProjectLblAnimation1());
    }

    IEnumerator StartProjectLblAnimation1()
    {
        yield return new WaitForSeconds(0.1f);
        //GameObject.Find("StartprojImg").GetComponent<UISprite>().width = (int)(widthofScreenvalue / 5);
        //GameObject.Find("StartprojImg").GetComponent<UISprite>().height = 30;//(int)(heightofscreenvalue / 15);
        //TweenPosition.Begin(GameObject.Find("StartprojImg"), 0, new Vector3(-(widthofScreenvalue / 2.5f) +15, 400, 0));
		//
		TweenPosition.Begin(GameObject.Find("startANewProjectLbl"), 0, new Vector3((-(widthofScreenvalue / 2.5f) + 15), 400, 0));
        TweenPosition.Begin(GameObject.Find("CanvastypeLbl"), 0, new Vector3((-(widthofScreenvalue / 2.5f) + 15), 400, 0));
        yield return new WaitForSeconds(1f);
		TweenAlpha.Begin(GameObject.Find("startANewProjectLbl"), 0, 1f);
        TweenAlpha.Begin(GameObject.Find("StartprojImg"), 0, 1f);
		//startANewProjectLbl
		TweenAlpha.Begin(GameObject.Find("startANewProjectLbl"), 0, 1f);
		TweenAlpha.Begin(GameObject.Find("CanvastypeLbl"), 1, 1f);
        //TweenPosition.Begin(GameObject.Find("StartprojImg"), 1f, new Vector3((-(widthofScreenvalue / 2.5f) + 15), 185, 0));
		//startANewProjectLbl
		TweenPosition.Begin(GameObject.Find("startANewProjectLbl"), 1f, new Vector3((-(widthofScreenvalue / 2.5f) + 15), 190, 0));
        TweenPosition.Begin(GameObject.Find("CanvastypeLbl"), 1.5f, new Vector3((-(widthofScreenvalue / 2.5f) + 15), 165, 0));
      
       StartCoroutine(selectnewproject1());
    }
    
    IEnumerator selectnewproject1()
    {
        yield return new WaitForSeconds(0.5f);
		GameObject.Find ("newprojectselectImg1").GetComponent<BoxCollider> ().enabled = false;
        TweenPosition.Begin(GameObject.Find("newprojectselectImg1"), 0, new Vector3(-(widthofScreenvalue / 2.5f) + 15, 70, 0));
        //TweenAlpha.Begin(GameObject.Find("newprojectselectImg1"), 1, 1);
        GameObject.Find("newprojectselectImg1").GetComponent<UISprite>().enabled = true;
        yield return new WaitForSeconds(0.10f);
        LeanTween.scale(GameObject.Find("newprojectselectImg1"), new Vector3(0.8f, 1.2f), 0.10f);
        yield return new WaitForSeconds(0.10f);
        LeanTween.scale(GameObject.Find("newprojectselectImg1"), new Vector3(0.55f, 1f), 0.10f);
        GameObject.Find("newprojectselectImg1").GetComponent<UISprite>().GetComponent<UIButton>().defaultColor = Color.white;
        StartCoroutine(selectnewproject2());
    }

    IEnumerator selectnewproject2()
    {
        TweenPosition.Begin(GameObject.Find("newprojectselectImg2"), 0, new Vector3(-(widthofScreenvalue / 2.5f)+15, -70, 0));
        TweenAlpha.Begin(GameObject.Find("newprojectselectImg2"), 1, 0.3f);
        GameObject.Find("newprojectselectImg2").GetComponent<UISprite>().enabled = true;
        yield return new WaitForSeconds(0.10f);
        LeanTween.scale(GameObject.Find("newprojectselectImg2"), new Vector3(0.8f, 1.2f),0.10f);
        yield return new WaitForSeconds(0.10f);
        LeanTween.scale(GameObject.Find("newprojectselectImg2"), new Vector3(0.55f, 1f),0.10f);
        GameObject.Find("newprojectselectImg2").GetComponent<UISprite>().GetComponent<UIButton>().defaultColor = Color.white;
        StartCoroutine(selectnewproject3());
    }

    IEnumerator selectnewproject3()
    {
        TweenPosition.Begin(GameObject.Find("newprojectselectImg3"), 0, new Vector3(-(widthofScreenvalue / 2.5f)+15,-210, 0));
        TweenAlpha.Begin(GameObject.Find("newprojectselectImg3"), 1, 0.3f);
        GameObject.Find("newprojectselectImg3").GetComponent<UISprite>().enabled = true;
        yield return new WaitForSeconds(0.10f);
        LeanTween.scale(GameObject.Find("newprojectselectImg3"), new Vector3(0.8f, 1.2f), 0.10f);
        yield return new WaitForSeconds(0.10f);
        LeanTween.scale(GameObject.Find("newprojectselectImg3"), new Vector3(0.55f, 1f), 0.10f);
        GameObject.Find("newprojectselectImg3").GetComponent<UISprite>().GetComponent<UIButton>().defaultColor = Color.white;
        StartCoroutine(selectnewproject4());
    }

    IEnumerator selectnewproject4()
    {
        TweenPosition.Begin(GameObject.Find("newprojectselectImg4"), 0, new Vector3(-(widthofScreenvalue / 2.5f)+15, -350, 0));
		TweenAlpha.Begin(GameObject.Find("newprojectselectImg4"), 1, 0.3f);
        GameObject.Find("newprojectselectImg4").GetComponent<UISprite>().enabled = true;
        yield return new WaitForSeconds(0.10f);
        LeanTween.scale(GameObject.Find("newprojectselectImg4"), new Vector3(0.8f, 1.2f), 0.10f);
        yield return new WaitForSeconds(0.10f);
        LeanTween.scale(GameObject.Find("newprojectselectImg4"), new Vector3(0.55f, 1f), 0.10f);
        GameObject.Find("newprojectselectImg4").GetComponent<UISprite>().GetComponent<UIButton>().defaultColor = Color.white;
        StartCoroutine(selectnewproject5());
    }

    IEnumerator selectnewproject5()
    {
        TweenPosition.Begin(GameObject.Find("newprojectselectImg5"), 0, new Vector3(-(widthofScreenvalue / 2.5f)+15, -490, 0));
		TweenAlpha.Begin(GameObject.Find("newprojectselectImg5"), 1, 0.3f);
        GameObject.Find("newprojectselectImg5").GetComponent<UISprite>().enabled = true;
        yield return new WaitForSeconds(0.10f);
        LeanTween.scale(GameObject.Find("newprojectselectImg5"), new Vector3(0.8f, 1.2f), 0.10f);
        yield return new WaitForSeconds(0.10f);
        LeanTween.scale(GameObject.Find("newprojectselectImg5"), new Vector3(0.55f, 1f), 0.10f);
        GameObject.Find("newprojectselectImg5").GetComponent<UISprite>().GetComponent<UIButton>().defaultColor = Color.white;
        StartCoroutine(selectnewproject6());
    }
    
    IEnumerator selectnewproject6()
    {
        TweenPosition.Begin(GameObject.Find("newprojectselectImg6"), 0, new Vector3(-(widthofScreenvalue / 2.5f)+15, -630, 0));
		TweenAlpha.Begin(GameObject.Find("newprojectselectImg6"), 1, 0.3f);
        GameObject.Find("newprojectselectImg6").GetComponent<UISprite>().enabled = true;
        yield return new WaitForSeconds(0.10f);
        LeanTween.scale(GameObject.Find("newprojectselectImg6"), new Vector3(0.8f, 1.2f), 0.10f);
        yield return new WaitForSeconds(0.10f);
        LeanTween.scale(GameObject.Find("newprojectselectImg6"), new Vector3(0.55f, 1f), 0.10f);
        //TweenPosition.Begin(GameObject.Find("WorkOnLbl"), 0, new Vector2(-(widthofScreenvalue / 4f), 180));
        TweenAlpha.Begin(GameObject.Find("WorkOnLbl"),0.5f,1);
        GameObject.Find("newprojectselectImg6").GetComponent<UISprite>().GetComponent<UIButton>().defaultColor = Color.white;
        //StartCoroutine(setDynamicContainerOnStart());
    }
/*
   public IEnumerator setDynamicContainerOnStart()
    {
        //TweenPosition.Begin(GameObject.Find("Image"), 1, new Vector3((GameObject.Find("DynamicContainer_").GetComponent<UIWidget>().width) / 4, 116, 0));
        TweenAlpha.Begin(GameObject.Find("ScrollContainer"), 0,0);
        yield return new WaitForSeconds(0.1f);
        //GameObject.Find("ScrollContainer").GetComponent<UIWidget>().width = (int)(widthofScreenvalue / 4);
        //LeanTween.moveLocalX(GameObject.Find("ScrollContainer"), (0 - (widthofScreenvalue / 2)), 2f);
        //print("   " + GameObject.Find("ScrollContainer").GetComponent<UIWidget>().transform.localPosition.x);
        print("aa" + (int)(widthofScreenvalue / 4));
      
        TweenPosition.Begin(GameObject.Find("DynamicContainer_"),0f, new Vector3(0, 11, 0));

        TweenAlpha.Begin(GameObject.Find("ScrollContainer"), 1, 1);
        //if (GameObject.Find("ScrollContainer").GetComponent<UIWidget>().transform.localPosition.x == (int)(widthofScreenvalue / 4))
        //{
        //    GameObject.Find("Scroll View").GetComponent<UIPanel>().depth = 1;
        //}
        // LeanTween.moveLocalX(GameObject.Find("ScrollContainer"), (0 - (widthofScreenvalue / 2)), 2f);

        // GameObject.Find("DynamicContainer").GetComponent<UIWidget>().width = (int)(widthofScreenvalue / 4);
        // TweenPosition.Begin(GameObject.Find("DynamicContainer"),1f,)
        //LeanTween.moveLocalX(GameObject.Find("DynamicContainer"), GameObject.Find("ScrollContainer").GetComponent<UIWidget>().width/2, 1f);
        // GameObject.Find("DynamicContainer (1)").GetComponent<UIWidget>().width = (int)(widthofScreenvalue / 4);
        // GameObject.Find("DynamicContainer (2)").GetComponent<UIWidget>().width = (int)(widthofScreenvalue / 4);

       
        GameObject.Find("Image").GetComponent<UIWidget>().width = (GameObject.Find("DynamicContainer_").GetComponent<UIWidget>().width) / 2;
        //GameObject.Find("Image1").GetComponent<UIWidget>().height= GameObject.Find("DynamicContainer").GetComponent<UIWidget>().height / 2;
        //GameObject.Find("Image2").GetComponent<UIWidget>().width = (GameObject.Find("DynamicContainer").GetComponent<UIWidget>().width) / 2;
    }
  */  
   public void newprojectPopup()
    {        
        TweenAlpha.Begin(GameObject.Find("WholeContainer"), 0, 0.3f);
        TweenAlpha.Begin(GameObject.Find("midleContainer"), 0, 0);
       // TweenAlpha.Begin(GameObject.Find("blackbg"),0,0.8f);
      //  LeanTween.alpha(GameObject.Find("BlackbgContainer"),0.8f,0.8f);        
		StartCoroutine (popupContainerAnimation());
    }

	IEnumerator popupContainerAnimation()
	{
		GameObject.Find ("blackbg").GetComponent<BoxCollider> ().enabled = false;
		TweenAlpha.Begin(GameObject.Find("popupContainer"), 0, 1);
		TweenAlpha.Begin(GameObject.Find("backgroundImg"), 0, 0);
		TweenAlpha.Begin(GameObject.Find("newprojectLbl"), 0, 0);
		TweenAlpha.Begin(GameObject.Find("createbtn"), 0, 0);
		TweenAlpha.Begin(GameObject.Find("TitleInput"), 0, 0);
		TweenAlpha.Begin(GameObject.Find("RemarksInput"), 0, 0);
		//TweenAlpha.Begin(GameObject.Find("BlackbgContainer"), 0, 1);
		TweenScale.Begin (GameObject.Find ("popupContainer"), 0, new Vector2 (0,0));
		yield return new WaitForSeconds (0);
		TweenAlpha.Begin(GameObject.Find("backgroundImg"), 0, 1);
		TweenScale.Begin (GameObject.Find ("popupContainer"), 0, new Vector2 (0,0.2f));
		TweenScale.Begin (GameObject.Find ("popupContainer"), 0.4f, new Vector2 (1,0.2f));
		yield return new WaitForSeconds (0.4f);
		TweenScale.Begin (GameObject.Find ("popupContainer"), 0.4f, new Vector2 (1,1f));
		yield return new WaitForSeconds (0.4f);
		TweenAlpha.Begin(GameObject.Find("newprojectLbl"), 0, 1);

		TweenPosition.Begin (GameObject.Find("TitleInput"),0f,new Vector2(-165,29));
		TweenPosition.Begin (GameObject.Find("TitleInput"),0.4f,new Vector2(-165,75));
		TweenAlpha.Begin (GameObject.Find ("TitleInput"), 0.4f, 1);
		yield return new WaitForSeconds (0.4f);

		TweenPosition.Begin (GameObject.Find("RemarksInput"),0f,new Vector2(-165,-26));
		TweenPosition.Begin (GameObject.Find("RemarksInput"),0.4f,new Vector2(-165,11));
		TweenAlpha.Begin (GameObject.Find("RemarksInput"), 0.4f, 1);
		yield return new WaitForSeconds (0.4f);

		TweenPosition.Begin (GameObject.Find("createbtn"),0f,new Vector2(0,-113));
		TweenPosition.Begin (GameObject.Find("createbtn"),0.4f,new Vector2(0,-85));
		TweenAlpha.Begin (GameObject.Find("createbtn"), 0.4f, 1);
		yield return new WaitForSeconds (0.4f);
		GameObject.Find ("blackbg").GetComponent<BoxCollider> ().enabled = true;
		blackbgcollider.enabled = true;
	}

    public void blackbgClick()
    {        
		StartCoroutine (blackBgClickAnimation());
    }

	IEnumerator blackBgClickAnimation()
	{
		GameObject.Find ("blackbg").GetComponent<BoxCollider> ().enabled = false;
		GameObject.Find ("TitleInput").GetComponent<UIInput> ().label.text = "Title";
		GameObject.Find ("RemarksInput").GetComponent<UIInput> ().label.text = "Remarks";

		TweenAlpha.Begin(GameObject.Find("newprojectLbl"), 0.2f, 0);
		TweenAlpha.Begin(GameObject.Find("createbtn"), 0.2f, 0);
		TweenAlpha.Begin(GameObject.Find("TitleInput"), 0.2f, 0);
		TweenAlpha.Begin(GameObject.Find("RemarksInput"), 0.2f, 0);
		yield return new WaitForSeconds (0.2f);
		TweenScale.Begin (GameObject.Find ("popupContainer"), 0.4f, new Vector2 (1,0.2f));
		yield return new WaitForSeconds (0.4f);
		TweenScale.Begin (GameObject.Find ("popupContainer"), 0.4f, new Vector2 (0,0.2f));
		TweenAlpha.Begin(GameObject.Find("popupContainer"), 0.5f, 0);
		yield return new WaitForSeconds (0.4f);
		//
		blackbgcollider.enabled = false;
		TweenAlpha.Begin(GameObject.Find("popupContainer"), 0, 0);
		TweenAlpha.Begin(GameObject.Find("WholeContainer"), 0, 1f);
		TweenAlpha.Begin(GameObject.Find("midleContainer"), 0, 1);
	}
}
/* public void newprojectPopup()
   {
       StartCoroutine(newprojectPopupEnumerator());
   }

   IEnumerator newprojectPopupEnumerator()
   {
       TweenAlpha.Begin(GameObject.Find("WholeContainer"), 0, 0.3f);
       LeanTween.alpha(GameObject.Find("BlackbgContainer"), 0.8f, 0.8f);
       blackbgcollider.enabled = true;
      //yield return new WaitForSeconds(0.1f);
       LeanTween.scaleX(GameObject.Find("popupContainer"), 1, 1);
       LeanTween.scaleY(GameObject.Find("popupContainer"),0.2f,1f);
       yield return new WaitForSeconds(1f);
       LeanTween.scaleY(GameObject.Find("popupContainer"), 1, 1);
   }
   */
