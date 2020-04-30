using System.Collections;
using System.Collections.Generic;
using Domino;
using UnityEngine;

public class ColorChanger : MonoBehaviour {
  public GameObject[] lightColoredParts;
  public GameObject[] darkColoredParts;
  public Material glowMaterial;
  public Material opaqueMaterial;
  public Material transparentMaterial;

  private Color currentColor;
  private RenderPriority renderPriority;

  public (Color, RenderPriority) Get() {
    return (currentColor, renderPriority);
  }

  public void Set(Color newCurrentColor, RenderPriority newRenderPriority) {
    Debug.LogError("Start here, have a static cache of color+priority to material");

    renderPriority = newRenderPriority;
    currentColor = newCurrentColor;
    Color lightColor = newCurrentColor;
    Color darkColor = lightColor;
    darkColor.r *= .8f;
    darkColor.g *= .8f;
    darkColor.b *= .8f;

    Material lightMaterial = InstantiateMaterial(lightColor, newRenderPriority);
    foreach (var part in lightColoredParts) {
      part.GetComponent<MeshRenderer>().material = lightMaterial;
      part.GetComponent<MeshRenderer>().enabled = lightColor.a > .001f;
    }

    Material darkMaterial = InstantiateMaterial(darkColor, newRenderPriority);
    foreach (var part in darkColoredParts) {
      part.GetComponent<MeshRenderer>().material = darkMaterial;
      part.GetComponent<MeshRenderer>().enabled = darkColor.a > .001f;
    }
  }

  private Material InstantiateMaterial(Color color, RenderPriority renderPriority) {
    if (color.a < .99f) {
      Material materialClone = Instantiate(transparentMaterial);
      materialClone.color = color;
      materialClone.renderQueue =
          (int)UnityEngine.Rendering.RenderQueue.Transparent +
          (int)renderPriority;
      return materialClone;
    } else if (color.a > 1.01f) {
      Material materialClone = Instantiate(glowMaterial);
      materialClone.color = color;
      materialClone.SetColor("_EmissionColor", color);
      return materialClone;
    } else {
      Material materialClone = Instantiate(opaqueMaterial);
      //Debug.Log("Setting color to " + color.r + " " + color.g + " " + color.b);
      materialClone.color = color;
      //Debug.Log("Now color is " + materialClone.color.r + " " + materialClone.color.g + " " + materialClone.color.b);
      return materialClone;
    }
  }
}
