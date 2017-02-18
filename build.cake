#tool nuget:?package=Wyam
#addin nuget:?package=Cake.Wyam
#addin nuget:?package=Cake.Git
#addin nuget:?package=Cake.FileHelpers

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var output = DirectoryPath.FromString("./docs");
//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Build")
    .Does(() =>
    {
        Wyam(new WyamSettings
        {
            Recipe = "Blog",
            Theme = "CleanBlog",
            UpdatePackages = true,
            OutputPath = output
        });        
    });
    
Task("Preview")
    .Does(() =>
    {
        Wyam(new WyamSettings
        {
            Recipe = "Blog",
            Theme = "CleanBlog",
            UpdatePackages = true,
            Preview = true,
            Watch = true,
            OutputPath = output
        });        
    });

Task("Debug")
    .Does(() =>
    {
        StartProcess("../Wyam/src/clients/Wyam/bin/Debug/wyam.exe",
            "-a \"../Wyam/src/**/bin/Debug/*.dll\" -r \"blog -i\" -t \"../Wyam/themes/Blog/CleanBlog\" -p --attach");
    });

Task("Deploy")
    .Does(() =>
    {
        var cname = File(output+"/CNAME");
        FileWriteText(cname, "blog.awaitWisdom.com");
        // commit
        var repo = DirectoryPath.FromString(".");
        GitAddAll(repo);
        GitCommit(repo, "jcgillespie", "jcgillespie@users.noreply.github.com", "Commit from AppVeyor");
        GitLogTip(repo);
        GitPush(repo, "a00dadac7f8bedb32c805df01e0c09022ca8eef9", "", "master");
    });
    
//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Preview");    
    
Task("AppVeyor")
    .IsDependentOn("Build")
    .IsDependentOn("Deploy");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);