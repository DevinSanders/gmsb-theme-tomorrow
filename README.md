# gmsb-theme-tomorrow

Tomorrow theme pack for [Game Master Sound Board](https://github.com/DevinSanders/game-master-soundboard).

Chris Kempson's classic [Tomorrow](https://github.com/chriskempson/tomorrow-theme) — one of the original "designer" code-editor palettes, organised around a single light base and four darker variants that share the same named accent vocabulary. All five canonical variants ship as independent selectable palettes:

| Palette                   | Base color                  | Notes |
|---------------------------|-----------------------------|-------|
| Tomorrow                  | #FFFFFF (clean white)       | The original light base. |
| Tomorrow Night            | #1D1F21 (neutral charcoal)  | The most widely-used variant — soft, low-contrast dark. |
| Tomorrow Night Bright     | #000000 (pure black)        | OLED-friendly; vivid accents on a true-black background. |
| Tomorrow Night Eighties   | #2D2D2D (retro charcoal)    | Saturated pastel accents — the retro/synth-wave variant. |
| Tomorrow Night Blue       | #002451 (midnight blue)     | Cool ice accents on a deep midnight base. |

All five variants share Tomorrow's named accent set — red, orange, yellow, green, aqua, blue, purple. Only the surfaces and the tone of the accents change between variants.

Each palette is a flat set of colours — one selectable look in the host's theme dropdown (shown as "Tomorrow: Tomorrow Night", etc.). There is no Dark/Light variant: the host applies the palette regardless of the active Avalonia variant and infers light/dark Fluent chrome (scrollbars, popups, focus rings) from the background luminance on its own. To switch bases, just pick the other palette.

## Install

Drop the released `.zip` onto Settings → Plugin Manager. Themes activate live — no restart needed. Pick the palette from Settings → Appearance → Theme.

Pre-built zips are attached to each [GitHub Release](../../releases).

## Build

```powershell
dotnet build src/TomorrowThemePlugin.csproj
pwsh scripts/package.ps1
# → dist/github.DevinSanders-theme.tomorrow-1.0.0.zip
```

Requires .NET 10 SDK. `SoundBoard.PluginApi` is restored from NuGet automatically — no sibling checkout needed.

## Plugin manifest

| Field     | Value                          |
|-----------|--------------------------------|
| publisher | `github.DevinSanders`          |
| id        | `theme.tomorrow`               |
| entryDll  | `TomorrowThemePlugin.dll`      |
| isTheme   | `true`                         |

## Attribution

Tomorrow color values from https://github.com/chriskempson/tomorrow-theme, © Chris Kempson, released under the MIT license. This pack adapts the palette to Game Master Sound Board's semantic key vocabulary; it is not an official Tomorrow port.

## License

Released under the [MIT License](LICENSE).

Tomorrow colors are © Chris Kempson, licensed under MIT — see https://github.com/chriskempson/tomorrow-theme/blob/master/LICENSE.md.
