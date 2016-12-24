using UnityEngine;
using System.Collections;
using System.Net.Mail;
using System;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class LoginCredential : MonoBehaviour
{
    public GameObject cubeobj;
    public void CheckValidationlogin()
    {
        //TweenAlpha.Begin(GameObject.Find("Credential_lbl"), 0, 0);
        string url = "http://bmc.bitwise.in/api/login";
        WWWForm form = new WWWForm();
        form.AddField("email", GameObject.Find("MailInputBox").GetComponent<UIInput>().value);
        form.AddField("password", GameObject.Find("PasswordInputBox").GetComponent<UIInput>().value);
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequestLogin(www));
    }

    IEnumerator WaitForRequestLogin(WWW www)
    {
        string resultData;
        yield return www;
        // check for errors
        if (www.error == null)
        {
            resultData = www.text;
            //print(resultData);
            JSONObject jsonData = new JSONObject(resultData);
            print(jsonData.GetField("status").n);
            if(jsonData.GetField("status").n == 1)
            {
                GameObject.Find("CredentialLbl").GetComponent<UILabel>().text = "";
                GameObject.Find("ScriptAttachment").GetComponent<LoginAnimation>().submitPassword();
            }
            else
            {
                GameObject.Find("CredentialLbl").GetComponent<UILabel>().text = "Password Is Incorreect";
            }
            //print(getData(resultData).ToString());
        }
        else
        {
            print("hi");
            Debug.Log("WWW Error: " + www.error);
            resultData = "error" + www.error;
            Debug.Log("resultData===" + resultData);
           
        }
    }
    
    public void checkRegisterEmail()
    {
        string url ="http://bmc.bitwise.in/api/checkemail";
        WWWForm form = new WWWForm();
        form.AddField("email", GameObject.Find("MailInputBox").GetComponent<UIInput>().value);
        WWW www = new WWW(url, form);
        StartCoroutine(WaitForRequestEmail(www));
    }

    IEnumerator WaitForRequestEmail(WWW www)
    {
        string resultData;
        yield return www;
        // check for errors
        if (www.error == null)
        {
            resultData = www.text;
            JSONObject jsonData = new JSONObject(resultData);
            print(jsonData.GetField("status").n);
            if(jsonData.GetField("status").n == 1)
            {
                //Camera.main.GetComponent<Script2>().Script2fun();
                GameObject.Find("ScriptAttachment").GetComponent<LoginAnimation>().submitMail();
                // print("success...");

            }
            else if(jsonData.GetField("status").n == 2)
            {
                GameObject.Find("CredentialLbl").GetComponent<UILabel>().text = "Please verify your Email id";
            }

            else
            {
                GameObject.Find("CredentialLbl").GetComponent<UILabel>().text = "Email id is not registered";
            }
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
            resultData = "error" + www.error;
            Debug.Log("resultData===" + resultData);
        }
    }


    public void checkEmail()
    {
       
        string Emailstring = GameObject.Find("MailInputBox").GetComponent<UIInput>().value;

        if (Emailstring == "")
        {
            GameObject.Find("CredentialLbl").GetComponent<UILabel>().text = "Email Address can't be null";
        }
        else
        {
            try
            {
                string email = Emailstring;
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(email);
                if (match.Success)
                {
                    checkRegisterEmail();
                    GameObject.Find("CredentialLbl").GetComponent<UILabel>().text = "";
                    print(email + " is correct");
                }
                else
                {
                    GameObject.Find("CredentialLbl").GetComponent<UILabel>().text = "Enter Valid Email Address";
                    print(email + " is incorrect");
                }
            }
            catch (FormatException)
            {
                print("false");
            }
        }
    }


    public void checkPassword()
    {
        string passwordstring = GameObject.Find("PasswordInputBox").GetComponent<UIInput>().value;

        if(passwordstring=="")
        {
            GameObject.Find("CredentialLbl").GetComponent<UILabel>().text = "Password Can not be null";
        }
        else
        {
            CheckValidationlogin();
        }
    }


  /*  public void forgotPasswordContainer()
    {
        TweenAlpha.Begin(GameObject.Find("LoginContainer"), 0.5f, 0.3f);
        TweenAlpha.Begin(GameObject.Find("ForgotPasswordContainer"), 0.5f, 1);
        cubeobj.SetActive(false);
    }
    */
    public void forgotPassword()
    { 
        //TweenAlpha.Begin(GameObject.Find("Credential_lbl"), 0, 0);
        string passurl = "http://bmc.bitwise.in/api/forgetpassword";
        WWWForm form = new WWWForm();
        form.AddField("email", GameObject.Find("MailInputBox").GetComponent<UIInput>().value);
       // form.AddField("password", GameObject.Find("PasswordInputBox").GetComponent<UIInput>().value);
        WWW www = new WWW(passurl, form);
        StartCoroutine(WaitForRequestforgotpassword(www));
    }

    IEnumerator WaitForRequestforgotpassword(WWW www)
        {
        yield return new WaitForSeconds(1f);
        string resultData;
        yield return www;
        TweenColor.Begin(GameObject.Find("EmailvalidationLbl"), 0, Color.red);
        // check for errors
        if (www.error == null)
        {
            resultData = www.text;
            JSONObject jsonData = new JSONObject(resultData);
            print("forgot status=="+jsonData.GetField("status").n);
            if (jsonData.GetField("status").n == 1)
            {
                StartCoroutine(forgotpasswordsuccess());
            }
            else
            {
                GameObject.Find("EmailvalidationLbl").GetComponent<UILabel>().text = "Email id is not registered";
            }
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
            resultData = "error" + www.error;
            Debug.Log("resultData===" + resultData);
        }
    }

    IEnumerator forgotpasswordsuccess()
    {
        yield return new WaitForSeconds(0.5f);
        TweenColor.Begin(GameObject.Find("EmailvalidationLbl"), 0, Color.green);
        GameObject.Find("EmailvalidationLbl").GetComponent<UILabel>().text = "Password is successfully sent";
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene("Login");
    }

    public void loginToForgotpass()
    {
        StartCoroutine(ienumloginToForgotpass());
    }
    IEnumerator ienumloginToForgotpass()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("forgotpassword");
    }

    public void forgotpassToLogin()
    {
        StartCoroutine(ienumforgotpassToLogin());
    }
    IEnumerator ienumforgotpassToLogin()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Login");
    }
}


