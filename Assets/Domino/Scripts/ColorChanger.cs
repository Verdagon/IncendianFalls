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

  private Material cachedGlowMaterial;
  private Material cachedOpaqueMaterial;
  private Material cachedTransparentMaterial;

  private Color currentColor;
  private RenderPriority renderPriority;

  public (Color, RenderPriority) Get() {
    return (currentColor, renderPriority);
  }

  public void Set(Color newCurrentColor, RenderPriority newRenderPriority) {
    if (newCurrentColor == currentColor && newRenderPriority == renderPriority) {
      return;
    }

    renderPriority = newRenderPriority;
    currentColor = newCurrentColor;

    Color lightColor = newCurrentColor;
    Color darkColor = new Color(currentColor.r * .8f, currentColor.g * .8f, currentColor.b * .8f, currentColor.a);

    Material lightMaterial = GetOrInstantiateMaterial(lightColor, newRenderPriority);
    foreach (var part in lightColoredParts) {
      var meshRenderer = part.GetComponent<MeshRenderer>();
      meshRenderer.material = lightMaterial;
      meshRenderer.enabled = lightColor.a > .001f;
    }

    if (darkColoredParts.Length > 0) {
      Material darkMaterial = GetOrInstantiateMaterial(darkColor, newRenderPriority);
      foreach (var part in darkColoredParts) {
        var meshRenderer = part.GetComponent<MeshRenderer>();
        meshRenderer.material = darkMaterial;
        meshRenderer.enabled = darkColor.a > .001f;
      }
    }
  }

  private Material GetOrInstantiateMaterial(Color color, RenderPriority renderPriority) {
    if (color.a < .99f) {
      if (cachedTransparentMaterial == null) {
        cachedTransparentMaterial = Instantiate(transparentMaterial);
      }
      cachedTransparentMaterial.color = color;
      cachedTransparentMaterial.renderQueue =
          (int)UnityEngine.Rendering.RenderQueue.Transparent +
          (int)renderPriority;
      return cachedTransparentMaterial;
    } else if (color.a > 1.01f) {
      if (cachedGlowMaterial == null) {
        cachedGlowMaterial = Instantiate(transparentMaterial);
      }
      cachedGlowMaterial.color = color;
      cachedGlowMaterial.SetColor("_EmissionColor", color);
      return cachedGlowMaterial;
    } else {
      if (cachedOpaqueMaterial == null) {
        cachedOpaqueMaterial = Instantiate(opaqueMaterial);
      }
      //Debug.Log("Setting color to " + color.r + " " + color.g + " " + color.b);
      cachedOpaqueMaterial.color = color;
      //Debug.Log("Now color is " + materialClone.color.r + " " + materialClone.color.g + " " + materialClone.color.b);
      return cachedOpaqueMaterial;
    }
  }
}
