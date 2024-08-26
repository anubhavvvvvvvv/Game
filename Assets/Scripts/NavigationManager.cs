using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static NavigationManager Instance { get; private set; }

    private Stack<GameObject> pageStack = new Stack<GameObject>();
    public GameObject currentPage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NavigateTo(GameObject newPage)
    {
        if (currentPage != null)
        {
            pageStack.Push(currentPage);
            currentPage.SetActive(false);
        }

        currentPage = newPage;
        Debug.Log(newPage.name);
        currentPage.SetActive(true);
    }

    public void NavigateBack()
    {
        if (pageStack.Count > 0)
        {
            currentPage.SetActive(false);
            currentPage = pageStack.Pop();
            currentPage.SetActive(true);
        }
    }
}
