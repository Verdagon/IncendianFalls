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
    public readonly SymbolId symbolId;
    public readonly IVector4Animation frontColor;
    public readonly float rotationDegrees;
    public readonly float scale;
    public readonly OutlineMode withOutline;
    public readonly IVector4Animation outlineColor;

    public SymbolDescription(
        string symbolId,
        IVector4Animation frontColor,
        float rotationDegrees,
        float scale,
        OutlineMode withOutline) : this(new SymbolId(symbolId, 100), frontColor, rotationDegrees, scale, withOutline) { }

    public SymbolDescription(
        SymbolId symbolId,
        IVector4Animation frontColor,
        float rotationDegrees,
        float scale,
        OutlineMode withOutline)
      : this(symbolId, frontColor, rotationDegrees, scale, withOutline, Vector4Animation.BLACK) { }

    public SymbolDescription(
        string symbolId,
        IVector4Animation frontColor,
        float rotationDegrees,
        float scale,
        OutlineMode withOutline,
        IVector4Animation outlineColor)
      : this(new SymbolId(symbolId, 100), frontColor, rotationDegrees, scale, withOutline, outlineColor) { }

    public SymbolDescription(
        SymbolId symbolId,
        IVector4Animation frontColor,
        float rotationDegrees,
        float scale,
        OutlineMode withOutline,
        IVector4Animation outlineColor) {
      this.symbolId = symbolId;
      this.frontColor = frontColor;
      this.rotationDegrees = rotationDegrees;
      this.scale = scale;
      this.withOutline = withOutline;
      this.outlineColor = outlineColor;

      Asserts.Assert(outlineColor != null);
    }

    public SymbolDescription WithFrontColor(IVector4Animation newFrontColor) {
      return new SymbolDescription(
        symbolId,
        newFrontColor,
        rotationDegrees,
        scale,
        withOutline);
    }
  }

  public class ExtrudedSymbolDescription {
    public readonly RenderPriority renderPriority;
    public readonly SymbolDescription symbol;
    public readonly bool extruded;
    public readonly IVector4Animation sidesColor;

    public ExtrudedSymbolDescription(
        RenderPriority renderPriority,
        SymbolDescription symbol,
        bool extruded,
        IVector4Animation sidesColor) {
      this.renderPriority = renderPriority;
      this.symbol = symbol;
      this.extruded = extruded;
      this.sidesColor = sidesColor;
    }

    public ExtrudedSymbolDescription WithSymbol(SymbolDescription newSymbol) {
      return new ExtrudedSymbolDescription(renderPriority, newSymbol, extruded, sidesColor);
    }
    public ExtrudedSymbolDescription WithSidesColor(IVector4Animation newSidesColor) {
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
    SymbolId symbolId;
    bool extruded;
    IVector4Animation frontColor;
    IVector4Animation sidesColor;
    float rotationDegrees;
    float scale;
    OutlineMode withOutline;
    IVector4Animation outlineColor;

    public void Init(
        IClock clock,
        Instantiator instantiator,
        bool mousable,
        // If true, z=0 will be the front of the symbol.
        // If false, z=0 will be the back of the symbol (only really makes sense for extruded symbols).
        bool originFront,
        ExtrudedSymbolDescription symbolDescription) {
      this.clock = clock;
      this.instantiator = instantiator;

      this.renderPriority = symbolDescription.renderPriority;
      frontObject.transform.SetParent(gameObject.transform, false);
      frontOutlineObject.transform.SetParent(gameObject.transform, false);
      sidesObject.transform.SetParent(gameObject.transform, false);

      if (!originFront) {
        frontObject.transform.localPosition = new Vector3(0, 0, 1);
        frontOutlineObject.transform.localPosition = new Vector3(0, 0, 1);
        sidesObject.transform.localPosition = new Vector3(0, 0, 1);
      }

      InnerSetSymbolId(symbolDescription.symbol.symbolId);
      InnerSetExtruded(symbolDescription.extruded);
      InnerSetOutline(symbolDescription.symbol.withOutline, symbolDescription.symbol.outlineColor);
      SetFrontColor(symbolDescription.symbol.frontColor);
      SetSidesColor(symbolDescription.sidesColor);
      InnerSetScale(symbolDescription.symbol.scale);
      instanceAlive = true;

      frontObject.GetComponent<MeshCollider>().enabled = mousable;
      frontOutlineObject.GetComponent<MeshCollider>().enabled = mousable;
      sidesObject.GetComponent<MeshCollider>().enabled = mousable;
    }


    public ExtrudedSymbolDescription GetDescription() {
      return new ExtrudedSymbolDescription(
          renderPriority,
          new SymbolDescription(
              symbolId, frontColor, rotationDegrees, scale, withOutline),
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
      rotationDegrees = description.symbol.rotationDegrees;
      scale = description.symbol.scale;
    }

    private void InnerSetSymbolId(SymbolId newSymbolId) {
      SymbolMeshes meshes = instantiator.GetSymbolMeshes(newSymbolId);
      frontObject.GetComponent<MeshFilter>().mesh = meshes.front;
      frontObject.GetComponent<MeshCollider>().sharedMesh = meshes.front;
      frontOutlineObject.GetComponent<MeshFilter>().mesh = meshes.frontOutline;
      frontOutlineObject.GetComponent<MeshCollider>().sharedMesh = meshes.frontOutline;
      sidesObject.GetComponent<MeshFilter>().mesh = meshes.sides;
      sidesObject.GetComponent<MeshCollider>().sharedMesh = meshes.sides;
      symbolId = newSymbolId;
      //InnerSetOutline(withOutline, outlineColor);
      //InnerSetFrontColor(frontColor);
      //InnerSetSidesColor(sidesColor);
    }

    private void InnerSetExtruded(bool newExtruded) {
      sidesObject.SetActive(newExtruded);
      extruded = newExtruded;
    }

    private void InnerSetOutline(OutlineMode newWithOutline, IVector4Animation newOutlineColor) {
      frontOutlineObject.SetActive(newWithOutline != OutlineMode.NoOutline);
      if (newWithOutline == OutlineMode.WithBackOutline) {
        frontOutlineObject.transform.localPosition = new Vector3(0, 0, -.01f);
        frontOutlineObject.transform.localScale = new Vector3(1, 1, .01f);
      }
      ColorAnimator.MakeOrGetFrom(clock, frontOutlineObject).Set(newOutlineColor, renderPriority);
      withOutline = newWithOutline;
      outlineColor = newOutlineColor;
    }

    public void SetFrontColor(IVector4Animation newColor) {
      ColorAnimator.MakeOrGetFrom(clock, frontObject).Set(newColor, renderPriority);

      frontColor = newColor;
    }

    public void SetSidesColor(IVector4Animation newColor) {
      ColorAnimator.MakeOrGetFrom(clock, sidesObject).Set(newColor, renderPriority);
      sidesColor = newColor;
    }

    private void InnerSetRotationDegrees(float newRotationDegrees) {
      transform.rotation = Quaternion.AngleAxis(rotationDegrees, Vector3.forward);
      rotationDegrees = newRotationDegrees;
    }

    private void InnerSetScale(float newScale) {
      frontObject.transform.localScale = new Vector3(newScale, newScale, newScale);
      sidesObject.transform.localScale = new Vector3(newScale, newScale, newScale);
      frontOutlineObject.transform.localScale = new Vector3(newScale, newScale, newScale);
      if (newScale > 9) {
        Debug.Log("yeah over 9");
      }
      scale = newScale;
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
      var frontAnimator = ColorAnimator.MakeOrGetFrom(clock, frontObject);
      frontAnimator.Set(
        FadeAnimator.FadeInThenOut(frontAnimator.Get(), clock.GetTimeMs(), inDurationMs, outDurationMs),
        renderPriority);
      var frontOutlineAnimator = ColorAnimator.MakeOrGetFrom(clock, frontOutlineObject);
      frontOutlineAnimator.Set(
        FadeAnimator.FadeInThenOut(frontOutlineAnimator.Get(), clock.GetTimeMs(), inDurationMs, outDurationMs),
        renderPriority);
      var sidesAnimator = ColorAnimator.MakeOrGetFrom(clock, sidesObject);
      sidesAnimator.Set(
        FadeAnimator.FadeInThenOut(sidesAnimator.Get(), clock.GetTimeMs(), inDurationMs, outDurationMs),
        renderPriority);
    }

    public void Fade(long durationMs) {
      var frontAnimator = ColorAnimator.MakeOrGetFrom(clock, frontObject);
      frontAnimator.Set(
        FadeAnimator.Fade(frontAnimator.Get(), clock.GetTimeMs(), durationMs),
        renderPriority);
      var frontOutlineAnimator = ColorAnimator.MakeOrGetFrom(clock, frontOutlineObject);
      frontOutlineAnimator.Set(
        FadeAnimator.Fade(frontOutlineAnimator.Get(), clock.GetTimeMs(), durationMs),
        renderPriority);
      var sidesAnimator = ColorAnimator.MakeOrGetFrom(clock, sidesObject);
      sidesAnimator.Set(
        FadeAnimator.Fade(sidesAnimator.Get(), clock.GetTimeMs(), durationMs),
        renderPriority);
    }
  }
}
