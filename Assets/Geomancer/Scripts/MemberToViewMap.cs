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
      entries.Add("grass", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.SideColorDescriptionForIDescription(new Color(0, .3f, 0)),
        new MemberToViewMapper.TopColorDescriptionForIDescription(new Color(0, .5f, 0)),
        new MemberToViewMapper.OutlineColorDescriptionForIDescription(new Color(0, 0, 0))
      });
      entries.Add("rocks", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.OverlayDescriptionForIDescription(
          new ExtrudedSymbolDescription(
            RenderPriority.TILE,
            new SymbolDescription("f",
                            50,new Color(.5f, .5f, .5f), 0, OutlineMode.WithOutline, new Color(0, 0, 0)),
            false,
            new Color(0, 0, 0)))
      });
      entries.Add("cave", new List<MemberToViewMapper.IDescription>() {
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
      entries.Add("dirt", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.TopColorDescriptionForIDescription(new Color(.6f, .3f, 0)),
        new MemberToViewMapper.SideColorDescriptionForIDescription(new Color(.4f, .2f, 0)),
        new MemberToViewMapper.OutlineColorDescriptionForIDescription(new Color(0, 0, 0))
      });
      entries.Add("mud", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.TopColorDescriptionForIDescription(new Color(.4f, .2f, 0)),
        new MemberToViewMapper.SideColorDescriptionForIDescription(new Color(.27f, .13f, 0)),
        new MemberToViewMapper.OutlineColorDescriptionForIDescription(new Color(0, 0, 0))
      });
      entries.Add("marker", new List<MemberToViewMapper.IDescription>() {
        new MemberToViewMapper.ItemDescriptionForIDescription(
          new ExtrudedSymbolDescription(
            RenderPriority.TILE,
            new SymbolDescription("a",
                            50, new Color(1f, 1f, 1f, 0), 180, OutlineMode.WithBackOutline, new Color(1, 1, 1)),
            false,
            new Color(0, 0, 0)))
      });
      return new MemberToViewMapper(entries);
    }
  }
}
