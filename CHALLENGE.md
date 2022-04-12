![FullStack Labs](/assets/FSL-logo-portrait.png)

# Before You Start

Please follow the [`Getting Started` section on README](./README.md) to set up
your environment before starting the challenge.

# Summary

For this code challenge a Candidate will clone and setup an existing .NET MVC
application. The application will contain routes, migrations, models, and
minimal views but with no actual functionality created. The candidate will show
all her/his expertise building apps with the .NET framework and problem
solving skills.

# Overview

HeyURL! is a service to create awesome friendly URLs to make it easier for
people to remember. Our team developed some mock views but they lack our awesome
functionality.

# Requirements

1. Implement actions to create a short URL based on a given full URL
1. If URL is not valid, the application returns an error message to the user
1. We want to be able to provide basic click metrics to our users for each URL they generate.
   1. Every time that someone clicks a short URL, it should record that click
   1. the record should include the user platform and browser detected from the user agent
1. We want to create a metrics panel for the user to view the stats for every short URL.
   1. The user should be able to see total clicks per day on the current month
   1. An additional chart with a breakdown of browsers and platforms
1. If someone tries to visit a invalid short URL then it should return a 404 page
1. Business logic and requirements should be tested with NUnit
1. Provide EF migrations that can generate a SQL database schema compatible with the models

# Spec for generating short URLs

- It MUST have 5 characters in length e.g. NELNT
- It MUST generate only upper case letters
- It MUST NOT generate special characters
- It MUST NOT generate whitespace
- It MUST be unique
- `ShortUrl` field should store only the generated code

# Recommendations

1. Check routes and actions defined in [`Controllers/UrlsController.cs`](./hey-url-challenge-code-dotnet/Controllers/UrlsController.cs)
1. Check views in [`Views/Urls`](./hey-url-challenge-code-dotnet/Views/Urls)
1. Check existing tests in [`tests`](./tests)
1. Google Charts is already added to display charts but you can use any library
1. Use the [`BrowserDetector` nuget package](https://github.com/kshyju/BrowserDetector) already installed
   to extract device information about each click
1. `Microsoft.EntityFrameworkCore.InMemory` is used to simplify application setup and development, but you can
   use a local instance of a relational database of your choice if you wish.

# Pages

The following pages/urls are already built into our app:

1. `GET /`: Contains the form and a list of the last 10 URL created with their
   click count
1. `GET /:url`: Redirects from a short URL to the original URL and should also
   track the click event
1. `GET /urls/:url`: Shows the metrics associated to the short URL

# API - Optional Bonus Points

We would like to have a way to retrieve the last 10 URLs created using an API
endpoint. It should be [JSON-API](https://jsonapi.org/) compliant. There are packages
like [JsonApiDotNetCore](https://www.jsonapi.net/index.html) that can help configure
this format.

Here is an example of a response from the API:

```
{
  "data": [
    {
      "type": "urls",
      "id": "1",
      "attributes": {
        "created-at": "2018-08-15T02:48:08.642Z",
        "original-url": "www.fullstacklabs.co/angular-developers",
        "url": "https://domain/fss1",
        "clicks": 2
      },
      "relationships": {
        "clicks": {
          "data": [
            {
              "id": 1,
              "type": "clicks"
            }
          ]
        }
      }
    }
  ],
  "included": [
    {
      "type": "clicks",
      "id": 1,
      "attributes": {
        ...
      }
    }
  ]
}
```

# Scoring

- Completed functionality 65%
- Completed NUnit tests 20%
- Completed bonus 15%
