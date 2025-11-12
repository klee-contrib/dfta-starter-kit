var oClient = "KleeContrib";
var oProjet = "DFTA";
var oApplication = "Dfta";

if (args.Length < 3)
{
    Console.WriteLine(
        "Vous devez spécifier un nom de client, un nom de projet et un nom d'application."
    );
    return 1;
}

var client = args[0];
var projet = args[1];
var application = args[2];

Console.WriteLine($"Nom de client : {client} (remplacera {oClient})");
Console.WriteLine($"Nom de projet : {projet} (remplacera {oProjet})");
Console.WriteLine($"Nom d'application : {application} (remplacera {oApplication})");
Console.WriteLine("Appuyer sur une touche pour confirmer");
Console.ReadKey();

foreach (var file in Directory.GetFiles("./", "*", SearchOption.AllDirectories))
{
    if (file.Contains(oClient) || file.Contains(oApplication))
    {
        var newFile = file.Replace(oClient, client).Replace(oApplication, application);
        var newDir = new FileInfo(newFile).DirectoryName!;
        Directory.CreateDirectory(newDir);
        File.Move(file, newFile, true);
    }
}

foreach (var file in Directory.GetFiles("./", "*", SearchOption.AllDirectories))
{
    if (file.Contains(oClient) || file.Contains(oApplication))
    {
        var newFile = file.Replace(oClient, client).Replace(oApplication, application);
        var newDir = new FileInfo(newFile).DirectoryName!;
        Directory.CreateDirectory(newDir);
        File.Move(file, newFile, true);
    }
}

foreach (
    var dir in Directory.GetDirectories(
        "./sources/back",
        $"{oClient}.{oApplication}.*",
        SearchOption.AllDirectories
    )
)
{
    Directory.Delete(dir, true);
}

foreach (
    var file in new[]
    {
        Directory.EnumerateFiles("./", "*.cs*", SearchOption.AllDirectories),
        Directory.EnumerateFiles("./model", "topmodel.lock", SearchOption.TopDirectoryOnly),
        Directory.EnumerateFiles("./model", "topmodel.config", SearchOption.TopDirectoryOnly),
        Directory.EnumerateFiles("./", "*.md", SearchOption.AllDirectories),
        Directory.EnumerateFiles("./", "*.sh", SearchOption.AllDirectories),
        Directory.EnumerateFiles("./infra", "*.tf*", SearchOption.AllDirectories),
        Directory.EnumerateFiles("./pipelines", "*.yml", SearchOption.AllDirectories),
        Directory.EnumerateFiles("./sources", "docker-compose.yml", SearchOption.TopDirectoryOnly),
        Directory.EnumerateFiles("./sources/back", "*.sln", SearchOption.TopDirectoryOnly),
        Directory.EnumerateFiles("./sources/back", "*.json", SearchOption.AllDirectories),
    }.SelectMany(x => x)
)
{
    var content = File.ReadAllText(file);
    if (
        content.Contains(oProjet)
        || content.Contains(oClient, StringComparison.InvariantCultureIgnoreCase)
        || content.Contains(oApplication, StringComparison.InvariantCultureIgnoreCase)
    )
    {
        File.WriteAllText(
            file,
            content
                .Replace(oProjet, projet)
                .Replace(oClient, client)
                .Replace(oClient.ToLower(), client.ToLower())
                .Replace(oApplication, application)
                .Replace(oApplication.ToLower(), application.ToLower())
        );
    }
}

return 0;
