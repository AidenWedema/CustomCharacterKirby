using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Relics;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Ancients;

public class Elfilin : CustomAncientModel
{
    public override bool IsValidForAct(ActModel act) => act.ActNumber() == 1; // Elfilin can only spawn on act 1
    
    protected override OptionPools MakeOptionPools => new OptionPools(
        [
            AncientOption<Nunchaku>(),
            AncientOption<Lantern>(),
            AncientOption<ArtOfWar>()
        ]
    );
    
    public override string? CustomScenePath => null;

    public override string? CustomMapIconPath => null;

    public override string? CustomMapIconOutlinePath => null;

    public override string? CustomRunHistoryIconPath => null;

    public override string? CustomRunHistoryIconOutlinePath => null;
}