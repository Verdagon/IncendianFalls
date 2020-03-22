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
    public readonly int qualityPercent;
    public readonly Color frontColor;
    public readonly float rotationDegrees;
    public readonly OutlineMode withOutline;
    public readonly Color outlineColor;

    public SymbolDescription(
        string symbolId,
        int qualityPercent,
        Color frontColor,
        float rotationDegrees,
        OutlineMode withOutline,
        Color outlineColor) {
      this.symbolId = symbolId;
      this.qualityPercent = qualityPercent;
      this.frontColor = frontColor;
      this.rotationDegrees = rotationDegrees;
      this.withOutline = withOutline;
      this.outlineColor = outlineColor;
    }

    public SymbolDescription WithFrontColor(Color newFrontColor) {
      return new SymbolDescription(
        symbolId,
        qualityPercent,
        newFrontColor,
        rotationDegrees,
        withOutline,
        outlineColor);
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

    public ExtrudedSymbolDescription WithSymbol(SymbolDescription newSymbol) {
      return new ExtrudedSymbolDescription(renderPriority, newSymbol, extruded, sidesColor);
    }
    public ExtrudedSymbolDescription WithSidesColor(Color newSidesColor) {
      return new ExtrudedSymbolDescription(renderPriority, symbol, extruded, newSidesColor);
    }
  }

  public class SymbolView : MonoBehaviour {
    public bool instanceAlive { get; private set; }

    private IClock clock;
    private ITimer timer;

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
    int qualityPercent_;
    bool extruded_;
    Color frontColor_;
    Color sidesColor_;
    float rotationDegrees_;
    OutlineMode withOutline_;
    Color outlineColor_;

    public void Init(
        IClock clock,
        Instantiator instantiator,
        bool mousable,
        ExtrudedSymbolDescription symbolDescription) {
      this.clock = clock;
      this.instantiator = instantiator;

      this.renderPriority = symbolDescription.renderPriority;
      frontObject.transform.SetParent(gameObject.transform, false);
      frontOutlineObject.transform.SetParent(gameObject.transform, false);
      sidesObject.transform.SetParent(gameObject.transform, false);
      InnerSetSymbolId(symbolDescription.symbol.symbolId, symbolDescription.symbol.qualityPercent);
      InnerSetExtruded(symbolDescription.extruded);
      InnerSetWithOutline(symbolDescription.symbol.withOutline);
      InnerSetFrontColor(symbolDescription.symbol.frontColor);
      InnerSetSidesColor(symbolDescription.sidesColor);
      InnerSetOutlineColor(symbolDescription.symbol.outlineColor);
      instanceAlive = true;

      frontObject.GetComponent<MeshCollider>().enabled = mousable;
      frontOutlineObject.GetComponent<MeshCollider>().enabled = mousable;
      sidesObject.GetComponent<MeshCollider>().enabled = mousable;
    }


    public ExtrudedSymbolDescription GetDescription() {
      return new ExtrudedSymbolDescription(
          renderPriority,
          new SymbolDescription(
              symbolId, qualityPercent, frontColor, rotationDegrees, withOutline, outlineColor),
          extruded, sidesColor);
    }

    public void Destruct() {
      CheckInstanceAlive();
      Destroy(gameObject);
      instanceAlive = false;
    }

    public void CheckInstanceAlive() {
      if (!instanceAlive) {
        throw new System.Exception("SymbolView component not initialized!");
      }
    }

    public void SetDescription(ExtrudedSymbolDescription description) {
      CheckInstanceAlive();
      symbolId = description.symbol.symbolId;
      extruded = description.extruded;
      withOutline = description.symbol.withOutline;
      frontColor = description.symbol.frontColor;
      sidesColor = description.sidesColor;
      outlineColor = description.symbol.outlineColor;
      rotationDegrees = description.symbol.rotationDegrees;
    }

    public int qualityPercent {
      get { CheckInstanceAlive(); return qualityPercent_; }
      set {
        CheckInstanceAlive();
        if (qualityPercent_ != value) {
          InnerSetSymbolId(symbolId, value);
        }
      }
    }

    public string symbolId {
      get { CheckInstanceAlive(); return symbolId_; }
      set {
        CheckInstanceAlive();
        if (symbolId_ != value) {
          InnerSetSymbolId(value, qualityPercent);
        }
      }
    }

    public bool extruded {
      get { CheckInstanceAlive(); return extruded_; }
      set {
        CheckInstanceAlive();
        if (extruded_ != value) {
          InnerSetExtruded(value);
        }
      }
    }

    public OutlineMode withOutline {
      get { CheckInstanceAlive(); return withOutline_; }
      set {
        CheckInstanceAlive();
        if (withOutline_ != value) {
          InnerSetWithOutline(value);
        }
      }
    }

    public Color frontColor {
      get { CheckInstanceAlive(); return frontColor_; }
      set {
        CheckInstanceAlive();
        if (!frontColor_.Equals(value)) {
          InnerSetFrontColor(value);
        }
      }
    }

    public Color outlineColor {
      get { CheckInstanceAlive(); return outlineColor_; }
      set {
        CheckInstanceAlive();
        if (!outlineColor_.Equals(value)) {
          InnerSetOutlineColor(value);
        }
      }
    }

    public Color sidesColor {
      get { CheckInstanceAlive(); return sidesColor_; }
      set {
        CheckInstanceAlive();
        if (!sidesColor_.Equals(value)) {
          InnerSetSidesColor(value);
        }
      }
    }

    public float rotationDegrees {
      get { CheckInstanceAlive(); return rotationDegrees_; }
      set {
        CheckInstanceAlive();
        if (rotationDegrees_ != value) {
          InnerSetRotationDegrees(rotationDegrees_);
        }
      }
    }

    private void InnerSetSymbolId(string newSymbolId, int qualityPercent) {
      SymbolMeshes meshes = instantiator.GetSymbolMeshes(newSymbolId, qualityPercent);
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
      CheckInstanceAlive();
    }

    public void FadeInThenOut(long inDurationMs, long outDurationMs) {
      GetOrCreateFrontOpacityAnimator().opacityAnimation =
          MakeFadeInThenOutAnimation(inDurationMs, outDurationMs);
      GetOrCreateFrontOutlineOpacityAnimator().opacityAnimation =
          MakeFadeInThenOutAnimation(inDurationMs, outDurationMs);
      GetOrCreateSidesOpacityAnimator().opacityAnimation =
          MakeFadeInThenOutAnimation(inDurationMs, outDurationMs);
    }

    private IFloatAnimation MakeFadeInThenOutAnimation(long inDurationMs, long outDurationMs) {
      return new ClampFloatAnimation(
          clock.GetTimeMs(), clock.GetTimeMs() + inDurationMs + outDurationMs,
          new ThenFloatAnimation(
              clock.GetTimeMs() + inDurationMs,
              new LinearFloatAnimation(clock.GetTimeMs(), 0.0f, 1.0f / inDurationMs),
              new LinearFloatAnimation(clock.GetTimeMs() + inDurationMs, 1.0f, -1.0f / outDurationMs)));
    }

    public void Fade(long durationMs) {
      GetOrCreateFrontOpacityAnimator().opacityAnimation = CreateFadeAnimation(durationMs);
      GetOrCreateFrontOutlineOpacityAnimator().opacityAnimation = CreateFadeAnimation(durationMs);
      GetOrCreateSidesOpacityAnimator().opacityAnimation = CreateFadeAnimation(durationMs);
    }

    private IFloatAnimation CreateFadeAnimation(long durationMs) {
      return new ClampFloatAnimation(clock.GetTimeMs(), clock.GetTimeMs() + durationMs,
          new LinearFloatAnimation(clock.GetTimeMs(), 1.0f, -1.0f / durationMs));
    }

    private OpacityAnimator GetOrCreateSidesOpacityAnimator() {
      CheckInstanceAlive();
      var animator = sidesObject.GetComponent<OpacityAnimator>();
      if (animator == null) {
        animator = sidesObject.AddComponent<OpacityAnimator>() as OpacityAnimator;
        animator.Init(clock, renderPriority);
      }
      return animator;
    }

    private OpacityAnimator GetOrCreateFrontOpacityAnimator() {
      CheckInstanceAlive();
      var animator = frontObject.GetComponent<OpacityAnimator>();
      if (animator == null) {
        animator = frontObject.AddComponent<OpacityAnimator>() as OpacityAnimator;
        animator.Init(clock, renderPriority);
      }
      return animator;
    }

    private OpacityAnimator GetOrCreateFrontOutlineOpacityAnimator() {
      CheckInstanceAlive();
      var animator = frontOutlineObject.GetComponent<OpacityAnimator>();
      if (animator == null) {
        animator = frontOutlineObject.AddComponent<OpacityAnimator>() as OpacityAnimator;
        animator.Init(clock, renderPriority);
      }
      return animator;
    }

  }
}
