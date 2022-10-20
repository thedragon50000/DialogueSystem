using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ReadingExcel_sc : MonoBehaviour
{
    // https: //script.google.com/macros/library/d/1KkbfT3mUEPsgyAnOhbonSJjkYvARdf-LGIvLGrwfsZHxFJV4gz8VVWXh/1
    private void Start()
    {
        StartCoroutine(UpLoad());
    }

    IEnumerator UpLoad()
    {
        WWWForm form = new WWWForm();
        form.AddField("method", "Read");
        form.AddField("row", 2);
        form.AddField("col", 3);

        using (UnityWebRequest www = UnityWebRequest.Post(
                   "https: //script.google.com/macros/library/d/1KkbfT3mUEPsgyAnOhbonSJjkYvARdf-LGIvLGrwfsZHxFJV4gz8VVWXh/1", form))
        {
            yield return www.SendWebRequest();         

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                Debug.Log("Up load Complete");
            }
        }
    }
}