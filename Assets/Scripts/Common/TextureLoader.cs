using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class TextureLoader : MonoBehaviour
{
    [SerializeField]
    private Vector2Int imageSize = new Vector2Int(300, 300);

    private string UrlImageSource => string.Format(urlRequest, GetRandomSeed(), imageSize.x, imageSize.y);

    private const string urlRequest = "https://picsum.photos/seed/{0}/{1}/{2}";

    [HideInInspector]
    public Texture2D loadedTexture;

    private static string GetRandomSeed()
    {
        return Random.Range(-999, 999).ToString();
    }

    public IEnumerator LoadingTexture()
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(UrlImageSource);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError($"Failure to download image with error: {www.error}");
        }
        else
        {
            loadedTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        }
    }

}
