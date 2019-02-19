using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class UpStaircaseFeature {
  public readonly Root root;
  public readonly int id;
  public UpStaircaseFeature(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public UpStaircaseFeatureIncarnation incarnation { get { return root.GetUpStaircaseFeatureIncarnation(id); } }
  public void AddObserver(IUpStaircaseFeatureEffectObserver observer) {
    root.AddUpStaircaseFeatureObserver(id, observer);
  }
  public void RemoveObserver(IUpStaircaseFeatureEffectObserver observer) {
    root.RemoveUpStaircaseFeatureObserver(id, observer);
  }
  public void Delete() {
    root.EffectUpStaircaseFeatureDelete(id);
  }
  public static UpStaircaseFeature Null = new UpStaircaseFeature(null, 0);
  public bool Exists() { return root != null && root.UpStaircaseFeatureExists(id); }
  public bool NullableIs(UpStaircaseFeature that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
  if (!this.Exists() || !that.Exists()) {
    return false;
  }
    return this.Is(that);
  }
  public bool Is(UpStaircaseFeature that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
       }
}
