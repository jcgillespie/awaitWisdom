Title: A Quick Introduction to Web Publish Profiles
Created: 2013-12-20T18:17:19.1670000+00:00
Published: 2013-12-20T18:25:17.2230000+00:00
Tags: 
 - Web Deploy
 - MSBuild
 - MSDeploy
 - Web Publish Series
---
Deployment used to be a big hassle. Copy/Paste to UNC folder paths. XCopy in the command line. Batch scripts to string them together. Error prone, brittle, and annoying. 

Deployment has evolved, but I've found a lot of people don't know about Publish Profiles or what they can do these days, so here we go.

There is a lot of shiny goodness to be had in publish profiles, including publishing web site projects, publishsettings files from hosting providers, and more, which I may cover in future posts. For now, here's a quick introduction to creating profiles. For the purposes of this post, I'll assume you want to use Web Deploy.

### Create a new profile
I've got a vanilla File > New... > Project web application project. You can get to the publish dialog from the Build menu...

![build menu publish option](/content/images/2013/Dec/BuildMenu.PNG)
 or the project context menu...
![web application project context menu publish option](/content/images/2013/Dec/PublishDialog.PNG)
Which gets you access to the Publish dialog. We're going to start by creating a new publish profile, called "Dev".
![Publish Web Dialog - adding a new profile](/content/images/2013/Dec/PublishDialogWindow.PNG)

### Add connection details
![Publish connection details](/content/images/2013/Dec/PublishConnection.PNG)
For a complete breakdown of the options on this screen, see the references section below. I've selected "Web Deploy" and plugged in the relevant server/site details.

### Settings
In the settings section you can select build configuration and publish options. Note that you can also configure Entity Framework Code First Migrations and connection string replacement here.  We'll come back to that in a future post.

![Publish Settings Tab](/content/images/2013/Dec/PublishSettings.PNG)

### Preview/Publish
The Preview tab gives allows you to (optionally) see what would happen if you ran the deployment without changing anything. This is a good way to not only verify the settings, but see exactly what files the deployment sees as needing an update.
![Publish Preview tab](/content/images/2013/Dec/PublishPreview.PNG)
From here you can also click "Publish" to deploy, or "Close" will prompt you to save the profile.

## Project Changes
So what did all this do to your project? If you expand the Properties folder, you'll see a new "PublishProfiles" folder.
![New PublishProfiles folder](/content/images/2013/Dec/ProjectChanges.PNG)
If you open up that "Dev.pubxml" file, you'll find ordinary XML.
![PubXml Snippet](/content/images/2013/Dec/pubxml.PNG)
If you're accustomed to MSBuild project syntax, that XML will look familiar.

Including the publish files in the project means it can be source controlled and everyone uses the same settings. Another dev pulling down this project will have the "Dev" publish settings already setup.

## Web Publish Series
+ [Config transformations](http://awaitwisdom.com/publish-profile-config-transform/) 
+ [Publishing Web Site Projects](http://awaitwisdom.com/publishing-website-projects)
+ [Automatic deployment with TFS Team Build](http://awaitwisdom.com/automatic-web-deployment-with-tfs-team-build)
+ Set up your web server for web deployment.
+ Using publishsettings files to publish to Azure (and other hosting providers)

## References
* [Web Deployment Overview for Visual Studio and ASP.NET](http://msdn.microsoft.com/en-us/library/dd394698.aspx)
* [How to: Deploy a Web Project Using One-Click Publish in Visual Studio](http://msdn.microsoft.com/en-us/library/dd465337.aspx)



















