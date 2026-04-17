using MegaCrit.Sts2.Core.Entities.Powers;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Powers;

public class RobobotArmorPower : CustomCharacterKirbyPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override bool AllowNegative => false;
}