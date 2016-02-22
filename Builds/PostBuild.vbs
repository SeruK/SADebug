'Args'
dim dllInPath
dim buildDir
dim configName
dim dllName

'Vars'
dim fileVerComps
dim fileVer
dim dllOutPath
dim dllParentDir

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
dllParentDir = fso.GetParentFolderName(dllName)
WScript.Echo "DllParentDir: " & dllParentDir
if 0 < Len(dllParentDir) then
	dllParentDir = dllParentDir & "\"
end if
dllOutPath = buildDir & fileVer & "\" & configName & "\" & dllParentDir

WScript.Echo "Writing to directory " & dllOutPath

if not fso.FolderExists(dllOutPath) then
	WScript.Echo "Creating build directory"
	BuildPath dllOutPath
end if

dllOutPath = dllOutPath & fso.GetFileName(dllName)
WScript.Echo "Copying file to " & dllOutPath
fso.CopyFile dllInPath, dllOutPath, True

Wscript.Quit

sub BuildPath(ByVal Path)
	if not fso.FolderExists(Path) then
		BuildPath fso.GetParentFolderName(Path)
		fso.CreateFolder Path
	end if
end sub