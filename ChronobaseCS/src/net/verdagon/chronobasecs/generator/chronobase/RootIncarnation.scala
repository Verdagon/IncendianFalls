package net.verdagon.chronobasecs.generator.chronobase

import net.verdagon.chronobasecs.compiled.{MutableS, SuperstructureS}

object RootIncarnation {

  def generateRootIncarnation(opt: ChronobaseOptions, ss: SuperstructureS): String = {

    val types: List[String] =
      ss.structs.filter(_.mutability == MutableS).map(_.name) ++
      ss.lists.filter(_.mutability == MutableS).map(_.name) ++
      ss.sets.filter(_.mutability == MutableS).map(_.name) ++
      ss.maps.filter(_.mutability == MutableS).map(_.name)

    s"public class RootIncarnation {\n" +
      s"  public readonly int version;\n" +
      s"  public int nextId;\n" +
      s"  public int hash;\n" +
      types
        .map(tyype => s"  public readonly SortedDictionary<int, VersionAndIncarnation<${tyype}Incarnation>> incarnations${tyype};\n")
        .mkString("") +
      s"  public RootIncarnation(int version, int nextId, int hash) {\n" +
      s"    this.version = version;\n" +
      s"    this.nextId = nextId;\n" +
      s"    this.hash = hash;\n" +
      types
        .map(tyype => s"    this.incarnations${tyype} = new SortedDictionary<int, VersionAndIncarnation<${tyype}Incarnation>>();\n")
        .mkString("") +
      s"  }\n" +
      s"  public RootIncarnation(\n" +
      s"      int newVersion,\n" +
      s"      int newNextId,\n" +
      s"      int newHash,\n" +
      s"      RootIncarnation that) {\n" +
      s"    this.version = newVersion;\n" +
      s"    this.nextId = newNextId;\n" +
      s"    this.hash = newHash;\n" +
      types
        .map(tyype => s"    this.incarnations${tyype} = new SortedDictionary<int, VersionAndIncarnation<${tyype}Incarnation>>(that.incarnations${tyype});\n")
        .mkString("") +
      s"  }\n" +
      s"}\n"
  }

}
