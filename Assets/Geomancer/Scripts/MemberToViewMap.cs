using Domino;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Geomancer {
  class MemberToViewMap {
    public static MemberToViewMapper MakeVivimap() {
      var entries = new Dictionary<string, List<MemberToViewMapper.IDescription>>();
      entries.Add("Grass", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.TopColorDescriptionForIDescription(new Color(0, .3f, 0)),
        new MemberToViewMapper.SideColorDescriptionForIDescription(new Color(0, .2f, 0)),
        new MemberToViewMapper.OutlineColorDescriptionForIDescription(new Color(0, 0, 0))
      });
      entries.Add("Rocks", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.OverlayDescriptionForIDescription(
          new ExtrudedSymbolDescription(
            RenderPriority.TILE,
            new SymbolDescription("f",
                            50,new Color(.5f, .5f, .5f, .2f), 0, OutlineMode.WithOutline, new Color(0, 0, 0)),
            false,
            new Color(0, 0, 0)))
      });
      entries.Add("Obsidian", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.OverlayDescriptionForIDescription(
          new ExtrudedSymbolDescription(
            RenderPriority.TILE,
            new SymbolDescription("f",
                            50, new Color(0f, 0f, 0f, .8f), 0, OutlineMode.WithOutline, new Color(0, 0, 0)),
            false,
            new Color(0, 0, 0)))
      });
      entries.Add("HealthPotion", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.OverlayDescriptionForIDescription(
                new ExtrudedSymbolDescription(
                    RenderPriority.SYMBOL,
                    new SymbolDescription(
                        "plus",
                              50,
                        new UnityEngine.Color(.8f, 0, .8f, 1.5f),
                        0,
                        OutlineMode.WithBackOutline,
                        new UnityEngine.Color(0, 0, 0)),
                    true,
                    new UnityEngine.Color(0f, 0f, 0)))
      });
      entries.Add("ManaPotion", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.OverlayDescriptionForIDescription(
          new ExtrudedSymbolDescription(
              RenderPriority.SYMBOL,
              new SymbolDescription(
                  "comma",
                        50,
                  new UnityEngine.Color(.25f, .7f, 1.0f, 1.5f),
                  0,
                  OutlineMode.WithBackOutline,
                  new UnityEngine.Color(0, 0, 0)),
              true,
              new UnityEngine.Color(0f, 0f, 0)))
      });
      entries.Add("Cave", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.FeatureDescriptionForIDescription(
                  new ExtrudedSymbolDescription(
                      RenderPriority.SYMBOL,
                      new SymbolDescription(
                          "p",
                            50,
                          new Color(0, 0, 0),
                          0,
                          OutlineMode.WithOutline,
                          new Color(1, 1, 1)),
                      false,
                      new Color(1f, 1f, 1f)))
      });
      entries.Add("Tree", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.FeatureDescriptionForIDescription(
                  new ExtrudedSymbolDescription(
                      RenderPriority.SYMBOL,
                      new SymbolDescription(
                          "n",
                            50,
                          new Color(0, .5f, 0),
                          0,
                          OutlineMode.WithOutline,
                          new Color(0, 0, 0)),
                      false,
                      new Color(0f, .3f, 0f)))
      });
      entries.Add("Fire", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.FeatureDescriptionForIDescription(
                  new ExtrudedSymbolDescription(
                      RenderPriority.SYMBOL,
                      new SymbolDescription(
                          "r",
                            50,
                          new Color(.8f, .4f, 0, .5f),
                          0,
                          OutlineMode.WithOutline,
                          new Color(0, 0, 0)),
                      false,
                      new Color(0f, .3f, 0f)))
      });
      entries.Add("Dirt", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.TopColorDescriptionForIDescription(new Color(.4f, .133f, 0)),
        new MemberToViewMapper.SideColorDescriptionForIDescription(new Color(.266f, .1f, 0)),
        new MemberToViewMapper.OutlineColorDescriptionForIDescription(new Color(0, 0, 0))
      });
      entries.Add("Mud", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.TopColorDescriptionForIDescription(new Color(.35f, .11f, 0)),
        new MemberToViewMapper.SideColorDescriptionForIDescription(new Color(.23f, .08f, 0)),
        new MemberToViewMapper.OutlineColorDescriptionForIDescription(new Color(0, 0, 0))
      });
      entries.Add("ObsidianFloor", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.TopColorDescriptionForIDescription(new Color(.1f, .1f, .05f)),
        new MemberToViewMapper.SideColorDescriptionForIDescription(new Color(.05f, .05f, .05f)),
        new MemberToViewMapper.OutlineColorDescriptionForIDescription(new Color(0, 0, 0))
      });
      entries.Add("Floor", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.TopColorDescriptionForIDescription(new Color(.2f, .2f, .2f)),
        new MemberToViewMapper.SideColorDescriptionForIDescription(new Color(.15f, .15f, .15f)),
        new MemberToViewMapper.OutlineColorDescriptionForIDescription(new Color(0, 0, 0))
      });
      entries.Add("Wall", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.TopColorDescriptionForIDescription(new Color(.3f, .3f, .3f)),
        new MemberToViewMapper.SideColorDescriptionForIDescription(new Color(.2f, .2f, .2f)),
        new MemberToViewMapper.OutlineColorDescriptionForIDescription(new Color(0, 0, 0))
      });
      entries.Add("CaveWall", new List<MemberToViewMapper.IDescription>() {
      new MemberToViewMapper.TopColorDescriptionForIDescription(new Color(.24f, .08f, 0)),
        new MemberToViewMapper.SideColorDescriptionForIDescription(new Color(.16f, .05f, 0)),
        new MemberToViewMapper.OutlineColorDescriptionForIDescription(new Color(0, 0, 0))
      });
      entries.Add("Water", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.TopColorDescriptionForIDescription(new Color(0f, .4f, .8f)),
        new MemberToViewMapper.SideColorDescriptionForIDescription(new Color(0f, .45f, .85f)),
        new MemberToViewMapper.OutlineColorDescriptionForIDescription(new Color(0, 0, 0))
      });
      entries.Add("Magma", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.TopColorDescriptionForIDescription(new Color(.4f, 0f, 0f)),
        new MemberToViewMapper.SideColorDescriptionForIDescription(new Color(.2f, 0f, 0f)),
        new MemberToViewMapper.OutlineColorDescriptionForIDescription(new Color(0, 0, 0)),
        new MemberToViewMapper.OverlayDescriptionForIDescription(
          new ExtrudedSymbolDescription(
              RenderPriority.SYMBOL,
              new SymbolDescription(
                  "f",
                  50,
                  new UnityEngine.Color(.5f, .0f, 0f),
                  0,
                  OutlineMode.NoOutline,
                  new UnityEngine.Color(0, 0, 0)),
              false,
              new UnityEngine.Color(0, 0, 0)))
    });
      entries.Add("Trigger", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.ItemDescriptionForIDescription(
          new ExtrudedSymbolDescription(
            RenderPriority.TILE,
            new SymbolDescription("n",
                            50, new Color(1f, 1f, 1f, 0.2f), 180, OutlineMode.NoOutline, new Color(1, 1, 1)),
            true,
            new Color(.6f, .6f, .6f, 0.2f)))
      });
      entries.Add("Marker", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.ItemDescriptionForIDescription(
          new ExtrudedSymbolDescription(
            RenderPriority.TILE,
            new SymbolDescription(
                "e", 50, new Color(1f, .75f, .5f, 1.2f), 0, OutlineMode.WithOutline, new Color(0, 0, 0)),
            false,
            new Color(0, 0, 0)))
      });
      return new MemberToViewMapper(entries);
    }
  }
}
