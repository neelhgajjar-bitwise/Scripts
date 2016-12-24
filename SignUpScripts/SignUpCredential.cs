using UnityEngine;
using System.Collections;
using System.Net.Mail;
using System;
using System.Text.RegularExpressions;

public class SignUpCredential : MonoBehaviour {

    public GameObject cubeobj;
    public void CheckValidationsignup()
    {
        //TweenAlpha.Begin(GameObject.Find("Credential_lbl"), 0, 0);
        string url = "http://bmc.bitwise.in/api/register";
        WWWForm form = new WWWForm();
        form.AddField("user_name", GameObject.Find("UserInputBox").GetComponent<UIInput>().value);
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
            if (jsonData.GetField("status").n == 1)
            {
                print("Record inserted");
                print(resultData);
                GameObject.Find("CredentialLbl").GetComponent<UILabel>().text = "";
                GameObject.Find("ScriptAttachment").GetComponent<SignUpanimation>().submitPassword();
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
        string url = "http://bmc.bitwise.in/api/checkemail";
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
            if (jsonData.GetField("status").n == 1)
            {
                //Camera.main.GetComponent<Script2>().Script2fun();
                GameObject.Find("CredentialLbl").GetComponent<UILabel>().text = "Email id is already registered";
                // print("success...");
            }
            else if(jsonData.GetField("status").n == 2)
            {
                GameObject.Find("CredentialLbl").GetComponent<UILabel>().text = "Email id is already registered but not Verify";
            }
            else
            {
                GameObject.Find("ScriptAttachment").GetComponent<SignUpanimation>().submitMail();
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

        if (passwordstring == "")
        {
            GameObject.Find("CredentialLbl").GetComponent<UILabel>().text = "Password Can not be null";
        }
       else if(passwordstring.Length<=6)
        {
            GameObject.Find("CredentialLbl").GetComponent<UILabel>().text = "Password is too short";
        }
        else
        {
            GameObject.Find("CredentialLbl").GetComponent<UILabel>().text = "";
            CheckValidationsignup();
        }
    }

    public void checkUsername()
    {
        string Usernamestring = GameObject.Find("UserInputBox").GetComponent<UIInput>().value;

        if (Usernamestring == "")
        {
            GameObject.Find("CredentialLbl").GetComponent<UILabel>().text = "UserName Can not be null";
        }
        else
        {
            GameObject.Find("CredentialLbl").GetComponent<UILabel>().text = "";
            GameObject.Find("ScriptAttachment").GetComponent<SignUpanimation>().submitUsername();
        }
    }

}
