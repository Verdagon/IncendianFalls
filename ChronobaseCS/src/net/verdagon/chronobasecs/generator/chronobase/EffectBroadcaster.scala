package net.verdagon.chronobasecs.generator.chronobase

import net.verdagon.chronobasecs.compiled.{MutableS, SuperstructureS}
import net.verdagon.chronobasecs.generator.chronobase.interface.InterfaceGenerator
import net.verdagon.chronobasecs.generator.chronobase.list.ListGenerator
import net.verdagon.chronobasecs.generator.chronobase.map.MapGenerator
import net.verdagon.chronobasecs.generator.chronobase.set.SetGenerator
import net.verdagon.chronobasecs.generator.chronobase.struct.StructGenerator

object EffectBroadcaster {

  def generateEffectBroadcaster(opt: ChronobaseOptions, ss: SuperstructureS): String = {
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

    s"""
       |public class EffectBroadcaster : IEffectVisitor {
         |""".stripMargin +
      ss.structs.filter(_.mutability == MutableS).map(struct => {
        StructGenerator.generateEffectBroadcasterMembers(opt, struct)
      }).mkString("") +
      ss.lists.filter(_.mutability == MutableS).map(list => {
        ListGenerator.generateEffectBroadcasterMembers(opt, list)
      }).mkString("") +
      ss.sets.filter(_.mutability == MutableS).map(set => {
        SetGenerator.generateEffectBroadcasterMembers(opt, set)
      }).mkString("") +
      ss.maps.filter(_.mutability == MutableS).map(map => {
        MapGenerator.generateEffectBroadcasterMembers(opt, map)
      }).mkString("") +
      s"""
         |  public EffectBroadcaster() {
         |  }
         |
       |""".stripMargin +
      ss.structs.filter(_.mutability == MutableS).map(struct => {
        StructGenerator.generateEffectBroadcasterMethods(struct)
      }).mkString("") +
      ss.lists.filter(_.mutability == MutableS).map(list => {
        ListGenerator.generateEffectBroadcasterMethods(list)
      }).mkString("") +
      ss.sets.filter(_.mutability == MutableS).map(set => {
        SetGenerator.generateEffectBroadcasterMethods(set)
      }).mkString("") +
      ss.maps.filter(_.mutability == MutableS).map(map => {
        MapGenerator.generateEffectBroadcasterMethods(map)
      }).mkString("") +
      s"""
         |  public void Route(IEffect effect) {
         |    effect.visitIEffect(this);
         |  }
         |}
         """.stripMargin
  }
}
