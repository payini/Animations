namespace AnimationsDemo;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnAnimationClicked(object sender, EventArgs e)
    {
        SemanticScreenReader.Announce(AnimationLabel.Text);
        RandomAnimation();
    }

    private void OnCancelAnimationClicked(object sender, EventArgs e)
    {
        SemanticScreenReader.Announce("Cancel animation");
        dotNetBotImage.CancelAnimations();
    }

    private void OnResetClicked(object sender, EventArgs e)
    {
        ResetProperties();
    }

    private async void ResetProperties()
    {
        SemanticScreenReader.Announce("Reset properties");
        await Task.Delay(1000);
        dotNetBotImage.Rotation = 0;
        dotNetBotImage.Scale = 1;
        dotNetBotImage.TranslationX = 0;
        dotNetBotImage.TranslationY = 0;
    }

    private async void RandomAnimation()
    {
        var animation = new Random().Next(1, 7);

        switch (animation)
        {
            case 1:
                await FadeToAnimation();
                break;
            case 2:
                await RotateAnimation();
                break;
            case 3:
                await ScaleAnimation();
                break;
            case 4:
                await TranslateAnimation();
                break;
            case 5:
                await CompositeAnimation();
                break;
            case 6:
                await EasingBounceOutAnimation();
                break;
            default:
                break;
        }
    }

    private void ChangeAnimationLabel(string text)
    {
        AnimationLabel.Text = $"Last animation: {text}";
    }

    private async Task FadeToAnimation()
    {
        ChangeAnimationLabel("FadeTo");

        await dotNetBotImage.FadeTo(0, 500);
        await dotNetBotImage.FadeTo(1, 500);
        ResetProperties();
    }

    private async Task RotateAnimation()
    {
        ChangeAnimationLabel("Rotate");

        await dotNetBotImage.RotateTo(360, 500);
        ResetProperties();
    }

    private async Task ScaleAnimation()
    {
        ChangeAnimationLabel("Scale");
        await dotNetBotImage.ScaleTo(2, 500);
        await dotNetBotImage.ScaleTo(1, 500);
        ResetProperties();
    }

    private async Task TranslateAnimation()
    {
        ChangeAnimationLabel("Translate");
        await dotNetBotImage.TranslateTo(-200, 0, 500);
        await dotNetBotImage.TranslateTo(0, 0, 500);
        ResetProperties();
    }

    private async Task CompositeAnimation()
    {
        ChangeAnimationLabel("Rotate");
        // Composite animation
        // A composite animation is a combination of animations where two or more animations run simultaneously.
        // Composite animations can be created by combining awaited and non-awaited animations:
        // Set Rotation to 0, otherwise the second time this animation runs, it won't rotate.
        dotNetBotImage.Rotation = 0;
        dotNetBotImage.RotateTo(360, 1000);
        await dotNetBotImage.ScaleTo(2, 500);
        await dotNetBotImage.ScaleTo(1, 500);
        ResetProperties();
    }

    private async Task EasingBounceOutAnimation()
    {
        ChangeAnimationLabel("Easing BounceOut");
        // This one took me like 2 hours to figure out. The last +25 it's because of the Spacing="25"
        // in the VerticalStackLayout setup. It works with any screen size.
        var translateToCoordinateY = Height - dotNetBotImage.Height - (dotNetBotImage.Height / 2) + 25;
        await dotNetBotImage.TranslateTo(0, translateToCoordinateY, 2000, Easing.BounceOut);
        ResetProperties();
    }
}
