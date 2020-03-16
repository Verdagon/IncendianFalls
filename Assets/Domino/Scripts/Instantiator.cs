﻿using System;
using System.Collections.Generic;
using AthPlayer;
using Atharia;
using Atharia.Model;
using UnityEngine;

namespace Domino {
  public class Instantiator : MonoBehaviour {
    public GameObject tilePrefab;

    // Doesnt have a domino or a glyph in it, you gotta bring your own.
    public GameObject unitPrefab;

    public GameObject dominoPrefab;

    public GameObject squarePrefab;

    public GameObject meterPrefab;

    public GameObject largeDominoPrefab;
    public GameObject smallDominoPrefab;

    public GameObject glyphPrefab;

    public GameObject symbolBarPrefab;

    public GameObject detailPrefab;

    public GameObject emptyUiObject;

    public Font symbolsFont;
    public Font cascadiaFont;
    public Font proseFont;

    private Dictionary<string, Dictionary<int, SymbolMeshes>> meshes;

    public class SymbolMeshes {
      public readonly Mesh front;
      public readonly Mesh frontOutline;
      public readonly Mesh sides;
      public readonly Mesh sidesOutline;

      public SymbolMeshes(
          Mesh front,
          Mesh frontOutline,
          Mesh sides,
          Mesh sidesOutline) {
        this.front = front;
        this.frontOutline = frontOutline;
        this.sides = sides;
        this.sidesOutline = sidesOutline;
      }
    }

    public GameObject CreateEmptyUiObject() {
      return Instantiate(emptyUiObject);
    }

    public Font GetFont(string font) {
      if (font == "symbols") {
        return symbolsFont;
      } else if (font == "cascadia") {
        return cascadiaFont;
      } else if (font == "prose") {
        return proseFont;
      } else {
        return symbolsFont;
      }
    }

    public GameObject CreateLargeDomino() {
      return Instantiate(largeDominoPrefab);
    }

    public GameObject CreateSmallDomino() {
      return Instantiate(smallDominoPrefab);
    }

    public SymbolMeshes GetSymbolMeshes(string symbolId, int qualityPercent) {
      if (meshes == null) {
        meshes = new Dictionary<string, Dictionary<int, SymbolMeshes>>();
      }
      if (meshes.ContainsKey(symbolId)) {
        if (meshes[symbolId].ContainsKey(qualityPercent)) {
          return meshes[symbolId][qualityPercent];
        }
      }
      
      var frontResourceName = symbolId + ".front";
      var frontGameObject = Resources.Load<GameObject>(frontResourceName);
      if (frontGameObject == null) {
        throw new Exception("Couldn't find " + frontResourceName);
      }
      var frontMesh = frontGameObject.GetComponentInChildren<MeshFilter>().sharedMesh;


      var frontOutlineResourceName = symbolId + ".outline.front";
      var frontOutlineGameObject = Resources.Load<GameObject>(frontOutlineResourceName);
      if (frontOutlineGameObject == null) {
        throw new Exception("Couldn't find " + frontOutlineResourceName);
      }
      var frontOutlineMesh = frontOutlineGameObject.GetComponentInChildren<MeshFilter>().sharedMesh;

      var sidesResourceName = symbolId + ".sides";
      var sidesGameObject = Resources.Load<GameObject>(sidesResourceName);
      if (sidesGameObject == null) {
        throw new Exception("Couldn't find " + sidesResourceName);
      }
      var sidesMesh = sidesGameObject.GetComponentInChildren<MeshFilter>().sharedMesh;

      var newSymbolMeshes = new SymbolMeshes(frontMesh, frontOutlineMesh, sidesMesh, null);
      if (!meshes.ContainsKey(symbolId)) {
        meshes.Add(symbolId, new Dictionary<int, SymbolMeshes>());
      }
      meshes[symbolId].Add(qualityPercent, newSymbolMeshes);
      return newSymbolMeshes;
    }

    public UnitView CreateUnitView(
      IClock clock,
        ITimer timer,
        Vector3 basePosition,
        UnitDescription description,
        Vector3 cameraAngle) {
      var unitGameObject = Instantiate(unitPrefab);
      var unitView = unitGameObject.GetComponent<UnitView>();
      unitView.Init(
        clock,
          this,
          timer,
          basePosition,
          description,
          cameraAngle);
      return unitView;
    }

    public TileView CreateTileView(
      IClock clock,
      ITimer timer,
        Vector3 basePosition,
        TileDescription description) {
      var tileGameObject = Instantiate(tilePrefab);
      var tileView = tileGameObject.GetComponent<TileView>();
      tileView.Init(
        clock,
        timer,
          this,
          basePosition,
          description);
      return tileView;
    }

    public MeterView CreateMeterView(IClock clock, float ratio, UnityEngine.Color filledColor, UnityEngine.Color emptyColor) {
      var meterGameObject = Instantiate(meterPrefab);
      var meterView = meterGameObject.GetComponent<MeterView>();
      meterView.Init(clock, this, filledColor, emptyColor, ratio);
      return meterView;
    }

    public SymbolView CreateSymbolView(
      IClock clock,
        bool mousable,
        ExtrudedSymbolDescription symbolDescription) {
      var symbolGameObject = Instantiate(glyphPrefab);
      var symbolView = symbolGameObject.GetComponent<SymbolView>();
      symbolView.Init(clock, this, mousable, symbolDescription);
      return symbolView;
    }

    public SymbolBarView CreateSymbolBarView(
      IClock clock,
        List<KeyValuePair<int, ExtrudedSymbolDescription>> symbolsIdsAndDescriptions) {
      var symbolBarGameObject = Instantiate(symbolBarPrefab);
      var symbolBarView = symbolBarGameObject.GetComponent<SymbolBarView>();
      symbolBarView.Init(clock, this, symbolsIdsAndDescriptions);
      return symbolBarView;
    }

    public DominoView CreateDominoView(
      IClock clock,
        DominoDescription description) {
      var dominoGameObject = Instantiate(dominoPrefab);
      var dominoView = dominoGameObject.GetComponent<DominoView>();
      dominoView.Init(clock, this, description);
      return dominoView;
    }

    public GameObject CreateSquare() {
      return Instantiate(squarePrefab);
    }

    public NewOverlayPanelView CreateOverlayPanelView(
        GameObject parent,
        IClock cinematicTimer,
        int horizontalAlignmentPercent,
        int verticalAlignmentPercent,
        int widthPercent,
        int heightPercent,
        int symbolsWide,
        int symbolsHigh) {
      var obj = Instantiate(emptyUiObject);
      var spv = obj.AddComponent<NewOverlayPanelView>();
      spv.Init(
        this,
        cinematicTimer,
        parent,
        horizontalAlignmentPercent,
        verticalAlignmentPercent,
        widthPercent,
        heightPercent,
        symbolsWide,
        symbolsHigh);
        //500, -500, -0, 500, 1000, -1000, -500);
      return spv;
    }

    //private Mesh Simplify(Mesh sourceMesh, int qualityPercent) {
    //  var meshSimplifier = new UnityMeshSimplifier.MeshSimplifier();
    //  //meshSimplifier.PreserveBorderEdges = true;
    //  //meshSimplifier.PreserveUVFoldoverEdges = true;
    //  //meshSimplifier.PreserveUVSeamEdges = true;
    //  meshSimplifier.Initialize(sourceMesh);
    //  meshSimplifier.SimplifyMesh(qualityPercent / 100.0f);
    //  var destMesh = meshSimplifier.ToMesh();
    //  return destMesh;
    //}
  }
}
