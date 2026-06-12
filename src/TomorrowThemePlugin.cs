using System.Collections.Generic;
using SoundBoard.PluginApi;

namespace TomorrowThemePlugin;

/// <summary>
/// Tomorrow — Chris Kempson's widely-ported colour scheme
/// (https://github.com/chriskempson/tomorrow-theme). One of the original
/// "designer" code-editor palettes, organised around a single light base
/// and four darker variants that share the same accent vocabulary.
///
/// <para>This pack exposes all five canonical variants as selectable
/// palettes:
/// <list type="bullet">
///   <item><b>Tomorrow</b> — the original light base (#FFFFFF).</item>
///   <item><b>Tomorrow Night</b> — neutral charcoal base (#1D1F21).</item>
///   <item><b>Tomorrow Night Bright</b> — pure-black base (#000000) with
///   vivid accents.</item>
///   <item><b>Tomorrow Night Eighties</b> — retro charcoal base (#2D2D2D)
///   with saturated pastel accents.</item>
///   <item><b>Tomorrow Night Blue</b> — deep midnight-blue base
///   (#002451) with cool ice accents.</item>
/// </list></para>
///
/// <para>All five variants share Tomorrow's named accent set
/// (red, orange, yellow, green, aqua, blue, purple) — only the surfaces
/// and the tone of the accents change between variants.</para>
///
/// <para>Each palette is a flat set of colours — one selectable look in
/// the host's theme dropdown. There is no Dark/Light variant: the host
/// applies the chosen palette regardless of the active Avalonia variant
/// and infers light/dark Fluent chrome from the background luminance on
/// its own.</para>
/// </summary>
public sealed class TomorrowThemePlugin : IThemePlugin
{
    public string Id => "theme.tomorrow";
    public string Name => "Tomorrow";
    public string Version => PluginVersion.OfAssembly(typeof(TomorrowThemePlugin));
    public string Author => "Devin Sanders";
    public string Description => "Chris Kempson's Tomorrow theme: five flat palettes — Tomorrow, Night, Night Bright, Night Eighties, Night Blue.";

    public void Initialize(IPluginContext context) { }
    public void Shutdown() { }

    public IEnumerable<ThemePalette> GetPalettes() => new[]
    {
        new ThemePalette("tomorrow",                "Tomorrow",
            new[] { "avares://TomorrowThemePlugin/Themes/Tomorrow.axaml" }),
        new ThemePalette("tomorrow-night",          "Tomorrow Night",
            new[] { "avares://TomorrowThemePlugin/Themes/TomorrowNight.axaml" }),
        new ThemePalette("tomorrow-night-bright",   "Tomorrow Night Bright",
            new[] { "avares://TomorrowThemePlugin/Themes/TomorrowNightBright.axaml" }),
        new ThemePalette("tomorrow-night-eighties", "Tomorrow Night Eighties",
            new[] { "avares://TomorrowThemePlugin/Themes/TomorrowNightEighties.axaml" }),
        new ThemePalette("tomorrow-night-blue",     "Tomorrow Night Blue",
            new[] { "avares://TomorrowThemePlugin/Themes/TomorrowNightBlue.axaml" }),
    };
}
