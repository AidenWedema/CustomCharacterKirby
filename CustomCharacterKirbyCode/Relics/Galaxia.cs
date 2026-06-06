using CustomCharacterKirby.CustomCharacterKirbyCode.Monsters;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using Godot;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Relics;

public class Galaxia() : CustomCharacterKirbyRelic
{
    public override RelicRarity Rarity => RelicRarity.Event;

    protected override IEnumerable<DynamicVar> CanonicalVars => [];


    public override async Task AfterBlockCleared(Creature creature)
    {
        Galaxia relic = this;

        if (creature.CombatState.RoundNumber != 2 || creature != relic.Owner.Creature) return;

        var metaKnight = ModelDb.Monster<MetaKnight>();
        var metaKnightCreature = await DreamFriendCmd.Befriend(relic.Owner, metaKnight, false);
        TalkCmd.Play(MonsterModel.L10NMonsterLookup($"{metaKnight.Id.Entry}.summon.speakLine1"), metaKnightCreature, VfxColor.White, VfxDuration.Standard);
        //
        // // Position the summoned creature
        // var pos = new Vector2(500f, -50f);
        // DreamFriendCmd.SetPositionRelativeToOwner(metaKnightCreature, relic.Owner, pos);
        //
        // // Re-position all already existing friends
        // foreach (var f in DreamFriendCmd.GetAllPets<MonsterModel>(creature))
        //     DreamFriendCmd.SetPositionRelativeToOwner(f.Creature, relic.Owner, pos);
    }
}