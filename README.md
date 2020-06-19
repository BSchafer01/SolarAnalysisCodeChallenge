# SolarAnalysisCodeChallenge

[![Current Version](https://img.shields.io/badge/version-1.0.0-green.svg)](https://github.com/BSchafer01/SolarAnalysisCodeChallenge) [![Live Demo](https://img.shields.io/badge/demo-online-green.svg)](https://cfsolaranalysis.azurewebsites.net/)

## Table of Contents
* [General info](#general-info)
* [Technologies](#technologies)
* [Setup](#setup)


## General info
This project was developed as an entry for the CoderFoundry Sunset Hills coding challenge. The main goal of the challenge is to accept an array of building heights and determine which buildings the sunset is visible from.

### Features
* Accepts an unlimited number of buildings with: 
    + Unique labels
    + Building Heights
    + Building Widths
* Determines which buildings will see the sunset, sunrise, both, or neither
* Plots the results in a stepped filled area chart
* Displays the building data in an easy to read table

* Accepts an address/postal code and date
* Pulls the total daylight time from sunrise-sunset.org's API
* Calculates the total sun exposure based off shadows of other buildings in the line `shadow at roof level = (TallBuildingHeight - ShortBuildingHeight)/tan(SunAngle)`
* Plots the total morning sun time and the total afternoon sun time for each building on a bar chart
 
 
## Technologies
Project is created with:
* .Net Core 3.1 Blazor Server-side
* .Net Standard Class Library
* [Google Maps API](https://cloud.google.com/maps-platform/)
* [Sunrise-Sunset.org API](https://sunrise-sunset.org/)
* [Syncfusion Blazor components](https://www.syncfusion.com/blazor-components)


## Setup

To run this project locally:
* clone this repository
* replace the redacted Syncfusion license with your Syncfusion community license
* replace the Google geocoding API key with your Google API key
