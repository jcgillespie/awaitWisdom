Title: Web.Config transforms with Publish Profiles
Published: 2013-12-20
Tags: 
 - Deployment
 - MSDeploy
 - Web Publish Series
---
When you create a publish profile, you may notice that there is an option to replace the connection string on the Settings tab.
![Publish dialog settings tab](/content/images/2013/Dec/PublishSettings.PNG)
You can specify a connection string there and it will (optionally) update the destination web.config.

But what if you have more than connection strings to change? What about <appSettings>? It is a lesser known feature, but publish profiles support config transformations.

When you create a new web application, a web.config transforms is added for both default build configurations, Debug and Release.
![Default web.config transforms](/content/images/2013/Dec/DefaultConfigTransforms.PNG)

We're going to add another transform that matches the publish profile we setup in that previous post. If you don't have a publish profile setup yet, check out [my previous post](http://awaitwisdom.com/intro-to-web-publish-profiles/)
## Step 1: Add a .config file.
**&lt;Edit&gt;**
There is an even easier way to do this. You can also select "Add Config Transform" from the context menu of the publish profile file (.pubxml file).
![](/content/images/2014/Jan/AddConfigTransform.PNG)
**&lt;/Edit&gt;**

Add a new item to your web project using the Project menu.
![Add New Item...](/content/images/2013/Dec/AddNewItem.PNG)
Select "Web Configuration File" and name it "Web.<publish profile name>.config". My profile is named "Dev", so my file is named "Web.Dev.config".
![new config file](/content/images/2013/Dec/AddNewConfig.PNG)

## Step 2: Complete the config transformation
You need to add the document transform namespace to your new config file. This goes on the configuration root element and will look like this. `<configuration xmlns:xdt="http://schemas.microsoft.com/XML-Document-Transform">`. With that complete, add the rest of your transformation. In my case, I want to remove the debug compilation attribute and change an appSetting.
![web.dev.config](/content/images/2013/Dec/web_dev_config.png)

## Step 3: Validate/Publish
If you select your new config transform file and right-click to see the context menu, you'll see a "Preview Transform" option, which shows you exactly how this will be applied.
![Web.config transform preview](/content/images/2013/Dec/TransformPreview.png)

And because the transform name matches the publish profile, it will be applied during the publish. I published my site and opened the resulting web.config to confirm the changes.
![Transformed config](/content/images/2013/Dec/TransformedConfig.png)

## Extra Credit: Properly nested config transforms
**&lt;Edit&gt;**
If you add your config transformations via the context menu option detailed in the edit above, this may already be done for you. It appears the Web Application Projects will nest the config, but Web Site Projects will not.
**&lt;/Edit&gt;**

This is totally unneccesary, but if you're like me, you'd rather the new config transform was nested under the main config with the other transforms.

![Not Nested](/content/images/2013/Dec/NotNested.png)

To "fix" this, unload your web project and edit the proj file. Find your transform, which should look something like `<Content include="Web.Dev.config" />` and add a child <DependentUpon> element pointing to the main config.
![Edited project file](/content/images/2013/Dec/EditedProject.png)
Reload the application and you'll find it properly nested under the main file.
![Properly Nested](/content/images/2013/Dec/nested.png)
Much Better!

## Web Publish Series
+ [Config transformations](http://awaitwisdom.com/publish-profile-config-transform/) 
+ [Publishing Web Site Projects](http://awaitwisdom.com/publishing-website-projects)
+ [Automatic deployment with TFS Team Build](http://awaitwisdom.com/automatic-web-deployment-with-tfs-team-build)
+ Set up your web server for web deployment.
+ Using publishsettings files to publish to Azure (and other hosting providers)

## References
* [Web.config Transformation Syntax for Web Project Deployment Using Visual Studio](http://msdn.microsoft.com/en-us/library/dd465326.aspx)