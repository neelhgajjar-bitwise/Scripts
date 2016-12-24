using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SignUpanimation : MonoBehaviour {

    public GameObject CubeObject;
    string widthofscreen;
    float widthofScreenvalue;

    void Start()
    {
        
        hideLabels();
        CubeObject.SetActive(false);
        //Hide the Logo At starting position
        TweenScale.Begin(GameObject.Find("LogoImg"), 0, new Vector3(0.01f, 0.01f, 0));
        //Take the width of device by taking BackgroundContainer's Width
        widthofscreen = GameObject.Find("BackGroundContainer").GetComponent<UIWidget>().width.ToString();
        //convert string to float
        widthofScreenvalue = float.Parse(widthofscreen);
        print(widthofScreenvalue);
        StartCoroutine(LogoAnimation());
        
    }
    //Just Hide The Labels at start of the app
  public void hideLabels()
    {
        TweenAlpha.Begin(GameObject.Find("RegisterLbl"), 0, 0);
        TweenAlpha.Begin(GameObject.Find("ConfirmationLbl"), 0, 0);
        TweenAlpha.Begin(GameObject.Find("BackgroundImg"), 0, 0);
        TweenAlpha.Begin(GameObject.Find("alreadyAccountLbl"), 0, 0);
        TweenAlpha.Begin(GameObject.Find("LoginLbl"), 0, 0);
    }
    //showLabels When it necessary
 public void showLabels()
    {
       // TweenAlpha.Begin(GameObject.Find("ForgotPasswordlLbl"), 0.5f, 1);
        TweenAlpha.Begin(GameObject.Find("alreadyAccountLbl"), 0.5f, 1);
        TweenAlpha.Begin(GameObject.Find("LoginLbl"), 0.5f, 1);
    }

    IEnumerator LogoAnimation()
    {
        yield return new WaitForSeconds(1f);
        //Show the Logo with Effect
        print("inlogo=="+widthofScreenvalue);
        // LeanTween.moveLocalX(GameObject.Find("BackgroundImg"), -widthofScreenvalue, 0f);
        TweenPosition.Begin(GameObject.Find("BackgroundImg"), 0, new Vector3(-widthofScreenvalue, 0, 0));
        TweenScale.Begin(GameObject.Find("LogoImg"), 1f, new Vector3(0.5f, 0.5f, 0));
        TweenAlpha.Begin(GameObject.Find("LogoImg"), 0.3f, 0.5f);
        yield return new WaitForSeconds(0.3f);
        TweenAlpha.Begin(GameObject.Find("LogoImg"), 0.3f, 1);
        TweenScale.Begin(GameObject.Find("LogoImg"), 0.3f, new Vector3(1f, 1f, 0));
        yield return new WaitForSeconds(0.3f);
        TweenScale.Begin(GameObject.Find("LogoImg"), 0.3f, new Vector3(0.7f, 0.7f, 0));
        //Move Logo Upward
        yield return new WaitForSeconds(0.25f);
        TweenPosition.Begin(GameObject.Find("LogoImg"), 0.25f, new Vector3(0, -10f, 0));
        yield return new WaitForSeconds(0.5f);
        TweenPosition.Begin(GameObject.Find("LogoImg"), 0.5f, new Vector3(0, 140, 0));
        StartCoroutine(BackgroundAnimation());
        /*
                TweenScale.Begin(GameObject.Find("LogoImg"), 1, new Vector3(0.7f, 0.7f, 0));
                yield return new WaitForSeconds(2f);
                //Move Logo Upward
                TweenPosition.Begin(GameObject.Find("LogoImg"), 1, new Vector3(0, 140, 0));
                StartCoroutine(BackgroundAnimation());
        */
    }

    IEnumerator BackgroundAnimation()
    {
        yield return new WaitForSeconds(1f);
        //Main Background Img goes to left hand side by using BackgroundContainer
        TweenAlpha.Begin(GameObject.Find("BackgroundImg"), 0, 1);
        yield return new WaitForSeconds(0.1f);
        //According to width half of the Main Background Img Comes again in uiroot Camera
        TweenPosition.Begin(GameObject.Find("BackgroundImg"), 0.3f, new Vector3(-(widthofScreenvalue / 1.50f), 0, 0));
        StartCoroutine(CubeAnimation());
    }

    IEnumerator CubeAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        CubeObject.SetActive(true);
        //Cube rotates around Z axis by 90 degree.
        LeanTween.rotateZ(GameObject.Find("Cube"),180, 0.5f);
        yield return new WaitForSeconds(0.5f);
        //Effect of Mail Icon 
        TweenScale.Begin(GameObject.Find("UserIconImg"), 0.5f, new Vector3(1, 1, 0));
        yield return new WaitForSeconds(0.5f);
        //Effect of submitMailImg
        TweenScale.Begin(GameObject.Find("SubmitUserImg"), 0.5f, new Vector3(1, 1, 0));
        yield return new WaitForSeconds(0.5f);
        showLabels();
    }

    public void submitUsername()
    {
        StartCoroutine(sunbmitUsernameenumerator());
    }

    IEnumerator sunbmitUsernameenumerator()
    {
        //Main Background Img Now Comes At Center.
        TweenPosition.Begin(GameObject.Find("BackgroundImg"), 0.3f, new Vector3(-(widthofScreenvalue / 3f), 0, 0));
        //Cube rotates around Z axis by 90 degree.
        LeanTween.rotateZ(GameObject.Find("Cube"), 90, 0.5f);
        yield return new WaitForSeconds(0.5f);
        //Effect of Mail Icon 
        TweenScale.Begin(GameObject.Find("MailIconImg"), 0.5f, new Vector3(1, 1, 0));
        yield return new WaitForSeconds(0.5f);
        //Effect of submitMailImg
        TweenScale.Begin(GameObject.Find("SubmitMailImg"), 0.5f, new Vector3(1, 1, 0));
        yield return new WaitForSeconds(0.5f);
    }

    public void submitMail()
    {
        StartCoroutine(submitMailenumerator());
    }

    IEnumerator submitMailenumerator()
    {
        //Main Background Img Now Comes At Center.
        TweenPosition.Begin(GameObject.Find("BackgroundImg"), 0.3f, new Vector3(0,0,0));
        //Cube rotates around Z axis by 90 degree.
        LeanTween.rotateZ(GameObject.Find("Cube"),0, 0.5f);
        yield return new WaitForSeconds(0.5f);
        //Effect of Mail Icon 
        TweenScale.Begin(GameObject.Find("PasswordIconImg"), 0.5f, new Vector3(1, 1, 0));
        yield return new WaitForSeconds(0.5f);
        //Effect of submitMailImg
        TweenScale.Begin(GameObject.Find("SubmitPasswordImg"), 0.5f, new Vector3(1, 1, 0));
        yield return new WaitForSeconds(0.5f);
    }

    public void submitPassword()
    {
        LeanTween.rotateZ(GameObject.Find("Cube"), -45f, 0.3f);
        StartCoroutine(submitPasswordEffect(0.3f));
    }


    IEnumerator submitPasswordEffect(float waittime)
    {
        yield return new WaitForSeconds(waittime);
        CubeObject.SetActive(false);
        TweenAlpha.Begin(GameObject.Find("alreadyAccountLbl"), 0, 0);
        TweenAlpha.Begin(GameObject.Find("LoginLbl"), 0, 0);
        yield return new WaitForSeconds(waittime);
        LeanTween.moveY(GameObject.Find("LogoImg"), 0, 0.3f);
        yield return new WaitForSeconds(0.5f);
        TweenAlpha.Begin(GameObject.Find("RegisterLbl"),0.5f,1);
        TweenAlpha.Begin(GameObject.Find("ConfirmationLbl"), 0.5f, 1);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Login");

    }



    public void LoginNow()
    {
        SceneManager.LoadScene("Login");
    }
}
