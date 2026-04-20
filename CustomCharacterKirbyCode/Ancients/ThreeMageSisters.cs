using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Relics;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Ancients;

public class ThreeMageSisters : CustomAncientModel
{
    public override bool IsValidForAct(ActModel act) => act.ActNumber() == 3; // Mage sisters can only spawn on act 3
    
    protected override OptionPools MakeOptionPools => new OptionPools(
        // Pool 1, Francisca
        [
            AncientOption<Nunchaku>(),
            AncientOption<Lantern>(),
            AncientOption<ArtOfWar>()
        ],
        // Pool 2, Flamberge
        [
            AncientOption<Nunchaku>(),
            AncientOption<Lantern>(),
            AncientOption<ArtOfWar>()
        ],
        // Pool 3, Zan Partizanne
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