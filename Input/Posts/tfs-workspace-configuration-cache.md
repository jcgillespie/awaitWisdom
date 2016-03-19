Title: TFS Workspace configuration cache
Created: 2013-12-13T19:39:13.4220000+00:00
Published: 2013-12-13T21:09:32.7190000+00:00
Tags:
 - Tech
 - Migrated
---
I have a client that recently upgraded their Team Foundation Server 2012 to 2013 and moved to new hardware in the process. Their TFS 2012 instance has been decommissioned and they hit a small workspace snag.

Developers were able to add the new server without issue, but when they tried to map the new workspace to the same local folders they used with the old workspace, they got an error.

> The path &lt;local filepath&gt; is already mapped in workspace &lt;workspace name&gt; [&lt;project collection url&gt;]

![Workspace Mapping Error Example](/content/images/2013/Dec/WorkspaceMapError.PNG)

They tried to use `tf workspace /delete` to remove the orphaned mapping. Which (correctly) returned that the old server no longer exists.
![tf workspace /delete screenshot](/content/images/2013/Dec/TfWorkspaceDelete.PNG)

So what gives? How can a non-existent server hold onto a local location? TFS caches your workspace settings to prevent just this sort of thing. Mapping multiple team projects onto the same local folder is asking for trouble.

The solution is to add an "s".
![tf workspaces /?](/content/images/2013/Dec/TfWorkspaces.PNG)
 Tf workspace**s** addresses just this problem and 
you can use it to clear out the locally cached information. The various options give the option to remove workspaces surgically

`tf workspaces /remove:MyWorkspace /collection:"http://doesnotexist:8080/tfs/Collection`

or you can simply scorch the earth and remove the entire local cache.
`tf workspaces /remove:*`

This tool isn't removing workspace mappings from the Team Foundation Server, just editing out your local cache, so either is fine.