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

  private MaterialCache lightMaterialCache;
  private MaterialCache darkMaterialCache;
  
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

    if (lightMaterialCache == null) {
      lightMaterialCache = new MaterialCache(this);
    }
    var lightMaterial = lightMaterialCache.GetMaterial(newCurrentColor, renderPriority);
    foreach (var part in lightColoredParts) {
      var meshRenderer = part.GetComponent<MeshRenderer>();
      meshRenderer.material = lightMaterial;
      meshRenderer.enabled = newCurrentColor.a > .001f;
    }

    if (darkColoredParts.Length > 0) {
      if (darkMaterialCache == null) {
        darkMaterialCache = new MaterialCache(this);
      }
      Color darkColor = new Color(currentColor.r * .8f, currentColor.g * .8f, currentColor.b * .8f, currentColor.a);
      var darkMaterial = darkMaterialCache.GetMaterial(darkColor, renderPriority);
      foreach (var part in darkColoredParts) {
        var meshRenderer = part.GetComponent<MeshRenderer>();
        meshRenderer.material = darkMaterial;
        meshRenderer.enabled = newCurrentColor.a > .001f;
      }
    }
  }

  class MaterialCache {
    private ColorChanger changer;
    private Material cachedGlowMaterial = null;
    private Material cachedOpaqueMaterial = null;
    private Material cachedTransparentMaterial = null;

    public MaterialCache(ColorChanger changer) {
      this.changer = changer;
    }

    public Material GetMaterial(Color color, RenderPriority renderPriority) {
      var darkColor = new Color(color.r * .8f, color.g * .8f, color.b * .8f, color.a);
      if (color.a < .99f) {
        if (ReferenceEquals(cachedTransparentMaterial, null)) {
          cachedTransparentMaterial = Instantiate(changer.transparentMaterial);
        }
        cachedTransparentMaterial.color = color;
        cachedTransparentMaterial.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent + (int)renderPriority;
        return cachedTransparentMaterial;
      } else if (color.a > 1.01f) {
        if (ReferenceEquals(cachedTransparentMaterial, null)) {
          cachedGlowMaterial = Instantiate(changer.transparentMaterial);
        }
        cachedGlowMaterial.color = color;
        cachedGlowMaterial.SetColor("_EmissionColor", color);
        return cachedGlowMaterial;
      } else {
        if (ReferenceEquals(cachedOpaqueMaterial, null)) {
          cachedOpaqueMaterial = Instantiate(changer.opaqueMaterial);
        }
        cachedOpaqueMaterial.color = color;
        return cachedOpaqueMaterial;
      }      
    }
  }
}
