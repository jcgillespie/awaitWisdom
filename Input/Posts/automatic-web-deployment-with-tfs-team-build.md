Title: Automatic Web Deployment with TFS Team Build
Created: 2013-12-20T18:28:40.7510000+00:00
Published: 2014-03-01T00:59:04.1660000+00:00
Tags: 
 - Tech
 - Migrated
 - Web Deploy
 - MSBuild
 - MSDeploy
 - Web Publish Series
---
If I have to do something three times, it gets automated. The first time I'm just figuring out how to make it work. The second time ensures I know the process and can repeat it without all the exploration. The third time it gets automated and I can stop thinking about it.

Everytime I touch something, there is the chance for error. An automated process should be perfectly repeatable. Once you get it just right, you can back away and leave it alone to run happily.

If someone from your team has to manually deploy your application, there is a better way.

In the past, we could [Publish with MS Deploy][Chris Kadel]. It simplified things, but you still had some hurdles to jump. You needed to edit your projects manually and add build configurations for each environment. This is still a valid means of automating deployment in your team build, but it is more painful than necessary.

## TFS Team Build
Microsoft has a build system as part of the Team Foundation set of tools. The on-premise version is called [Team Foundation Server][TFS], or there is an "in the cloud" service called [Visual Studio Online][VSOnline]. Both flavors offer build capabilities, which I'll refer to jointly as TFS Team Build.

TFS Team Build offers a lot of power and flexibility, but we can leverage the [publish profiles][Publish Profiles] I introduced earlier to deploy a website or web app with just a couple of parameters.

###Create a build definition
Creating a TFS Team Build definition is pretty simple. Here is an extremely quick run through with just the bare minimum so we can get to the deployment part.
1\. From the Team Explorer, go the the Builds tab (`Ctrl+0, B`) and click "New Build Definition".
2\. On the General tab, give your build a name.
![TFS Build Definition General Tab](/content/images/2014/Mar/Build_General_Tab.PNG)
3\. On the Source Settings tab, select the source control folder that has your solution/web project. 
4\. On the Process tab, pick your Build Process template (I'm using "TfvcTemplate.12.xaml". Select your project to build.

**This is the important part**. Still on the Process tab, drop down to the MSBuild Arguments line (maybe be hidden under an Advanced toggle).  The exact location may vary according to your Build Process Template.

Add `/p:DeployOnBuild=true;PublishProfile=<Publish Profile Name>`. The Publish Profile Name does NOT include the .pubxml.

Here's what mine looked like 
![TFS Build Definition Process Tab](/content/images/2014/Mar/Build_Process_Tab.PNG)

That's it! A very simple build definition will get the job done.

## Web Publish Series
+ [Config transformations](http://awaitwisdom.com/publish-profile-config-transform/) 
+ [Publishing Web Site Projects](http://awaitwisdom.com/publishing-website-projects)
+ [Automatic deployment with TFS Team Build](http://awaitwisdom.com/automatic-web-deployment-with-tfs-team-build)
+ Set up your web server for web deployment.
+ Using publishsettings files to publish to Azure (and other hosting providers)

##References
* [Publishing with MS Deploy][Chris Kadel]
* [Microsoft Team Foundation Server][TFS]
* [Visual Studio Online][VSOnline]
* [Intro to web publish profiles][Publish Profiles]
* [Web.config transforms with Publish Profiles][Web.config transforms]

[Chris Kadel]:http://chriskadel.com/2013/03/using-tfs-to-build-and-deploy-during-the-build-process-with-ms-deploy/
[TFS]:http://msdn.microsoft.com/en-us/vstudio/ff637362.aspx
[VSOnline]:http://www.visualstudio.com/products/visual-studio-online-overview-vs
[Publish Profiles]:http://awaitwisdom.com/intro-to-web-publish-profiles/
[Web.config transforms]:http://awaitwisdom.com/publish-profile-config-transform/