'Args'
dim dllInPath
dim buildDir
dim configName
dim dllName

'Vars'
dim fileVerComps
dim fileVer
dim dllOutPath

set args = WScript.Arguments
set fso = CreateObject("Scripting.FileSystemObject")

WScript.Echo( "SADebug PostBuild" )

dllInPath = args(0)
buildDir = args(1)
configName = args(2)
dllName = args(3)

WScript.Echo "    dllInPath:  " & dllInPath
WScript.Echo "    buildDir:   " & buildDir
WScript.Echo "    configName: " & configName
WScript.Echo "    dllName:    " & dllName

fileVerComps = Split(fso.GetFileVersion(args(0)),".")
fileVer = fileVerComps(0) & "." & fileVerComps(1)
dllOutPath = buildDir & fileVer & "\" & configName & "\"

WScript.Echo "Writing to " & dllOutPath

if not fso.FolderExists(dllOutPath) Then
	WScript.Echo "Creating build directory"
	fso.CreateFolder(dllOutPath)
end if

dllOutPath = dllOutPath & dllName
WScript.Echo "Copying file to " & dllOutPath
fso.CopyFile dllInPath, dllOutPath, True

Wscript.Quit