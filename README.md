# Astronomy Picture Of The Day
A standard library for calling the NASA Astronomy Picture Of The Day web service

![.NET Core](https://github.com/vb2ae/AstronomyPictureOfTheDay/workflows/.NET%20Core/badge.svg)

[![CodeQL](https://github.com/Ken-Tucker/AstronomyPictureOfTheDay/actions/workflows/codeql.yml/badge.svg)](https://github.com/Ken-Tucker/AstronomyPictureOfTheDay/actions/workflows/codeql.yml)

![badge](https://img.shields.io/endpoint?url=https://gist.githubusercontent.com/vb2ae/870c49615ed7c8b1af25a0f5f0d8f7a4/raw/nasa-
[![CodeFactor](https://www.codefactor.io/repository/github/ken-tucker/AstronomyPictureOfTheDay/badge)](https://www.codefactor.io/repository/github/ken-tucker/AstronomyPictureOfTheDay)code-coverage.json)

#How to use the library

The library has 2 methods GetTodaysPictureAsync and GetMarsPictureAsync.  These methods allow you to get pictures from Nasa from one of the mars rovers or there astronomy picture of the day.

You will need an api key from the Nasa website to use the apis.  You can get a key at https://api.nasa.gov/


# GetTodaysPictureAsync
  Required parameters ApiKey which you get from the nasa web site
  
This method will return an PictureOfTheDayResponse.  The Success property will tell you if the call was sucessful.   Exception property will return any exceptions when you try and get the daily picture.

The PictureofTheDay property will include information about the picture like the title, and url of image.  


# GetMarsPictureAsync 

This method requires ApiKey like the method above.
You will need to select which Rover you would like a picture from.  
You will need to select the image date you would like see pictures for.

The return list the above method.  The pictures returned fill be from the following list of available cameras on the rovers.

Abbreviation	Camera

FHAZ	Front Hazard Avoidance Camera

RHAZ	Rear Hazard Avoidance Camera	

MAST	Mast Camera		

CHEMCAM	Chemistry and Camera Complex

MAHLI	Mars Hand Lens Imager	

MARDI	Mars Descent Imager	

NAVCAM	Navigation Camera	

PANCAM	Panoramic Camera

MINITES	Miniature Thermal Emission Spectrometer (Mini-TES)		

# Example app

A WPF example is included in the source code
