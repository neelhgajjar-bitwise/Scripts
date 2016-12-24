using UnityEngine;
using System.Collections;

public class CheckInternet : MonoBehaviour {

    // Use this for initialization
    bool loadingImgStatus = false;
    bool networkStatus = true;
    public GameObject cubeobj;
    void Start ()
    {
        StartCoroutine(checkInternetConnection());
    }
	
    IEnumerator IEloadingImg()
    {        
        loadingImgStatus = true;

       
        //GameObject.Find("connectingLbl").GetComponent<UILabel>().enabled = true;
        // GameObject.Find("loadingImg").GetComponent<UISprite>().enabled = true;

        LeanTween.rotateZ(GameObject.Find("loadingImg"), 0f, 0f);
        int LoadingStep = 2;
        float loadinRotationSpeed = 0.2f;

        while (loadingImgStatus)
        {
            if (LoadingStep == 1)
            {
                LeanTween.rotateZ(GameObject.Find("loadingImg"), 0f, loadinRotationSpeed);
                LoadingStep = 2;
            }
            else if (LoadingStep == 2)
            {
                LeanTween.rotateZ(GameObject.Find("loadingImg"), -90f, loadinRotationSpeed);
                LoadingStep = 3;
            }
            else if (LoadingStep == 3)
            {
                LeanTween.rotateZ(GameObject.Find("loadingImg"), -180f, loadinRotationSpeed);
                LoadingStep = 4;
            }
            else if (LoadingStep == 4)
            {
                LeanTween.rotateZ(GameObject.Find("loadingImg"), 90f, loadinRotationSpeed);
                LoadingStep = 1;
            }
            yield return new WaitForSeconds(loadinRotationSpeed);
        }
       // GameObject.Find("connectingLbl").GetComponent<UILabel>().enabled = false;
        GameObject.Find("loadingImg").GetComponent<UISprite>().enabled = false;
    }

    IEnumerator checkInternetConnection()
    {
        bool varBool = true;
        while (varBool)
        {
            yield return new WaitForSeconds(7);
            WWW www = new WWW("http://google.com");
            yield return www;
            loadingImgStatus = false;
            if (www.error != null)
            {
                //if (networkStatus == true) 
                //{		
                loadingImgStatus = false;
                GameObject.Find("NoInternetContainer").GetComponent<UIWidget>().alpha = 1;
                GameObject.Find("connectingserverlbl").GetComponent<UILabel>().text = "";
                GameObject.Find("loadingImg").GetComponent<UISprite>().enabled = false;
                GameObject.Find("NoInternetButtonContainer").GetComponent<UIWidget>().alpha = 1;

                GameObject.Find("BackGroundContainer").GetComponent<UIWidget>().alpha = .3f;
                cubeobj.SetActive(false);
                networkStatus = false;
                varBool = false;                
                //}
            }
            else
            {
                if (networkStatus == false)
                {
                    GameObject.Find("NoInternetContainer").GetComponent<UIWidget>().alpha = 0;
                    GameObject.Find("BackGroundContainer").GetComponent<UIWidget>().alpha = 1f;
                    cubeobj.SetActive(true);
                    networkStatus = true;
                }
            }
        }
    }
    public void exitFromApplication()
    {
        Application.Quit();
    }
    public void retryInternet()
    {
        StartCoroutine(checkInternetConnection());
        GameObject.Find("loadingImg").GetComponent<UISprite>().enabled = true;
        GameObject.Find("connectingserverlbl").GetComponent<UILabel>().text= "Connecting To Server";
        GameObject.Find("NoInternetButtonContainer").GetComponent<UIWidget>().alpha = 0;
        StartCoroutine(IEloadingImg());        
    }

}
