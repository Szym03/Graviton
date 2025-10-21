using UnityEngine;

public class BlitScript : MonoBehaviour
{
    public RenderTexture lowResTexture;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (lowResTexture != null)
        {
            Graphics.Blit(lowResTexture, dest);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}
