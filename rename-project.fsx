open System
open System.IO
open System.Linq

let oClient = "KleeContrib"
let oProjet = "DFTA"
let oApplication = "Dfta"

if fsi.CommandLineArgs.Length < 4 then 
    printfn "Vous devez spécifier un nom de client, un nom de projet et un nom d'application"
    exit 1

let client = fsi.CommandLineArgs[1]
let projet = fsi.CommandLineArgs[2]
let application = fsi.CommandLineArgs[3]

printfn $"Nom de client : {client} (remplacera {oClient})"
printfn $"Nom de projet : {projet} (remplacera {oProjet})"
printfn $"Nom d'application : {application} (remplacera {oApplication})"
printfn "Appuyer sur une touche pour confirmer"
Console.ReadKey() |> ignore

for file in Directory.GetFiles("./", "*", SearchOption.AllDirectories) do
    if file.Contains(oClient) || file.Contains(oApplication) then
        let newFile = file.Replace(oClient, client).Replace(oApplication, application)
        let newDir = (new FileInfo(newFile)).DirectoryName
        Directory.CreateDirectory(newDir) |> ignore
        File.Move(file, newFile, true) |> ignore

for dir in Directory.GetDirectories("./sources/back", $"{oClient}.{oApplication}.*", SearchOption.AllDirectories) do
    Directory.Delete(dir, true) |> ignore


let files = 
    [|
        ("./", "*.cs*", SearchOption.AllDirectories)
        ("./model", "topmodel.lock", SearchOption.TopDirectoryOnly)
        ("./model", "topmodel.config", SearchOption.TopDirectoryOnly)
        ("./", "*.md", SearchOption.AllDirectories)
        ("./", "*.sh", SearchOption.AllDirectories)
        ("./infra", "*.tf*", SearchOption.AllDirectories)
        ("./pipelines", "*.yml", SearchOption.AllDirectories)
        ("./sources", "docker-compose.yml", SearchOption.TopDirectoryOnly)
        ("./sources/back", "*.sln", SearchOption.TopDirectoryOnly)
        ("./sources/back", "*.json", SearchOption.AllDirectories)
    |]
    |> Seq.collect Directory.EnumerateFiles
    |> Seq.toList

for file in files do
    let content = File.ReadAllText(file)
    if content.Contains(oProjet) || content.ToLower().Contains(oClient.ToLower()) || content.ToLower().Contains(oApplication.ToLower()) then
        File.WriteAllText(
            file, 
            content
                .Replace(oProjet, projet)
                .Replace(oClient, client)
                .Replace(oClient.ToLower(), client.ToLower())
                .Replace(oApplication, application)
                .Replace(oApplication.ToLower(), application.ToLower()))