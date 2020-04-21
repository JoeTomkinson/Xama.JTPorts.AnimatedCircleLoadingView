# Xamarin Animated Circle Loading View
[![platform](https://img.shields.io/badge/platform-Xamarin.Android-brightgreen.svg)](https://www.xamarin.com/)
[![API](https://img.shields.io/badge/API-10%2B-orange.svg?style=flat)](https://android-arsenal.com/api?level=10s)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)
[![NuGet](https://img.shields.io/nuget/v/Xama.JTPorts.RippleBackground.svg?label=NuGet)](https://www.nuget.org/packages/Xama.JTPorts.RippleBackground/)

C# Port of the Android Java library [AnimatedCircleLoadingView](https://github.com/jlmd/AnimatedCircleLoadingView) by [jlmd](https://github.com/jlmd)

> _A determinate/indeterminate loading view animation.
> Based on [android-watch-loading-animation](http://www.materialup.com/posts/android-watch-loading-animation) 
> by [Nils Banner](http://www.materialup.com/NilsMedia)_

### Namespace: Xama.JTPorts.AnimatedCircleLoadingView

![!gif](https://github.com/DigitalSa1nt/Xama.JTPorts.AnimatedCircleLoadingView/blob/master/images/20190216_225349.gif?raw=true)

## Outstanding Tasks:

- [x] Ensure Interfaces are implemented as per OOP standards.

- [x] Apply standard capitalizations across the library as per .Net guidelines.

- [ ] Fix xml attributes

- [ ] Replace android 'listeners' with events.

- [x] Replace colour implementations to match Xamarin.Android property requirements.

- [ ] Ensure that non-optional parameters are all auto-populated if not supplied for the OO implementation.

- [ ] Check for missing functionality in ported library and task out implementation.

- [x] Create NuGet package.

- [x] Fill basic usage information.

# How to Install

![NuGetIcon](https://raw.githubusercontent.com/DigitalSa1nt/Xama.JTPorts.AnimatedCircleLoadingView/master/images/nugetIcon.png)

Simply add the [NuGet package](https://www.nuget.org/packages/Xama.JTPorts.RippleBackground/) directly to your Xamarin.Android solution, or use one of the following:

Package Manager:
> Install-Package Xama.JTPorts.AnimatedCircleLoadingView -Version 1.0.1

.NET CLI:
> dotnet add package Xama.JTPorts.AnimatedCircleLoadingView --version 1.0.1

## Basic Usage:

Create control in your xml layout:

```cs
<Xama.JTPorts.AnimatedCircleLoadingView.AnimatedCircleLoadingView
  android:id="@+id/circle_loading_view_indeterminate"
  android:layout_width="250dp"
  android:layout_height="250dp"
  android:background="@color/white" />
```
### Indeterminate Progress

Get control and assign the colors, **this is important** as currently the control can't infer these if not provided

```cs
AnimatedCircleLoadingView animatedCircleLoadingView = FindViewById<AnimatedCircleLoadingView>(Resource.Id.circle_loading_view_indeterminate);

animatedCircleLoadingView.MainColor = Resource.Color.colorPrimaryDark;
animatedCircleLoadingView.SecondaryColor = Resource.Color.risualOrange;
animatedCircleLoadingView.TextColor = Resource.Color.colorAccent;
animatedCircleLoadingView.CheckMarkTintColor = Color.White;
```

You can define a center body of text to sit inside of the loading view if needed

```cs
animatedCircleLoadingView.TitleText = "Loading";
```
Then simply start the animation

```cs
animatedCircleLoadingView.StartIndeterminate();
```

### Determinate Progress

Get control and assign the colors, this is important as currently the control can't infer these if not provided

```cs
AnimatedCircleLoadingView animatedCircleLoadingView = FindViewById<AnimatedCircleLoadingView>(Resource.Id.circle_loading_view_indeterminate);

animatedCircleLoadingView.MainColor = Resource.Color.colorPrimaryDark;
animatedCircleLoadingView.SecondaryColor = Resource.Color.risualOrange;
animatedCircleLoadingView.TextColor = Resource.Color.colorAccent;
animatedCircleLoadingView.CheckMarkTintColor = Color.White;
```

Start the animation

```cs
animatedCircleLoadingView.StartDeterminate();
```

Then simply set the percentage using this method (takes an integer value)

```cs
animatedCircleLoadingView.SetPercentage(50);
```

Currently when the control hits 100 percent it automatically adds the FinishedOK view, but you can also fire this manually.

```cs
animatedCircleLoadingView.StopOk();
```

or the failed view

```cs
animatedCircleLoadingView.StopFailure();
```

# Useful?

<a href="https://www.buymeacoffee.com/JTT" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/default-red.png" alt="Buy Me A Coffee" tyle="height: 41px !important;width: 174px !important;box-shadow: 0px 3px 2px 0px rgba(190, 190, 190, 0.5) !important;-webkit-box-shadow: 0px 3px 2px 0px rgba(190, 190, 190, 0.5) !important;" ></a>

_You know, only if you want to._
