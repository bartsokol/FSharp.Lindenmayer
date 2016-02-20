#r @"packages/FAKE/tools/FakeLib.dll"
open Fake

RestorePackages ()

let buildDir = "./build/"

Target "Clean" (fun _ ->
    CleanDir buildDir
)

Target "Build" (fun _ ->
    !! "src/*.fsproj"
    |> MSBuildRelease buildDir "Build"
    |> Log "AppBuild-Output: "
)

"Clean"
  ==> "Build"

RunTargetOrDefault "Build"