using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

public class MassAttack() : CustomCharacterKirbyCard(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(4M, ValueProp.Move), new RepeatVar(4)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        MassAttack card = this;
        ArgumentNullException.ThrowIfNull((object)play.Target, "play.Target");
        
        await DamageCmd.Attack(card.DynamicVars.Damage.BaseValue).WithHitCount(card.DynamicVars.Repeat.IntValue).FromCard(card).Targeting(play.Target).Execute(choiceContext);
    }

    protected override void OnUpgrade() => DynamicVars.Repeat.UpgradeValueBy(2);
}