using System.Collections;
using BaseLib.Utils;
using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using CustomCharacterKirby.CustomCharacterKirbyCode.Character;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

[Pool(typeof(CustomCharacterKirbyCardPool))]
public class Hypernova() : CustomCharacterKirbyCard(3, CardType.Skill, CardRarity.Rare, TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new IntVar("SwallowPercent", 20M)];

    protected override bool IsPlayable => SwallowableEnemies.Count > 0 && this.CombatState?.Encounter?.RoomType != RoomType.Boss;

    private IReadOnlyList<Creature> SwallowableEnemies
    {
        get {
            List<Creature> swallowableEnemies = new();
            if (this.CombatState != null)
            {
                var swallowPercent = DynamicVars["SwallowPercent"].BaseValue / 100;
                foreach (var enemy in this.CombatState.HittableEnemies)
                {
                    var swallowThreshold = enemy.MaxHp * swallowPercent;
                    if (enemy.CurrentHp <= swallowThreshold)
                        swallowableEnemies.Add(enemy);
                }
            }
            return swallowableEnemies;
        }
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Hypernova card = this;

        var swallowableEnemies = SwallowableEnemies;
        foreach (var enemy in swallowableEnemies)
        {
            await CreatureCmd.Kill(enemy);
        }
    }

    protected override void OnUpgrade() => DynamicVars["SwallowPercent"].UpgradeValueBy(10M);
}