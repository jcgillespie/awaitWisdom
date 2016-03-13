Title: Publishing Web Site Projects
Published: 2013-12-20
Tags: 
 - Programming
 - Web Deploy
 - MSBuild
 - Deployment
 - MSDeploy
---
If you've worked with Web Site Projects (WSPs), you know they can be somewhat limited. Deployment is one area where they lagged behind Web Application Projects (WAPs), but it appears that has been updated. 

**Quick aside**: if you're not familiar with the differences between WSPs and WAPS, see [this][WAPvsWSP] link from the References section below.

> TL;DR version: Publish Profiles work with Web Site Projects, not just Web Application Projects.

I hadn't worked with WSPs in a while so I missed the news, but publish profiles can be used with WSPs starting in Visual Studio 2012's [Web Tools 2012.2 release][WebTools1012.2].

##Publishing a Web Site
Creating a publish profile couldn't be easier. It is the same experience as [Creating a publish profile for Web Application Projects][PublishProfile]. The differences are in the implementation, not the user experience.

Publish Profiles for Web Application Projects are stored in a PublishProfiles folder under the Properties. 
![Publish profile in WAP](/content/images/2013/Dec/ProjectChanges.PNG)
Since websites don't have a Properties folder, they are instead stuck in App_Data.
![Publish Profile in Website](/content/images/2014/Feb/websitepublishprofile.PNG)
The XML in the .pubxml file is exactly the same, but websites have an extra component. Since websites don't have a .*proj file (e.g. .csproj) for MSBuild to use, a new file named "website.publishproj" is added to the root of your site for you.
![website.publishproj](/content/images/2014/Feb/website_publishproj.png)
This is a standard MSBuild that lists project references and whatnot.  When you publish your site, this is what MSBuild will use to compile your website. Next, it hands the compiled site and your selected publish profile off to MSDeploy to do the actual deployment.

## Using .dll.refresh files
I've been snagged by a small problem with .refresh files and publish profiles. The default website.publishproj doesn't appear to "respect" .refresh files, meaning needed /bin references won't be picked up. wongweava

This appears to be a known [bug][CopyConditionBug] for [.refresh Files][RefreshFile]. The workaround is a quick change to the website.publishproj.
![.publishproj fix](/content/images/2014/Feb/TargetFix.PNG)
Just after the `<Import Project="$(_WebPublishTargetsPath)\Web\Microsoft.WebSite.Publishing.targets" />` line at the end of the file, add the following:

      <Target Name="CopyRefreshFiles" AfterTargets="_ResolveAssemblyReferencesWithRefreshFile">
        <Copy SourceFiles="@(References->'%(FullPath)')" DestinationFolder="$(_FullSourceWebDir)\Bin\" Condition="Exists('%(References.Identity)')" ContinueOnError="true" SkipUnchangedFiles="false" Retries="$(CopyRetryCount)" RetryDelayMilliseconds="$(CopyRetryDelayMilliseconds)" />
      </Target>`

## Web Publish Series
+ [Config transformations](http://awaitwisdom.com/publish-profile-config-transform/) 
+ [Publishing Web Site Projects](http://awaitwisdom.com/publishing-website-projects)
+ [Automatic deployment with TFS Team Build](http://awaitwisdom.com/automatic-web-deployment-with-tfs-team-build)
+ Set up your web server for web deployment.
+ Using publishsettings files to publish to Azure (and other hosting providers)

##References
* [Web Application Projects versus Web Site Projects in Visual Studio][WAPvsWSP]
* [Web Publish Updates with Windows Azure SDK 1.8][AzureSdk]
* [Released: ASP.NET and Web Tools 2012.2 in Context][WebTools1012.2]
* [A Quick Introduction to Web Publish Profiles][PublishProfile]
* [Copy Condition Bug][CopyConditionBug]
* [Refresh File References are not resolved][RefreshFile]

[AzureSdk]:http://blogs.msdn.com/b/webdev/archive/2012/11/20/new-web-publish-updates.aspx "Web Publish Updates with Windows Azure SDK 1.8"
[WAPvsWSP]:http://msdn.microsoft.com/en-us/library/dd547590.aspx "Web Application Projects versus Web Site Projects in Visual Studio"
[WebTools1012.2]:http://www.hanselman.com/blog/ReleasedASPNETAndWebTools20122InContext.aspx
[PublishProfile]:awaitwisdom.com/intro-to-web-publish-profiles/
[CopyConditionBug]:http://social.msdn.microsoft.com/Forums/vstudio/en-US/37acf947-c598-4230-bb79-36ba7e45927d/microsoftwebsitepublishingtargets-resolveassemblyreferenceswithrefreshfile-copy-condition-bug?forum=msbuild
[RefreshFile]:https://connect.microsoft.com/VisualStudio/feedback/details/811149/refresh-file-references-are-not-resolved-when-publishing-a-web-site-project  