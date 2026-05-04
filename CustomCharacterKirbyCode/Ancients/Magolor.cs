using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using CustomCharacterKirby.CustomCharacterKirbyCode.Relics;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Relics;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Ancients;

public class Magolor : CustomAncientModel
{
    public override bool IsValidForAct(ActModel act) => act.ActNumber() > 1; // Magolor can spawn on act 2 or 3
    
    protected override OptionPools MakeOptionPools => new OptionPools(
        [
            AncientOption<AbilityEssence>(),
            AncientOption<InvincibilityCandy>(),
            AncientOption<Keeby>()
        ]
    );
    
    public override string? CustomScenePath => "scenes/events/background_scenes/customcharacterkirby-magolor.tscn";

    public override string? CustomMapIconPath => "CustomCharacterKirby/images/ancients/magolor/map_icon.png";

    public override string? CustomMapIconOutlinePath => "CustomCharacterKirby/images/ancients/magolor/map_icon_outline.png";

    public override string? CustomRunHistoryIconPath => "CustomCharacterKirby/images/ancients/magolor/run_history_icon.png";

    public override string? CustomRunHistoryIconOutlinePath => "CustomCharacterKirby/images/ancients/magolor/run_history_icon_outline.png";
}