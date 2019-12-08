using System;
using System.Collections;
using System.Collections.Generic;
using Domino;
using UnityEngine;
using static Domino.Instantiator;

namespace Domino {
  public enum OutlineMode {
    NoOutline = 0,
    WithOutline = 1,
    WithBackOutline = 2
  }

  public class SymbolDescription {
    public readonly string symbolId;
    public readonly Color frontColor;
    public readonly float rotationDegrees;
    public readonly OutlineMode withOutline;
    public readonly Color outlineColor;

    public SymbolDescription(
        string symbolId,
        Color frontColor,
        float rotationDegrees,
        OutlineMode withOutline,
        Color outlineColor) {
      this.symbolId = symbolId;
      this.frontColor = frontColor;
      this.rotationDegrees = rotationDegrees;
      this.withOutline = withOutline;
      this.outlineColor = outlineColor;
    }
  }

  public class ExtrudedSymbolDescription {
    public readonly RenderPriority renderPriority;
    public readonly SymbolDescription symbol;
    public readonly bool extruded;
    public readonly Color sidesColor;

    public ExtrudedSymbolDescription(
        RenderPriority renderPriority,
        SymbolDescription symbol,
        bool extruded,
        Color sidesColor) {
      this.renderPriority = renderPriority;
      this.symbol = symbol;
      this.extruded = extruded;
      this.sidesColor = sidesColor;
    }
  }

  public class SymbolView : MonoBehaviour, IButts {
    private bool initialized = false;

    public List<IButts> observers = new List<IButts>();

    public void OnMouseClick() {
      foreach (var observer in observers) {
        observer.OnMouseClick();
      }
    }

    public void OnMouseIn() {
      foreach (var observer in observers) {
        observer.OnMouseIn();
      }
    }

    public void OnMouseOut() {
      foreach (var observer in observers) {
        observer.OnMouseOut();
      }
    }

    // The main object that lives in world space. It has no rotation or scale,
    // just a translation to the center of the tile the unit is in.
    // public GameObject gameObject; (provided by unity)

    // Object with a transform for the mesh, for example for rotating it.
    // Lives inside this.gameObject.
    // Specified by unity.
    public GameObject frontObject;
    public GameObject frontOutlineObject;
    public GameObject sidesObject;

    Instantiator instantiator;

    RenderPriority renderPriority;
    string symbolId_;
    bool extruded_;
    Color frontColor_;
    Color sidesColor_;
    float rotationDegrees_;
    OutlineMode withOutline_;
    Color outlineColor_;

    public void Init(
        Instantiator instantiator,
        bool mousable,
        ExtrudedSymbolDescription symbolDescription) {
      this.instantiator = instantiator;

      frontObject.GetComponent<ClickListener>().observers.Add(this);
      frontOutlineObject.GetComponent<ClickListener>().observers.Add(this);
      sidesObject.GetComponent<ClickListener>().observers.Add(this);

      this.renderPriority = symbolDescription.renderPriority;
      frontObject.transform.SetParent(gameObject.transform, false);
      frontOutlineObject.transform.SetParent(gameObject.transform, false);
      sidesObject.transform.SetParent(gameObject.transform, false);
      InnerSetSymbolId(symbolDescription.symbol.symbolId);
      InnerSetExtruded(symbolDescription.extruded);
      InnerSetWithOutline(symbolDescription.symbol.withOutline);
      InnerSetFrontColor(symbolDescription.symbol.frontColor);
      InnerSetSidesColor(symbolDescription.sidesColor);
      InnerSetOutlineColor(symbolDescription.symbol.outlineColor);
      initialized = true;

      frontObject.GetComponent<MeshCollider>().enabled = mousable;
      frontOutlineObject.GetComponent<MeshCollider>().enabled = mousable;
      sidesObject.GetComponent<MeshCollider>().enabled = mousable;
    }


    public ExtrudedSymbolDescription GetDescription() {
      return new ExtrudedSymbolDescription(
          renderPriority,
          new SymbolDescription(
              symbolId, frontColor, rotationDegrees, withOutline, outlineColor),
          extruded, sidesColor);
    }

    public void Destruct() {
      CheckInitialized();
      Destroy(gameObject);
      initialized = false;
    }

    public void CheckInitialized() {
      if (!initialized) {
        throw new System.Exception("SymbolView component not initialized!");
      }
    }

    public void SetDescription(ExtrudedSymbolDescription description) {
      CheckInitialized();
      symbolId = description.symbol.symbolId;
      extruded = description.extruded;
      withOutline = description.symbol.withOutline;
      frontColor = description.symbol.frontColor;
      sidesColor = description.sidesColor;
      outlineColor = description.symbol.outlineColor;
      rotationDegrees = description.symbol.rotationDegrees;
    }

    public string symbolId {
      get { CheckInitialized(); return symbolId_; }
      set {
        CheckInitialized();
        if (symbolId_ != value) {
          InnerSetSymbolId(value);
        }
      }
    }

    public bool extruded {
      get { CheckInitialized(); return extruded_; }
      set {
        CheckInitialized();
        if (extruded_ != value) {
          InnerSetExtruded(value);
        }
      }
    }

    public OutlineMode withOutline {
      get { CheckInitialized(); return withOutline_; }
      set {
        CheckInitialized();
        if (withOutline_ != value) {
          InnerSetWithOutline(value);
        }
      }
    }

    public Color frontColor {
      get { CheckInitialized(); return frontColor_; }
      set {
        CheckInitialized();
        if (!frontColor_.Equals(value)) {
          InnerSetFrontColor(value);
        }
      }
    }

    public Color outlineColor {
      get { CheckInitialized(); return outlineColor_; }
      set {
        CheckInitialized();
        if (!outlineColor_.Equals(value)) {
          InnerSetOutlineColor(value);
        }
      }
    }

    public Color sidesColor {
      get { CheckInitialized(); return sidesColor_; }
      set {
        CheckInitialized();
        if (!sidesColor_.Equals(value)) {
          InnerSetSidesColor(value);
        }
      }
    }

    public float rotationDegrees {
      get { CheckInitialized(); return rotationDegrees_; }
      set {
        CheckInitialized();
        if (rotationDegrees_ != value) {
          InnerSetRotationDegrees(rotationDegrees_);
        }
      }
    }

    private void InnerSetSymbolId(string newSymbolId) {
      SymbolMeshes meshes = instantiator.GetSymbolMeshes(newSymbolId);
      frontObject.GetComponent<MeshFilter>().mesh = meshes.front;
      frontObject.GetComponent<MeshCollider>().sharedMesh = meshes.front;
      frontOutlineObject.GetComponent<MeshFilter>().mesh = meshes.frontOutline;
      frontOutlineObject.GetComponent<MeshCollider>().sharedMesh = meshes.frontOutline;
      sidesObject.GetComponent<MeshFilter>().mesh = meshes.sides;
      sidesObject.GetComponent<MeshCollider>().sharedMesh = meshes.sides;
      symbolId_ = newSymbolId;
      InnerSetWithOutline(withOutline_);
      InnerSetFrontColor(frontColor_);
      InnerSetSidesColor(sidesColor_);
    }

    private void InnerSetExtruded(bool newExtruded) {
      sidesObject.SetActive(newExtruded);
      extruded_ = newExtruded;
    }

    private void InnerSetWithOutline(OutlineMode newWithOutline) {
      frontOutlineObject.SetActive(newWithOutline != OutlineMode.NoOutline);
      if (newWithOutline == OutlineMode.WithBackOutline) {
        frontOutlineObject.transform.localPosition = new Vector3(0, 0, -.01f);
        frontOutlineObject.transform.localScale = new Vector3(1, 1, .01f);
      }
      withOutline_ = newWithOutline;
    }

    private void InnerSetFrontColor(Color newColor) {
      frontObject.GetComponent<ColorChanger>().SetColor(
          newColor, renderPriority);

      var blackWithNewOpacity = new Color(0, 0, 0, newColor.a);
      frontOutlineObject.GetComponent<ColorChanger>().SetColor(
          blackWithNewOpacity, renderPriority);

      frontColor_ = newColor;
    }

    private void InnerSetSidesColor(Color newColor) {
      sidesObject.GetComponent<ColorChanger>().SetColor(newColor, renderPriority);
      sidesColor_ = newColor;
    }

    private void InnerSetOutlineColor(Color newColor) {
      frontOutlineObject.GetComponent<ColorChanger>().SetColor(newColor, renderPriority);
      outlineColor_ = newColor;
    }

    private void InnerSetRotationDegrees(float newRotationDegrees) {
      transform.rotation = Quaternion.AngleAxis(rotationDegrees, Vector3.forward);
      rotationDegrees_ = newRotationDegrees;
    }

    private static Vector3[] GetMinAndMax(Mesh mesh) {
      Vector3 min = mesh.vertices[0];
      Vector3 max = mesh.vertices[0];
      foreach (var vertex in mesh.vertices) {
        min.x = Math.Min(min.x, vertex.x);
        min.y = Math.Min(min.y, vertex.y);
        min.z = Math.Min(min.z, vertex.z);
        max.x = Math.Max(max.x, vertex.x);
        max.y = Math.Max(max.y, vertex.y);
        max.z = Math.Max(max.z, vertex.z);
      }
      return new Vector3[] { min, max };
    }

    private static Matrix4x4 CalculateSymbolTransform(bool front) {
      MatrixBuilder transform = new MatrixBuilder(Matrix4x4.identity);

      //if (!front) {
      //  transform.Translate(new Vector3(0, 0, 1));
      //}

      // When we generated the .obj models, they had varying X and Y coordinates, and the
      // Z was flat. We imagined them as lying on the ground.

      // Our models were made in right-hand space. Unity tries to "help" by multiplying
      // all X coordinates by -1. This flipped all our models horizontally.

      // In the end, this means that they're standing upright, with their X flipped.

      // We undo the horizontal flip here.
      //transform.Scale(new Vector3(-1, 1, 1));

      // They're still standing upright, remember.

      // Our models have their normals pointing +Z, and have the sides going -Z, because
      // we assume the camera is high in the +Z.
      // In Unity, the camera is low in the -Z, so rotate to face it.
      //transform.Rotate(Quaternion.AngleAxis(180, Vector3.up));

      // One would think that we'd have to flip things horizontally since we're looking
      // at it from the other side, but we don't since Unity flips all .obj vertices'
      // X coordinates by -1 when it imports.


      //// Somehow, the above shifted the Z by a lot. We shift it back.
      //transform.Translate(new Vector3(0, 0, 1));

      // Now, it's centered inside 0,0 1,1.
      return transform.matrix;
    }

    public void Start() {
      CheckInitialized();
    }

    public void FadeInThenOut(float inDuration, float outDuration) {
      GetOrCreateFrontOpacityAnimator().opacityAnimation =
          MakeFadeInThenOutAnimation(inDuration, outDuration);
      GetOrCreateFrontOutlineOpacityAnimator().opacityAnimation =
          MakeFadeInThenOutAnimation(inDuration, outDuration);
      GetOrCreateSidesOpacityAnimator().opacityAnimation =
          MakeFadeInThenOutAnimation(inDuration, outDuration);
    }

    private IFloatAnimation MakeFadeInThenOutAnimation(float inDuration, float outDuration) {
      return new ClampFloatAnimation(
          Time.time, Time.time + inDuration + outDuration,
          new ThenFloatAnimation(
              Time.time + inDuration,
              new LinearFloatAnimation(Time.time, 0.0f, 1.0f / inDuration),
              new LinearFloatAnimation(Time.time + inDuration, 1.0f, -1.0f / outDuration)));
    }

    public void Fade(float duration) {
      GetOrCreateFrontOpacityAnimator().opacityAnimation = CreateFadeAnimation(duration);
      GetOrCreateFrontOutlineOpacityAnimator().opacityAnimation = CreateFadeAnimation(duration);
      GetOrCreateSidesOpacityAnimator().opacityAnimation = CreateFadeAnimation(duration);
    }

    private IFloatAnimation CreateFadeAnimation(float duration) {
      return new ClampFloatAnimation(Time.time, Time.time + duration,
          new LinearFloatAnimation(Time.time, 1.0f, -1.0f / duration));
    }

    private OpacityAnimator GetOrCreateSidesOpacityAnimator() {
      CheckInitialized();
      var animator = sidesObject.GetComponent<OpacityAnimator>();
      if (animator == null) {
        animator = sidesObject.AddComponent<OpacityAnimator>() as OpacityAnimator;
        animator.renderPriority = renderPriority;
      }
      return animator;
    }

    private OpacityAnimator GetOrCreateFrontOpacityAnimator() {
      CheckInitialized();
      var animator = frontObject.GetComponent<OpacityAnimator>();
      if (animator == null) {
        animator = frontObject.AddComponent<OpacityAnimator>() as OpacityAnimator;
        animator.renderPriority = renderPriority;
      }
      return animator;
    }

    private OpacityAnimator GetOrCreateFrontOutlineOpacityAnimator() {
      CheckInitialized();
      var animator = frontOutlineObject.GetComponent<OpacityAnimator>();
      if (animator == null) {
        animator = frontOutlineObject.AddComponent<OpacityAnimator>() as OpacityAnimator;
        animator.renderPriority = renderPriority;
      }
      return animator;
    }

  }
}
