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
- [License](#license)

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
