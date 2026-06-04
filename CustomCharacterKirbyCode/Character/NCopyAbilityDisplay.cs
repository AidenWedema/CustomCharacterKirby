using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using Godot;
using MegaCrit.Sts2.Core.Entities.Players;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Character;

[GlobalClass]
public partial class NCopyAbilityDisplay : Control
{
    private Player? _player;

    private Panel? _panel;
    private TextureRect? _icon;
    private Label? _label;
    
    private Tween? _flashTween;
    
    public void SetIcon(TextureRect icon) => _icon = icon;
    public void SetLabel(Label label) => _label = label;
    public void SetPanel(Panel panel) => _panel = panel;
    public void SetPlayer(Player player) => _player = player;

    public override void _ExitTree()
    {
        CopyAbilityCmd.AbilityChanged -= OnAbilityChanged;
    }
    
    public static NCopyAbilityDisplay Create(Player player)
    {
        var control = new NCopyAbilityDisplay();

        var panel = new Panel();
        panel.Name = "Panel";
        panel.Size = new Vector2(96, 96);
        panel.Position = new Vector2(75, 75);
        panel.MouseFilter = MouseFilterEnum.Pass;
        panel.SelfModulate = new Color(0f, 0f, 0f, 0f);
        panel.PivotOffset = panel.Size * 0.5f;
        control.AddChild(panel);

        var icon = new TextureRect();
        icon.Name = "Icon";
        // icon.Texture = ResourceLoader.Load<Texture2D>("res://CustomCharacterKirby/images/powers/big/leaf_ability.png");
        icon.SetAnchorsPreset(LayoutPreset.FullRect);
        icon.ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize;
        icon.StretchMode = TextureRect.StretchModeEnum.KeepAspectCentered;
        panel.AddChild(icon);

        var label = new Label();
        label.Name = "Label";
        label.Position = new Vector2(0, 16);
        label.SetAnchorsPreset(LayoutPreset.FullRect);
        label.HorizontalAlignment = HorizontalAlignment.Center;
        label.VerticalAlignment = VerticalAlignment.Bottom;
        // label.Text = "Leaf";
        label.LabelSettings = new LabelSettings();
        label.LabelSettings.FontSize = 28;
        label.LabelSettings.FontColor = new("f6f6f6");
        label.LabelSettings.OutlineSize = 12;
        label.LabelSettings.OutlineColor = new("eb7295");
        panel.AddChild(label);

        control.SetIcon(icon);
        control.SetLabel(label);
        control.SetPanel(panel);
        control.SetPlayer(player);
        
        control.Visible = false;
        
        CopyAbilityCmd.AbilityChanged += control.OnAbilityChanged;
        
        return control;
    }

    private void Refresh()
    {
        var player = _player;
        if (_icon == null || _label == null || player == null) return;
        
        var combatState = player.PlayerCombatState;
        if (combatState == null) return;

        var ability = CopyAbilityCmd.GetCurrent(combatState);

        if (ability == null)
        {
            Visible = false;
            return;
        }

        Visible = true;

        _label.Text = ability.DisplayName;

        _icon.Texture = ResourceLoader.Load<Texture2D>(ability.SpritePath);
    }
    
    private void OnAbilityChanged(PlayerCombatState state, CopyAbility? oldAbility, CopyAbility? newAbility)
    {
        if (_player?.PlayerCombatState != state || !IsInsideTree())
            return;

        NCopyAbilityDisplay nCopyAbilityDisplay = this;
        
        nCopyAbilityDisplay._flashTween?.Kill();

        nCopyAbilityDisplay._panel.Scale = Vector2.One;

        nCopyAbilityDisplay._flashTween = nCopyAbilityDisplay.CreateTween();

        var scaleTime = 0.25f;
        
        // Scale to 0
        nCopyAbilityDisplay._flashTween.TweenProperty(nCopyAbilityDisplay._panel, "scale", Vector2.Zero, scaleTime).SetEase(Tween.EaseType.In);
        // Refresh the display
        nCopyAbilityDisplay._flashTween.TweenCallback(Callable.From(Refresh));
        // Scale to 1.2
        nCopyAbilityDisplay._flashTween.TweenProperty(nCopyAbilityDisplay._panel, "scale", Vector2.One * 1.2f, scaleTime).SetEase(Tween.EaseType.InOut);
        // Scale to 1
        nCopyAbilityDisplay._flashTween.TweenProperty(nCopyAbilityDisplay._panel, "scale", Vector2.One, scaleTime * 0.25f).SetEase(Tween.EaseType.In);
    }
}