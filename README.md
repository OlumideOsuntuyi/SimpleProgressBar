# SimpleProgressBar

The `SimpleProgressBar` script is a versatile Unity component that allows you to create and customize a simple progress bar. This script provides a range of options for visualizing and customizing the progress bar's appearance and behavior.

## Features

- Customizable progress bar appearance and behavior
- Options for different progress visualization types
- Gradient-based color interpolation
- Adjustable minimum and maximum values
- Support for background and outline components
- Option to show percentage value

## Getting Started

1. <a href="https://assetstore.unity.com/packages/slug/263410">Install the Unity package</a>
2. Attach the `SimpleProgressBar` script to a GameObject in your scene.
3. Customize the various settings in the Inspector to tailor the progress bar to your needs.

## Script Details

### Graphic Settings

- **Graphic**: The Image component used for the progress bar.
- **Sprite**: The default sprite for the progress bar.
- **Size**: The size of the progress bar.
- **Gradient**: The gradient used for color interpolation.
- **Color**: The single color used for fill.
- **Min Color**: The minimum color for gradient fill.
- **Max Color**: The maximum color for gradient fill.
- **Background**: The Image component for the background.
- **Outline Perimeter**: The Image component for the outline.
- **Outline Color**: The color of the outline.
- **Background Color**: The single color used for fill background.
- **Outline Thickness**: The thickness of the outline.

### Progress Settings

- **Progress Type**: The type of progress visualization.
- **Fill Type**: The type of fill color.
- **Fill Method**: The fill method for the progress bar.
- **Value**: The current value of the progress bar.
- **Min**: The minimum value of the progress bar.
- **Max**: The maximum value of the progress bar.
- **Show Percentage**: Whether to show the percentage value.
- **Percentage**: The TMP_Text component to display the percentage value.
- **Filled**: The calculated filled amount of the progress bar.

### Methods

- **SetFill**: Sets the fill amount of the graphic.
- **SetColor**: Sets the color of the graphic based on the fill type.
- **SetMin**: Sets the minimum value of the progress bar.
- **SetMax**: Sets the maximum value of the progress bar.
- **SetMinMax**: Sets both the minimum and maximum values.
- **SetValue**: Sets the value of the progress bar.
- **Reset**: Resets the progress bar to its default settings.
- **SetDefaultSprite**: Sets the default sprite for the progress bar.
- **UpdateGraphic**: Updates the graphic settings of the progress bar.
- **Add**: Adds a specified amount to the current value.

## Example Usage

```csharp
// Attach this script to an empty GameObject with no children.
// Customize the settings in the Inspector to create your desired progress bar.
