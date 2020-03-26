package net.verdagon.chronobasecs.generator.chronobase

import net.verdagon.chronobasecs.compiled.{MutableS, SuperstructureS}
import net.verdagon.chronobasecs.generator.chronobase.list.ListGenerator
import net.verdagon.chronobasecs.generator.chronobase.map.MapGenerator
import net.verdagon.chronobasecs.generator.chronobase.set.SetGenerator
import net.verdagon.chronobasecs.generator.chronobase.struct.StructGenerator

object EffectApplier {

  def generateEffectApplier(opt: ChronobaseOptions, ss: SuperstructureS): String = {
    val rootStructName =
      ss.structs.find(_.isRoot) match {
        case None => throw new RuntimeException("No root struct!")
        case Some(struct) => struct.name
      }
    val instanceTypeNames =
      (ss.structs.filter(_.mutability == MutableS).map(_.name) ++
        ss.lists.filter(_.mutability == MutableS).map(_.name) ++
        ss.sets.filter(_.mutability == MutableS).map(_.name) ++
        ss.maps.filter(_.mutability == MutableS).map(_.name))
    val effectVisitorNames = instanceTypeNames.map(n => ",\nI" + n + "EffectVisitor").mkString("")

    s"""
       |public class EffectApplier : IEffectVisitor ${effectVisitorNames} {
       |  Root root;
         |  public EffectApplier(Root root) {
         |    this.root = root;
         |  }
         |
       |""".stripMargin +
      ss.structs.filter(_.mutability == MutableS).map(struct => {
        StructGenerator.generateEffectApplierMethods(struct)
      }).mkString("") +
      ss.lists.filter(_.mutability == MutableS).map(list => {
        ListGenerator.generateEffectApplierMethods(list)
      }).mkString("") +
      ss.sets.filter(_.mutability == MutableS).map(set => {
        SetGenerator.generateEffectApplierMethods(set)
      }).mkString("") +
      ss.maps.filter(_.mutability == MutableS).map(map => {
        MapGenerator.generateEffectApplierMethods(map)
      }).mkString("") +
      s"""
         |  public void Apply(IEffect effect) {
         |    effect.visitIEffect(this);
         |  }
         |}
         """.stripMargin
  }
}
