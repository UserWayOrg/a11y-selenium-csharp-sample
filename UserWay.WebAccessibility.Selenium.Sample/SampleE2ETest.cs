using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using UserWay.WebAccessibility.Selenium.Model.Config;
using UserWay.WebAccessibility.Selenium.Sample.utils;
using Level = UserWay.WebAccessibility.Selenium.Model.Report.Level;

namespace UserWay.WebAccessibility.Selenium.Sample;

public class SampleE2ETest : IDisposable
{
    private const string ReportPath = "../../../uw-a11y-reports/";
    
    private readonly WebDriver _driver;
    
    public SampleE2ETest()
    {
        var options = new ChromeOptions();
        options.AddArgument("--headless");
        _driver = new ChromeDriver(options);
    }
    
    void IDisposable.Dispose()
    {
        _driver.Quit();
        RecursiveDirectoryDelete.DeleteDirectoryRecursively(ReportPath);
        GC.SuppressFinalize(this);
    }
    
    
    [Fact]
    public void SampleUserWaySeleniumTest()
    {
        // Open page
        _driver.Url = "https://userway.org/blog";

        // Prepare analysis configuration
        var analysisConfig = new AnalysisConfigBuilder()
            .WithLevel(Level.AAA)
            .IncludeBestPractices(true)
            .IncludeExperimental(true)
            .Build();

        var auditConfig = new AuditConfigBuilder()
            .Driver(_driver)
            .AnalysisConfig(analysisConfig)
            .SaveReport(true)
            .ReportPath(ReportPath)
            .Build();

        // Execute analysis on the page
        var result = AccessibilityAuditor.UserWayAnalysis(auditConfig);

        // Do assertions
        Assert.NotEqual(0, result.CountA);
        Assert.NotEqual(0, result.CountAA);
        Assert.NotEqual(0, result.CountAAA);

        var jsonDir = new DirectoryInfo(Path.Combine(ReportPath, "reports"));
        jsonDir.Exists.Should().BeTrue("The reports directory should exist.");
        jsonDir.Attributes.Should().HaveFlag(FileAttributes.Directory, "The reports path should be a directory.");
        jsonDir.GetFiles().Should().NotBeEmpty("The reports directory should not be empty.");
        
        var htmlDir = new DirectoryInfo(Path.Combine(ReportPath, "pages"));
        htmlDir.Exists.Should().BeTrue("The pages directory should exist.");
        htmlDir.Attributes.Should().HaveFlag(FileAttributes.Directory, "The pages path should be a directory.");
        htmlDir.GetFiles().Should().NotBeEmpty("The pages directory should not be empty.");

        var screenshotsDir = new DirectoryInfo(Path.Combine(ReportPath, "pages"));
        screenshotsDir.Exists.Should().BeTrue("The screenshots directory should exist.");
        screenshotsDir.Attributes.Should().HaveFlag(FileAttributes.Directory, "The screenshots path should be a directory.");
        screenshotsDir.GetFiles().Should().NotBeEmpty("The screenshots directory should not be empty.");    }

}