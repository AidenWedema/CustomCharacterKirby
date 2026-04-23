using BaseLib.Utils;
using CustomCharacterKirby.CustomCharacterKirbyCode.Cards;
using CustomCharacterKirby.CustomCharacterKirbyCode.Character;
using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Cards;

public class RobobotArmor() : CustomCharacterKirbyCard(3, CardType.Power, CardRarity.Rare, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar("StrengthGain", 4M), new DynamicVar("DexterityGain", 4M)];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<StrengthPower>(), HoverTipFactory.FromPower<DexterityPower>()];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        RobobotArmor card = this;
        
        // Give Robobot Armor
        await PowerCmd.Apply<RobobotArmorPower>(card.Owner.Creature, 1, card.Owner.Creature, card);
        
        // Give strength
        await PowerCmd.Apply<StrengthPower>(card.Owner.Creature, DynamicVars["StrengthGain"].BaseValue, card.Owner.Creature, card);
        
        // Give dexterity
        await PowerCmd.Apply<DexterityPower>(card.Owner.Creature, DynamicVars["DexterityGain"].BaseValue, card.Owner.Creature, card);
    }

    protected override void OnUpgrade() => EnergyCost.UpgradeBy(-1);
}