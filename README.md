# COCUS Weather Challenge

A cross-platform weather application built with .NET MAUI using the Open-Meteo APIs.

## Features

* Search weather by city name
* Current weather information
* 5-day forecast
* Offline handling with cached results
* Light/Dark mode support
* MVVM architecture
* Dependency Injection
* Unit tests for ViewModels
* Clean and testable architecture

---

# Technologies

* .NET MAUI (.NET 10)
* CommunityToolkit.MVVM
* Open-Meteo API
* xUnit
* Moq

---

# Architecture

The application follows a lightweight clean architecture approach.

## Projects

### CocusWeatherChallenge

MAUI application project responsible for:

* UI
* Navigation
* Platform-specific services

### CocusWeatherChallenge.Core

Core library containing:

* ViewModels
* Models
* Interfaces
* Business logic

### CocusWeatherChallenge.Tests

Unit test project containing ViewModel tests using mocked services.

---

# Architectural Decisions

## MVVM

The application uses the MVVM pattern to separate UI from business logic and improve maintainability and testability.

## Dependency Injection

Services are injected into ViewModels through interfaces, reducing coupling and improving testability.

## Network-first cache strategy

When internet connectivity is available, the app prioritizes fresh data from the API and updates the local cache.

When offline, the app falls back to the latest cached result if available.

## Offline handling

The application detects connectivity state and gracefully handles offline scenarios without crashing.

## Unit Testing

ViewModels are tested using xUnit and Moq with mocked dependencies such as:

* navigation
* connectivity
* API services
* cache services

---

# Running the Project

## Requirements

* Visual Studio 2022
* .NET 10 SDK
* MAUI workload installed

## Run

1. Open the solution in Visual Studio 2022
2. Set `CocusWeatherChallenge` as startup project
3. Run on Android emulator or physical device

Alternatively, a prebuilt APK is available in the `/apk` folder for quick testing.

---

# APIs

The project uses:

* Open-Meteo Weather API
* Open-Meteo Geocoding API

---

# Future Improvements

Possible future improvements:

* SQLite cache instead of Preferences
* Search history
* Favorite cities
* Weather icons
* Better animations
* Localization support
