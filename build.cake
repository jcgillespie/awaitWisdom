#tool nuget:?package=Wyam
#addin nuget:?package=Cake.Wyam
#addin nuget:?package=Cake.Git

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");

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
            UpdatePackages = true
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
            Watch = true
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
        // move the files outside the git repo
        var repo = DirectoryPath.FromString(".");

		DeleteDirectory("../output/", recursive:true);
        MoveDirectory("./output/", "../output/");
        
        // switch branch
        GitCheckout(repo, "gh-pages");
        var branch = GitBranchCurrent(repo);

        if(branch.FriendlyName != "gh-pages") {
            throw new Exception("wrong branch");
        }

        // clean everything up
		Func<IFileSystemInfo, bool> excludeGit = fsi =>!fsi.Path.FullPath.EndsWith(".git", StringComparison.OrdinalIgnoreCase);
		CleanDirectories(repo, excludeGit);
		MoveDirectory("../output/", "../output/");
        // copy the output back in

		CopyFiles("../output/.*",  repo, true);		
        // commit
        // string token = EnvironmentVariable("NETLIFY_DAVEAGLICK");
        // if(string.IsNullOrEmpty(token))
        // {
        //     throw new Exception("Could not get NETLIFY_DAVEAGLICK environment variable");
        // }
        
        // This uses the Netlify CLI, but it hits the 200/min API rate limit
        // To use this, also need #addin "Cake.Npm"
        // Npm.Install(x => x.Package("netlify-cli"));
        // StartProcess(
        //    MakeAbsolute(File("./node_modules/.bin/netlify.cmd")), 
        //    "deploy -p output -s daveaglick -t " + token);

        // Upload via curl and zip instead
        // Zip("./output", "output.zip", "./output/**/*");
        // StartProcess("curl", "--header \"Content-Type: application/zip\" --header \"Authorization: Bearer " + token + "\" --data-binary \"@output.zip\" --url https://api.netlify.com/api/v1/sites/daveaglick.netlify.com/deploys");
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