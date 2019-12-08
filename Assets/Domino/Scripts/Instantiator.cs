using System;
using System.Collections.Generic;
using IncendianFalls;
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

    private Dictionary<string, SymbolMeshes> meshes;

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

    public GameObject CreateLargeDomino() {
      return Instantiate(largeDominoPrefab);
    }

    public GameObject CreateSmallDomino() {
      return Instantiate(smallDominoPrefab);
    }

    public SymbolMeshes GetSymbolMeshes(string symbolId) {
      if (meshes == null) {
        meshes = new Dictionary<string, SymbolMeshes>();
      }
      if (meshes.ContainsKey(symbolId)) {
        return meshes[symbolId];
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
      meshes.Add(symbolId, newSymbolMeshes);
      return newSymbolMeshes;
    }

    public UnitView CreateUnitView(
        ITimer timer,
        Vector3 basePosition,
        UnitDescription description) {
      var unitGameObject = Instantiate(unitPrefab);
      var unitView = unitGameObject.GetComponent<UnitView>();
      unitView.Init(
          this,
          timer,
          basePosition,
          description);
      return unitView;
    }

    public TileView CreateTileView(
        Vector3 basePosition,
        TileDescription description) {
      var tileGameObject = Instantiate(tilePrefab);
      var tileView = tileGameObject.GetComponent<TileView>();
      tileView.Init(
          this,
          basePosition,
          description);
      return tileView;
    }

    public MeterView CreateMeterView(float ratio, Color filledColor, Color emptyColor) {
      var meterGameObject = Instantiate(meterPrefab);
      var meterView = meterGameObject.GetComponent<MeterView>();
      meterView.Init(this, filledColor, emptyColor, ratio);
      return meterView;
    }

    public SymbolView CreateSymbolView(
        bool mousable,
        ExtrudedSymbolDescription symbolDescription) {
      var symbolGameObject = Instantiate(glyphPrefab);
      var symbolView = symbolGameObject.GetComponent<SymbolView>();
      symbolView.Init(this, mousable, symbolDescription);
      return symbolView;
    }

    public SymbolBarView CreateSymbolBarView(
        List<KeyValuePair<int, ExtrudedSymbolDescription>> symbolsIdsAndDescriptions) {
      var symbolBarGameObject = Instantiate(symbolBarPrefab);
      var symbolBarView = symbolBarGameObject.GetComponent<SymbolBarView>();
      symbolBarView.Init(this, symbolsIdsAndDescriptions);
      return symbolBarView;
    }

    public DominoView CreateDominoView(
        DominoDescription description) {
      var dominoGameObject = Instantiate(dominoPrefab);
      var dominoView = dominoGameObject.GetComponent<DominoView>();
      dominoView.Init(this, description);
      return dominoView;
    }

    public GameObject CreateSquare() {
      return Instantiate(squarePrefab);
    }
  }
}
