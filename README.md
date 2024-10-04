# UserWay C# Selenium accessibility testing example

This is a sample project which shows use cases of UserWay's
C# Selenium Accessibility testing integration.

To use UserWay Selenium testing solution you should first of all
install all the required dependencies:

Selenium WebDriver
```shell
dotnet add package Selenium.WebDriver
```

UserWay Integration
```shell
dotnet add package Userway.WebAccessibility.Selenium
```

To run tests you should prepare analysis configuration
and execute analysis with it. You can find example of test run
in file `SampleE2ETest`

And then you can run Manual-mode tests only:
```shell
dotnet test
```