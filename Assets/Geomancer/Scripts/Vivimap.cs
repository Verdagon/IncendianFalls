using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geomancer {
  public class Vivimap {
    public interface IDescription { }
    public class TopColorDescriptionForIDescription : IDescription {
      public readonly UnityEngine.Color color;
      public TopColorDescriptionForIDescription(UnityEngine.Color color) { this.color = color; }
    }
    public class SideColorDescriptionForIDescription : IDescription {
      public readonly UnityEngine.Color color;
      public SideColorDescriptionForIDescription(UnityEngine.Color color) { this.color = color; }
    }
    public class OverlayDescriptionForIDescription : IDescription {
      public readonly Domino.ExtrudedSymbolDescription symbol;
      public OverlayDescriptionForIDescription(Domino.ExtrudedSymbolDescription symbol) { this.symbol = symbol; }
    }
    public class FeatureDescriptionForIDescription : IDescription {
      public readonly Domino.ExtrudedSymbolDescription symbol;
      public FeatureDescriptionForIDescription(Domino.ExtrudedSymbolDescription symbol) { this.symbol = symbol; }
    }
    public class FaceDescriptionForIDescription : IDescription {
      public readonly Domino.ExtrudedSymbolDescription symbol;
      public FaceDescriptionForIDescription(Domino.ExtrudedSymbolDescription symbol) { this.symbol = symbol; }
    }
    public class DominoDescriptionForIDescription : IDescription {
      public readonly Domino.DominoDescription domino;
      public DominoDescriptionForIDescription(Domino.DominoDescription domino) { this.domino = domino; }
    }
    public class ItemDescriptionForIDescription : IDescription {
      public readonly Domino.ExtrudedSymbolDescription symbol;
      public ItemDescriptionForIDescription(Domino.ExtrudedSymbolDescription symbol) { this.symbol = symbol; }
    }
    public class DetailDescriptionForIDescription : IDescription {
      public readonly Domino.ExtrudedSymbolDescription symbol;
      public DetailDescriptionForIDescription(Domino.ExtrudedSymbolDescription symbol) { this.symbol = symbol; }
    }

    Dictionary<string, List<IDescription>> entries;
    public Vivimap(Dictionary<string, List<IDescription>> entries) {
      this.entries = entries;
    }

    public (Domino.TileDescription, Domino.UnitDescription) Vivify(
        Domino.TileDescription defaultTile,
        Domino.UnitDescription defaultUnit,
        List<String> members) {
      UnityEngine.Color topColor = defaultTile.tileSymbolDescription.symbol.frontColor;
      UnityEngine.Color sideColor = defaultTile.tileSymbolDescription.sidesColor;
      Domino.ExtrudedSymbolDescription maybeOverlay = null;
      Domino.ExtrudedSymbolDescription maybeFeature = null;
      List<Domino.ExtrudedSymbolDescription> items = new List<Domino.ExtrudedSymbolDescription>();
      Domino.ExtrudedSymbolDescription maybeUnitFace = null;
      Domino.DominoDescription maybeUnitDomino = null;
      List<Domino.ExtrudedSymbolDescription> unitDetails = new List<Domino.ExtrudedSymbolDescription>();

      foreach (var member in members) {
        if (entries.TryGetValue(member, out var memberEntries)) {
          foreach (var memberEntry in memberEntries) {
            if (memberEntry is TopColorDescriptionForIDescription) {
              topColor = (memberEntry as TopColorDescriptionForIDescription).color;
            }
            if (memberEntry is SideColorDescriptionForIDescription) {
              sideColor = (memberEntry as SideColorDescriptionForIDescription).color;
            }
            if (memberEntry is OverlayDescriptionForIDescription) {
              maybeOverlay = (memberEntry as OverlayDescriptionForIDescription).symbol;
            }
            if (memberEntry is FeatureDescriptionForIDescription) {
              maybeFeature = (memberEntry as FeatureDescriptionForIDescription).symbol;
            }
            if (memberEntry is ItemDescriptionForIDescription) {
              items.Add((memberEntry as ItemDescriptionForIDescription).symbol);
            }
            if (memberEntry is FaceDescriptionForIDescription) {
              maybeUnitFace = (memberEntry as FaceDescriptionForIDescription).symbol;
            }
            if (memberEntry is DominoDescriptionForIDescription) {
              maybeUnitDomino = (memberEntry as DominoDescriptionForIDescription).domino;
            }
            if (memberEntry is DetailDescriptionForIDescription) {
              unitDetails.Add((memberEntry as DetailDescriptionForIDescription).symbol);
            }
          }
        }
      }

      var tileSymbolDescription =
          new Domino.ExtrudedSymbolDescription(
              defaultTile.tileSymbolDescription.renderPriority,
              new Domino.SymbolDescription(
                  defaultTile.tileSymbolDescription.symbol.symbolId,
                  topColor,
                  defaultTile.tileRotationDegrees,
                  defaultTile.tileSymbolDescription.symbol.withOutline,
                  defaultTile.tileSymbolDescription.symbol.outlineColor),
              defaultTile.tileSymbolDescription.extruded,
              sideColor);

      var itemSymbolDescriptionByItemId = new SortedDictionary<int, Domino.ExtrudedSymbolDescription>();
      for (int i = 0; i < items.Count; i++) {
        itemSymbolDescriptionByItemId.Add(i, items[i]);
      }

      var tileDescription =
          new Domino.TileDescription(
              defaultTile.elevationStepHeight,
              defaultTile.tileRotationDegrees,
              defaultTile.depth,
              tileSymbolDescription,
              maybeOverlay,
              maybeFeature,
              itemSymbolDescriptionByItemId);

      Domino.UnitDescription maybeUnitDescription = null;
      if (maybeUnitFace != null || maybeUnitDomino != null || unitDetails.Count > 0) {
        var details = new List<KeyValuePair<int, Domino.ExtrudedSymbolDescription>>();
        for (int i = 0; i < items.Count; i++) {
          details.Add(new KeyValuePair<int, Domino.ExtrudedSymbolDescription>(i, items[i]));
        }

        maybeUnitDescription =
          new Domino.UnitDescription(
              null,
              maybeUnitDomino ?? defaultUnit.dominoDescription,
              maybeUnitFace ?? defaultUnit.faceSymbolDescription,
              details,
              1,
              1);
      }

      return (tileDescription, maybeUnitDescription);
    }
  }
}
