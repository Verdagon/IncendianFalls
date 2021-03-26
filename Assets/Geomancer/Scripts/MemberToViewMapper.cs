using Domino;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geomancer {
  public class MemberToViewMapper {
    public interface IDescriptionVisitor {
      void visitTileTopColor(TopColorDescriptionForIDescription color);
      void visitTileSideColor(SideColorDescriptionForIDescription color);
      void visitTileOverlay(OverlayDescriptionForIDescription symbol);
      void visitTileFeature(FeatureDescriptionForIDescription symbol);
      void visitUnitDomino(DominoDescriptionForIDescription domino);
      void visitUnitFace(FaceDescriptionForIDescription symbol);
      void visitUnitDetail(DetailDescriptionForIDescription symbol);
      void visitTileItem(ItemDescriptionForIDescription symbol);
    }

    public interface IDescription {
      void visit(IDescriptionVisitor visitor);
    }
    public class TopColorDescriptionForIDescription : IDescription {
      public readonly Vector4Animation color;
      public TopColorDescriptionForIDescription(Vector4Animation color) { this.color = color; }
      public void visit(IDescriptionVisitor visitor) { visitor.visitTileTopColor(this); }
    }
    public class SideColorDescriptionForIDescription : IDescription {
      public readonly Vector4Animation color;
      public SideColorDescriptionForIDescription(Vector4Animation color) { this.color = color; }
      public void visit(IDescriptionVisitor visitor) { visitor.visitTileSideColor(this); }
    }
    // public class OutlineColorDescriptionForIDescription : IDescription {
    //   public readonly Vector4Animation color;
    //   public OutlineColorDescriptionForIDescription(Vector4Animation color) { this.color = color; }
    // }
    public class OverlayDescriptionForIDescription : IDescription {
      public readonly Domino.ExtrudedSymbolDescription symbol;
      public OverlayDescriptionForIDescription(Domino.ExtrudedSymbolDescription symbol) { this.symbol = symbol; }
      public void visit(IDescriptionVisitor visitor) { visitor.visitTileOverlay(this); }
    }
    public class FeatureDescriptionForIDescription : IDescription {
      public readonly Domino.ExtrudedSymbolDescription symbol;
      public FeatureDescriptionForIDescription(Domino.ExtrudedSymbolDescription symbol) { this.symbol = symbol; }
      public void visit(IDescriptionVisitor visitor) { visitor.visitTileFeature(this); }
    }
    public class FaceDescriptionForIDescription : IDescription {
      public readonly Domino.ExtrudedSymbolDescription symbol;
      public FaceDescriptionForIDescription(Domino.ExtrudedSymbolDescription symbol) { this.symbol = symbol; }
      public void visit(IDescriptionVisitor visitor) { visitor.visitUnitFace(this); }
    }
    public class DominoDescriptionForIDescription : IDescription {
      public readonly Domino.DominoDescription domino;
      public DominoDescriptionForIDescription(Domino.DominoDescription domino) { this.domino = domino; }
      public void visit(IDescriptionVisitor visitor) { visitor.visitUnitDomino(this); }
    }
    public class ItemDescriptionForIDescription : IDescription {
      public readonly Domino.ExtrudedSymbolDescription symbol;
      public ItemDescriptionForIDescription(Domino.ExtrudedSymbolDescription symbol) { this.symbol = symbol; }
      public void visit(IDescriptionVisitor visitor) { visitor.visitTileItem(this); }
    }
    public class DetailDescriptionForIDescription : IDescription {
      public readonly Domino.ExtrudedSymbolDescription symbol;
      public DetailDescriptionForIDescription(Domino.ExtrudedSymbolDescription symbol) { this.symbol = symbol; }
      public void visit(IDescriptionVisitor visitor) { visitor.visitUnitDetail(this); }
    }

    Dictionary<string, List<IDescription>> entries;
    public MemberToViewMapper(Dictionary<string, List<IDescription>> entries) {
      this.entries = entries;
    }

    public List<IDescription> getEntries(string name) {
      return entries[name];
    }

    public (Domino.TileDescription, Domino.UnitDescription) Vivify(
        Domino.TileDescription defaultTile,
        Domino.UnitDescription defaultUnit,
        List<String> members) {
      bool specifiedTopColor = false;
      Vector4Animation topColor = Vector4Animation.BLACK;
      bool specifiedOutlineColor = false;
      Vector4Animation outlineColor = Vector4Animation.BLACK;
      bool specifiedSideColor = false;
      Vector4Animation sideColor = Vector4Animation.BLACK;
      Domino.ExtrudedSymbolDescription maybeOverlay = null;
      Domino.ExtrudedSymbolDescription maybeFeature = null;
      List<Domino.ExtrudedSymbolDescription> items = new List<Domino.ExtrudedSymbolDescription>();
      Domino.ExtrudedSymbolDescription maybeUnitFace = null;
      Domino.DominoDescription maybeUnitDomino = null;
      List<Domino.ExtrudedSymbolDescription> unitDetails = new List<Domino.ExtrudedSymbolDescription>();

      foreach (var memberString in members) {
        var memberClass =
          memberString.Contains("(") ?
          memberString.Substring(0, memberString.IndexOf("(")) :
          memberString;
        if (entries.TryGetValue(memberClass, out var memberEntries)) {
          foreach (var memberEntry in memberEntries) {
            if (memberEntry is TopColorDescriptionForIDescription) {
              topColor = (memberEntry as TopColorDescriptionForIDescription).color;
              specifiedTopColor = true;
            }
            if (memberEntry is SideColorDescriptionForIDescription) {
              sideColor = (memberEntry as SideColorDescriptionForIDescription).color;
              specifiedSideColor = true;
            }
            // if (memberEntry is OutlineColorDescriptionForIDescription) {
            //   outlineColor = (memberEntry as OutlineColorDescriptionForIDescription).color;
            //   specifiedOutlineColor = true;
            // }
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
          items.Add(new Domino.ExtrudedSymbolDescription(
            Domino.RenderPriority.SYMBOL,
            new Domino.SymbolDescription(
              "e",
              Vector4Animation.Color(0, 1.0f, 1.0f),
              0,
              1,
              Domino.OutlineMode.WithBackOutline,
              Vector4Animation.Color(0, 1.0f, 0.5f)),
            true,
            Vector4Animation.Color(0.5f, 1.0f, 1.0f)));
        }
      }

      var outlineMode = specifiedOutlineColor ? Domino.OutlineMode.WithOutline : defaultTile.tileSymbolDescription.symbol.withOutline;

      var tileSymbolDescription =
          new Domino.ExtrudedSymbolDescription(
              defaultTile.tileSymbolDescription.renderPriority,
              new Domino.SymbolDescription(
                  defaultTile.tileSymbolDescription.symbol.symbolId,
                  specifiedTopColor ? topColor : defaultTile.tileSymbolDescription.symbol.frontColor,
                  defaultTile.tileRotationDegrees,
                  1,
                  outlineMode,
                  specifiedOutlineColor ? outlineColor : defaultTile.tileSymbolDescription.symbol.outlineColor),
              defaultTile.tileSymbolDescription.extruded,
              specifiedSideColor ? sideColor : defaultTile.tileSymbolDescription.sidesColor);

      var itemSymbolDescriptionByItemId = new List<(ulong, Domino.ExtrudedSymbolDescription)>();
      for (int i = 0; i < items.Count; i++) {
        itemSymbolDescriptionByItemId.Add(((ulong)i, items[i]));
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
        var details = new List<(ulong, ExtrudedSymbolDescription)>();
        for (int i = 0; i < items.Count; i++) {
          details.Add(((ulong)i, items[i]));
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
