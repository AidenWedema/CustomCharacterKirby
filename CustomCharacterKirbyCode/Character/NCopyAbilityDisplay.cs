using CustomCharacterKirby.CustomCharacterKirbyCode.Powers;
using Godot;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Nodes.HoverTips;

namespace CustomCharacterKirby.CustomCharacterKirbyCode.Character;

[GlobalClass]
public partial class NCopyAbilityDisplay : Control
{
    private Player? _player;

    private Panel? _panel;
    private TextureRect? _icon;
    private Label? _label;
    
    private Tween? _flashTween;
    
    private HoverTip? _hoverTip;
    
    public void SetIcon(TextureRect icon) => _icon = icon;
    public void SetLabel(Label label) => _label = label;
    public void SetPanel(Panel panel) => _panel = panel;
    public void SetPlayer(Player player) => _player = player;
    public void SetHoverTip(HoverTip hoverTip) => _hoverTip = hoverTip;

    public override void _ExitTree()
    {
        CopyAbilityCmd.AbilityChanged -= OnAbilityChanged;
    }

    private void OnHovered()
    {
        if (_hoverTip == null)
            return;

        var hoverOrigin = _panel?.GlobalPosition ?? GlobalPosition;
        NHoverTipSet.CreateAndShow(this, _hoverTip).GlobalPosition = hoverOrigin + new Vector2(-70f, -200f);
    }

    private void OnUnhovered() => NHoverTipSet.Remove(this);
    
    public static NCopyAbilityDisplay Create(Player player)
    {
        var control = new NCopyAbilityDisplay();

        var panel = new Panel();
        panel.Name = "Panel";
        panel.Size = new Vector2(96, 96);
        panel.Position = new Vector2(75, 75);
        panel.MouseFilter = MouseFilterEnum.Stop;
        panel.SelfModulate = new Color(0f, 0f, 0f, 0f);
        panel.PivotOffset = panel.Size * 0.5f;
        control.AddChild(panel);

        var icon = new TextureRect();
        icon.Name = "Icon";
        // icon.Texture = ResourceLoader.Load<Texture2D>("res://CustomCharacterKirby/images/powers/big/leaf_ability.png");
        icon.SetAnchorsPreset(LayoutPreset.FullRect);
        icon.ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize;
        icon.StretchMode = TextureRect.StretchModeEnum.KeepAspectCentered;
        icon.MouseFilter = MouseFilterEnum.Ignore;
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
        label.MouseFilter = MouseFilterEnum.Ignore;
        panel.AddChild(label);

        control.SetIcon(icon);
        control.SetLabel(label);
        control.SetPanel(panel);
        control.SetPlayer(player);
        
        control.Visible = false;
        
        CopyAbilityCmd.AbilityChanged += control.OnAbilityChanged;
        
        panel.Connect(Control.SignalName.MouseEntered, Callable.From(control.OnHovered));
        panel.Connect(Control.SignalName.MouseExited, Callable.From(control.OnUnhovered));
        
        return control;
    }

    private void Refresh()
    {
        NCopyAbilityDisplay nCopyAbilityDisplay = this;
        
        var player = nCopyAbilityDisplay._player;
        if (nCopyAbilityDisplay._icon == null || nCopyAbilityDisplay._label == null || player == null) return;
        
        var combatState = player.PlayerCombatState;
        if (combatState == null) return;

        var ability = CopyAbilityCmd.GetCurrent(combatState);

        if (ability == null)
        {
            nCopyAbilityDisplay.Visible = false;
            return;
        }

        nCopyAbilityDisplay.Visible = true;

        nCopyAbilityDisplay._label.Text = ability.DisplayName;

        nCopyAbilityDisplay._icon.Texture = ResourceLoader.Load<Texture2D>(ability.SpritePath);
        
        LocString description = new LocString("powers", $"{ability.DisplayName.ToLower()}.description");
        nCopyAbilityDisplay.SetHoverTip(new HoverTip(new LocString("powers", $"{ability.DisplayName.ToLower()}.title"), description));
    }
    
    private void OnAbilityChanged(PlayerCombatState state, CopyAbility? oldAbility, CopyAbility? newAbility)
    {
        NCopyAbilityDisplay nCopyAbilityDisplay = this;
        
        if (nCopyAbilityDisplay._player?.PlayerCombatState != state || !IsInsideTree() || nCopyAbilityDisplay._panel == null)
            return;
        
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