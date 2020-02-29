using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geomancer {
  public class MemberToViewMapper {
    public interface IDescription { }
    public class TopColorDescriptionForIDescription : IDescription {
      public readonly UnityEngine.Color color;
      public TopColorDescriptionForIDescription(UnityEngine.Color color) { this.color = color; }
    }
    public class SideColorDescriptionForIDescription : IDescription {
      public readonly UnityEngine.Color color;
      public SideColorDescriptionForIDescription(UnityEngine.Color color) { this.color = color; }
    }
    public class OutlineColorDescriptionForIDescription : IDescription {
      public readonly UnityEngine.Color color;
      public OutlineColorDescriptionForIDescription(UnityEngine.Color color) { this.color = color; }
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
    public MemberToViewMapper(Dictionary<string, List<IDescription>> entries) {
      this.entries = entries;
    }

    public (Domino.TileDescription, Domino.UnitDescription) Vivify(
        Domino.TileDescription defaultTile,
        Domino.UnitDescription defaultUnit,
        List<String> members) {
      bool specifiedTopColor = false;
      UnityEngine.Color topColor = new UnityEngine.Color();
      bool specifiedOutlineColor = false;
      UnityEngine.Color outlineColor = new UnityEngine.Color();
      bool specifiedSideColor = false;
      UnityEngine.Color sideColor = new UnityEngine.Color();
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
              specifiedTopColor = true;
            }
            if (memberEntry is SideColorDescriptionForIDescription) {
              sideColor = (memberEntry as SideColorDescriptionForIDescription).color;
              specifiedSideColor = true;
            }
            if (memberEntry is OutlineColorDescriptionForIDescription) {
              outlineColor = (memberEntry as OutlineColorDescriptionForIDescription).color;
              specifiedOutlineColor = true;
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
        } else {
          items.Add(new Domino.ExtrudedSymbolDescription(Domino.RenderPriority.SYMBOL, new Domino.SymbolDescription("e", 100, new UnityEngine.Color(0, 1.0f, 1.0f), 0, Domino.OutlineMode.WithBackOutline, new UnityEngine.Color(0, 0, 0)), true, new UnityEngine.Color(0, 0, 0)));
        }
      }

      var outlineMode = specifiedOutlineColor ? Domino.OutlineMode.WithOutline : defaultTile.tileSymbolDescription.symbol.withOutline;

      var tileSymbolDescription =
          new Domino.ExtrudedSymbolDescription(
              defaultTile.tileSymbolDescription.renderPriority,
              new Domino.SymbolDescription(
                  defaultTile.tileSymbolDescription.symbol.symbolId,
                  defaultTile.tileSymbolDescription.symbol.qualityPercent,
                  specifiedTopColor ? topColor : defaultTile.tileSymbolDescription.symbol.frontColor,
                  defaultTile.tileRotationDegrees,
                  outlineMode,
                  specifiedOutlineColor ? outlineColor : defaultTile.tileSymbolDescription.symbol.outlineColor),
              defaultTile.tileSymbolDescription.extruded,
              specifiedSideColor ? sideColor : defaultTile.tileSymbolDescription.sidesColor);

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
