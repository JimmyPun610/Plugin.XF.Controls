# Plugin.XF.Controls
Xamarin Forms Controls and Effects, WebView with Headres, Basic Authentication, Borderless Entry, Borderless Editor, Entry with Toolbar Effects, Snackbar

Please refer to Sample app for usage.


### Support Platform
1. iOS 10+
2. Android 9+

### Installation
Install the package to Xamarin.Forms, Android and iOS project
```
Install-Package Plugin.XF.Controls
```

### Controls
#### [EnhancedWebView](#EnhancedWebView)
```
1. Custom Header Support
2. Basic Authentication Support
```
#### [EntrywithToolbarEffects](#Entry with Toolbar Effects (Only avaliable on iOS))
```
1. Single / Multiple custom uibarbuttonitem in keyboard view
```
```XAML
      <Entry HorizontalTextAlignment="Start" Text="This is entry with multi action" BackgroundColor="Blue">
                    <Entry.Effects>
                        <effects:EntryWithToolbarEffect>
                            <effects:EntryWithToolbarEffect.ActionButtons>
                                <effects:ActionButton Parameter="Please bind to object" Title="Action 1" Clicked="ActionButton1_Clicked"                                                       BarButtonItemStyle="Plain" DismissKeyboard="False" FlexibleSpaceBehind="False"/>
                                <effects:ActionButton Parameter="Please bind to object" Title="Action 2" Clicked="ActionButton2_Clicked"                                                       BarButtonItemStyle="Plain" DismissKeyboard="False" FlexibleSpaceBehind="True"/>
                                <effects:ActionButton Title="Done" BarButtonItemStyle="Done" FlexibleSpaceBehind="False"/>
                            </effects:EntryWithToolbarEffect.ActionButtons>
                        </effects:EntryWithToolbarEffect>
                    </Entry.Effects>
                </Entry>
```
<table>
  <tr>
    <td> Single </td>
    <td> Multiple </td>
  </tr>
  <tr>
    <td> <img src="https://github.com/JimmyPun610/Plugin.XF.Controls/blob/master/Screenshots/IMG_9455.PNG?raw=true" height="400">
    </td>
    <td>
      <img src="https://github.com/JimmyPun610/Plugin.XF.Controls/blob/master/Screenshots/IMG_9454.PNG?raw=true" height="400">
    </td>
  </tr>
</table>

#### [BorderlessEntryEffects](#Borderless_Entry_Effects)
#### [AnnotatedEntry](#AnnotatedEntry)
```
1. Custom annotation on Entry
2. Support Xamarin Form Material
```
To set in it runtime.
```C#
AnnotatedEntry.SetAnnotation(bool isShow, string text, Color textColor);
AnnotatedEntry.HideAnnotation()
```
#### [Snackbar](#Snackbar)
```C#
 Plugin.XF.Controls.Shared.Services.DialogService.ShowSnackbar(randomMessage, 3, Color.White, Color.Blue, 0.75f, "OK", Color.Yellow, null);
```
<table>
  <tr>
    <td> Android </td>
    <td> iOS </td>
  </tr>
  <tr>
    <td> <img src="https://github.com/JimmyPun610/Plugin.XF.Controls/blob/master/Screenshots/Screenshot_20191210-114303.png?raw=true" height="400">
    </td>
    <td>
      <img src="https://github.com/JimmyPun610/Plugin.XF.Controls/blob/master/Screenshots/IMG_9459.PNG?raw=true" height="400">
    </td>
  </tr>
</table>

