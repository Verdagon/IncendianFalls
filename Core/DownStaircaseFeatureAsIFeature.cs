using System;
using System.Collections.Generic;

namespace Atharia.Model {

public class DownStaircaseFeatureAsIFeature : IFeature {
  public readonly DownStaircaseFeature obj;
public int id => obj.id;
public Root root => obj.root;
public void Delete() { obj.Delete(); }
public bool NullableIs(IFeature that) {
  if (!this.Exists() && !that.Exists()) {
    return true;
  }
  if (!this.Exists() || !that.Exists()) {
    return false;
  }
  return this.Is(that);
}
public bool Is(IFeature that) {
  if (!this.Exists()) {
    throw new Exception("Called Is on a null!");
  }
  if (!that.Exists()) {
    throw new Exception("Called Is on a null!");
  }
  return root == that.root && obj.id == that.id;
}
public bool Exists() { return obj.Exists(); }
public DownStaircaseFeatureAsIFeature(DownStaircaseFeature obj) {
  this.obj = obj;
}
     
}
public static class DownStaircaseFeatureAsIFeatureCaster {
  public static DownStaircaseFeatureAsIFeature AsIFeature(this DownStaircaseFeature obj) {
    return new DownStaircaseFeatureAsIFeature(obj);
  }
}

}
