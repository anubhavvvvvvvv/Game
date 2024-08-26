using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using System.Collections;
using System.IO;

public class profileSelection : MonoBehaviour
{
    /* public Image profileImage; // Reference to the UI Image where the selected image will be displayed.

     private string imagePath; // Stores the selected image's file path.

     public void SelectProfilePicture()
     {
         // Check if the platform supports opening a file picker dialog
         if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor || Application.platform==RuntimePlatform.Android)
         {
             // Open the file picker dialog
             string[] allowedExtensions = { "png", "jpg", "jpeg" }; // Define the allowed image file extensions
             string path;// = EditorUtility.OpenFilePanel("Select a profile picture", "", string.Join(",", allowedExtensions));

             if (!string.IsNullOrEmpty(path))
             {
                 imagePath = path;

                 // Load the selected image into the UI Image
                 StartCoroutine(LoadImage());
             }
         }
         else
         {
             // Platform doesn't support opening a file picker dialog
             Debug.LogWarning("File picking is not supported on this platform.");
         }
     }

     private IEnumerator LoadImage()
     {
         // Load the selected image as a texture
         if (File.Exists(imagePath))
         {
             byte[] imageData = File.ReadAllBytes(imagePath);
             Texture2D texture = new Texture2D(2, 2);
             texture.LoadImage(imageData);

             // Set the loaded texture as the profile picture
             profileImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
             profileImage.rectTransform.sizeDelta = new Vector2(100, 100); // Adjust the size as needed
             profileImage.rectTransform.anchoredPosition = new Vector2(0, 0);
         }
         yield return null;
     }
 }*/

   /* private string imagePath;
    public RawImage imageDisplay;

    void Start()
    {
        if (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {
            // Permission granted, you can proceed to select an image
        }
        else
        {
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
        }
    }

    public void OnSelectImage()
    {
        // Open the gallery and select an image
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            if (path != null)
            {
                // Load and display the selected image
                imagePath = path;
                StartCoroutine(LoadImage());
            }
        });

        Debug.Log("Permission result: " + permission);
    }

    IEnumerator LoadImage()
    {
        // Load the selected image
        WWW www = new WWW("file://" + imagePath);
        yield return www;

        // Display the image
        imageDisplay.texture = www.texture;
    }*/
}




