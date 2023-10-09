# WeatherForecastAPI

The Weather Forecast API is a .NET application that retrieves weather data from the OpenWeatherMap API and provides average temperature information for specified cities within a given time frame.

## Table of Contents

- [Introduction](#introduction)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Usage](#usage)
  - [API Endpoints](#api-endpoints)
  - [Configuration](#configuration)
- [Contributing](#contributing)

## Introduction

The Weather Forecast API is designed to fetch and display average temperature data for selected cities based on OpenWeatherMap's weather forecasts. It helps users obtain weather information easily and efficiently.

## Getting Started

### Prerequisites

Before you can use the Weather Forecast API, you need to have the following prerequisites installed:

- .NET 6 SDK
- An OpenWeatherMap API key (Sign up and get your API key [here](https://openweathermap.org/api))

### Installation

1. Clone this repository to your local machine:

   ```bash
   git clone https://github.com/todorbelic/WeatherForecastAPI.git
   cd WeatherForecastAPI
   dotnet run

## Usage

### API Endpoints

### Get Available Cities

Returns a list of available cities for weather forecasting.

- **Endpoint**: `GET /api/getCities`

### Get Average Temperatures

Returns the average temperatures for specified cities within a given time range.

- **Endpoint**: `GET /api/getAverageTemperatures`

**Query Parameters:**

- `cityIds`: List of city IDs (e.g., 1, 2, 3)
- `startTime`: Range start time (MM/dd/yyyy HH:mm)
- `endTime`: Range end time (MM/dd/yyyy HH:mm)
- `ascending`: Boolean indicating ascending or descending order

### Configuration

To configure the API, you can modify the appsettings.json file. Make sure to provide your OpenWeatherMap API key in the configuration.

1. Configure CityOptions in appsettings.json
   ```bash
   "CityOptions": {
    "GeocodingApiUrl": "http://api.openweathermap.org/geo/1.0/direct?q={city name},{country code}&limit={limit}&appid={API key}",
    "ApiKey": "YOUR_API_KEY",
    "AvailableCities": [
      {
        "Name": "Novi Sad",
        "ZipCode": "21000",
        "CountryCode": "RS"
      },
      {
        "Name": "Valjevo",
        "ZipCode": "14000",
        "CountryCode": "RS"
      },
      {
        "Name": "Sombor",
        "ZipCode": "25000",
        "CountryCode": "RS"
      }
    ]
   },
   ```
    You can use other Cities if you provide valid Names and Zip Codes, after that you have to update the database by running following
    commands in package manager console
    ```bash
    add-migration NameOfYourMigration
    update-database
    ```
    After that, your cities should be added to the database
  
2. Configure Weather Data Sync Options in appsettings.json
     ```bash
     "WeatherDataSyncOptions": {
      "OpenWeatherApiUrl": "http://api.openweathermap.org/data/2.5/forecast?lat={latitude}&lon={longitude}&units=metric&appid={API key}",
      "ApiKey": "YOUR_API_KEY",
      "DaysForecasted": 5,
      "SyncFrequencyHours": 3
     }
     ```
    With these settings new weather forecast data is fetched every 3 hours using background service in OpenWeatherAPIClient Package -> WeatherDataSyncService.cs
    and old data is invalidated.
    Anti-corruption Layer is used to translate data fetched to Model classes.

## Contributing
We welcome contributions from the community. If you want to contribute to the Weather Forecast API, please follow our contribution guidelines.
