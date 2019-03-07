using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class NullILevelController : ILevelController {
  public static NullILevelController Null = new NullILevelController();

  public Root root { get { return null; } }
  public int id { get { return 0; } }
  public void Delete() {
    throw new Exception("Can't delete a null!");
  }
  public bool Exists() { return false; }
  public bool Is(ILevelController that) {
    throw new Exception("Called Is on a null!");
  }
  public void FindReachableObjects(SortedSet<int> foundIds) { }
  public bool NullableIs(ILevelController that) {
    return !that.Exists();
  }
  public ILevelController AsILevelController() {
    return this;
  }
       
  public string GetName() {
    throw new Exception("Called GetName on a null!");
  }
             
  public bool ConsiderCornersAdjacent() {
    throw new Exception("Called ConsiderCornersAdjacent on a null!");
  }
             
  public Location GetEntryLocation(Game game, LevelSuperstate levelSuperstate, Level fromLevel, int fromLevelPortalIndex) {
    throw new Exception("Called GetEntryLocation on a null!");
  }
             }
}
