using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Firebase;
//using Firebase.Auth;
//using Google;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GoogleSignInDemo : MonoBehaviour
{
   /* public TextMeshProUGUI infoText;
    public string webClientId = "<your client id here>";

    private FirebaseAuth auth;
    private GoogleSignInConfiguration configuration;

    private void Awake()
    {
        configuration = new GoogleSignInConfiguration { WebClientId = webClientId, RequestEmail = true, RequestIdToken = true };
        CheckFirebaseDependencies();
    }

    private void CheckFirebaseDependencies()
    {
        try
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    if (task.Result == DependencyStatus.Available)
                        auth = FirebaseAuth.DefaultInstance;
                    else
                        AddToInformation("Could not resolve all Firebase dependencies: " + task.Result.ToString());
                }
                else
                {
                    AddToInformation("Dependency check was not completed. Error : " + task.Exception.Message);
                }
            });
        }catch(Exception e) { }
    }

    public void SignInWithGoogle() { OnSignIn(); }
    public void SignOutFromGoogle() { OnSignOut(); }

    private void OnSignIn()
    {
        try
        {
            GoogleSignIn.Configuration = configuration;
            GoogleSignIn.Configuration.UseGameSignIn = false;
            GoogleSignIn.Configuration.RequestIdToken = true;
            AddToInformation("Calling SignIn");

            GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
            AddToInformation("after Calling SignIn");
        }catch(Exception e) { }
    }

    private void OnSignOut()
    {
        AddToInformation("Calling SignOut");
        GoogleSignIn.DefaultInstance.SignOut();
    }

    public void OnDisconnect()
    {
        try
        {
            AddToInformation("Calling Disconnect");
            GoogleSignIn.DefaultInstance.Disconnect();
        }catch(Exception e) { }
    }

    internal void OnAuthenticationFinished(Task<GoogleSignInUser> task)
    {
        try
        {
            AddToInformation("OnAuthenticationFinished");
            if (task.IsFaulted)
            {
                using (IEnumerator<Exception> enumerator = task.Exception.InnerExceptions.GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        GoogleSignIn.SignInException error = (GoogleSignIn.SignInException)enumerator.Current;
                        AddToInformation("Got Error: " + error.Status + " " + error.Message);
                    }
                    else
                    {

                        AddToInformation("Got Unexpected Exception?!?" + task.Exception);
                    }
                }
            }
            else if (task.IsCanceled)
            {
                AddToInformation("Canceled");
            }
            else
            {
                AddToInformation("Welcome: " + task.Result.DisplayName + "!");
                AddToInformation("Email = " + task.Result.Email);
                AddToInformation("Google ID Token = " + task.Result.IdToken);
                AddToInformation("Email = " + task.Result.Email);
                SignInWithGoogleOnFirebase(task.Result.IdToken);
            }
        }
        catch(Exception e) { }
    }

    private void SignInWithGoogleOnFirebase(string idToken)
    {
        try{
            AddToInformation("SignInWithGoogleOnFirebase");
            Credential credential = GoogleAuthProvider.GetCredential(idToken, null);

            auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
            {
                AggregateException ex = task.Exception;
                if (ex != null)
                {
                    if (ex.InnerExceptions[0] is FirebaseException inner && (inner.ErrorCode != 0))
                        AddToInformation("\nError code = " + inner.ErrorCode + " Message = " + inner.Message);
                }
                else
                {
                    transform.GetComponent<DisablePage>().MoveNext(true);
                    AddToInformation("Sign In Successful.");
                }
            });
        }catch(Exception e) { }
    }

    public void OnSignInSilently()
    {
        try
        {
            GoogleSignIn.Configuration = configuration;
            GoogleSignIn.Configuration.UseGameSignIn = false;
            GoogleSignIn.Configuration.RequestIdToken = true;
            AddToInformation("Calling SignIn Silently");

            GoogleSignIn.DefaultInstance.SignInSilently().ContinueWith(OnAuthenticationFinished);
        }catch(Exception e) { }
    }

    public void OnGamesSignIn()
    {
        try
        {
            GoogleSignIn.Configuration = configuration;
            GoogleSignIn.Configuration.UseGameSignIn = true;
            GoogleSignIn.Configuration.RequestIdToken = false;

            AddToInformation("Calling Games SignIn");

            GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
        }catch(Exception e) { }
    }

    private void AddToInformation(string str)
    {
        StartCoroutine(MessageText(str));
    }
    IEnumerator MessageText(string str)
    {
        infoText.gameObject.SetActive(true);
        infoText.text += "\n" + str;
        yield return new WaitForSeconds(4.0f);
        infoText.text = "";
        infoText.gameObject.SetActive(false);
        
    }*/
}