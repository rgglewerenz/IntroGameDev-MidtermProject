using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerFlashing : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> playerComponents;

    private float opacity = 1f;
    private float time_scaler = 5f;
    private bool increasingOpacity = false;



    public void Solidify()
    {

        foreach (var player in playerComponents)
        {

            var playerRenderer = player.GetComponent<Renderer>();

            // Get the current material color
            Color currentColor = playerRenderer.material.color;

            // Create a new color with the desired opacity
            Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, 1);

            // Apply the new color to the material
            playerRenderer.material.color = newColor;
        }
    }


    public void Flash()
    {
        if (!increasingOpacity)
        {
            opacity = Mathf.Lerp(opacity, 0.2f, Time.deltaTime * time_scaler);
        }
        else
        {
            opacity = Mathf.Lerp(opacity, 1, Time.deltaTime * time_scaler);
        }

        if (opacity >= 0.95f || opacity <= 0.25f)
        {
            increasingOpacity = !increasingOpacity;
        }

        foreach (var player in playerComponents)
        {

            var playerRenderer = player.GetComponent<Renderer>();

            // Get the current material color
            Color currentColor = playerRenderer.material.color;

            // Create a new color with the desired opacity
            Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, opacity);

            // Apply the new color to the material
            playerRenderer.material.color = newColor;
        }
    }

}
