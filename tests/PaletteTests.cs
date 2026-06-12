using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Media;
using Avalonia.Styling;
using FluentAssertions;
using SoundBoard.PluginApi;
using Xunit;

namespace TomorrowThemePlugin.Tests;

public class PaletteTests
{
    // The five palettes this pack ships, as (id, name, avares-uri). Kept in
    // lockstep with TomorrowThemePlugin.GetPalettes(); the catalog test below
    // is what enforces that they stay in sync.
    public static readonly (string Id, string Name, string Uri)[] Expected =
    {
        ("tomorrow",                "Tomorrow",
            "avares://TomorrowThemePlugin/Themes/Tomorrow.axaml"),
        ("tomorrow-night",          "Tomorrow Night",
            "avares://TomorrowThemePlugin/Themes/TomorrowNight.axaml"),
        ("tomorrow-night-bright",   "Tomorrow Night Bright",
            "avares://TomorrowThemePlugin/Themes/TomorrowNightBright.axaml"),
        ("tomorrow-night-eighties", "Tomorrow Night Eighties",
            "avares://TomorrowThemePlugin/Themes/TomorrowNightEighties.axaml"),
        ("tomorrow-night-blue",     "Tomorrow Night Blue",
            "avares://TomorrowThemePlugin/Themes/TomorrowNightBlue.axaml"),
    };

    // The 25 semantic brush keys the host resolves against any theme. Every
    // palette must define all of them, or a user selecting the theme meets an
    // unstyled control. Keep in sync with the host's theme vocabulary.
    public static readonly string[] SemanticKeys =
    {
        "SidebarBackground", "ContentBackground",
        "PanelBackground1", "PanelBackground2", "PanelBackground3", "SubtleBorder",
        "PrimaryAccent", "PrimaryAccentHover", "OnPrimaryAccent", "SecondaryAccent",
        "TextPrimary", "TextSecondary",
        "SuccessBackground", "SuccessForeground",
        "DangerBackground", "DangerForeground",
        "InfoBackground", "InfoForeground",
        "WarningBackground", "WarningForeground",
        "DropZoneHighlight", "WaveformBrush",
        "LoopInheritForeground", "LoopForceOnForeground", "LoopForceOffForeground",
    };

    public static IEnumerable<object[]> PaletteUris() =>
        Expected.Select(p => new object[] { p.Uri });

    public static IEnumerable<object[]> PaletteKeyMatrix() =>
        from p in Expected
        from key in SemanticKeys
        select new object[] { p.Uri, key };

    private static ResourceDictionary Load(string uriString)
    {
        var uri = new Uri(uriString);
        var include = new ResourceInclude(uri) { Source = uri };
        return (ResourceDictionary)include.Loaded;
    }

    // ── Palette catalog ──────────────────────────────────────────────────

    [Fact]
    public void GetPalettes_returns_the_shipped_catalog()
    {
        var palettes = new TomorrowThemePlugin().GetPalettes().ToArray();

        palettes.Select(p => (p.Id, p.Name, Uri: p.ResourceUris.Single()))
            .Should().Equal(Expected);
    }

    [Fact]
    public void Plugin_identity_matches_the_manifest()
    {
        var plugin = new TomorrowThemePlugin();

        plugin.Id.Should().Be("theme.tomorrow");
        plugin.Name.Should().Be("Tomorrow");
        plugin.Author.Should().Be("Devin Sanders");
    }

    // ── Resources resolve ────────────────────────────────────────────────

    [AvaloniaTheory]
    [MemberData(nameof(PaletteUris))]
    public void Palette_dictionary_loads_and_is_not_empty(string uri)
    {
        var dict = Load(uri);
        dict.Count.Should().BeGreaterThan(0);
    }

    // ── Semantic-key completeness (the important test) ───────────────────

    [AvaloniaTheory]
    [MemberData(nameof(PaletteKeyMatrix))]
    public void Every_semantic_key_resolves_to_a_brush(string uri, string key)
    {
        var dict = Load(uri);

        dict.TryGetResource(key, null, out var value)
            .Should().BeTrue($"palette '{uri}' must define '{key}'");
        value.Should().BeOfType<SolidColorBrush>($"'{key}' must be a SolidColorBrush");
    }

    // ── Flatness guard ───────────────────────────────────────────────────

    [AvaloniaTheory]
    [MemberData(nameof(PaletteUris))]
    public void Palette_is_flat_with_no_theme_variants(string uri)
    {
        var dict = Load(uri);

        dict.ThemeDictionaries.Should().BeEmpty(
            "themes are flat — no Dark/Light ThemeDictionaries blocks");

        // A flat palette must resolve identically under every variant; a
        // variant-split dictionary would only resolve under its own key.
        foreach (var variant in new[] { ThemeVariant.Default, ThemeVariant.Light, ThemeVariant.Dark })
        {
            dict.TryGetResource("PrimaryAccent", variant, out var value)
                .Should().BeTrue($"'PrimaryAccent' must resolve under {variant}");
            value.Should().BeOfType<SolidColorBrush>();
        }
    }
}
