
# CarbonColorExtensions

**CarbonColorExtensions** is a library for working with colors, including color conversion, color retrieval by name, and random color generation. The project is created within the Carbon system and provides convenient methods for working with colors in RGB and HEX formats.

## Features

- Load and manage named colors from a JSON file.
- Convert HEX colors to RGBA format.
- Retrieve colors by name with support for transparency.
- Random color generation in RGBA and HEX formats.

## Installation

1. Download and extract the project.

2. Copy the contents of the `build` folder to the `carbon` folder of your Rust server.

## Usage

To use the library methods, you need to include:

```csharp
using static Carbon.Extensions.ColorUtility;
```

### Core Methods

1. **HexToRGBA**

```csharp
string rgbaColor = HexToRGBA("#FFAABBCC");
```

**Response:**
```
"1.00 0.67 0.73 0.80"
```

2. **GetHexColorByName**

```csharp
string hexColor = GetHexColorByName("blue");
```

**Response:**
```
"#0000FFFF"
```

```csharp
string hexColorWithTransparency = GetHexColorByName("blue", 0.5f);
```

**Response:**
```
"0.00 0.00 1.00 0.50"
```

```csharp
string hexColorWithTransparencyString = GetHexColorByName("blue", "80");
```

**Response:**
```
"#0000FF80"
```

3. **GetRGBAColorByName**

```csharp
string rgbaColor = GetRGBAColorByName("red", 0.75f);
```

**Response:**
```
"1.00 0.00 0.00 0.75"
```

4. **GetRandomRGBAColor**

```csharp
string randomRGBA = GetRandomRGBAColor();
```

**Response:**
```
"0.34 0.56 0.78 1.00" (values may vary)
```

5. **GetRandomHEXColor**

1. Example:

```csharp
string randomHex = GetRandomHEXColor();
```

**Response:**
```
"#AB12CDFF" (values may vary)
```

2. Example:

```csharp
string randomHexWithTransparency = GetRandomHEXColor("AA");
```

**Response:**
```
"#AB12CDAA" (values may vary)
```

### Example Usage in a Project

```csharp
namespace Carbon.Plugins
{
    [Info("____db_template", "CreepOK", "1.0.1")]
    public class ____db_template : CarbonPlugin
    {
        private void OnServerInitialized()
        {
            var result = GetRandomHEXColor("AA");
            Puts(result);
            result = GetHexColorByName("blue");
            Puts(result);
            result = GetRGBAColorByName("green");
            Puts(result);
            result = HexToRGBA("#7d7d7dAA");
            Puts(result);
        }
    }
}
```

## colors.json File

Example of the `colors.json` file, which should be located at `carbon/extensions/data/ColorUtility/colors.json`:

```json
{
    "blue": {
        "hex": "#0000FF",
        "rgb": "0 0 255"
    },
    "red": {
        "hex": "#FF0000",
        "rgb": "255 0 0"
    },
    "green": {
        "hex": "#00FF00",
        "rgb": "0 255 0"
    }
    // Add other colors as needed
}
```

## License

This project is licensed under the MIT License.

## Author

- [@relfost](https://t.me/relfost) on Telegram
